using System;
using System.Collections.Generic;

namespace FIPToolKit.Models
{
    [Serializable]
    public class FIPSlideShowProperties : FIPPageProperties
    {
        public event EventHandler OnImagesChangedStart;
        public event EventHandler OnImagesChangedEnd;

        public FIPSlideShowProperties() : base()
        {
            _delay = 5000;
            _images = new List<string>();
            Name = "Slide Show";
            IsDirty = false;
        }

        private List<string> _images;
        public List<string> Images
        {
            get
            {
                return _images;
            }
            set
            {
                OnImagesChangedStart?.Invoke(this, EventArgs.Empty);
                _images = value;
                OnImagesChangedEnd?.Invoke(this, EventArgs.Empty);
                IsDirty = true;
            }
        }

        private int _delay;
        public int Delay
        {
            get
            {
                return _delay;
            }
            set
            {
                if (_delay != value)
                {
                    _delay = value;
                    IsDirty = true;
                }
            }
        }
    }
}
