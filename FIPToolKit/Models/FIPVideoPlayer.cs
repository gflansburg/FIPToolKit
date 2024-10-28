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
using SpotifyAPI.Web.Enums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FIPToolKit.Models
{
    public class FIPVideoPlayerEventArgs : EventArgs
    {
        public FIPVideoPlayer Page { get; private set; }
        
        public FIPVideoPlayerEventArgs(FIPVideoPlayer page) : base()
        {
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

        private const uint Width = 320;
        private const uint Height = 240;

        /// <summary>
        /// RGBA is used, so 4 byte per pixel, or 24 bits.
        /// </summary>
        private const uint BytePerPixel = 3;

        /// <summary>
        /// the number of bytes per "line"
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 24.
        /// </summary>
        private static readonly uint Pitch;

        /// <summary>
        /// The number of lines in the buffer.
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 24.
        /// </summary>
        private static readonly uint Lines;

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
                    player.Stop();
                    _filename = value;
                    if (media != null)
                    {
                        media.Dispose();
                        media = null;
                    }
                    LoadVideo();
                    IsDirty = true;
                }
            }
        }

        private bool Stop { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public static bool PreviewVideo { get; set; }

        public delegate void FIPVideoPlayerEventHandler(object sender, FIPVideoPlayerEventArgs e);
        public event FIPVideoPlayerEventHandler OnVideoLoop;

        static FIPVideoPlayer()
        {
            Pitch = Align(Width * BytePerPixel);
            Lines = Align(Height);

            uint Align(uint size)
            {
                if (size % 24 == 0)
                {
                    return size;
                }

                return ((size / 24) + 1) * 24;// Align on the next multiple of 24
            }
        }

        public FIPVideoPlayer() : base()
        {
            Name = "Video Player";
            IsDirty = false;
            libVLC = new LibVLC(true, new string[] { "--network-caching", "50", "--no-playlist-autostart", "--quiet", "--no-sout-video", "--sout-transcode-scale=Auto", string.Format("--sout-transcode-width={0}", Width), string.Format("--sout-transcode-height={0}", Height), string.Format("--sout-transcode-maxwidth={0}", Width), string.Format("--sout-transcode-maxheight={0}", Height) });
            player = new MediaPlayer(libVLC);
            player.SetVideoFormat("RV24", Width, Height, Pitch);
            player.SetVideoCallbacks(Lock, null, Display);
            player.EnableHardwareDecoding = true;
            player.EncounteredError += (s, e) =>
            {
                Stop = true;
            };
            player.Playing += (s, e) =>
            {
                Stop = false;
            };
            player.Stopped += (s, e) =>
            {
                Stop = true;
            };
            player.EndReached += (s, e) =>
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    player.Stop();
                    player.Position = CurrentFrame = 0;
                    player.Play();
                    OnVideoLoop?.Invoke(this, new FIPVideoPlayerEventArgs(this));
                });
            };
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
                        using (var outputFile = new MemoryStream())
                        {
                            image.Mutate(ctx => ctx.Crop((int)Width, (int)Height));
                            image.SaveAsBmp(outputFile);
                            using (Bitmap bmp = new Bitmap(outputFile))
                            {
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
                    break;
            }
            FireSoftButtonNotifcation(softButton);
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
            return (softButton == SoftButtons.Button1);
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            return (softButton != SoftButtons.Button1);
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
    }
}
