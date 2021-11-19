using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GMap.NET.WindowsForms;
using GMap.NET;
using FIPToolKit.Drawing;

namespace FIPToolKit.FlightSim
{
    public class GImageMarker : GMapMarker
    {
        private Bitmap _bitmap;
        public Bitmap BitmapImage
        { 
            get
            {
                return _bitmap;
            }
            set
            {
                _bitmap = value;
                Size = new System.Drawing.Size(_bitmap.Width, _bitmap.Height);
                Offset = new Point(-Size.Width / 2, -Size.Height / 2);
            }
        }

        public GImageMarker(PointLatLng p) : base(p)
        {
        }

        public GImageMarker(PointLatLng p, Bitmap bitmap) : base(p)
        {
            BitmapImage = bitmap;
        }

        public override void OnRender(Graphics g)
        {
            Size = new System.Drawing.Size(_bitmap.Width, _bitmap.Height);
            Offset = new Point(-Size.Width / 2, -Size.Height / 2);
            g.DrawImage(BitmapImage, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
        }
    }
}
