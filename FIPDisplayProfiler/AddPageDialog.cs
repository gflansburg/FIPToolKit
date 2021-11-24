using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public enum PageType
    {
        AnalogClock,
        Slideshow,
        VideoPlayer,
        SpotifyPlayer,
        ScreenMirror,
        FlightShare,
        SimConnectMap,
        SimConnectAirspeed,
        SimConnectAltimeter,
        FSUIPCMap,
        FSUIPCAirspeed,
        FSUIPCAltimeter
    }

    public partial class AddPageDialog : Form
    {
        public PageType PageType { get; set; }

        public bool Settable
        {
            get
            {
                return cbSettable.Checked;
            }
            set
            {
                cbSettable.Checked = value;
            }
        }

        public AddPageDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void rbAnalogClock_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.AnalogClock;
            btnOK.Enabled = true;
            cbSettable.Enabled = true;
        }

        private void rbSlideShow_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.Slideshow;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbVideoPlayer_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.VideoPlayer;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbSpotifyPlayer_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.SpotifyPlayer;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbFlightShare_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.FlightShare;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbSimConnectMap_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.SimConnectMap;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbFSUIPCAirspeed_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.FSUIPCAirspeed;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbFSUIPCMap_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.FSUIPCMap;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbSimConnectAirspeed_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.SimConnectAirspeed;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbSimConnectAltimeter_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.SimConnectAltimeter;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbFSUIPCAltimeter_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.FSUIPCAltimeter;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbScreenMirror_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.ScreenMirror;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }
    }
}
