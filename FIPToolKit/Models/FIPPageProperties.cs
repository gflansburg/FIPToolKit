﻿using FIPToolKit.Drawing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace FIPToolKit.Models
{
    [Serializable]
    [XmlInclude(typeof(FIPAnalogClockProperties))]
    [XmlInclude(typeof(FIPSettableAnalogClockProperties))]
    [XmlInclude(typeof(FIPSlideShowProperties))]
    [XmlInclude(typeof(FIPSpotifyPlayerProperties))]
    [XmlInclude(typeof(FIPMusicPlayerProperties))]
    [XmlInclude(typeof(FIPVideoPlayerProperties))]
    [XmlInclude(typeof(FIPMapProperties))]
    [XmlInclude(typeof(FIPAirspeedProperties))]
    [XmlInclude(typeof(FIPAltimeterProperties))]
    [XmlInclude(typeof(FIPScreenMirrorProperties))]
    [XmlInclude(typeof(FIPRadioProperties))]
    public class FIPPageProperties
    {
        public event EventHandler OnSettingsChanged;
        public event EventHandler OnNameChanged;

        public FIPPageProperties()
        {
            _id = Guid.NewGuid();
            _name = string.Empty;
            _font = new Font("Microsoft Sans Serif", 14.0F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
            _fontColor = Color.White;
            Buttons = new List<FIPButton>();
            IsDirty = false;
        }

        public string ControlType { get; set; }

        private Guid _id;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    IsDirty = true;
                }
            }
        }

        private FontEx _font;
        public virtual FontEx Font
        {
            get
            {
                return _font;
            }
            set
            {
                if (!_font.FontFamily.Name.Equals(value.FontFamily.Name, StringComparison.OrdinalIgnoreCase) || _font.Size != value.Size || _font.Bold != value.Bold || _font.Italic != value.Italic || _font.Strikeout != value.Strikeout || _font.Underline != value.Underline || _font.Unit != value.Unit || _font.GdiCharSet != value.GdiCharSet)
                {
                    _font = value;
                    IsDirty = true;
                }
            }
        }

        private ColorEx _fontColor;
        public virtual ColorEx FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                if (_fontColor.Color != value.Color)
                {
                    _fontColor = value;
                    IsDirty = true;
                }
            }
        }

        private uint _page;
        public uint Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (_page != value)
                {
                    _page = value;
                    IsDirty = true;
                }
            }
        }

        private bool _isDirty;
        [XmlIgnore]
        [JsonIgnore]
        public bool IsDirty
        {
            get
            {
                if (_isDirty)
                {
                    return true;
                }
                foreach (FIPButton button in Buttons)
                {
                    if (button.IsDirty)
                    {
                        return true;
                    }
                }
                return false;
            }
            set
            {
                _isDirty = value;
                if (_isDirty == true)
                {
                    OnSettingsChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        public List<FIPButton> Buttons { get; set; }

        private string _name;
        public string Name
        {
            get
            {
                return String.IsNullOrEmpty(_name) ? this.GetType().ToString() : _name;
            }
            set
            {
                if (!(_name ?? String.Empty).Equals((value ?? String.Empty), StringComparison.OrdinalIgnoreCase))
                {
                    _name = value;
                    IsDirty = true;
                    OnNameChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
