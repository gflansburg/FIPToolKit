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
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;

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

        public delegate void FIPVideoPlayerEventHandler(object sender, FIPVideoPlayerEventArgs e);
        public event FIPVideoPlayerEventHandler OnVideoLoop;
        public event FIPVideoPlayerEventHandler OnSettingsUpdated;
        public event FIPVideoPlayerEventHandler OnActive;
        public event FIPVideoPlayerEventHandler OnInactive;
        private bool opening = false;
        private float start = 0;

        public FIPVideoPlayer(FIPVideoPlayerProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            properties.OnVolumeChanged += Properties_OnVolumeChanged;
            properties.OnPositionChanged += Properties_OnPositionChanged;
            properties.OnPortraitModeChanged += Properties_OnPortraitModeChanged;
            Core.Initialize();
            libVLC = new LibVLC(true, new string[] { "--network-caching", "50", "--no-playlist-autostart", "--no-mouse-events", "--no-keyboard-events", "--quiet", "--no-sout-video", "--sout-transcode-scale=Auto", string.Format("--sout-transcode-width={0}", Width), string.Format("--sout-transcode-height={0}", Height), string.Format("--sout-transcode-maxwidth={0}", Width), string.Format("--sout-transcode-maxheight={0}", Height) });
            CreatePlayer();
        }

        private void Properties_OnPortraitModeChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (player != null)
                {
                    Play(VideoPlayerProperties.Filename);
                }
            });
        }

        private void Properties_OnPositionChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (media != null && player != null && player.Position != VideoPlayerProperties.Position)
                {
                    player.Position = VideoPlayerProperties.Position;
                }
            });
        }

        private void Properties_OnVolumeChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (player != null && player.Volume != VideoPlayerProperties.Volume)
                {
                    player.Volume = VideoPlayerProperties.Volume;
                }
            });
        }

        private FIPVideoPlayerProperties VideoPlayerProperties
        {
            get
            {
                return Properties as FIPVideoPlayerProperties;
            }
        }

        public override void StartTimer()
        {
            if (media == null)
            {
                LoadVideo();
            }
        }

        public bool CanPlayOther
        {
            get
            {
                return !(VideoPlayerProperties.PauseOtherMedia && player.IsPlaying);
            }
        }

        private void CreatePlayer()
        {
            if (player == null)
            {
                player = new MediaPlayer(libVLC);
                player.SetVideoCallbacks(Lock, null, Display);
                player.EnableHardwareDecoding = true;
                player.EncounteredError += (s, e) =>
                {
                    ThreadPool.QueueUserWorkItem(_ =>
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
                    });
                };
                player.VolumeChanged += (s, e) =>
                {
                    if (player != null)
                    {
                        VideoPlayerProperties.SetVolume(player.Volume);
                    }
                };
                player.Opening += (s, e) =>
                {
                    opening = true;
                    start = VideoPlayerProperties.Position;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        using (Bitmap bmp = ImageHelper.GetErrorImage(VideoPlayerProperties.Name))
                        {
                            SendImage(bmp);
                        }
                    });
                };
                player.Playing += (s, e) =>
                {
                    if (opening)
                    {
                        opening = false;
                        if (VideoPlayerProperties.ResumePlayback)
                        {
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                if (player != null)
                                {
                                    player.Position = start;
                                }
                                if (VideoPlayerProperties.PauseOtherMedia)
                                {
                                    OnActive?.Invoke(this, new FIPVideoPlayerEventArgs(this));
                                }
                            });
                        }
                    }
                };
                player.EndReached += (s, e) =>
                {
                    VideoPlayerProperties.SetPosition(0);
                    LoadVideo();
                    OnVideoLoop?.Invoke(this, new FIPVideoPlayerEventArgs(this));
                };
            }
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
            if (player != null && CurrentMappedFile != null)
            {
                try
                {
                    VideoPlayerProperties.SetPosition(player.Position);
                    if (CurrentFrame % 2 == 0)
                    {
                        using (var image = new Image<SixLabors.ImageSharp.PixelFormats.Rgb24>((int)(Pitch / BytePerPixel), (int)Lines))
                        {
                            if (CurrentMappedFile != null)
                            {
                                try
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
                                                    if (VideoPlayerProperties.MaintainAspectRatio)
                                                    {
                                                        graphics.DrawImage(image.ToArray().ToNetImage(), new System.Drawing.Rectangle((320 - (int)Width) / 2, (240 - (int)Height) / 2, (int)Width, (int)Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                                                    }
                                                    else
                                                    {
                                                        graphics.DrawImage(image.ToArray().ToNetImage(), new System.Drawing.Rectangle(0, 0, 320, 240), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                                                    }
                                                }
                                                if (VideoPlayerProperties.ShowControls && player != null)
                                                {
                                                    graphics.AddButtonIcon(player.IsPlaying ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, System.Drawing.Color.White, false, SoftButtons.Button1);
                                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.rotate, System.Drawing.Color.White, false, SoftButtons.Button2);
                                                }
                                                DrawButtons(graphics);
                                                SendImage(bmp);
                                            }
                                        }
                                    }
                                }
                                catch(Exception)
                                {
                                }
                            }
                        }
                    }
                }
                catch(Exception)
                {
                }
            }
            CurrentFrame++;
            if (CurrentMappedViewAccessor != null)
            {
                CurrentMappedViewAccessor.Dispose();
                CurrentMappedViewAccessor = null;
            }
            if (CurrentMappedFile != null)
            {
                CurrentMappedFile.Dispose();
                CurrentMappedFile = null;
            }
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Button1:
                    if (player != null && media != null)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            if (player.IsPlaying)
                            {
                                player.Pause();
                            }
                            else
                            {
                                player.Play();
                            }
                        });
                    }
                    else if (player != null && media == null && !string.IsNullOrEmpty(VideoPlayerProperties.Filename))
                    {
                        LoadVideo();
                    }
                    break;
                case SoftButtons.Button2:
                    VideoPlayerProperties.PortraitMode = !VideoPlayerProperties.PortraitMode;
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
                default:
                    base.ExecuteSoftButton(softButton);
                    break;
            }
            FireSoftButtonNotifcation(softButton);
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
            if (softButton == SoftButtons.Button1 || softButton == SoftButtons.Button2)
            {
                return true;
            }
            return base.IsLEDOn(softButton);
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
            if (player != null && media != null)
            {
                player.Pause();
                if (VideoPlayerProperties.PauseOtherMedia)
                {
                    OnInactive?.Invoke(this, new FIPVideoPlayerEventArgs(this));
                }
            }
        }

        public override void Active()
        {
            base.Active();
            if (player != null && media != null)
            {
                player.Play();
                if (VideoPlayerProperties.PauseOtherMedia)
                {
                    OnActive?.Invoke(this, new FIPVideoPlayerEventArgs(this));
                }
            }
        }

        private void LoadVideo()
        {
            if (player != null)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Play(VideoPlayerProperties.Filename);
                });
            }
        }

        private void Play(string filename)
        {
            Stop();
            if (media != null)
            {
                media.Dispose();
                media = null;
            }
            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                using (var file = TagLib.File.Create(filename))
                {
                    FrameSize = new System.Drawing.SizeF(VideoPlayerProperties.PortraitMode ? file.Properties.VideoHeight : file.Properties.VideoWidth, VideoPlayerProperties.PortraitMode ? file.Properties.VideoWidth : file.Properties.VideoHeight);
                    Duration = file.Properties.Duration;
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
                CreatePlayer();
                player.SetVideoFormat(VideoFormat, Width, Height, Pitch);
                media = new Media(libVLC, filename);
                player.Media = media;
                CurrentFrame = 0;
                if (IsActive)
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

        private void Stop()
        {
            MediaPlayer oldPlayer = player;
            player = null;
            Task.Run(() =>
            {
                if (oldPlayer != null)
                {
                    oldPlayer.Stop();
                    oldPlayer.Dispose();
                    oldPlayer = null;
                }
            });
        }

        public void UpdateSettings(int index, string name, string filename, Font font, System.Drawing.Color fontColor, bool maintainAspectRatio, bool portraitMode, bool showControls, bool resumePlayback, bool pauseOtherMedia)
        {
            VideoPlayerProperties.IsDirty = true;
            VideoPlayerProperties.Name = name;
            VideoPlayerProperties.Font = font;
            VideoPlayerProperties.FontColor = fontColor;
            VideoPlayerProperties.MaintainAspectRatio = maintainAspectRatio;
            VideoPlayerProperties.ShowControls = showControls;
            VideoPlayerProperties.PauseOtherMedia = pauseOtherMedia;
            VideoPlayerProperties.ResumePlayback = resumePlayback;
            if (!(VideoPlayerProperties.Filename ?? string.Empty).Equals(filename ?? string.Empty, StringComparison.OrdinalIgnoreCase))
            {
                VideoPlayerProperties.SetPosition(0);
                VideoPlayerProperties.SetPortraitMode(portraitMode);
                VideoPlayerProperties.Filename = filename;
            }
            else 
            {
                VideoPlayerProperties.PortraitMode = portraitMode;
            }
            OnSettingsUpdated?.Invoke(this, new FIPVideoPlayerEventArgs(this, index));
        }
    }
}
