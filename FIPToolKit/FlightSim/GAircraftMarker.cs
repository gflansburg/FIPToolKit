using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GMap.NET.WindowsForms;
using GMap.NET;
using FIPToolKit.Drawing;
using System.Drawing.Drawing2D;

namespace FIPToolKit.FlightSim
{
    public class GAircraftMarker : GImageMarker
    {
        public GAircraftMarker(PointLatLng p, Aircraft aircraft) : base(p)
        {
            Aircraft = aircraft;
        }

        public int CurrentAltitude { get; set; }
        public float CurrentHeading { get; set; }
        public bool ShowHeading { get; set; }

        private Aircraft _aircraft;
        public Aircraft Aircraft
        { 
            get
            {
                return _aircraft;
            }
            set
            {
                _aircraft = value;
                switch(_aircraft.EngineType)
                {
                    case EngineType.Helo:
                        BitmapImage = Properties.Resources.helocopter.ChangeToColor(Color.DarkGray);
                        break;
                    case EngineType.Jet:
                        BitmapImage = (_aircraft.IsHeavy ? Properties.Resources.airplane_heavy : Properties.Resources.airplane_jet).ChangeToColor(Color.DarkGray);
                        break;
                    case EngineType.Sailplane:
                        BitmapImage = Properties.Resources.sailplane.ChangeToColor(Color.DarkGray);
                        break;
                    case EngineType.Rocket:
                        BitmapImage = Properties.Resources.rocket.ChangeToColor(Color.DarkGray);
                        break;
                    case EngineType.Car:
                        BitmapImage = Properties.Resources.car.ChangeToColor(Color.DarkGray);
                        break;
                    default:
                        BitmapImage = Properties.Resources.airplane_sm.ChangeToColor(Color.DarkGray);
                        break;
                }
                Position = new PointLatLng(_aircraft.Latitude, _aircraft.Longitude);
            }
        }

        public override void OnRender(Graphics g)
        {
            /*float rotate = ShowHeading ? Aircraft.Heading - CurrentHeading : Aircraft.Heading;
            if (rotate < 0)
            {
                rotate += 360;
            }
            if (rotate > 360)
            {
                rotate -= 360;
            }*/
            using (Bitmap bmp = BitmapImage.RotateImage(Aircraft.Heading))
            {
                Size = new Size(bmp.Width, bmp.Height);
                Offset = new Point(-Size.Width / 2, -Size.Height / 2);
                g.DrawImage(bmp, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
            }
            if (Aircraft.GetType() != typeof(FlightShareAircraft))
            {
                using (Font font = new Font("Arial", 14f, FontStyle.Bold))
                {
                    if (Aircraft.Altitude > CurrentAltitude)
                    {
                        g.DrawString("+", font, Brushes.DarkGray, LocalPosition.X + 12, LocalPosition.Y - 12);
                    }
                    else if (Aircraft.Altitude < CurrentAltitude)
                    {
                        g.DrawString("-", font, Brushes.DarkGray, LocalPosition.X + 12, LocalPosition.Y - 12);
                    }
                }
            }
        }
    }
}
