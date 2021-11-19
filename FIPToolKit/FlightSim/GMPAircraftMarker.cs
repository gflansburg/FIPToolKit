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
    public class GMPAircraftMarker : GImageMarker
    {
        public Font Font { get; private set; }

        public bool ShowDetails { get; set; }
        public bool ShowHeading { get; set; }
        public int CurrentAltitude { get; set; }
        public float CurrentHeading { get; set; }

        public GMPAircraftMarker(PointLatLng p, Aircraft aircraft) : base(p)
        {
            Aircraft = aircraft;
            Font = new Font("Arial", 7f);
            IsHitTestVisible = true;
        }

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
                if (_aircraft.GetType() == typeof(FlightShareAircraft))
                {
                    switch (_aircraft.EngineType)
                    {
                        case EngineType.Helo:
                            BitmapImage = Properties.Resources.helocopter.ChangeToColor(Color.Red);
                            break;
                        case EngineType.Jet:
                            BitmapImage = (_aircraft.IsHeavy ? Properties.Resources.airplane_heavy : Properties.Resources.airplane_jet).ChangeToColor(Color.Red);
                            break;
                        case EngineType.Sailplane:
                            BitmapImage = Properties.Resources.sailplane.ChangeToColor(Color.Red);
                            break;
                        case EngineType.Rocket:
                            BitmapImage = Properties.Resources.rocket.ChangeToColor(Color.Red);
                            break;
                        case EngineType.Car:
                            BitmapImage = Properties.Resources.car.ChangeToColor(Color.Red);
                            break;
                        default:
                            BitmapImage = Properties.Resources.airplane_sm.ChangeToColor(Color.Red);
                            break;
                    }
                }
                else
                {
                    BitmapImage = Properties.Resources.airplane_heavy.ChangeToColor(Color.Green);
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
            if(rotate > 360)
            {
                rotate -= 360;
            }*/
            using (Bitmap bmp = BitmapImage.RotateImage(Aircraft.Heading))
            {
                Size = new Size(bmp.Width, bmp.Height);
                Offset = new Point(-Size.Width / 2, -Size.Height / 2);
                g.DrawImage(bmp, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
            }
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            using (Font font = new Font("Arial", 14f, FontStyle.Bold))
            {
                if (Aircraft.Altitude > CurrentAltitude)
                {
                    g.DrawString("+", font, _aircraft.GetType() == typeof(FlightShareAircraft) ? Brushes.Red : Brushes.Green, LocalPosition.X + 12, LocalPosition.Y - 12);
                }
                else if (Aircraft.Altitude < CurrentAltitude)
                {
                    g.DrawString("-", font, _aircraft.GetType() == typeof(FlightShareAircraft) ? Brushes.Red : Brushes.Green, LocalPosition.X + 12, LocalPosition.Y - 12);
                }
            }
            if (ShowDetails)
            {
                string text = string.Format("{0}\n{1} ft.\n{2}°", Aircraft.Callsign, Aircraft.Altitude, Aircraft.Heading);
                SizeF sizeF = g.MeasureString(text, Font);
                Offset = new Point(38, 0);
                Rectangle rectangle = new Rectangle((LocalPosition.X - (int)(sizeF.Width / 2)) - 1, (LocalPosition.Y - (int)(sizeF.Height / 2)) - 1, (int)sizeF.Width + 2, (int)sizeF.Height + 2);
                g.FillRoundedRectangle(Brushes.Black, rectangle, 5);
                Point point = new Point((LocalPosition.X - (int)(sizeF.Width / 2)) + 1, LocalPosition.Y - (int)(sizeF.Height / 2) + 1);
                g.DrawString(text, Font, Brushes.White, point);
            }
        }
    }
}
