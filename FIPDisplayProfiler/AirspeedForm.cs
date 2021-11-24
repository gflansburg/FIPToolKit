using FIPToolKit.Models;
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
    public partial class AirspeedForm : Form
    {
        public FIPPage AirspeedGauge { get; set; }
        public bool AutoSelectAircraft 
        { 
            get
            {
                return chkAutoSelectAircraft.Checked;
            }
            set
            {
                chkAutoSelectAircraft.Checked = value;
            }
        }

        private Font _fontHolder;

        public AirspeedForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AirspeedGauge.Font = _fontHolder;
            AirspeedGauge.FontColor = btnFontColor.BackColor;
            AirspeedGauge.IsDirty = true;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = _fontHolder;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _fontHolder = fontDialog1.Font;
                tbFont.Font = new Font(fontDialog1.Font.FontFamily, tbFont.Font.Size, fontDialog1.Font.Style, fontDialog1.Font.Unit, fontDialog1.Font.GdiCharSet);
                tbFont.Text = fontDialog1.Font.Name;
            }
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnFontColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnFontColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void AirspeedForm_Load(object sender, EventArgs e)
        {
            _fontHolder = AirspeedGauge.Font;
            tbFont.Font = new Font(AirspeedGauge.Font.FontFamily, tbFont.Font.Size, AirspeedGauge.Font.Style, AirspeedGauge.Font.Unit, AirspeedGauge.Font.GdiCharSet);
            tbFont.Text = AirspeedGauge.Font.FontFamily.Name;
            btnFontColor.BackColor = AirspeedGauge.FontColor;
        }
    }
}
