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
using Unosquare.Swan;

namespace FIPToolKit.Models
{
    public enum MusicPlaylistType
    {
        Library,
        Artist,
        Album,
        Song
    }

    public enum RadioDistance
    {
        Any,
        NM50,
        NM100,
        NM250,
        NM500
    }

    [Serializable]
    public class FIPMusicPlayerProperties : FIPPageProperties
    {
        public event EventHandler OnVolumeChanged;
        public event EventHandler OnShuffleChanged;
        public event EventHandler OnRepeatChanged;
        public event EventHandler OnPathChanged;
        public event EventHandler OnRadioDistanceChanged;

        public FIPMusicPlayerProperties() : base() 
        {
            Name = "Music Player";
            Font = new Font("Arial", 12.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            _artistFont = new Font("Arial", 10.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            FontColor = Color.White;
            IsDirty = false;
        }

        private string _path;
        public string Path
        {
            get
            {
                return (_path ?? string.Empty);
            }
            set
            {
                if (!(_path ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _path = value;
                    IsDirty = true;
                    OnPathChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private string _lastSong;
        public string LastSong
        {
            get
            {
                return (_lastSong ?? string.Empty);
            }
            set
            {
                if (!(_lastSong ?? string.Empty).Equals(value ?? string.Empty, StringComparison.OrdinalIgnoreCase))
                {
                    _lastSong = value;
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

        private MusicPlaylistType _playlistType = MusicPlaylistType.Library;
        public MusicPlaylistType PlaylistType
        {
            get
            {
                return _playlistType;
            }
            set
            {
                if (_playlistType != value)
                {
                    _playlistType = value;
                    IsDirty = true;
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

        private FontEx _artistFont;
        public FontEx ArtistFont
        {
            get
            {
                return _artistFont;
            }
            set
            {
                if (!_artistFont.FontFamily.Name.Equals(value.FontFamily.Name, StringComparison.OrdinalIgnoreCase) || _artistFont.Size != value.Size || _artistFont.Bold != value.Bold || _artistFont.Italic != value.Italic || _artistFont.Strikeout != value.Strikeout || _artistFont.Underline != value.Underline || _artistFont.Unit != value.Unit || _artistFont.GdiCharSet != value.GdiCharSet)
                {
                    _artistFont = value;
                    IsDirty = true;
                }
            }
        }

        private bool _autoPlay = false;
        public bool AutoPlay
        {
            get
            {
                return _autoPlay;
            }
            set
            {
                if (_autoPlay != value)
                {
                    _autoPlay = value;
                    IsDirty = true;
                }
            }
        }

        private bool _resume = false;
        public bool Resume
        {
            get
            {
                return _resume;
            }
            set
            {
                if (_resume != value)
                {
                    _resume = value;
                    IsDirty = true;
                }
            }
        }

        private bool _shuffle = false;
        public bool Shuffle
        {
            get
            {
                return _shuffle;
            }
            set
            {
                if (_shuffle != value)
                {
                    _shuffle = value;
                    IsDirty = true;
                    OnShuffleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private RadioDistance _radioDistance = RadioDistance.Any;
        public RadioDistance RadioDistance
        {
            get
            {
                return _radioDistance;
            }
            set
            {
                if (_radioDistance != value)
                {
                    _radioDistance = value;
                    IsDirty = true;
                    OnRadioDistanceChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private MusicRepeatState _repeat = MusicRepeatState.Off;
        public MusicRepeatState Repeat
        {
            get
            {
                return _repeat;
            }
            set
            {
                if (_repeat != value)
                {
                    _repeat = value;
                    IsDirty = true;
                    OnRepeatChanged?.Invoke(this, EventArgs.Empty);
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
    }
}
