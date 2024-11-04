using FIPToolKit.Tools;
using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Drawing
{
    public static class ImageHelper
    {
        public static bool IsImageTransparent(this Bitmap image, string optionalBgColorGhost = null)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var pixel = image.GetPixel(i, j);
                    if (pixel.A != 255)
                    {
                        return true;
                    }
                }
            }
            //Check 4 corners to check if all of them are with the same color!
            if (!string.IsNullOrEmpty(optionalBgColorGhost))
            {
                if (image.GetPixel(0, 0).ToArgb() == GetColorFromString(optionalBgColorGhost).ToArgb())
                {
                    if (image.GetPixel(image.Width - 1, 0).ToArgb() == GetColorFromString(optionalBgColorGhost).ToArgb())
                    {
                        if (image.GetPixel(0, image.Height - 1).ToArgb() ==
                            GetColorFromString(optionalBgColorGhost).ToArgb())
                        {
                            if (image.GetPixel(image.Width - 1, image.Height - 1).ToArgb() == GetColorFromString(optionalBgColorGhost).ToArgb())
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static Color GetColorFromString(string colorHex)
        {
            return ColorTranslator.FromHtml(colorHex);
        }
        static public Image GetBitmapResource(string filename, bool isIcon = false)
        {
            if (filename.StartsWith("resources:"))
            {
                System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("FIPToolKit.Properties.Resources", typeof(FIPToolKit.Properties.Resources).Assembly);
                if(isIcon)
                {
                    return ((Icon)resourceManager.GetObject(filename.Substring(10), System.Globalization.CultureInfo.CurrentCulture)).ToBitmap();
                }
                return (Bitmap)resourceManager.GetObject(filename.Substring(10), System.Globalization.CultureInfo.CurrentCulture);
            }
            else if (System.IO.File.Exists(filename))
            {
                return Image.FromFile(filename);
            }
            return null;
        }

        static public Bitmap ResizeImage(this Image image, int width, int height, bool fast = false)
        {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destImage = null;
            if (fast)
            {
                destImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                using (Graphics graphics = Graphics.FromImage(destImage))
                {
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                }
            }
            else
            {
                destImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                using (Graphics graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }
            }
            return destImage;
        }

        static public int BitsPerPixel(PixelFormat pixelFormat)
        {
            int bitsPerPixel = 0;
            switch (pixelFormat)
            {
                case PixelFormat.Format8bppIndexed:
                    bitsPerPixel = 8;
                    break;
                case PixelFormat.Format24bppRgb:
                    bitsPerPixel = 24;
                    break;
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                    bitsPerPixel = 32;
                    break;
                default:
                    bitsPerPixel = 0;
                    break;
            }
            return bitsPerPixel;
        }

        public static Bitmap ConvertTo24bpp(this Image img)
        {
            if (img.PixelFormat != PixelFormat.Format24bppRgb)
            {
                var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                }
                return bmp;
            }
            return new Bitmap(img);
        }

        static public byte[] ImageToByte(this Bitmap img)
        {
            var bitmapData = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, img.PixelFormat);
            int lineSize = bitmapData.Width * (BitsPerPixel(img.PixelFormat) / 8);
            int byteCount = lineSize * bitmapData.Height;
            byte[] bmpBytes = new byte[byteCount];
            byte[] bmpLine = new byte[lineSize];
            try
            {
                unsafe
                {
                    IntPtr scan = bitmapData.Scan0;
                    for (int i = 0; i < bitmapData.Height; i++)
                    {
                        System.Runtime.InteropServices.Marshal.Copy(scan, bmpLine, 0, lineSize);
                        Buffer.BlockCopy(bmpLine, 0, bmpBytes, ((bitmapData.Height - 1) - i) * lineSize, lineSize);
                        scan += bitmapData.Stride;
                    }
                    //System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, bmpBytes, 0, byteCount);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                img.UnlockBits(bitmapData);
            }
            return bmpBytes;
        }

        public static byte[] ImageToByteArray(this System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(this byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        public static SizeF MeasureString(string s, Font font)
        {
            SizeF result;
            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    result = g.MeasureString(s, font);
                }
            }
            return result;
        }

        static public void AddButtonText(this Bitmap bmp, string text, Color color, Font font, SoftButtons softButton)
        {
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.AddButtonText(text, color, font, softButton);
            }
        }

        static public Bitmap ChangeToColor(this Image image, Color c)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height, image.PixelFormat);
            bmp.MakeTransparent();
            using (Graphics g = Graphics.FromImage(bmp))
            {
                float tr = c.R / 255f;
                float tg = c.G / 255f;
                float tb = c.B / 255f;
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {tr, tg, tb, 0, 1}
                });
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }

        static public void AddButtonIcon(this Bitmap bmp, Image icon, Color color, bool reColor, SoftButtons softButton, int level = 0)
        {
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.AddButtonIcon(icon, color, reColor, softButton, level);
            }
        }

        static public void AddButtonIconSmall(this Bitmap bmp, Image icon, Color color, bool reColor, SoftButtons softButton, int level = 0)
        {
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.AddButtonIconSmall(icon, color, reColor, softButton, level);
            }
        }

        static public void AddButtonText(Bitmap bmp, string text, Color color, SoftButtons softButton)
        {
            using (Font font = new Font(SystemFonts.DefaultFont.FontFamily, 12.0f, FontStyle.Bold))
            {
                AddButtonText(bmp, text, color, font, softButton);
            }
        }

        static public Bitmap BitmapImageToBitmap(this System.Windows.Media.Imaging.BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                System.Windows.Media.Imaging.BitmapEncoder enc = new System.Windows.Media.Imaging.BmpBitmapEncoder();
                enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }

        public static System.Windows.Media.Imaging.BitmapImage BitmapToBitmapImage(this Bitmap bitmap)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                bitmap.Save(outStream, ImageFormat.Bmp);
                outStream.Position = 0;
                System.Windows.Media.Imaging.BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = outStream;
                bitmapImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }

        public static Bitmap GetErrorImage(string text)
        {
            Bitmap bmp = new System.Drawing.Bitmap(320, 240, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.White))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    using (Font font = new Font(System.Drawing.SystemFonts.DefaultFont.FontFamily, 12.0f, System.Drawing.FontStyle.Bold))
                    {
                        using (StringFormat drawFormat = new StringFormat())
                        {
                            drawFormat.Alignment = StringAlignment.Center;
                            drawFormat.LineAlignment = StringAlignment.Center;
                            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            graphics.DrawString(text, font, brush, new RectangleF(0, 0, 320, 240), drawFormat);
                        }
                    }
                }
            }
            return bmp;
        }

        public static Bitmap CaptureWindow(IntPtr hWnd)
        {
            if (hWnd != IntPtr.Zero)
            {
                Rect rect = new Rect();
                IntPtr error = NativeMethods.GetWindowRect(hWnd, ref rect);
                // sometimes it gives error.
                while (error == (IntPtr)0)
                {
                    error = NativeMethods.GetWindowRect(hWnd, ref rect);
                }
                int width = rect.right - rect.left;
                int height = rect.bottom - rect.top;
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmp);
                IntPtr hDC1 = g.GetHdc();
                IntPtr hDC2 = NativeMethods.GetWindowDC(hWnd);
                if (hDC2 != IntPtr.Zero)
                {
                    NativeMethods.BitBlt(hDC1, 0, 0, width, height, hDC2, 0, 0, TernaryRasterOperations.SRCCOPY);
                    NativeMethods.ReleaseDC(hWnd, hDC2);
                }
                g.ReleaseHdc(hDC1);
                return bmp;
            }
            return null;
        }

        public static Bitmap CaptureWindow(IntPtr hWnd, Size size)
        {
            if (hWnd != IntPtr.Zero)
            {
                Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmp);
                IntPtr hDC1 = g.GetHdc();
                IntPtr hDC2 = NativeMethods.GetWindowDC(hWnd);
                if (hDC2 != IntPtr.Zero)
                {
                    NativeMethods.BitBlt(hDC1, 0, 0, size.Width, size.Height, hDC2, 0, 0, TernaryRasterOperations.SRCCOPY);
                    NativeMethods.ReleaseDC(hWnd, hDC2);
                }
                g.ReleaseHdc(hDC1);
                return bmp;
            }
            return null;
        }

        public static Bitmap CaptureApplication(IntPtr hWnd, bool makeForeground = true)
        {
            if (makeForeground)
            {
                // You need to focus on the application
                NativeMethods.SetForegroundWindow(hWnd);
                NativeMethods.ShowWindow(hWnd, ShowWindowFlags.SW_RESTORE);
            }
            Rect rect = new Rect();
            IntPtr error = NativeMethods.GetWindowRect(hWnd, ref rect);
            // sometimes it gives error.
            while (error == (IntPtr)0)
            {
                error = NativeMethods.GetWindowRect(hWnd, ref rect);
            }
            int width = (rect.right - rect.left) - 1;
            int height = (rect.bottom - rect.top) - 1;
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Graphics.FromImage(bmp).CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            return bmp;
        }

        public static Bitmap CaptureApplication(Process proc, IntPtr childhWnd, bool makeForeground = true)
        {
            if (makeForeground)
            {
                // You need to focus on the application
                NativeMethods.SetForegroundWindow(proc.MainWindowHandle);
                NativeMethods.ShowWindow(proc.MainWindowHandle, ShowWindowFlags.SW_RESTORE);
            }
            return CaptureApplication(childhWnd, makeForeground);
        }

        public static Bitmap CaptureApplication(Process proc, bool makeForeground = true)
        {
            if (makeForeground)
            {
                // You need to focus on the application
                NativeMethods.SetForegroundWindow(proc.MainWindowHandle);
                NativeMethods.ShowWindow(proc.MainWindowHandle, ShowWindowFlags.SW_RESTORE);
            }
            return CaptureApplication(proc.MainWindowHandle);
        }

        public static Bitmap CaptureApplication(string procName, bool makeForeground = true)
        {
            Process[] procs = Process.GetProcessesByName(procName);
            if(procs == null || procs.Length == 0)
            {
                return null;
            }
            return CaptureApplication(procs[0], makeForeground);
        }

        public static Bitmap CaptureScreen(int displayIndex)
        {
            if (displayIndex >= 0 && displayIndex < System.Windows.Forms.Screen.AllScreens.Length)
            {
                Rectangle rect = System.Windows.Forms.Screen.AllScreens[displayIndex].Bounds;
                Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
                using(Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
                }
                return bmp;
            }
            return null;
        }

        public static Image Crop(this Image img)
        {
            return img.Crop(4, 3);
        }

        public static Image Crop(this Image img, double aspectRatio_X, double aspectRatio_Y)
        {
            double imgWidth = Convert.ToDouble(img.Width);
            double imgHeight = Convert.ToDouble(img.Height);

            if (imgWidth / imgHeight > (aspectRatio_X / aspectRatio_Y))
            {
                double extraWidth = imgWidth - (imgHeight * (aspectRatio_X / aspectRatio_Y));
                double cropStartFrom = extraWidth / 2;
                Bitmap bmp = new Bitmap((int)(img.Width - extraWidth), img.Height);
                Graphics grp = Graphics.FromImage(bmp);
                grp.DrawImage(img, new Rectangle(0, 0, (int)(img.Width - extraWidth), img.Height), new Rectangle((int)cropStartFrom, 0, (int)(imgWidth - extraWidth), img.Height), GraphicsUnit.Pixel);
                return (Image)bmp;
            }
            return null;
        }

        public static Bitmap RotateImage(this Bitmap bmp, float angle)
        {
            if (angle != 0)
            {
                int l = bmp.Width;
                int h = bmp.Height;
                double an = angle * Math.PI / 180;
                double cos = Math.Abs(Math.Cos(an));
                double sin = Math.Abs(Math.Sin(an));
                int nl = (int)(l * cos + h * sin);
                int nh = (int)(l * sin + h * cos);
                Bitmap returnBitmap = new Bitmap(nl, nh, bmp.PixelFormat);
                returnBitmap.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
                using (Graphics g = Graphics.FromImage(returnBitmap))
                {
                    g.TranslateTransform((float)(nl - l) / 2, (float)(nh - h) / 2);
                    g.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
                    g.RotateTransform(angle);
                    g.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
                    g.DrawImage(bmp, new Point(0, 0));
                }
                return returnBitmap;
            }
            return new Bitmap(bmp);
        }

        public static Bitmap RotateImageByRadians(this Bitmap bmp, double an)
        {
            if (an != 0)
            {
                int l = bmp.Width;
                int h = bmp.Height;
                float angle = (float)(an * 180 / Math.PI);
                double cos = Math.Abs(Math.Cos(an));
                double sin = Math.Abs(Math.Sin(an));
                int nl = (int)(l * cos + h * sin);
                int nh = (int)(l * sin + h * cos);
                Bitmap returnBitmap = new Bitmap(nl, nh, bmp.PixelFormat);
                returnBitmap.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
                using (Graphics g = Graphics.FromImage(returnBitmap))
                {
                    g.TranslateTransform((float)(nl - l) / 2, (float)(nh - h) / 2);
                    g.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
                    g.RotateTransform(angle);
                    g.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
                    g.DrawImage(bmp, new Point(0, 0));
                }
                return returnBitmap;
            }
            return new Bitmap(bmp);
        }

        public static Image SetOpacity(this Image image, float opacity)
        {
            var colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = opacity;
            var imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            var output = new Bitmap(image.Width, image.Height, image.PixelFormat);
            output.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var gfx = Graphics.FromImage(output))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            return output;
        }
    }
}
