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
    public partial class MapForm : Form
    {
        public FIPMapProperties MapProperties { get; set; }

        private Font _fontHolder;

        public MapForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MapProperties.Font = _fontHolder;
            MapProperties.FontColor = btnFontColor.BackColor;
            MapProperties.VatSimId = Convert.ToInt32(tbVatSimId.Text);
            MapProperties.MaxAIAircraft = Convert.ToInt32(numMaxAIAircraft.Value);
            MapProperties.MaxMPAircraft = Convert.ToInt32(numMaxMPAircraft.Value);
            MapProperties.AIPClientToken = tbAIPClientToken.Text;
            MapProperties.IsDirty = true;
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

        private void FSUIPCMapForm_Load(object sender, EventArgs e)
        {
            _fontHolder = MapProperties.Font;
            tbFont.Font = new Font(MapProperties.Font.FontFamily, tbFont.Font.Size, MapProperties.Font.Style, MapProperties.Font.Unit, MapProperties.Font.GdiCharSet);
            tbFont.Text = MapProperties.Font.FontFamily.Name;
            btnFontColor.BackColor = MapProperties.FontColor;
            tbVatSimId.Text = MapProperties.VatSimId.ToString();
            numMaxAIAircraft.Value = MapProperties.MaxAIAircraft;
            numMaxMPAircraft.Value = MapProperties.MaxMPAircraft;
            tbAIPClientToken.Text = MapProperties.AIPClientToken;
        }

        private void tbVatSimId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
