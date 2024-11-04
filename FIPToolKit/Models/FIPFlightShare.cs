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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    public class FIPFlightShare : FIPPage
    {
		[XmlIgnore]
		[JsonIgnore]
		public AbortableBackgroundWorker Timer { get; set; }

		private bool Stop { get; set; }

		[XmlIgnore]
		[JsonIgnore]
		public IntPtr Map { get; set; }

		[XmlIgnore]
		[JsonIgnore]
		public IntPtr FollowMyPlane { get; set; }

		[XmlIgnore]
		[JsonIgnore]
		public IntPtr LockWaypoints { get; set; }

		[XmlIgnore]
		[JsonIgnore]
		public IntPtr ClearTracks { get; set; }

		[XmlIgnore]
		[JsonIgnore]
		public IntPtr GoToMyPlane { get; set; }

		private const int PIXEL_SCROLL = 50;

		public FIPFlightShare(FIPPageProperties properties) : base(properties)
        {
            Properties.Name = "Flight Share";
            Properties.IsDirty = false;
            Properties.ControlType = GetType().FullName;
            Map = IntPtr.Zero;
			FollowMyPlane = IntPtr.Zero;
			LockWaypoints = IntPtr.Zero;
			ClearTracks = IntPtr.Zero;
			GoToMyPlane = IntPtr.Zero;
		}

        public override void Dispose()
        {
			base.Dispose();
        }

		public static bool CloseFlightShare()
        {
			List<NativeMethods.WindowTitle> flightShare = NativeMethods.FindWindowsStartsWithText("FlightShare").ToList();
			if (flightShare != null && flightShare.Count == 1)
			{
				NativeMethods.PostMessage(flightShare[0].MainWindowHandle, NativeMethods.WM_CLOSE, 0, 0);
				return true;
			}
			return false;
		}

        public override bool IsButtonAssignable(SoftButtons softButton)
        {
            return false;
        }

        public override bool IsLEDOn(SoftButtons softButton)
        {
			switch(softButton)
            {
				case SoftButtons.Button1:
					return GoToMyPlane != IntPtr.Zero;
				case SoftButtons.Button2:
					return FollowMyPlane != IntPtr.Zero;
				case SoftButtons.Button3:
					return LockWaypoints != IntPtr.Zero;
				case SoftButtons.Button4:
					return ClearTracks != IntPtr.Zero;
				case SoftButtons.Button5:
					return Map != IntPtr.Zero;
				case SoftButtons.Button6:
					return Map != IntPtr.Zero;
			}
			return false;
		}

		private static int directionUp = 1;
		private static int directionDown = -1;

		public override void ExecuteSoftButton(SoftButtons softButton)
        {
			switch (softButton)
            {
				case SoftButtons.Button1:   //Go To My Plane
					if (GoToMyPlane != IntPtr.Zero)
					{
						NativeMethods.SendMessage(GoToMyPlane, NativeMethods.WM_CLICK, IntPtr.Zero, IntPtr.Zero);
					}
					break;
				case SoftButtons.Button2:   //Follow My Plane
					if (FollowMyPlane != IntPtr.Zero)
					{
						NativeMethods.SendMessage(FollowMyPlane, NativeMethods.WM_CLICK, IntPtr.Zero, IntPtr.Zero);
					}
					break;
				case SoftButtons.Button3:   //Lock Waypoints
					if (LockWaypoints != IntPtr.Zero)
					{
						NativeMethods.SendMessage(LockWaypoints, NativeMethods.WM_CLICK, IntPtr.Zero, IntPtr.Zero);
					}
					break;
				case SoftButtons.Button4:   //Clear Tracks
					if (ClearTracks != IntPtr.Zero)
					{
						NativeMethods.SendMessage(ClearTracks, NativeMethods.WM_CLICK, IntPtr.Zero, IntPtr.Zero);
					}
					break;
				case SoftButtons.Button5:   //Zoom Map In
					if(Map != IntPtr.Zero)
					{
						Rect rect = new Rect();
						IntPtr error = NativeMethods.GetWindowRect(Map, ref rect);
						// sometimes it gives error.
						while (error == (IntPtr)0)
						{
							error = NativeMethods.GetWindowRect(Map, ref rect);
						}
						//Find the center of the map in screen corrdinates
						Point p = new Point(rect.left + ((rect.right - rect.left) / 2), rect.top + ((rect.bottom - rect.top) / 2));
						IntPtr wParam = NativeMethods.MAKEWPARAM(directionUp, 1, WinMsgMouseKey.MK_NONE);
						IntPtr lParam = NativeMethods.MAKELPARAM(p.X, p.Y);
						NativeMethods.PostMessage(Map, NativeMethods.WM_MOUSEMOVE, wParam, lParam);
						NativeMethods.PostMessage(Map, NativeMethods.WM_MOUSEWHEEL, wParam, lParam);
					}
					break;
				case SoftButtons.Button6:   //Zoom Map Out
					if (Map != IntPtr.Zero)
					{
						Rect rect = new Rect();
						IntPtr error = NativeMethods.GetWindowRect(Map, ref rect);
						// sometimes it gives error.
						while (error == (IntPtr)0)
						{
							error = NativeMethods.GetWindowRect(Map, ref rect);
						}
						//Find the center of the map in screen corrdinates
						Point p = new Point(rect.left + ((rect.right - rect.left) / 2), rect.top + ((rect.bottom - rect.top) / 2));
						IntPtr wParam = NativeMethods.MAKEWPARAM(directionDown, 1, WinMsgMouseKey.MK_NONE);
						IntPtr lParam = NativeMethods.MAKELPARAM(p.X, p.Y);
						NativeMethods.PostMessage(Map, NativeMethods.WM_MOUSEMOVE, wParam, lParam);
						NativeMethods.PostMessage(Map, NativeMethods.WM_MOUSEWHEEL, wParam, lParam);
					}
					break;
				case SoftButtons.Right:      //Scroll Map Right
					if (Map != IntPtr.Zero)
					{
						Rect rect = new Rect();
						IntPtr error = NativeMethods.GetWindowRect(Map, ref rect);
						// sometimes it gives error.
						while (error == (IntPtr)0)
						{
							error = NativeMethods.GetWindowRect(Map, ref rect);
						}
						//Find the center of the map in screen corrdinates
						Point p = new Point(rect.left + ((rect.right - rect.left) / 2), rect.top + ((rect.bottom - rect.top) / 2));
						NativeMethods.ScreenToClient(Map, ref p);
						IntPtr wParam = (IntPtr)NativeMethods.MK_LBUTTON;
						NativeMethods.PostMessage(Map, NativeMethods.WM_LBUTTONDOWN, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
						p.X = Math.Max(0, p.X - PIXEL_SCROLL);
						NativeMethods.PostMessage(Map, NativeMethods.WM_MOUSEMOVE, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
						NativeMethods.PostMessage(Map, NativeMethods.WM_LBUTTONUP, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
					}
					break;
				case SoftButtons.Left:     //Scroll Map Left
					if (Map != IntPtr.Zero)
					{
						Rect rect = new Rect();
						IntPtr error = NativeMethods.GetWindowRect(Map, ref rect);
						// sometimes it gives error.
						while (error == (IntPtr)0)
						{
							error = NativeMethods.GetWindowRect(Map, ref rect);
						}
						//Find the center of the map in screen corrdinates
						Point p = new Point(rect.left + ((rect.right - rect.left) / 2), rect.top + ((rect.bottom - rect.top) / 2));
						NativeMethods.ScreenToClient(Map, ref p);
						IntPtr wParam = (IntPtr)NativeMethods.MK_LBUTTON;
						NativeMethods.PostMessage(Map, NativeMethods.WM_LBUTTONDOWN, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
						p.X = Math.Min(rect.right - rect.left, p.X + PIXEL_SCROLL);
						NativeMethods.PostMessage(Map, NativeMethods.WM_MOUSEMOVE, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
						NativeMethods.PostMessage(Map, NativeMethods.WM_LBUTTONUP, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
					}
					break;
				case SoftButtons.Up:      //Scroll Map Up
					if (Map != IntPtr.Zero)
					{
						Rect rect = new Rect();
						IntPtr error = NativeMethods.GetWindowRect(Map, ref rect);
						// sometimes it gives error.
						while (error == (IntPtr)0)
						{
							error = NativeMethods.GetWindowRect(Map, ref rect);
						}
						//Find the center of the map in screen corrdinates
						Point p = new Point(rect.left + ((rect.right - rect.left) / 2), rect.top + ((rect.bottom - rect.top) / 2));
						NativeMethods.ScreenToClient(Map, ref p);
						IntPtr wParam = (IntPtr)NativeMethods.MK_LBUTTON;
						NativeMethods.PostMessage(Map, NativeMethods.WM_LBUTTONDOWN, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
						p.Y = Math.Min(rect.right - rect.left, p.Y + PIXEL_SCROLL);
						NativeMethods.PostMessage(Map, NativeMethods.WM_MOUSEMOVE, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
						NativeMethods.PostMessage(Map, NativeMethods.WM_LBUTTONUP, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
					}
					break;
				case SoftButtons.Down:        //Scroll Map Down
					if (Map != IntPtr.Zero)
					{
						Rect rect = new Rect();
						IntPtr error = NativeMethods.GetWindowRect(Map, ref rect);
						// sometimes it gives error.
						while (error == (IntPtr)0)
						{
							error = NativeMethods.GetWindowRect(Map, ref rect);
						}
						//Find the center of the map in screen corrdinates
						Point p = new Point(rect.left + ((rect.right - rect.left) / 2), rect.top + ((rect.bottom - rect.top) / 2));
						NativeMethods.ScreenToClient(Map, ref p);
						IntPtr wParam = (IntPtr)NativeMethods.MK_LBUTTON;
						NativeMethods.PostMessage(Map, NativeMethods.WM_LBUTTONDOWN, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
						p.Y = Math.Max(0, p.Y - PIXEL_SCROLL);
						NativeMethods.PostMessage(Map, NativeMethods.WM_MOUSEMOVE, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
						NativeMethods.PostMessage(Map, NativeMethods.WM_LBUTTONUP, wParam, NativeMethods.MAKELPARAM(p.X, p.Y));
					}
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
				Timer.DoWork += ProcessFlightShare;
				Timer.RunWorkerAsync(this);
			}
		}

		private WindowHandle FindWindow(List<WindowHandle> childHandles, string name)
		{
			foreach (WindowHandle windowHandle in childHandles)
			{
				string caption = NativeMethods.GetText(windowHandle.hWnd);
				if (caption.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					return windowHandle;
				}
				WindowHandle child = FindWindow(windowHandle.Children, name);
				if (child != null)
				{
					return child;
				}
			}
			return null;
		}

		private void ProcessFlightShare(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			try
			{
				bool isLoading = false;
				while (!Stop)
				{
					try
					{
						List<NativeMethods.WindowTitle> flightShare = NativeMethods.FindWindowsStartsWithText("FlightShare").ToList();
						if (flightShare != null && flightShare.Count == 1)
						{
							if (Map == IntPtr.Zero)
							{
								if(isLoading)
                                {
									//FlightShare just started, give it time to finish loading.
									Thread.Sleep(3000);
                                }
								try
								{
									List<WindowHandle> allChildWindows = new WindowHandleInfo(flightShare[0].MainWindowHandle).GetAllChildHandles();
									FollowMyPlane = FindWindow(allChildWindows, "Follow My Plane") != null ? FindWindow(allChildWindows, "Follow My Plane").hWnd : IntPtr.Zero;
									LockWaypoints = FindWindow(allChildWindows, "Lock Waypoints") != null ? FindWindow(allChildWindows, "Lock Waypoints").hWnd : IntPtr.Zero;
									ClearTracks = FindWindow(allChildWindows, "Clear Tracks") != null ? FindWindow(allChildWindows, "Clear Tracks").hWnd : IntPtr.Zero;
									GoToMyPlane = FindWindow(allChildWindows, "Go to My Plane") != null ? FindWindow(allChildWindows, "Go to My Plane").hWnd : IntPtr.Zero;
									WindowHandle airPortCode = FindWindow(allChildWindows, "AIRPORT CODE");
									if (airPortCode != null)
									{
										Map = NativeMethods.FindWindowEx(airPortCode.hWndParent, airPortCode.hWnd, "WindowsForms10.Window.8.app.0.2386859_r6_ad1", String.Empty);
									}
									SetLEDs();
									isLoading = false;
								}
								catch
                                {
                                }
							}
							if (Map != IntPtr.Zero)
							{
								//Ensure FlightShare is visible.
								WINDOWPLACEMENT wnd = NativeMethods.GetPlacement(flightShare[0].MainWindowHandle);
								if (wnd.showCmd == ShowWindowCommands.Minimized || wnd.showCmd == ShowWindowCommands.Hide)
								{
									NativeMethods.ShowWindow(flightShare[0].MainWindowHandle, ShowWindowFlags.SW_RESTORE);
								}
								Size size = new Size(wnd.rcNormalPosition.Size.Width, wnd.rcNormalPosition.Size.Height);
								Bitmap bmp = new Bitmap(320, 240, PixelFormat.Format32bppArgb);
								using (Graphics graphics = Graphics.FromImage(bmp))
								{
									using (Bitmap map = ImageHelper.CaptureWindow(Map))
									{
										graphics.FillRectangle(Brushes.Black, 0, 0, 320, 240);
										double ratioX = 286d / map.Width; // Give some room for the softbutton icons
										double ratioY = (double)bmp.Height / map.Height;
										double ratio = Math.Min(ratioX, ratioY);
										int newWidth = (int)(map.Width * ratio);
										int newHeight = (int)(map.Height * ratio);
										Rectangle destRect = new Rectangle(34 + ((286 - newWidth) / 2), (240 - newHeight) / 2, newWidth, newHeight);
										Rectangle srcRect = new Rectangle(0, 0, map.Width, map.Height);
										graphics.DrawImage(map, destRect, srcRect, GraphicsUnit.Pixel);
										graphics.AddButtonIcon(FIPToolKit.Properties.Resources.go, Color.Black, false, SoftButtons.Button1);
										graphics.AddButtonIcon(FIPToolKit.Properties.Resources.follow, Color.Black, false, SoftButtons.Button2);
										graphics.AddButtonIcon(FIPToolKit.Properties.Resources._lock, Color.Black, false, SoftButtons.Button3);
										graphics.AddButtonIcon(FIPToolKit.Properties.Resources.clear, Color.Black, false, SoftButtons.Button4);
										graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomin, Color.Black, false, SoftButtons.Button5);
										graphics.AddButtonIcon(FIPToolKit.Properties.Resources.map_zoomout, Color.Black, false, SoftButtons.Button6);
									}
								}
								SendImage(bmp);
								bmp.Dispose();
							}
						}
						else if (!isLoading)
                        {
							if (Map != IntPtr.Zero || !isLoading)
							{
								Bitmap bmp = Drawing.ImageHelper.GetErrorImage("FlightShare isn't running.\nPlease start FlightShare.");
								SendImage(bmp);
								bmp.Dispose();
								Map = IntPtr.Zero;
								FollowMyPlane = IntPtr.Zero;
								LockWaypoints = IntPtr.Zero;
								ClearTracks = IntPtr.Zero;
								GoToMyPlane = IntPtr.Zero;
								SetLEDs();
							}
							isLoading = true;
						}
						//10 frames per second should be good enough
						Thread.Sleep(100);
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
