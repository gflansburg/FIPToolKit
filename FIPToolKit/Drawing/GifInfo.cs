using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FIPToolKit.Drawing
{
    public class GifFrame : IDisposable
    {
        public Image Image { get; set; }
        public int Delay { get; set; }

        ~GifFrame()
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

    public class GifInfo : IDisposable
    {
        #region Fileds  
        private FileInfo fileInfo;
        private IList<GifFrame> frames;
        private Size size;
        private bool animated;
        private bool loop;
        private int frameRate;
        private TimeSpan animationDuration;
        #endregion

        #region Properties  
        public FileInfo FileInfo
        {
            get
            {
                return this.fileInfo;
            }
        }

        public IList<GifFrame> Frames
        {
            get
            {
                return this.frames;
            }
        }

        public int FrameRate
        {
            get
            {
                return this.frameRate;
            }
        }

        public Size Size
        {
            get
            {
                return this.size;
            }
        }

        public bool Animated
        {
            get
            {
                return this.animated;
            }
        }

        public bool Loop
        {
            get
            {
                return this.loop;
            }
        }

        public TimeSpan AnimationDuration
        {
            get
            {
                return this.animationDuration;
            }
        }

        #endregion

        #region Constructors  

        ~GifInfo()
        {
            Dispose();
        }

        public GifInfo(String filePath)
        {
            this.frames = new List<GifFrame>();
            if (File.Exists(filePath))
            {
                using (var image = Image.FromFile(filePath))
                {
                    this.size = new Size(image.Width, image.Height);
                    if (image.RawFormat.Equals(ImageFormat.Gif))
                    {
                        this.fileInfo = new FileInfo(filePath);
                        if (ImageAnimator.CanAnimate(image))
                        {
                            //Get frames  
                            var dimension = new FrameDimension(image.FrameDimensionsList[0]);
                            int frameCount = image.GetFrameCount(dimension);

                            int index = 0;
                            int duration = 0;
                            for (int i = 0; i < frameCount; i++)
                            {
                                image.SelectActiveFrame(dimension, i);
                                var delay = BitConverter.ToInt32(image.GetPropertyItem(20736).Value, index) * 10;
                                delay = (delay < 100 ? 100 : delay);
                                var frame = new GifFrame() { Image = image.Clone() as Image, Delay = delay };
                                frames.Add(frame);
                                duration += delay;
                                index += 4;
                            }
                            this.frameRate = duration / (1000 * frameCount);
                            this.animationDuration = TimeSpan.FromMilliseconds(duration);
                            this.animated = true;
                            this.loop = BitConverter.ToInt16(image.GetPropertyItem(20737).Value, 0) != 1;
                        }
                        else
                        {
                            var frame = new GifFrame() { Image = image.Clone() as Image };
                            this.frames.Add(frame);
                        }
                    }
                    else
                    {
                        throw new FormatException("Not valid GIF image format");
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (GifFrame frame in this.frames)
            {
                frame.Dispose();
            }
            frames.Clear();
            frames = new List<GifFrame>();
        }
        #endregion
    }
}
