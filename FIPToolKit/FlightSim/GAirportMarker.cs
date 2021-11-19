using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GMap.NET.WindowsForms;
using GMap.NET;
using FIPToolKit.Drawing;
using GMap.NET.WindowsForms.Markers;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FIPToolKit.FlightSim
{
    public class GAirportMarker : GMapMarker
    {
        public string ICAO { get; set; }
        
        public Font Font { get; private set; }

        public bool ShowHeading { get; set; }
        
        public float CurrentHeading { get; set; }

        public GAirportMarker(PointLatLng p, string icao) : base(p)
        {
            ICAO = icao;
            Font = new Font("Arial", 8f, FontStyle.Bold, GraphicsUnit.Point);
            IsHitTestVisible = true;
        }

        public override void OnRender(Graphics g)
        {
            float rotate = ShowHeading ? CurrentHeading : 0f;
            SizeF sizeF = g.MeasureString(ICAO, Font);
            Rectangle rectangle = new Rectangle(0, 0, (int)sizeF.Width, (int)sizeF.Height);
            using (Bitmap label = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(label))
                {
                    graphics.FillRoundedRectangle(Brushes.Black, rectangle, 5);
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    graphics.DrawString(ICAO, Font, Brushes.White, 0, 0);
                }
                using (Bitmap rotated = label.RotateImage(rotate))
                {
                    Point point = new Point(LocalPosition.X - (int)(label.Width / 2), LocalPosition.Y - (int)(label.Height / 2));
                    g.DrawImage(rotated, point);
                }
            }
        }
    }
}
