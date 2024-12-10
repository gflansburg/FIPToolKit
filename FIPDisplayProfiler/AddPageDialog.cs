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
        MusicPlayer,
        SpotifyPlayer,
        ScreenMirror,
        FlightShare,
        SimConnectMap,
        SimConnectAirspeed,
        SimConnectAltimeter,
        SimConnectRadio,
        FSUIPCMap,
        FSUIPCAirspeed,
        FSUIPCAltimeter,
        FSUIPCRadio,
        XPlaneMap,
        XPlaneAirspeed,
        XPlaneAltimeter,
        XPlaneRadio,
        DCSWorldMap,
        DCSWorldAirspeed,
        DCSWorldAltimeter,
        DCSWorldRadio,
        FalconBMSMap,
        FalconBMSAirspeed,
        FalconBMSAltimeter,
        FalconBMSRadio
    }

    public partial class AddPageDialog : Form
    {
        public PageType PageType { get; set; }

        private void UncheckRadioButtons(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c is RadioButton)
                {
                    ((RadioButton)c).Checked = false;
                }
                else if (c.HasChildren)
                {
                    UncheckRadioButtons(c);
                }
            }
        }

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

        private void rbMusicPlayer_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.MusicPlayer;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbSimConnectRadio_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.SimConnectRadio;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbFSUIPCRadio_CheckedChanged(object sender, EventArgs e)
        {
            PageType= PageType.FSUIPCRadio;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void AddPageDialog_Load(object sender, EventArgs e)
        {
            UncheckRadioButtons(this);
        }

        private void rbXPlaneMap_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.XPlaneMap;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbXPlaneAirspeed_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.XPlaneAirspeed;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbXPlaneAltimeter_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.XPlaneAltimeter;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbXPlaneRadio_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.XPlaneRadio;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbMap_CheckedChanged(object sender, EventArgs e)
        {
            panelMap.Enabled = true;
            panelAirspeed.Enabled = false;
            panelAltimeter.Enabled = false;
            panelRadio.Enabled = false;
            btnOK.Enabled = rbSimConnectMap.Checked || rbFSUIPCMap.Checked || rbXPlaneMap.Checked;
        }

        private void rbAirspeed_CheckedChanged(object sender, EventArgs e)
        {
            panelMap.Enabled = false;
            panelAirspeed.Enabled = true;
            panelAltimeter.Enabled = false;
            panelRadio.Enabled = false;
            btnOK.Enabled = rbSimConnectAirspeed.Checked || rbFSUIPCAirspeed.Checked || rbXPlaneAirspeed.Checked;
        }

        private void rbAltimeter_CheckedChanged(object sender, EventArgs e)
        {
            panelMap.Enabled = false;
            panelAirspeed.Enabled = false;
            panelAltimeter.Enabled = true;
            panelRadio.Enabled = false;
            btnOK.Enabled = rbSimConnectAltimeter.Checked || rbFSUIPCAltimeter.Checked || rbXPlaneAltimeter.Checked;
        }

        private void rbRadio_CheckedChanged(object sender, EventArgs e)
        {
            panelMap.Enabled = false;
            panelAirspeed.Enabled = false;
            panelAltimeter.Enabled = false;
            panelRadio.Enabled = true;
            btnOK.Enabled = rbSimConnectRadio.Checked || rbFSUIPCRadio.Checked || rbXPlaneRadio.Checked;
        }

        private void rbDCSWorldMap_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.DCSWorldMap;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbDCSWorldAirspeed_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.DCSWorldAirspeed;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbDCSWorldAltimeter_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.DCSWorldAltimeter;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbDCSWorldRadio_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.DCSWorldRadio;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbFalconBMSMap_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.FalconBMSMap;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbFalconBMSAirspeed_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.FalconBMSAirspeed;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void rbFalconBMSAltimeter_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.FalconBMSAltimeter;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }

        private void tbFalconBMSRadio_CheckedChanged(object sender, EventArgs e)
        {
            PageType = PageType.FalconBMSRadio;
            btnOK.Enabled = true;
            cbSettable.Enabled = false;
        }
    }
}
