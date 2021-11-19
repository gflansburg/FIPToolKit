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
    public partial class SimConnectMapForm : Form
    {
        public FIPSimConnectMap SimMap { get; set; }

        private Font _fontHolder;

        public SimConnectMapForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SimMap.Font = _fontHolder;
            SimMap.FontColor = btnFontColor.BackColor;
            SimMap.VatSimId = Convert.ToInt32(tbVatSimId.Text);
            SimMap.MaxAIAircraft = Convert.ToInt32(numMaxAIAircraft.Value);
            SimMap.MaxMPAircraft = Convert.ToInt32(numMaxMPAircraft.Value);
            SimMap.SearchRadius = Convert.ToUInt32(numRadius.Value) * 1000;
            SimMap.IsDirty = true;
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

        private void SimMapForm_Load(object sender, EventArgs e)
        {
            _fontHolder = SimMap.Font;
            tbFont.Font = new Font(SimMap.Font.FontFamily, tbFont.Font.Size, SimMap.Font.Style, SimMap.Font.Unit, SimMap.Font.GdiCharSet);
            tbFont.Text = SimMap.Font.FontFamily.Name;
            btnFontColor.BackColor = SimMap.FontColor;
            tbVatSimId.Text = SimMap.VatSimId.ToString();
            numMaxAIAircraft.Value = SimMap.MaxAIAircraft;
            numMaxMPAircraft.Value = SimMap.MaxMPAircraft;
            numRadius.Value = SimMap.SearchRadius / 1000;
        }

        private void tbVatSimId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
