using FIPToolKit.Drawing;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPVideoPlayerProperties : FIPPageProperties
    {
        public event EventHandler OnVolumeChanged;
        public event EventHandler OnPositionChanged;
        public event EventHandler OnPortraitModeChanged;
        public event EventHandler OnFilenameChanged;

        public FIPVideoPlayerProperties() : base() 
        {
            Name = "Video Player";
            Font = new Font("Arial", 12.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            _subTitleFont = new Font("Arial", 10.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            IsDirty = false;
        }

        private FontEx _subTitleFont;
        public FontEx SubtitleFont
        {
            get
            {
                return _subTitleFont;
            }
            set
            {
                if (!_subTitleFont.FontFamily.Name.Equals(value.FontFamily.Name, StringComparison.OrdinalIgnoreCase) || _subTitleFont.Size != value.Size || _subTitleFont.Bold != value.Bold || _subTitleFont.Italic != value.Italic || _subTitleFont.Strikeout != value.Strikeout || _subTitleFont.Underline != value.Underline || _subTitleFont.Unit != value.Unit || _subTitleFont.GdiCharSet != value.GdiCharSet)
                {
                    _subTitleFont = value;
                    IsDirty = true;
                }
            }
        }

        private string _filename;
        public string Filename
        {
            get
            {
                return (_filename ?? string.Empty);
            }
            set
            {
                if (!(_filename ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _filename = value;
                    IsDirty = true;
                    OnFilenameChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _showControls = true;
        public bool ShowControls
        {
            get
            {
                return _showControls;
            }
            set
            {
                if (_showControls != value)
                {
                    _showControls = value;
                    IsDirty = true;
                }
            }
        }

        private int _volume = 100;
        public int Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (_volume != value)
                {
                    SetVolume(value);
                    OnVolumeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _maintainAspectRatio = true;
        public bool MaintainAspectRatio
        {
            get
            {
                return _maintainAspectRatio;
            }
            set
            {
                if (_maintainAspectRatio != value)
                {
                    _maintainAspectRatio = value;
                    IsDirty = true;
                }
            }
        }

        private bool _resumePlayback = true;
        public bool ResumePlayback
        {
            get
            {
                return _resumePlayback;
            }
            set
            {
                if (_resumePlayback != value)
                {
                    _resumePlayback = value;
                    IsDirty = true;
                }
            }
        }

        private float _position = 0;
        public float Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (_position != value)
                {
                    SetPosition(value);
                    OnPositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool _portraitMode = false;
        public bool PortraitMode
        {
            get
            {
                return _portraitMode;
            }
            set
            {
                if (_portraitMode != value)
                {
                    SetPortraitMode(value);
                    OnPortraitModeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void SetVolume(int volume)
        {
            if (_volume != volume)
            {
                _volume = volume;
                IsDirty = true;
            }
        }

        public void SetPosition(float position)
        {
            if (_position != position)
            {
                _position = position;
                IsDirty = true;
            }
        }

        public void SetPortraitMode(bool portraitMode)
        {
            if(_portraitMode != portraitMode)
            {
                _portraitMode = portraitMode;
                IsDirty = true;
            }
        }

        private bool _pauseOtherMedia = true;
        public bool PauseOtherMedia
        {
            get
            {
                return _pauseOtherMedia;
            }
            set
            {
                if (_pauseOtherMedia != value)
                {
                    _pauseOtherMedia = value;
                    IsDirty = true;
                }
            }
        }
        private bool _mute = false;
        public bool Mute
        {
            get
            {
                return _mute;
            }
            set
            {
                if (_mute != value)
                {
                    _mute = value;
                    IsDirty = true;
                }
            }
        }

        private string _lastTrack;
        public string LastTrack
        {
            get
            {
                return (_lastTrack ?? string.Empty);
            }
            set
            {
                if (!(_lastTrack ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _lastTrack = value;
                    IsDirty = true;
                }
            }
        }
    }
}
