using FIPToolKit.Drawing;
using FIPToolKit.FlightSim;
using System;
using System.Drawing;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPMapProperties : FIPPageProperties
    {
        public event EventHandler OnLoadMapSettings;
        public event EventHandler OnUpdateMap;
        public event EventHandler OnShowTrackChanged;
        public event EventHandler OnAIPClientTokenChanged;

        public FIPMapProperties() : base()
        {
            Font = new Font("Microsoft Sans Serif", 10.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            FontColor = Color.Black;
            _followMyPlane = true;
            _mapType = MapType.Normal;
            _maxAIAircraft = 100;
            _maxMPAircraft = 100;
            _searchRadius = SimConnect.Instance.SearchRadius = 200000;
            OverlayColor = Color.Black;
            _showTrack = true;
            _showTraffic = true;
            _trackColor = Color.Magenta;
            _zoomLevel = 4;
            IsDirty = false;
        }

        private TemperatureUnit _temperatureUnit;
        public TemperatureUnit TemperatureUnit
        {
            get
            {
                return _temperatureUnit;
            }
            set
            {
                if (_temperatureUnit != value)
                {
                    _temperatureUnit = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private CompassMode _compassMode;
        public CompassMode CompassMode
        {
            get
            {
                return _compassMode;
            }
            set
            {
                _compassMode = value;
                IsDirty = true;
                OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
            }
        }

        private FlightSim.MapType _mapType;
        public FlightSim.MapType MapType
        {
            get
            {
                return _mapType;
            }
            set
            {
                _mapType = value;
                IsDirty = true;
                OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool _followMyPlane;
        public bool FollowMyPlane
        {
            get
            {
                return _followMyPlane;
            }
            set
            {
                if (_followMyPlane != value)
                {
                    _followMyPlane = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _showAdf;
        public bool ShowAdf
        {
            get
            {
                return _showAdf;
            }
            set
            {
                if (_showAdf != value)
                {
                    _showAdf = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _showNav1;
        public bool ShowNav1
        {
            get
            {
                return _showNav1;
            }
            set
            {
                if (_showNav1 != value)
                {
                    _showNav1 = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _showNav2;
        public bool ShowNav2
        {
            get
            {
                return _showNav2;
            }
            set
            {
                if (_showNav2 != value)
                {
                    _showNav2 = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _showGps;
        public bool ShowGPS
        {
            get
            {
                return _showGps;
            }
            set
            {
                if (_showGps != value)
                {
                    _showGps = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _showTraffic;
        public bool ShowTraffic
        {
            get
            {
                return _showTraffic;
            }
            set
            {
                if (_showTraffic != value)
                {
                    _showTraffic = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private int _vatSimId;
        public int VatSimId
        {
            get
            {
                return _vatSimId;
            }
            set
            {
                if (_vatSimId != value)
                {
                    _vatSimId = value;
                    IsDirty = true;
                }
            }
        }

        private int _maxMPAircraft;
        public int MaxMPAircraft
        {
            get
            {
                return _maxMPAircraft;
            }
            set
            {
                if (_maxMPAircraft != value)
                {
                    _maxMPAircraft = value;
                    IsDirty = true;
                    OnUpdateMap?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private int _maxAIAircraft;
        public int MaxAIAircraft
        {
            get
            {
                return _maxAIAircraft;
            }
            set
            {
                if (_maxAIAircraft != value)
                {
                    _maxAIAircraft = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private uint _searchRadius;
        public uint SearchRadius
        {
            get
            {
                return _searchRadius;
            }
            set
            {
                if (_searchRadius != value)
                {
                    _searchRadius = value;
                    SimConnect.Instance.SearchRadius = _searchRadius;
                    IsDirty = true;
                }
            }
        }

        private bool _showHeading;
        public bool ShowHeading
        {
            get
            {
                return _showHeading;
            }
            set
            {
                if (_showHeading != value)
                {
                    _showHeading = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _showTrack;
        public bool ShowTrack
        {
            get
            {
                return _showTrack;
            }
            set
            {
                if (_showTrack != value)
                {
                    _showTrack = value;
                    IsDirty = true;
                    OnShowTrackChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private ColorEx _trackColor;
        public ColorEx TrackColor
        {
            get
            {
                return _trackColor;
            }
            set
            {
                if (_trackColor.Color != value.Color)
                {
                    _trackColor = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public ColorEx OverlayColor
        {
            get
            {
                return FontColor;
            }
            set
            {
                if (FontColor.Color != value.Color)
                {
                    FontColor = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override FontEx Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (!base.Font.Name.Equals(value.Font.Name, StringComparison.OrdinalIgnoreCase))
                {
                    base.Font = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private int _zoomLevel;
        public int ZoomLevel
        {
            get
            {
                return _zoomLevel;
            }
            set
            {
                if (_zoomLevel != value)
                {
                    _zoomLevel = value;
                    IsDirty = true;
                    OnLoadMapSettings?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private string _aipClientToken;
        public string AIPClientToken
        {
            get
            {
                return (_aipClientToken ?? string.Empty);
            }
            set
            {
                if (!(_aipClientToken ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _aipClientToken = value;
                    IsDirty = true;
                    OnAIPClientTokenChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
