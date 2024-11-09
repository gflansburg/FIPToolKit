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
    public partial class FSUIPCMapForm : Form
    {
        public FIPMapProperties FSUIPCMap { get; set; }

        private Font _fontHolder;

        public FSUIPCMapForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FSUIPCMap.Font = _fontHolder;
            FSUIPCMap.FontColor = btnFontColor.BackColor;
            FSUIPCMap.VatSimId = Convert.ToInt32(tbVatSimId.Text);
            FSUIPCMap.MaxAIAircraft = Convert.ToInt32(numMaxAIAircraft.Value);
            FSUIPCMap.MaxMPAircraft = Convert.ToInt32(numMaxMPAircraft.Value);
            FSUIPCMap.AIPClientToken = tbAIPClientToken.Text;
            FSUIPCMap.IsDirty = true;
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
            _fontHolder = FSUIPCMap.Font;
            tbFont.Font = new Font(FSUIPCMap.Font.FontFamily, tbFont.Font.Size, FSUIPCMap.Font.Style, FSUIPCMap.Font.Unit, FSUIPCMap.Font.GdiCharSet);
            tbFont.Text = FSUIPCMap.Font.FontFamily.Name;
            btnFontColor.BackColor = FSUIPCMap.FontColor;
            tbVatSimId.Text = FSUIPCMap.VatSimId.ToString();
            numMaxAIAircraft.Value = FSUIPCMap.MaxAIAircraft;
            numMaxMPAircraft.Value = FSUIPCMap.MaxMPAircraft;
            tbAIPClientToken.Text = FSUIPCMap.AIPClientToken;
        }

        private void tbVatSimId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
