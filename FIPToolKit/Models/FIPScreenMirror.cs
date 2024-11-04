using FIPToolKit.Drawing;
using FIPToolKit.Threading;
using FIPToolKit.Tools;
using Newtonsoft.Json;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public class FIPScreenMirror : FIPPage
    {
		[XmlIgnore]
		[JsonIgnore]
		public AbortableBackgroundWorker Timer { get; set; }

		private bool Stop { get; set; }

		public FIPScreenMirror(FIPScreenMirrorProperties properties) : base(properties)
        {
            Properties.ControlType = GetType().FullName;
        }

        private FIPScreenMirrorProperties ScreenMirrorProperties
		{
			get
			{
				return Properties as FIPScreenMirrorProperties;
			}
		}

        public override void Dispose()
        {
			base.Dispose();
        }

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
			switch(softButton)
            {
				case SoftButtons.Button6:
				case SoftButtons.Left:
				case SoftButtons.Right:
				case SoftButtons.Up:
				case SoftButtons.Down:
					return false;
            }
            return true;
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
			switch(softButton)
            {
				case SoftButtons.Button6:
				case SoftButtons.Left:
				case SoftButtons.Right:
				case SoftButtons.Up:
				case SoftButtons.Down:
					return true;
			}
			return base.IsLEDOn(softButton);
		}

		public override void ExecuteSoftButton(SoftButtons softButton)
        {
			switch (softButton)
            {
				case SoftButtons.Button6:
                    {
						int index = ScreenMirrorProperties.ScreenIndex;
						index++;
						if(index >= System.Windows.Forms.Screen.AllScreens.Length)
                        {
							index = 0;
                        }
                        ScreenMirrorProperties.ScreenIndex = index;
                    }
					break;
				case SoftButtons.Right:
                    {
						int index = (int)ScreenMirrorProperties.Fit;
						index++;
						if(index > 2)
                        {
							index = 0;
                        }
                        ScreenMirrorProperties.Fit = (ScreenFit)index;
                    }
					break;
				case SoftButtons.Left:
					{
						int index = (int)ScreenMirrorProperties.Fit;
						index--;
						if (index < 0)
						{
							index = 2;
						}
                        ScreenMirrorProperties.Fit = (ScreenFit)index;
					}
					break;
				case SoftButtons.Up:
					KeyPress.KeyBdEvent(KeyPressLengths.Zero, new VirtualKeyCode[] { VirtualKeyCode.VOLUME_UP }, KeyPressLengths.ThirtyTwoMilliseconds);
					break;
				case SoftButtons.Down:
					KeyPress.KeyBdEvent(KeyPressLengths.Zero, new VirtualKeyCode[] { VirtualKeyCode.VOLUME_DOWN }, KeyPressLengths.ThirtyTwoMilliseconds);
					break;
				default:
					base.ExecuteSoftButton(softButton);
					break;
            }
			FireSoftButtonNotifcation(softButton);
        }

        public override void StopTimer(int timeOut = 100)
		{
			if (Timer != null)
			{
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
			Stop = false;
			base.StartTimer();
			if (Timer == null)
			{
				Timer = new AbortableBackgroundWorker();
				Timer.DoWork += CaptureScreen;
				Timer.RunWorkerAsync(this);
			}
		}

		private void CaptureScreen(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			try
			{
				while (!Stop)
				{
					try
					{
						Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
						using (Graphics graphics = Graphics.FromImage(bmp))
						{
							using (Bitmap screen = ImageHelper.CaptureScreen(ScreenMirrorProperties.ScreenIndex))
							{
								if (screen != null)
								{
									graphics.FillRectangle(Brushes.Black, 0, 0, 320, 240);
									double ratioX = (double)bmp.Width / screen.Width;
									double ratioY = (double)bmp.Height / screen.Height;
									double ratio = Math.Min(ratioX, ratioY);
									int newWidth = (int)(screen.Width * ratio);
									int newHeight = (int)(screen.Height * ratio);
									Rectangle destRect = new Rectangle((320 - newWidth) / 2, (240 - newHeight) / 2, newWidth, newHeight);
									Rectangle srcRect = new Rectangle(0, 0, screen.Width, screen.Height);
									if (ScreenMirrorProperties.Fit == ScreenFit.Stretch)
									{
										destRect = new Rectangle(0, 0, 320, 240);
									}
									if (ScreenMirrorProperties.Fit == ScreenFit.Fill)
									{
										using (Image img = screen.Crop())
										{
											destRect = new Rectangle(0, 0, 320, 240);
											srcRect = new Rectangle(0, 0, img.Width, img.Height);
											graphics.DrawImage(img, destRect, srcRect, GraphicsUnit.Pixel);
										}
									}
									else
									{
										graphics.DrawImage(screen, destRect, srcRect, GraphicsUnit.Pixel);
									}
								}
							}
						}
						SendImage(bmp);
						bmp.Dispose();
						Thread.Sleep(10);
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
	}
}
