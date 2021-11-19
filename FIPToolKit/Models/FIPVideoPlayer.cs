using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        [XmlIgnore]
        [JsonIgnore]
        public AbortableBackgroundWorker Timer { get; set; }

        private AbortableBackgroundWorker VideoBuffer { get; set; }

        private Accord.Video.FFMPEG.VideoFileReader VideoFileReader { get; set; }

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
                    StopTimer();
                    _filename = value;
                    StartTimer();
                    IsDirty = true;
                }
            }
        }

        private List<VideoFrame> Images { get; set; }
        private bool StopBuffering { get; set; }
        private bool Stop { get; set; }
        private Size Size { get; set; }
        private int FrameCount { get; set; }
        private double FrameRate { get; set; }
        private int StartFrame { get; set; }
        private bool FirstFrame { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public static bool PreviewVideo { get; set; }

        public delegate void FIPVideoPlayerEventHandler(object sender, FIPVideoPlayerEventArgs e);
        public event FIPVideoPlayerEventHandler OnVideoLoop;

        public FIPVideoPlayer() : base()
        {
            Name = "Video Player";
            IsDirty = false;
        }

		public override void StopTimer(int timeOut = 100)
		{
			if (Timer != null)
			{
                StopVideoBuffer(timeOut);
                Stop = true;
                DateTime stopTime = DateTime.Now;
                while (Timer.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > timeOut)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                    if (Timer == null)
                    {
                        break;
                    }
                }
                if (Timer != null && Timer.IsRunning)
                {
                    Timer.Abort();
                }
				Timer = null;
            }
            if(VideoFileReader != null)
            {
                VideoFileReader.Close();
                VideoFileReader.Dispose();
                VideoFileReader = null;
            }
            if (Images != null)
            {
                Images.Clear();
                Images = null;
            }
        }

        public override void StartTimer()
		{
            if (!String.IsNullOrEmpty(Filename) && System.IO.File.Exists(Filename))
            {
                if (Timer == null)
                {
                    base.StartTimer();
                    Stop = false;
                    StopBuffering = false;
                    VideoFileReader = new Accord.Video.FFMPEG.VideoFileReader();
                    VideoFileReader.Open(Filename);
                    Timer = new AbortableBackgroundWorker();
                    Timer.DoWork += ProcessVideo;
                    Timer.RunWorkerAsync(this);
                }
            }
            else
            {
                Bitmap bmp = Drawing.ImageHelper.GetErrorImage("Select a video to play.");
                SendImage(bmp);
                bmp.Dispose();
            }
        }

        private Bitmap GetBufferingImage(string text)
        {
            Bitmap bmp = new System.Drawing.Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.White))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    using (Font font = new Font(System.Drawing.SystemFonts.DefaultFont.FontFamily, 12.0f, System.Drawing.FontStyle.Bold))
                    {
                        SizeF size = graphics.MeasureString("Buffering...", font);
                        int x = (int)((bmp.Width - size.Width) / 2);
                        int y = (int)((bmp.Height - size.Height) / 2);
                        System.Drawing.Point p = new System.Drawing.Point(x, y);
                        graphics.DrawString(text, font, brush, p);
                    }
                }
            }
            return bmp;
        }

        private class VideoFrame : IDisposable
        {
            public Bitmap Image { get; set; }
            public int Frame { get; set; }
            public bool Reload { get; set; }
            public int Delay { get; set; }

            ~VideoFrame()
            {
                Dispose();
            }

            public void Dispose()
            {
                if(Image != null)
                {
                    Image.Dispose();
                    Image = null;
                }
            }
        }

        private void ProcessVideo(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
            try
			{
                int bufferTime = 1;
                int frame = 0;
                DateTime frameStart;
                FirstFrame = true;
                FrameCount = (int)VideoFileReader.FrameCount;
                FrameRate = (double)VideoFileReader.FrameRate;
                Size = new Size(VideoFileReader.Width, VideoFileReader.Height);
                Images = new List<VideoFrame>(FrameCount);
                for (int i = 0; i < FrameCount; i++)
                {
                    Images.Add(null);
                }
                // SD
                if (Size.Width >= 640 || Size.Height >= 480)
                {
                    bufferTime = 2;
                    // HD
                    if (Size.Width >= 1280 || Size.Height >= 720)
                    {
                        bufferTime = 4;
                        // Full HD
                        if (Size.Width >= 1920 || Size.Height >= 1080)
                        {
                            bufferTime = 8;
                            // 4K
                            if (Size.Width >= 3840 || Size.Height >= 2160)
                            {
                                bufferTime = 32;
                            }
                        }
                    }
                }
                VideoBuffer = new AbortableBackgroundWorker();
                VideoBuffer.DoWork += VideoBuffer_DoWork;
                VideoBuffer.RunWorkerAsync();
                DateTime startTime = DateTime.Now;
                while (!Stop)
                {
                    frameStart = DateTime.Now;
                    if (Reload)
                    {
                        Reload = false;
                        for (int i = 0; i < Images.Count; i++)
                        {
                            if(Stop)
                            {
                                break;
                            }
                            if (Images[i] != null)
                            {
                                Images[i].Reload = true;
                            }
                        }
                        StartFrame = frame;
                    }
                    if (!Stop)
                    {
                        if (Images[frame] == null)
                        {
                            //Buffering Video.
                            StartFrame = frame;
                            int bufferSize = (int)(FrameRate * bufferTime);
                            int lastFrame = Math.Min(frame + bufferSize, FrameCount) - 1;
                            int dotCount = -1;
                            DateTime bufferStartTime = DateTime.Now;
                            while (!Stop)
                            {
                                if (dotCount == -1 || (DateTime.Now - bufferStartTime).TotalMilliseconds >= 500)
                                {
                                    string text = "Buffering";
                                    dotCount = Math.Max(dotCount, 0);
                                    for (int i = 0; i < dotCount; i++)
                                    {
                                        text += ".";
                                    }
                                    Bitmap bufferingImage = GetBufferingImage(text);
                                    SendImage(bufferingImage);
                                    bufferingImage.Dispose();
                                    dotCount++;
                                    if (dotCount > 3)
                                    {
                                        dotCount = 0;
                                    }
                                    bufferStartTime = DateTime.Now;
                                }
                                Thread.Sleep(10);
                                if (Images[frame] != null && Images[lastFrame] != null)
                                {
                                    break;
                                }
                            }
                            startTime += (DateTime.Now - frameStart);
                        }
                        if (!Stop)
                        {
                            VideoFrame videoFrame = Images[frame];
                            if (videoFrame != null && videoFrame.Image != null)
                            {
                                SendImage(videoFrame.Image, FirstFrame || PreviewVideo);
                                FirstFrame = false;
                                Thread.Sleep(Math.Max(0, videoFrame.Delay - (int)(DateTime.Now - frameStart).TotalMilliseconds));
                            }
                            frame = Math.Max(frame, GetFrameNumber(DateTime.Now - startTime));
                            if (frame >= FrameCount)
                            {
                                startTime = DateTime.Now;
                                frame = 0;
                                OnVideoLoop?.Invoke(this, new FIPVideoPlayerEventArgs(this));
                            }
                        }
                    }
                }
                StopVideoBuffer();
			}
            catch(System.Threading.ThreadAbortException)
            {
                //We can ignore this. Stopping the playback sometimes takes a bit longer than normal.
            }
            catch(Exception ex)
            {
                //Something bad happend.  Video Codec issues?
                Bitmap bmp = Drawing.ImageHelper.GetErrorImage(ex.Message);
                SendImage(bmp);
                bmp.Dispose();
            }
        }
        
        private void StopVideoBuffer(int timeOut = 100)
        {
            if (VideoBuffer != null)
            {
                StopBuffering = true;
                DateTime stopTime = DateTime.Now;
                while (VideoBuffer.IsRunning)
                {
                    TimeSpan span = DateTime.Now - stopTime;
                    if (span.TotalMilliseconds > timeOut)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                if (VideoBuffer.IsRunning)
                {
                    VideoBuffer.Abort();
                }
                VideoBuffer = null;
            }
        }

        public int GetFrameNumber(TimeSpan span)
        {
            return (int)(span.TotalMilliseconds / (1000 / this.FrameRate));
        }

        public TimeSpan GetTimeForFrame(int frame)
        {
            return new TimeSpan(0, 0, 0, 0, (int)((1000 / this.FrameRate) * frame));
        }

        private void VideoBuffer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                StopBuffering = false;
                int frame = Math.Max(0, StartFrame);
                StartFrame = -1;
                while (!StopBuffering)
                {
                    if (VideoFileReader == null || !VideoFileReader.IsOpen)
                    {
                        break;
                    }
                    if (StartFrame != -1)
                    {
                        frame = StartFrame;
                        StartFrame = -1;
                    }
                    if (Images[frame] == null || Images[frame].Reload)
                    {
                        try
                        {
                            if(Images[frame] == null)
                            {
                                Images[frame] = new VideoFrame();
                            }
                            Image image = VideoFileReader.ReadVideoFrame(frame);
                            Images[frame].Reload = false;
                            Images[frame].Delay = (int)(1000 / FrameRate);
                            Images[frame].Image = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
                            Images[frame].Frame = frame;
                            double ratioX = (double)Images[frame].Image.Width / image.Width;
                            double ratioY = (double)Images[frame].Image.Height / image.Height;
                            double ratio = Math.Min(ratioX, ratioY);
                            int newWidth = (int)(image.Width * ratio);
                            int newHeight = (int)(image.Height * ratio);
                            using (Graphics graphics = Graphics.FromImage(Images[frame].Image))
                            {
                                graphics.FillRectangle(Brushes.Black, 0, 0, 320, 240);
                                graphics.DrawImage(image, new Rectangle((320 - newWidth) / 2, (240 - newHeight) / 2, newWidth, newHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                                DrawButtons(graphics);
                            }
                        }
                        catch
                        {
                            break;
                        }
                    }
                    frame++;
                    if (frame >= FrameCount)
                    {
                        frame = 0;
                    }
                }
            }
            catch
            {
                // Thread aborted?
            }
        }
    }
}
