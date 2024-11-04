using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Models
{
    public enum ScreenFit
    {
        Fit,
        Fill,
        Stretch
    }

    [Serializable]
    public class FIPScreenMirrorProperties : FIPPageProperties
    {
        public FIPScreenMirrorProperties() : base()
        {
            Name = "Screen Mirror";
            IsDirty = false;
        }

        private ScreenFit _fit = ScreenFit.Fit;

        public ScreenFit Fit
        {
            get
            {
                return _fit;
            }
            set
            {
                if (_fit != value)
                {
                    _fit = value;
                    IsDirty = true;
                }
            }
        }

        private int _screenIndex = 0;

        public int ScreenIndex
        {
            get
            {
                return _screenIndex;
            }
            set
            {
                if (_screenIndex != value)
                {
                    _screenIndex = value;
                    IsDirty = true;
                }
            }
        }
    }
}
