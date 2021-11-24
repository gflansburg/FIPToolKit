using Saitek.DirectOutput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Drawing
{
    public static class GraphicsHelper
    {
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (pen == null)
                throw new ArgumentNullException("pen");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (brush == null)
                throw new ArgumentNullException("brush");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        static public void AddButtonText(this Graphics graphics, string text, Color color, Font font, SoftButtons softButton)
        {
            using (Brush brush = new SolidBrush(color))
            {
                SizeF size = graphics.MeasureString(text, font);
                int y = 0;
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        y = 2;
                        break;
                    case SoftButtons.Button2:
                        y = 46;
                        break;
                    case SoftButtons.Button3:
                        y = 90;
                        break;
                    case SoftButtons.Button4:
                        y = 132;
                        break;
                    case SoftButtons.Button5:
                        y = 176;
                        break;
                    case SoftButtons.Button6:
                        y = 218;
                        break;
                    default:
                        return;
                }
                Point p = new System.Drawing.Point(0, y);
                using (StringFormat format = new StringFormat())
                {
                    format.LineAlignment = StringAlignment.Near;
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    graphics.DrawString(text, font, brush, new RectangleF(p.X, p.Y, 320, 44), format);
                }
            }
        }

        static public void AddButtonIcon(this Graphics graphics, Image icon, Color color, bool reColor, SoftButtons softButton, int level = 0)
        {
            if (icon != null)
            {
                double ratioX = (double)32 / icon.Width;
                double ratioY = (double)32 / icon.Height;
                double ratio = Math.Min(ratioX, ratioY);
                int newWidth = (int)(icon.Width * ratio);
                int newHeight = (int)(icon.Height * ratio);
                int x = Math.Max(0, (32 - newWidth) / 2) + (34 * level);
                int y;
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        y = 0;
                        break;
                    case SoftButtons.Button2:
                        y = 42;
                        break;
                    case SoftButtons.Button3:
                        y = 86;
                        break;
                    case SoftButtons.Button4:
                        y = 128;
                        break;
                    case SoftButtons.Button5:
                        y = 170;
                        break;
                    case SoftButtons.Button6:
                        y = 208;
                        break;
                    case SoftButtons.Left:
                    case SoftButtons.Right:
                        x = 65 - (newWidth / 2);
                        y = 208;
                        break;
                    case SoftButtons.Up:
                    case SoftButtons.Down:
                        x = 250 - (newWidth / 2);
                        y = 208;
                        break;
                    default:
                        return;
                }
                y = Math.Max(0, y + ((32 - newHeight) / 2));
                Rectangle destRect = new Rectangle(x, y, newWidth, newHeight);
                Rectangle srcRect = new Rectangle(0, 0, icon.Width, icon.Height);
                if (reColor)
                {
                    graphics.DrawImage(icon.ChangeToColor(color), destRect, srcRect, GraphicsUnit.Pixel);
                }
                else
                {
                    graphics.DrawImage(icon, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }

        static public void AddButtonIconSmall(this Graphics graphics, Image icon, Color color, bool reColor, SoftButtons softButton, int level = 0)
        {
            if (icon != null)
            {
                double ratioX = (double)16 / icon.Width;
                double ratioY = (double)16 / icon.Height;
                double ratio = Math.Min(ratioX, ratioY);
                int newWidth = (int)(icon.Width * ratio);
                int newHeight = (int)(icon.Height * ratio);
                int x = Math.Max(0, (16 - newWidth) / 2) + (18 * level);
                int y;
                switch (softButton)
                {
                    case SoftButtons.Button1:
                        y = 0;
                        break;
                    case SoftButtons.Button2:
                        y = 50;
                        break;
                    case SoftButtons.Button3:
                        y = 94;
                        break;
                    case SoftButtons.Button4:
                        y = 136;
                        break;
                    case SoftButtons.Button5:
                        y = 178;
                        break;
                    case SoftButtons.Button6:
                        y = 224;
                        break;
                    case SoftButtons.Left:
                    case SoftButtons.Right:
                        x = 80 - (newWidth / 2);
                        y = 208;
                        break;
                    case SoftButtons.Up:
                    case SoftButtons.Down:
                        x = 240 - (newWidth / 2);
                        y = 208;
                        break;
                    default:
                        return;
                }
                y = Math.Max(0, y + ((16 - newHeight) / 2));
                Rectangle destRect = new Rectangle(x, y, newWidth, newHeight);
                Rectangle srcRect = new Rectangle(0, 0, icon.Width, icon.Height);
                if (reColor)
                {
                    graphics.DrawImage(icon.ChangeToColor(color), destRect, srcRect, GraphicsUnit.Pixel);
                }
                else
                {
                    graphics.DrawImage(icon, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }
        public static void DrawRotatedImage(this Graphics gr, float angle, Image img, float x, float y)
        {
            // Save the graphics state.
            GraphicsState state = gr.Save();
            gr.ResetTransform();

            // Rotate.
            gr.RotateTransform(angle);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            gr.TranslateTransform(x, y, MatrixOrder.Append);

            // Draw the text at the origin.
            gr.DrawImage(img, -(img.Width / 2), -(img.Height /2));

            // Restore the graphics state.
            gr.Restore(state);
        }

        public static void DrawRotatedTextAt(this Graphics gr, float angle, string txt, float x, float y, Font the_font, Brush the_brush)
        {
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                gr.DrawRotatedTextAt(angle, txt, x, y, the_font, the_brush, format);
            }
        }

        // Draw a rotated string at a particular position.
        public static void DrawRotatedTextAt(this Graphics gr, float angle, string txt, float x, float y, Font the_font, Brush the_brush, StringFormat format)
        {
            // Save the graphics state.
            GraphicsState state = gr.Save();
            gr.ResetTransform();

            // Rotate.
            gr.RotateTransform(angle);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            gr.TranslateTransform(x, y, MatrixOrder.Append);

            // Draw the text at the origin.
            gr.DrawString(txt, the_font, the_brush, 0, 0, format);

            // Restore the graphics state.
            gr.Restore(state);
        }
    }
}
