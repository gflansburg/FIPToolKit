using FIPToolKit.Drawing;
using FIPToolKit.Tools;
using LibVLCSharp.Shared;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;
using System.Threading;
using Saitek.DirectOutput;
using System.Linq;
using System.Windows;
using System.Diagnostics;

namespace FIPToolKit.Models
{
    public class FIPVideoPlayerEventArgs : EventArgs
    {
        public FIPVideoPlayer Page { get; private set; }
        public int Index { get; private set; }

        public FIPVideoPlayerEventArgs(FIPVideoPlayer page) : base()
        {
            Page = page;
        }

        public FIPVideoPlayerEventArgs(FIPVideoPlayer page, int index) : base()
        {
            Index = index;
            Page = page;
        }
    }

    [Serializable]
    public class FIPVideoPlayer : FIPPage
    {
        private LibVLC libVLC = null;
        private MediaPlayer player = null;
        private Media media = null;
        private MemoryMappedFile CurrentMappedFile;
        private MemoryMappedViewAccessor CurrentMappedViewAccessor;
        private int CurrentFrame = 0;

        private uint Width = 320;
        private uint Height = 240;

        [XmlIgnore]
        [JsonIgnore]
        public TimeSpan Duration { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public double Fps { get; private set; }

        [XmlIgnore]
        [JsonIgnore]
        public System.Drawing.SizeF FrameSize { get; private set; }

        /// <summary>
        /// RGB is used, so 3 byte per pixel, or 24 bits.
        /// </summary>
        private const uint BytePerPixel = 3;

        private const string VideoFormat = "RV24";

        /// <summary>
        /// the number of bytes per "line"
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 24.
        /// </summary>
        private uint Pitch;

        /// <summary>
        /// The number of lines in the buffer.
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 24.
        /// </summary>
        private uint Lines;

        private string _filename;
        public string Filename
        { 
            get
            {
                return (_filename ?? string.Empty);
            }
            set
            {
                if (!(_filename ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        if (player.IsPlaying)
                        {
                            player.Stop();
                        }
                        _filename = value;
                        if (media != null)
                        {
                            media.Dispose();
                            media = null;
                            player.Media = null;
                        }
                        LoadVideo();
                    });
                    IsDirty = true;
                }
            }
        }

        private bool _showControls = true;
        public bool ShowControls
        {
            get
            {
                return _showControls;
            }
            set
            {
                if (_showControls != value)
                {
                    _showControls = value;
                    IsDirty = true;
                }
            }
        }

        private int _volume = 100;
        public int Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    IsDirty = true;
                    if (player != null)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            player.Volume = _volume;
                        });
                    }
                }
            }
        }

        private bool _maintainAspectRatio = true;
        public bool MaintainAspectRatio 
        { 
            get
            {
                return _maintainAspectRatio;
            }
            set
            {
                if (_maintainAspectRatio != value)
                {
                    _maintainAspectRatio = value;
                    IsDirty = true;
                }
            }
        }

        private bool _resumePlayback = true;
        public bool ResumePlayback
        {
            get
            {
                return _resumePlayback;
            }
            set
            {
                if (_resumePlayback != value)
                {
                    _resumePlayback = value;
                    IsDirty = true;
                }
            }
        }

        private float _position = 0;
        public float Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    if (media != null && player != null)
                    {
                        player.Position = _position;
                    }
                    IsDirty = true;
                }
            }
        }

        private bool _portraitMode = false;
        public bool PortraitMode
        {
            get
            {
                return _portraitMode;
            }
            set
            {
                if (_portraitMode != value)
                {
                    _portraitMode = value;
                    if (media != null)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            if(player.IsPlaying)
                            {
                                player.Stop();
                            }
                            FrameSize = new System.Drawing.SizeF(FrameSize.Height, FrameSize.Width);
                            // Figure out the ratio
                            double ratioX = 320 / FrameSize.Width;
                            double ratioY = 240 / FrameSize.Height;
                            // use whichever multiplier is smaller
                            double ratio = ratioX < ratioY ? ratioX : ratioY;
                            Height = Convert.ToUInt32(FrameSize.Height * ratio);
                            Width = Convert.ToUInt32(FrameSize.Width * ratio);
                            Pitch = Align(Width * BytePerPixel);
                            Lines = Align(Height);
                            player.SetVideoFormat(VideoFormat, Width, Height, Pitch);
                            if (IsActive)
                            {
                                player.Play();
                            }
                        });
                    }
                    IsDirty = true;
                }
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public static bool PreviewVideo { get; set; }

        public delegate void FIPVideoPlayerEventHandler(object sender, FIPVideoPlayerEventArgs e);
        public event FIPVideoPlayerEventHandler OnVideoLoop;
        public event FIPVideoPlayerEventHandler OnSettingsUpdated;
        private bool opening = false;
        private float start = 0;

        public FIPVideoPlayer() : base()
        {
            Name = "Video Player";
            IsDirty = false;
            Core.Initialize();
            libVLC = new LibVLC(true, new string[] { "--network-caching", "50", "--no-playlist-autostart", "--quiet", "--no-sout-video", "--sout-transcode-scale=Auto", string.Format("--sout-transcode-width={0}", Width), string.Format("--sout-transcode-height={0}", Height), string.Format("--sout-transcode-maxwidth={0}", Width), string.Format("--sout-transcode-maxheight={0}", Height) });
            player = new MediaPlayer(libVLC);
            player.SetVideoCallbacks(Lock, null, Display);
            player.EnableHardwareDecoding = true;
            player.EncounteredError += (s, e) =>
            {
                if (media != null)
                {
                    media.Dispose();
                    media = null;
                }
                player.Media = null;
                using (Bitmap bmp = ImageHelper.GetErrorImage("An Error Has Occured."))
                {
                    SendImage(bmp);
                }
            };
            player.VolumeChanged += (s, e) =>
            {
                Volume = player.Volume;
            };
            player.Opening += (s, e) =>
            {
                opening = true;
                start = _position;
                using (Bitmap bmp = ImageHelper.GetErrorImage(Name))
                {
                    SendImage(bmp);
                }
            };
            player.Playing += (s, e) =>
            {
                if (opening)
                {
                    opening = false;
                    if (_resumePlayback)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            player.Position = start;
                        });
                    }
                }
            };
            player.EndReached += (s, e) =>
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    player.Stop();
                    _position = 0;
                    IsDirty = true;
                    player.Position = CurrentFrame = 0;
                    player.Play();
                    OnVideoLoop?.Invoke(this, new FIPVideoPlayerEventArgs(this));
                });
            };
        }

        private uint Align(uint size)
        {
            if (size % 24 == 0)
            {
                return size;
            }
            return ((size / 24) + 1) * 24;// Align on the next multiple of 24
        }

        public override void Dispose()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (media != null)
                {
                    media.Dispose();
                }
                player.Stop();
                player.Dispose();
                libVLC.Dispose();
                if (CurrentMappedViewAccessor != null)
                {
                    CurrentMappedViewAccessor.Dispose();
                }
                if (CurrentMappedFile != null)
                {
                    CurrentMappedFile.Dispose();
                }
            });
            base.Dispose();
        }
        private IntPtr Lock(IntPtr opaque, IntPtr planes)
        {
            CurrentMappedFile = MemoryMappedFile.CreateNew(null, Pitch * Lines);
            CurrentMappedViewAccessor = CurrentMappedFile.CreateViewAccessor();
            Marshal.WriteIntPtr(planes, CurrentMappedViewAccessor.SafeMemoryMappedViewHandle.DangerousGetHandle());
            return IntPtr.Zero;
        }

        private void Display(IntPtr opaque, IntPtr picture)
        {
            _position = player.Position;
            IsDirty = true;
            if (CurrentFrame % 2 == 0)
            {
                using (var image = new Image<SixLabors.ImageSharp.PixelFormats.Rgb24>((int)(Pitch / BytePerPixel), (int)Lines))
                {
                    using (var sourceStream = CurrentMappedFile.CreateViewStream())
                    {
                        var mg = image.GetPixelMemoryGroup();
                        for (int i = 0; i < mg.Count; i++)
                        {
                            sourceStream.Read(MemoryMarshal.AsBytes(mg[i].Span));
                        }
                        image.Mutate(ctx => ctx.Crop((int)Width, (int)Height));
                        using (Bitmap bmp = new Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                        {
                            using (Graphics graphics = Graphics.FromImage(bmp))
                            {
                                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                                {
                                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                                    if (MaintainAspectRatio)
                                    {
                                        graphics.DrawImage(image.ToArray().ToNetImage(), new System.Drawing.Rectangle((320 - (int)Width) / 2, (240 - (int)Height) / 2, (int)Width, (int)Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                                    }
                                    else
                                    {
                                        graphics.DrawImage(image.ToArray().ToNetImage(), new System.Drawing.Rectangle(0, 0, 320, 240), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                                    }
                                }
                                if (ShowControls)
                                {
                                    graphics.AddButtonIcon(player.IsPlaying ? Properties.Resources.pause : Properties.Resources.play, System.Drawing.Color.White, false, SoftButtons.Button1);
                                    graphics.AddButtonIcon(Properties.Resources.rotate, System.Drawing.Color.White, false, SoftButtons.Button2);
                                }
                                DrawButtons(graphics);
                                SendImage(bmp);
                            }
                        }
                    }
                }
            }
            CurrentFrame++;
            CurrentMappedViewAccessor.Dispose();
            CurrentMappedFile.Dispose();
            CurrentMappedFile = null;
            CurrentMappedViewAccessor = null;
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Button1:
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        if (player != null && media != null)
                        {
                            if (player.IsPlaying)
                            {
                                player.Pause();
                            }
                            else
                            {
                                player.Play();
                            }
                        }
                        else if (player != null && media == null && !string.IsNullOrEmpty(Filename))
                        {
                            LoadVideo();
                        }
                    });
                    break;
                case SoftButtons.Button2:
                    PortraitMode = !PortraitMode;
                    break;
                case SoftButtons.Up:
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        int newVolume = player.Volume + 5;
                        newVolume = Math.Max(0, Math.Min(newVolume, 100));
                        player.Volume = newVolume;
                    });
                    break;
                case SoftButtons.Down:
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        int newVolume = player.Volume - 5;
                        newVolume = Math.Max(0, Math.Min(newVolume, 100));
                        player.Volume = newVolume;
                    });
                    break;
                case SoftButtons.Left:
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        float pos = (player.Position * (float)Duration.TotalMilliseconds) - 30000;
                        player.Position = Math.Max(pos / (float)Duration.TotalMilliseconds, 0);
                    });
                    break;
                case SoftButtons.Right:
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        float pos = (player.Position * (float)Duration.TotalMilliseconds) + 30000;
                        player.Position = Math.Min(pos / (float)Duration.TotalMilliseconds, 1);
                    });
                    break;
            }
            FireSoftButtonNotifcation(softButton);
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
            return (softButton == SoftButtons.Button1 || softButton == SoftButtons.Button2);
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Button1:
                case SoftButtons.Button2:
                case SoftButtons.Left:
                case SoftButtons.Right:
                case SoftButtons.Up:
                case SoftButtons.Down:
                    return false;
            }
            return true;
        }

        public override void Inactive()
        {
            base.Inactive();
            if (player != null && player.IsPlaying)
            {
                player.Pause();
            }
        }

        public override void Active()
        {
            base.Active();
            if (!player.IsPlaying && media != null)
            {
                player.Play();
            }
        }

        private void LoadVideo()
        {
            if (!string.IsNullOrEmpty(Filename) && File.Exists(Filename))
            {
                if (media == null)
                {
                    var inputFile = new MediaToolkit.Model.MediaFile
                    {
                        Filename = Filename
                    };
                    using (var engine = new MediaToolkit.Engine())
                    {
                        engine.GetMetadata(inputFile);
                        var size = inputFile.Metadata.VideoData.FrameSize.Split(new[] { 'x' }).Select(o => int.Parse(o)).ToArray();
                        FrameSize = new System.Drawing.SizeF(size[PortraitMode ? 1 : 0], size[PortraitMode ? 0 : 1]);
                        Duration = inputFile.Metadata.Duration;
                        Fps = inputFile.Metadata.VideoData.Fps;
                    }
                    // Figure out the ratio
                    double ratioX = 320 / FrameSize.Width;
                    double ratioY = 240 / FrameSize.Height;
                    // use whichever multiplier is smaller
                    double ratio = ratioX < ratioY ? ratioX : ratioY;
                    Height = Convert.ToUInt32(FrameSize.Height * ratio);
                    Width = Convert.ToUInt32(FrameSize.Width * ratio);
                    Pitch = Align(Width * BytePerPixel);
                    Lines = Align(Height);
                    player.SetVideoFormat(VideoFormat, Width, Height, Pitch);
                    media = new Media(libVLC, Filename);
                    player.Media = media;
                    if (IsActive)
                    {
                        player.Play();
                    }
                }
                else if (!player.IsPlaying)
                {
                    player.Play();
                }
            }
            else
            {
                using (Bitmap bmp = ImageHelper.GetErrorImage("Select a video to play."))
                {
                    SendImage(bmp);
                }
            }
        }

        public void UpdateSettings(int index, string name, string filename, Font font, System.Drawing.Color fontColor, bool maintainAspectRatio, bool portraitMode, bool showControls, bool resumePlayback)
        {
            IsDirty = true;
            Name = name;
            Font = font;
            FontColor = fontColor;
            MaintainAspectRatio = maintainAspectRatio;
            ShowControls = showControls;
            ResumePlayback = resumePlayback;
            if (!(Filename ?? string.Empty).Equals(filename ?? string.Empty, StringComparison.OrdinalIgnoreCase))
            {
                _position = 0;
                _portraitMode = portraitMode;
                Filename = filename;
            }
            else 
            {
                PortraitMode = portraitMode;
            }
            OnSettingsUpdated?.Invoke(this, new FIPVideoPlayerEventArgs(this, index));
        }
    }
}
