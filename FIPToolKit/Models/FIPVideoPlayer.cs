using FIPToolKit.Drawing;
using FIPToolKit.Tools;
using LibVLCSharp.Shared;
using System;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;
using System.Threading;
using Saitek.DirectOutput;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using M3U.Media;
using System.Web;

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

    public class FIPMediaFile : IDisposable
    {
        public event EventHandler ArtworkDownloaded;

        public string Filename { get; set; }
        public M3U.Media.M3UMedia MetaData { get; set; }
        public bool IsStream
        {
            get
            {
                return (!string.IsNullOrEmpty(Filename) && (Filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || Filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase)));
            }
        }

        private Bitmap _artwork = null;
        private bool _checked = false;
        public Bitmap Artwork
        {
            get
            {
                if (_artwork == null && MetaData != null && MetaData.Attributes != null && !string.IsNullOrEmpty(MetaData.Attributes.TvgLogo) && !_checked)
                {
                    _checked = true;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {

                        System.Drawing.Image img = MetaData.Attributes.TvgLogo.DownloadImageFromUrl();
                        if (img != null)
                        {
                            _artwork = new Bitmap(img);
                            ArtworkDownloaded?.Invoke(this, new EventArgs());
                        }
                        else
                        {
                            _artwork = new Bitmap(Properties.Resources.Video.ChangeToColor(SystemColors.Highlight));
                        }
                    });
                }
                else if (_artwork == null && File.Exists(Filename) && !_checked)
                {
                    _checked = true;
                    using (var tag = TagLib.File.Create(Filename))
                    {
                        if (tag.Tag != null)
                        {
                            if (tag.Tag.Pictures.Length > 0)
                            {
                                using (System.Drawing.Image img = System.Drawing.Image.FromStream(new MemoryStream(tag.Tag.Pictures[0].Data.Data)))
                                {
                                    _artwork = new Bitmap(img);
                                }
                            }
                            else
                            {
                                _artwork = new Bitmap(Properties.Resources.Video.ChangeToColor(SystemColors.Highlight));
                            }
                        }
                        else
                        {
                            _artwork = new Bitmap(Properties.Resources.Video.ChangeToColor(SystemColors.Highlight));
                        }
                    }
                }
                return _artwork;
            }
            set
            {
                _artwork = value;
            }
        }

        public void Dispose()
        {
            if (_artwork != null)
            {
                _artwork.Dispose();
            }
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

        private List<FIPMediaFile> Playlist { get; set; }
        private FIPMediaFile CurrentTrack { get; set; }
        private string SubTitle { get; set; }
        private string Error { get; set; }
        private Bitmap VideoFrame { get; set; }
        public TimeSpan? Duration { get; private set; }
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
        public event FIPVideoPlayerEventHandler OnSettingsUpdated;
        public event FIPPageEventHandler OnVideoLoop;
        public event FIPPageEventHandler OnNameChanged;
        public event FIPPageEventHandler OnMuteChanged;
        public event FIPPageEventHandler OnVolumeChanged;
        private bool opening = false;
        private float start = 0;

        public FIPVideoPlayer(FIPVideoPlayerProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            properties.OnVolumeChanged += Properties_OnVolumeChanged;
            properties.OnPositionChanged += Properties_OnPositionChanged;
            properties.OnPortraitModeChanged += Properties_OnPortraitModeChanged;
            properties.OnFilenameChanged += Properties_OnFilenameChanged;
            properties.OnNameChanged += Properties_OnNameChanged;
            properties.OnMuteChanged += Properties_OnMuteChanged;
            CreatePlayer();
            CreatePlaylist();
        }

        private void Properties_OnMuteChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                player.Mute = VideoPlayerProperties.Mute;
                UpdatePage();
            });
            OnMuteChanged?.Invoke(this, new FIPPageEventArgs(this));
        }

        private void Properties_OnNameChanged(object sender, EventArgs e)
        {
            OnNameChanged?.Invoke(this, new FIPPageEventArgs(this));    
        }

        private void Properties_OnFilenameChanged(object sender, EventArgs e)
        {
            CreatePlaylist();
            CurrentTrack = null;
            CurrentFrame = 0;
            VideoPlayerProperties.SetPosition(0);
            LoadVideo();
        }

        public override void UpdatePage()
        {
            if (player != null && player.VideoTrack == -1)
            {
                DrawMusicArtwork();
            }
            else if (player != null && !player.IsPlaying)
            {
                UpdateVideoFrame();
            }
            base.UpdatePage();
        }

        private void CreatePlaylist()
        {
            Playlist = new List<FIPMediaFile>();
            if (Path.GetExtension(VideoPlayerProperties.Filename).Equals(".m3u", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(VideoPlayerProperties.Filename).Equals(".m3u8", StringComparison.OrdinalIgnoreCase))
            {
                M3UMediaContainer playlist;
                if (VideoPlayerProperties.Filename.StartsWith("http://") || VideoPlayerProperties.Filename.StartsWith("https://"))
                {
                    playlist = M3U.M3UParser.Parse(Net.GetURL(VideoPlayerProperties.Filename));
                }
                else
                {
                    playlist = M3U.M3UParser.ParseFromFile(VideoPlayerProperties.Filename);
                }
                foreach (M3UMedia media in playlist.Medias)
                {
                    FIPMediaFile track = new FIPMediaFile()
                    {
                        Filename = media.Path,
                        MetaData = media
                    };
                    track.ArtworkDownloaded += Track_ArtworkDownloaded;
                    Playlist.Add(track);
                } 
            }
            else
            {
                Playlist.Add(new FIPMediaFile() { Filename = VideoPlayerProperties.Filename });
            }
        }

        private void Track_ArtworkDownloaded(object sender, EventArgs e)
        {
            if (player != null && player.VideoTrack == -1 && sender == CurrentTrack)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    UpdatePage();
                });
            }
        }

        private void Properties_OnPortraitModeChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (player != null && !string.IsNullOrEmpty(CurrentTrack.Filename))
                {
                    Play(CurrentTrack, VideoPlayerProperties.Position);
                }
            });
        }

        private void Properties_OnPositionChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (player != null && player.Media != null && player.Position != VideoPlayerProperties.Position)
                {
                    player.Position = VideoPlayerProperties.Position;
                }
            });
        }

        private void Properties_OnVolumeChanged(object sender, EventArgs e)
        {
            if (player != null && player.Volume != VideoPlayerProperties.Volume)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    player.Volume = VideoPlayerProperties.Volume;
                });
            }
            OnVolumeChanged?.Invoke(this, new FIPPageEventArgs(this));
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
                return !(VideoPlayerProperties.PauseOtherMedia && player != null && player.IsPlaying);
            }
        }

        private void CreatePlayer()
        {
            if (libVLC == null)
            {
                try
                {
                    libVLC = new LibVLC(true, new string[] { "--network-caching", "50", "--no-playlist-autostart", "--no-mouse-events", "--no-keyboard-events", "--quiet", "--no-sout-video", "--sout-transcode-scale=Auto", string.Format("--sout-transcode-width={0}", Width), string.Format("--sout-transcode-height={0}", Height), string.Format("--sout-transcode-maxwidth={0}", Width), string.Format("--sout-transcode-maxheight={0}", Height) });
                }
                catch(Exception)
                {
                }
            }
            if (player == null && libVLC != null)
            {
                try
                {
                    player = new MediaPlayer(libVLC);
                    player.SetVideoCallbacks(Lock, null, Display);
                    player.Volume = VideoPlayerProperties.Volume;
                    player.Mute = VideoPlayerProperties.Mute;
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
                            if (Playlist.Count > 1)
                            {
                                PlayNextTrack();
                            }
                            else
                            {
                                Error = "An Error Has Occured";
                                if (player.VideoTrack == -1)
                                {
                                    ThreadPool.QueueUserWorkItem(_ =>
                                    {
                                        UpdatePage();
                                    });
                                }
                                else
                                {
                                    UpdateMessage("An Error Has Occured");
                                }
                            }
                        });
                    };
                    player.Muted += (s, e) =>
                    {
                        if (player != null)
                        {
                            VideoPlayerProperties.SetMute(player.Mute);
                            OnMuteChanged?.Invoke(this, new FIPPageEventArgs(this));
                        }
                    };
                    player.VolumeChanged += (s, e) =>
                    {
                        if (player != null)
                        {
                            VideoPlayerProperties.SetVolume(player.Volume);
                            OnVolumeChanged?.Invoke(this, new FIPPageEventArgs(this));
                        }
                    };
                    player.Opening += (s, e) =>
                    {
                        opening = true;
                        start = VideoPlayerProperties.Position;
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            SubTitle = null;
                            if (CurrentTrack.Artwork != null)
                            {
                                UpdatePage();
                            }
                            else
                            {
                                UpdateMessage(SubTitle);
                            }
                        });
                    };
                    player.Playing += (s, e) =>
                    {
                        if (opening)
                        {
                            opening = false;
                            ThreadPool.QueueUserWorkItem(_ =>
                            {
                                if (player != null && VideoPlayerProperties.ResumePlayback)
                                {
                                    player.Position = start;
                                }
                                // May be an audio stream with no video.
                                UpdatePage();
                            });
                        }
                        if (VideoPlayerProperties.PauseOtherMedia)
                        {
                            SendActive();
                        }
                    };
                    player.Paused += (s, e) =>
                    {
                        if (VideoPlayerProperties.PauseOtherMedia)
                        {
                            SendInactive();
                        }
                    };
                    player.EndReached += (s, e) =>
                    {
                        PlayNextTrack();
                    };
                }
                catch(Exception)
                {
                }
            }
        }

        private void Player_Paused(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private uint Align(uint size)
        {
            if (size % 24 == 0)
            {
                return size;
            }
            return ((size / 24) + 1) * 24;// Align on the next multiple of 24
        }

        private void PlayNextTrack()
        {
            if (Playlist != null && Playlist.Count > 0)
            {
                int index = Playlist.IndexOf(CurrentTrack);
                index++;
                if (index > Playlist.Count - 1)
                {
                    index = 0;
                    OnVideoLoop?.Invoke(this, new FIPPageEventArgs(this));
                }
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Play(Playlist[index], 0);
                });
            }
        }

        private void PlayPreviousTrack()
        {
            if (Playlist != null && Playlist.Count > 0)
            {
                int index = Playlist.IndexOf(CurrentTrack);
                index--;
                if (index < 0)
                {
                    index = Playlist.Count - 1;
                }
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Play(Playlist[index], 0);
                });
            }
        }

        public override void Dispose()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (media != null)
                {
                    media.Dispose();
                    media = null;
                }
                Stop();
                if (libVLC != null)
                {
                    libVLC.Dispose();
                    libVLC = null;
                }
                if (CurrentMappedViewAccessor != null)
                {
                    CurrentMappedViewAccessor.Dispose();
                }
                if (CurrentMappedFile != null)
                {
                    CurrentMappedFile.Dispose();
                }
                if (VideoFrame != null)
                {
                    VideoFrame.Dispose();
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

        private void UpdateMessage(string message)
        {
            using (Bitmap bmp = new Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    System.Drawing.Color color = CurrentTrack.MetaData != null && CurrentTrack.MetaData.Adornments != null && CurrentTrack.MetaData.Adornments.Color.HasValue ? CurrentTrack.MetaData.Adornments.Color.Value : VideoPlayerProperties.FontColor;
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        using (FontEx font = new FontEx(VideoPlayerProperties.Font, CurrentTrack.MetaData != null && CurrentTrack.MetaData.Adornments != null && CurrentTrack.MetaData.Adornments.FontStyle.HasValue ? CurrentTrack.MetaData.Adornments.FontStyle.Value : VideoPlayerProperties.Font.Style))
                        {
                            using (StringFormat drawFormat = new StringFormat())
                            {
                                drawFormat.Alignment = StringAlignment.Center;
                                drawFormat.LineAlignment = StringAlignment.Center;
                                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                                graphics.DrawString(message, font, brush, new System.Drawing.RectangleF(0, 0, 320, 240), drawFormat);
                            }
                        }
                    }
                    if (VideoPlayerProperties.ShowControls && player != null)
                    {
                        using (SolidBrush translucentBrush = new SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0)))
                        {
                            graphics.FillRectangle(translucentBrush, 0, 0, 34, 240);
                            graphics.AddButtonIcon(player.Mute ? FIPToolKit.Properties.Resources.media_mute : FIPToolKit.Properties.Resources.media_volumeup, System.Drawing.Color.White, true, SoftButtons.Button1);
                            graphics.AddButtonIcon(player.IsPlaying ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, System.Drawing.Color.White, false, SoftButtons.Button2);
                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.rotate, System.Drawing.Color.White, false, SoftButtons.Button3);
                            if (Playlist.Count > 1)
                            {
                                graphics.AddButtonIcon(FIPToolKit.Properties.Resources.media_previoustrack, System.Drawing.Color.White, false, SoftButtons.Button4);
                                graphics.AddButtonIcon(FIPToolKit.Properties.Resources.media_nexttrack, System.Drawing.Color.White, false, SoftButtons.Button5);
                            }
                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.Hide, System.Drawing.Color.White, false, SoftButtons.Button6);
                        }
                    }
                }
                SendImage(bmp);
                bmp.Dispose();
            }
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
                                                VideoFrame = new Bitmap(bmp);
                                                if (VideoPlayerProperties.ShowControls && player != null)
                                                {
                                                    using (SolidBrush translucentBrush = new SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0)))
                                                    {
                                                        graphics.FillRectangle(translucentBrush, 0, 0, 34, 240);
                                                        graphics.AddButtonIcon(player.Mute ? FIPToolKit.Properties.Resources.media_mute : FIPToolKit.Properties.Resources.media_volumeup, System.Drawing.Color.White, true, SoftButtons.Button1);
                                                        graphics.AddButtonIcon(player.IsPlaying ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, System.Drawing.Color.White, false, SoftButtons.Button2);
                                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.rotate, System.Drawing.Color.White, false, SoftButtons.Button3);
                                                        if (Playlist.Count > 1)
                                                        {
                                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.media_previoustrack, System.Drawing.Color.White, false, SoftButtons.Button4);
                                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.media_nexttrack, System.Drawing.Color.White, false, SoftButtons.Button5);
                                                        }
                                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.Hide, System.Drawing.Color.White, false, SoftButtons.Button6);
                                                    }
                                                }
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

        private void UpdateVideoFrame()
        {
            if (VideoFrame != null)
            {
                using (Bitmap bmp = new Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                {
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        graphics.DrawImage(VideoFrame, new System.Drawing.Rectangle(0, 0, VideoFrame.Width, VideoFrame.Height), 0, 0, VideoFrame.Width, VideoFrame.Height, GraphicsUnit.Pixel);
                        if (VideoPlayerProperties.ShowControls && player != null)
                        {
                            using (SolidBrush translucentBrush = new SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0)))
                            {
                                graphics.FillRectangle(translucentBrush, 0, 0, 34, 240);
                                graphics.AddButtonIcon(player.Mute ? FIPToolKit.Properties.Resources.media_mute : FIPToolKit.Properties.Resources.media_volumeup, System.Drawing.Color.White, true, SoftButtons.Button1);
                                graphics.AddButtonIcon(player.IsPlaying ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, System.Drawing.Color.White, false, SoftButtons.Button2);
                                graphics.AddButtonIcon(FIPToolKit.Properties.Resources.rotate, System.Drawing.Color.White, false, SoftButtons.Button3);
                                if (Playlist.Count > 1)
                                {
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.media_previoustrack, System.Drawing.Color.White, false, SoftButtons.Button4);
                                    graphics.AddButtonIcon(FIPToolKit.Properties.Resources.media_nexttrack, System.Drawing.Color.White, false, SoftButtons.Button5);
                                }
                                graphics.AddButtonIcon(FIPToolKit.Properties.Resources.Hide, System.Drawing.Color.White, false, SoftButtons.Button6);
                            }
                        }
                        SendImage(bmp);
                    }
                }
            }
        }

        private void DrawMusicArtwork()
        {
            if (CurrentTrack != null)
            {
                using (Bitmap bmp = new Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
                {
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                        {
                            wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                            using (StringFormat format = new StringFormat())
                            {
                                format.Alignment = StringAlignment.Center;
                                format.LineAlignment = StringAlignment.Center;
                                System.Drawing.Color color = CurrentTrack.MetaData != null && CurrentTrack.MetaData.Adornments != null && CurrentTrack.MetaData.Adornments.Color.HasValue ? CurrentTrack.MetaData.Adornments.Color.Value : VideoPlayerProperties.FontColor;
                                using (SolidBrush brush = new SolidBrush(color))
                                {
                                    using (FontEx font = new FontEx(VideoPlayerProperties.Font, CurrentTrack.MetaData != null && CurrentTrack.MetaData.Adornments != null && CurrentTrack.MetaData.Adornments.FontStyle.HasValue ? CurrentTrack.MetaData.Adornments.FontStyle.Value : VideoPlayerProperties.Font.Style))
                                    {
                                        Bitmap artwork = CurrentTrack.Artwork;
                                        if (artwork != null)
                                        {

                                            try
                                            {
                                                int titleHeight = (int)graphics.MeasureString(VideoPlayerProperties.Name, VideoPlayerProperties.Font, 320, format).Height;
                                                int subTitleHeight = (int)graphics.MeasureString(!string.IsNullOrEmpty(Error) ? Error : SubTitle, VideoPlayerProperties.SubtitleFont, 320, format).Height;
                                                int maxImageWidth = 320;
                                                int maxImageHeight = 240 - (titleHeight + subTitleHeight);
                                                //Just in case the artwork isn't a square. I have seen landscape photos and cliped album artwork.
                                                double ratioX = (double)maxImageWidth / artwork.Width;
                                                double ratioY = (double)maxImageHeight / artwork.Height;
                                                double ratio = Math.Min(ratioX, ratioY);
                                                int imageWidth = (int)(artwork.Width * ratio);
                                                int imageHeight = (int)(artwork.Height * ratio);
                                                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle((maxImageWidth - imageWidth) / 2, (maxImageHeight - imageHeight) / 2, imageWidth, imageHeight);
                                                graphics.DrawImage(artwork, destRect, 0, 0, artwork.Width, artwork.Height, GraphicsUnit.Pixel);
                                                graphics.DrawString(VideoPlayerProperties.Name, font, brush, new System.Drawing.RectangleF(0, maxImageHeight, maxImageWidth, titleHeight), format);
                                                using (SolidBrush subTitleBrush = new SolidBrush(VideoPlayerProperties.FontColor))
                                                {
                                                    graphics.DrawString(!string.IsNullOrEmpty(Error) ? Error : SubTitle, VideoPlayerProperties.SubtitleFont, subTitleBrush, new System.Drawing.RectangleF(0, maxImageHeight + titleHeight, maxImageWidth, subTitleHeight), format);
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                string text = string.Format("{0}\n{1}", VideoPlayerProperties.Name, !string.IsNullOrEmpty(Error) ? Error : SubTitle);
                                                graphics.DrawString(text, VideoPlayerProperties.Font, brush, new System.Drawing.RectangleF(0, 0, 320, 240), format);
                                            }
                                        }
                                        else
                                        {
                                            string text = string.Format("{0}\n{1}", VideoPlayerProperties.Name, !string.IsNullOrEmpty(Error) ? Error : SubTitle);
                                            graphics.DrawString(text, VideoPlayerProperties.Font, brush, new System.Drawing.RectangleF(0, 320, 288, 240), format);
                                        }
                                    }
                                }
                                if (VideoPlayerProperties.ShowControls && player != null)
                                {
                                    using (SolidBrush translucentBrush = new SolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0)))
                                    {
                                        graphics.FillRectangle(translucentBrush, 0, 0, 34, 240);
                                        graphics.AddButtonIcon(player.Mute ? FIPToolKit.Properties.Resources.media_mute : FIPToolKit.Properties.Resources.media_volumeup, System.Drawing.Color.White, true, SoftButtons.Button1);
                                        graphics.AddButtonIcon(player.IsPlaying ? FIPToolKit.Properties.Resources.pause : FIPToolKit.Properties.Resources.play, System.Drawing.Color.White, false, SoftButtons.Button2);
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.rotate, System.Drawing.Color.White, false, SoftButtons.Button3);
                                        if (Playlist.Count > 1)
                                        {
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.media_previoustrack, System.Drawing.Color.White, false, SoftButtons.Button4);
                                            graphics.AddButtonIcon(FIPToolKit.Properties.Resources.media_nexttrack, System.Drawing.Color.White, false, SoftButtons.Button5);
                                        }
                                        graphics.AddButtonIcon(FIPToolKit.Properties.Resources.Hide, System.Drawing.Color.White, false, SoftButtons.Button6);
                                    }
                                }
                            }
                        }
                    }
                    SendImage(bmp);
                }
            }
        }

        public override void ExecuteSoftButton(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Button2:
                    if (player != null && player.Media != null)
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
                            UpdatePage();
                        });
                    }
                    else if (player != null && player.Media == null && Playlist != null && Playlist.Count > 0)
                    {
                        LoadVideo();
                    }
                    break;
                case SoftButtons.Button3:
                    VideoPlayerProperties.PortraitMode = !VideoPlayerProperties.PortraitMode;
                    break;
                case SoftButtons.Button1:
                    VideoPlayerProperties.Mute = !VideoPlayerProperties.Mute;
                    break;
                case SoftButtons.Button4:
                    PlayPreviousTrack();
                    break;
                case SoftButtons.Button5:
                    PlayNextTrack();
                    break;
                case SoftButtons.Button6:
                    VideoPlayerProperties.ShowControls = !VideoPlayerProperties.ShowControls;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        UpdatePage();
                    });
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
                    if (Duration != null)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            float pos = (player.Position * (float)Duration.Value.TotalMilliseconds) - 30000;
                            player.Position = Math.Max(pos / (float)Duration.Value.TotalMilliseconds, 0);
                        });
                    }
                    else
                    {
                        PlayPreviousTrack();
                    }
                    break;
                case SoftButtons.Right:
                    if (Duration != null)
                    {
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            float pos = (player.Position * (float)Duration.Value.TotalMilliseconds) + 30000;
                            player.Position = Math.Min(pos / (float)Duration.Value.TotalMilliseconds, 1);
                        });
                    }
                    else
                    {
                        PlayNextTrack();
                    }
                    break;
                default:
                    base.ExecuteSoftButton(softButton);
                    break;
            }
            FireSoftButtonNotifcation(softButton);
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
            switch (softButton)
            {
                case SoftButtons.Button2:
                    return Playlist != null && Playlist.Count > 0;
                case SoftButtons.Button1:
                case SoftButtons.Button3:
                case SoftButtons.Button6:
                    return true;
                case SoftButtons.Button4:
                case SoftButtons.Button5:
                    return Playlist != null && Playlist.Count > 1;
                default:
                    return base.IsLEDOn(softButton);
            }
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            return false;
        }

        public override void Inactive(bool sendEvent)
        {
            if (player != null && player.Media != null)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (player.IsPlaying)
                    {
                        player.Pause();
                    }
                });
            }
            base.Inactive(false);
        }

        public override void Active(bool sendEvent)
        {
            if (player != null && player.Media != null)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (!player.IsPlaying)
                    {
                        player.Play();
                    }
                });
            }
            base.Active(false);
        }

        private void LoadVideo()
        {
            if (player != null)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Play(CurrentTrack == null ? Playlist != null ? GetFirstOrLastPlayed() : null : CurrentTrack, VideoPlayerProperties.Position);
                });
            }
        }

        private FIPMediaFile GetFirstOrLastPlayed()
        {
            FIPMediaFile lastPlayed = Playlist.FirstOrDefault(p => p.Filename.Equals(VideoPlayerProperties.LastTrack, StringComparison.OrdinalIgnoreCase));
            if (lastPlayed != null && VideoPlayerProperties.ResumePlayback)
            {
                return lastPlayed;
            }
            return Playlist.FirstOrDefault();
        }

        private void Play(FIPMediaFile track, float position)
        {
            if (libVLC != null)
            {
                Stop();
                while (!_stopped)
                {
                    Thread.Sleep(100);
                }
                if (media != null)
                {
                    media.Dispose();
                    media = null;
                }
                if (track != null && !string.IsNullOrEmpty(track.Filename))
                {
                    string title = track.MetaData != null && !string.IsNullOrEmpty(track.MetaData.Title) ? track.MetaData.Title : track.MetaData != null && track.MetaData.Attributes != null && !string.IsNullOrEmpty(track.MetaData.Attributes.TvgName) ? track.MetaData.Attributes.TvgName : track.MetaData != null && track.MetaData.Attributes != null && !string.IsNullOrEmpty(track.MetaData.Attributes.TvgId) ? track.MetaData.Attributes.TvgId : HttpUtility.UrlDecode(Path.GetFileNameWithoutExtension(track.Filename));
                    if (File.Exists(track.Filename))
                    {
                        using (var file = TagLib.File.Create(track.Filename))
                        {
                            FrameSize = new System.Drawing.SizeF(VideoPlayerProperties.PortraitMode ? file.Properties.VideoHeight : file.Properties.VideoWidth, VideoPlayerProperties.PortraitMode ? file.Properties.VideoWidth : file.Properties.VideoHeight);
                            Duration = file.Properties.Duration;
                            if (file.Tag != null && !string.IsNullOrEmpty(file.Tag.Title))
                            {
                                title = file.Tag.Title;
                            }
                        }
                    }
                    else
                    {
                        // Assume streaming at 16:9
                        FrameSize = new System.Drawing.SizeF(VideoPlayerProperties.PortraitMode ? 180 : 320, VideoPlayerProperties.PortraitMode ? 320 : 180);
                        Duration = null;
                    }
                    if ((FrameSize.Width == 0 || FrameSize.Height == 0) && File.Exists(track.Filename))
                    {
                        var inputFile = new MediaToolkit.Model.MediaFile
                        {
                            Filename = track.Filename
                        };
                        using (var engine = new MediaToolkit.Engine())
                        {
                            engine.GetMetadata(inputFile);
                            var size = inputFile.Metadata.VideoData.FrameSize.Split(new[] { 'x' }).Select(o => int.Parse(o)).ToArray();
                            FrameSize = new System.Drawing.SizeF(VideoPlayerProperties.PortraitMode ? size[1] : size[0], VideoPlayerProperties.PortraitMode ? size[0] : size[1]);
                            if (!Duration.HasValue)
                            {
                                Duration = inputFile.Metadata.Duration;
                            }
                        }
                    }
                    if (FrameSize.Width == 0)
                    {
                        FrameSize = new System.Drawing.SizeF(VideoPlayerProperties.PortraitMode ? 240 : 320, FrameSize.Height);
                    }
                    if (FrameSize.Height == 0)
                    {
                        FrameSize = new System.Drawing.SizeF(FrameSize.Width, VideoPlayerProperties.PortraitMode ? 320 : 240);
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
                    try
                    {
                        VideoPlayerProperties.LastTrack = track.Filename;
                        CurrentTrack = track;
                        CurrentFrame = 0;
                        if (player != null)
                        {
                            player.SetVideoFormat(VideoFormat, Width, Height, Pitch);
                        }
                        if (track.Filename.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || track.Filename.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                        {
                            media = new Media(libVLC, track.Filename, FromType.FromLocation);
                        }
                        else if (System.IO.File.Exists(track.Filename))
                        {
                            media = new Media(libVLC, track.Filename);
                        }
                        media.MetaChanged += Media_MetaChanged;
                        VideoPlayerProperties.SetPosition(position);
                        Properties.Name = title;
                        if (IsActive)
                        {
                            if (player != null)
                            {
                                player.Play(media);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    UpdateMessage("Select a video to play");
                }
            }
            else
            {
                UpdateMessage("LibVLC failed to initialize.");
            }
        }

        private bool _stopped = false;
        private void Stop()
        {
            _stopped = false;
            MediaPlayer oldPlayer = player;
            player = null;
            Error = null;
            SubTitle = null;
            Task.Run(() =>
            {
                if (oldPlayer != null)
                {
                    oldPlayer.Stop();
                    oldPlayer.Dispose();
                    oldPlayer = null;
                }
                _stopped = true;
            });
        }

        private void Media_MetaChanged(object sender, MediaMetaChangedEventArgs e)
        {
            if (player != null)
            {
                string meta = player.Media.Meta(e.MetadataType);
                if (e.MetadataType == MetadataType.ArtworkURL && !string.IsNullOrEmpty(meta))
                {
                    bool isLocal = false;
                    if (meta.StartsWith("file:///"))
                    {
                        meta = HttpUtility.UrlDecode(meta.Substring(8)).Replace("/", "\\");
                        isLocal = true;
                    }
                    else if (meta.StartsWith("file://"))
                    {
                        meta = HttpUtility.UrlDecode(meta.Substring(7)).Replace("/", "\\");
                        isLocal = true;
                    }
                    if (isLocal && File.Exists(meta))
                    {
                        CurrentTrack.Artwork = new Bitmap(meta);
                    }
                    else if (meta.StartsWith("http://") || meta.StartsWith("https://"))
                    {
                        FIPMediaFile track = CurrentTrack;
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            System.Drawing.Image img = meta.DownloadImageFromUrl();
                            if (img != null)
                            {
                                track.Artwork = new Bitmap(img);
                                UpdatePage();
                            }
                        });
                    }
                    else if (CurrentTrack.Artwork == null)
                    {
                        CurrentTrack.Artwork = new Bitmap(player.VideoTrack == -1 ? FIPToolKit.Properties.Resources.Music : FIPToolKit.Properties.Resources.Video);
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            UpdatePage();
                        });
                    }
                }
                else if (e.MetadataType == MetadataType.NowPlaying)
                {
                    SubTitle = meta;
                    if (CurrentTrack.Artwork == null)
                    {
                        CurrentTrack.Artwork = new Bitmap(player.VideoTrack == -1 && CurrentTrack.Filename.IsAudio()  ? FIPToolKit.Properties.Resources.Music : FIPToolKit.Properties.Resources.Video);
                    }
                    // May be an audio stream with no video.
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        UpdatePage();
                    });
                }
            }
        }

        public void UpdateSettings(int index, string name, string filename, Font font, Font subtitleFont, System.Drawing.Color fontColor, bool maintainAspectRatio, bool portraitMode, bool showControls, bool resumePlayback, bool pauseOtherMedia)
        {
            VideoPlayerProperties.IsDirty = true;
            VideoPlayerProperties.Name = name;
            VideoPlayerProperties.Font = font;
            VideoPlayerProperties.FontColor = fontColor;
            VideoPlayerProperties.SubtitleFont = subtitleFont;
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
            ThreadPool.QueueUserWorkItem(_ =>
            {
                UpdatePage();
            });
            OnSettingsUpdated?.Invoke(this, new FIPVideoPlayerEventArgs(this, index));
        }

        public bool Mute
        {
            get
            {
                return VideoPlayerProperties.Mute;
            }
            set
            {
                VideoPlayerProperties.Mute = value;
            }
        }

        public int Volume
        {
            get
            {
                return VideoPlayerProperties.Volume;
            }
            set
            {
                VideoPlayerProperties.Volume = value;
            }
        }
    }
}
