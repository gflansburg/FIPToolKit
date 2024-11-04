using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
	public class FIPSlideShowEventArgs : EventArgs
	{
		public FIPSlideShow Page { get; private set; }

		public FIPSlideShowEventArgs(FIPSlideShow page) : base()
		{
			Page = page;
		}
	}
	
    public class FIPSlideShow : FIPPage
    {
		[XmlIgnore]
		[JsonIgnore]
		public AbortableBackgroundWorker Timer { get; set; }
		private AbortableBackgroundWorker AnimationBackgroundWorker { get; set; }

		private bool StopAnimation { get; set; } //For stopping animated GIFs.
		private bool Stop { get; set; }

		public delegate void FIPSlideShowEventHandler(object sender, FIPSlideShowEventArgs e);
		public event FIPSlideShowEventHandler OnSlideShowLoop;

		public FIPSlideShow(FIPSlideShowProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
            properties.OnImagesChangedStart += Properties_OnImagesChangedStart;
            properties.OnImagesChangedEnd += Properties_OnImagesChangedEnd;
        }

        private void Properties_OnImagesChangedEnd(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void Properties_OnImagesChangedStart(object sender, EventArgs e)
        {
            StopTimer();
        }

        private FIPSlideShowProperties SlideShowProperties
		{
			get
			{
				return Properties as FIPSlideShowProperties;
            }
		}

        public override void StopTimer(int timeOut = 100)
		{
			if (Timer != null)
			{
				StopAnimating();
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
		}

		public override void StartTimer()
		{
			if (SlideShowProperties.Images.Count > 0)
			{
				Stop = false;
				StopAnimation = false;
				base.StartTimer();
				if (Timer == null)
				{
					Timer = new AbortableBackgroundWorker();
					Timer.DoWork += ProcessSlideShow;
					Timer.RunWorkerAsync(this);
				}
			}
			else
            {
				Bitmap bmp = Drawing.ImageHelper.GetErrorImage("Select some images to view.");
				SendImage(bmp);
				bmp.Dispose();
			}
		}

		private void ProcessSlideShow(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			try
			{
				int index = 0;
				bool isAnimating = false;
				DateTime frameStart = DateTime.Now;
				while (!Stop)
				{
					try
					{
						StopAnimating();
						isAnimating = false;
						if (SlideShowProperties.Images.Count > 0)
						{
							if (!Reload)
							{
								frameStart = DateTime.Now;
							}
							Reload = false;
							if (index >= SlideShowProperties.Images.Count)
							{
								index = 0;
								OnSlideShowLoop?.Invoke(this, new FIPSlideShowEventArgs(this));
							}
							if (System.IO.Path.GetExtension(SlideShowProperties.Images[index]).Equals(".gif", StringComparison.OrdinalIgnoreCase))
							{
								try
								{
									GifInfo gifInfo = new GifInfo(SlideShowProperties.Images[index]);
									if (gifInfo.Animated && gifInfo.Frames.Count > 1)
									{
										AnimationBackgroundWorker = new AbortableBackgroundWorker();
										AnimationBackgroundWorker.DoWork += AnimationBackgroundWorker_DoWork;
										AnimationBackgroundWorker.RunWorkerAsync(gifInfo);
										isAnimating = true;
									}
								}
								catch
								{
									//Don't animate I guess.
								}
							}
							if(!isAnimating)
							{
								Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
								using (Graphics graphics = Graphics.FromImage(bmp))
								{
									using (Bitmap image = new Bitmap(SlideShowProperties.Images[index]))
									{
										graphics.FillRectangle(Brushes.Black, 0, 0, 320, 240);
										double ratioX = (double)bmp.Width / image.Width;
										double ratioY = (double)bmp.Height / image.Height;
										double ratio = Math.Min(ratioX, ratioY);
										int newWidth = (int)(image.Width * ratio);
										int newHeight = (int)(image.Height * ratio);
										graphics.CompositingMode = CompositingMode.SourceCopy;
										graphics.CompositingQuality = CompositingQuality.HighQuality;
										graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
										graphics.SmoothingMode = SmoothingMode.HighQuality;
										graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
										using (var wrapMode = new ImageAttributes())
										{
											wrapMode.SetWrapMode(WrapMode.TileFlipXY);
											graphics.DrawImage(image, new Rectangle((320 - newWidth) / 2, (240 - newHeight) / 2, newWidth, newHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
										}
									}
									DrawButtons(graphics);
								}
								SendImage(bmp);
								bmp.Dispose();
							}
							int delay = Math.Max(0, SlideShowProperties.Delay - (int)(DateTime.Now - frameStart).TotalMilliseconds);
							while (delay > 0)
							{
								Thread.Sleep(10);
								delay = Math.Max(0, SlideShowProperties.Delay - (int)(DateTime.Now - frameStart).TotalMilliseconds);
								if(Reload || Stop)
                                {
									break;
                                }
							}
							if (!Reload)
							{
								index++;
							}
							FireStateChanged();
						}
					}
					catch
					{
					}
				}
			}
			finally
			{
			}
		}

		private void StopAnimating(int timeOut = 100)
        {
			if (AnimationBackgroundWorker != null)
			{
				StopAnimation = true;
				DateTime stopTime = DateTime.Now;
				while (AnimationBackgroundWorker.IsRunning)
				{
					TimeSpan span = DateTime.Now - stopTime;
					if (span.TotalMilliseconds > timeOut)
					{
						break;
					}
					Thread.Sleep(100);
					if (AnimationBackgroundWorker == null)
					{
						break;
					}
				}
				if (AnimationBackgroundWorker != null && AnimationBackgroundWorker.IsRunning)
				{
					AnimationBackgroundWorker.Abort();
				}
				AnimationBackgroundWorker = null;
			}
		}

		private void AnimationBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
			GifInfo gifInfo = e.Argument as GifInfo;
			StopAnimation = false;
			int frame = 0;
			DateTime frameStart;
			try
			{
				while (!StopAnimation)
				{
					frameStart = DateTime.Now;
					Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
					using (Graphics graphics = Graphics.FromImage(bmp))
					{
						graphics.FillRectangle(Brushes.Black, 0, 0, 320, 240);
						Image image = gifInfo.Frames[frame].Image;
						double ratioX = (double)bmp.Width / image.Width;
						double ratioY = (double)bmp.Height / image.Height;
						double ratio = Math.Min(ratioX, ratioY);
						int newWidth = (int)(image.Width * ratio);
						int newHeight = (int)(image.Height * ratio);
						graphics.CompositingMode = CompositingMode.SourceCopy;
						graphics.CompositingQuality = CompositingQuality.HighQuality;
						graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphics.SmoothingMode = SmoothingMode.HighQuality;
						graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
						using (var wrapMode = new ImageAttributes())
						{
							wrapMode.SetWrapMode(WrapMode.TileFlipXY);
							graphics.DrawImage(image, new Rectangle((320 - newWidth) / 2, (240 - newHeight) / 2, newWidth, newHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
						}
						DrawButtons(graphics);
					}
					SendImage(bmp);
					bmp.Dispose();
					Thread.Sleep(Math.Max(0, gifInfo.Frames[frame].Delay - (int)(DateTime.Now - frameStart).TotalMilliseconds));
					frame++;
					if (frame >= gifInfo.Frames.Count)
					{
						frame = 0;
					}
				}
			}
			finally
			{
				if (gifInfo != null)
				{
					gifInfo.Dispose();
				}
			}
		}
    }
}
