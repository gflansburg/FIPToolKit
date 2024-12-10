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
    public partial class RadioForm : Form
    {
        public FIPRadioProperties Radio { get; set; }

        private Font _fontHolderTitle;
        private Font _fontHolderArtist;

        public RadioForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Radio.Font = _fontHolderTitle;
            Radio.ArtistFont = _fontHolderArtist;
            Radio.FontColor = btnFontColor.BackColor;
            Radio.AutoPlay = cbAutoPlay.Checked;
            Radio.Resume = cbResume.Checked;
            Radio.PauseOtherMedia = cbPauseOtherMedia.Checked;
            Radio.RadioDistance = rb50.Checked ? RadioDistance.NM50 : rb100.Checked ? RadioDistance.NM100 : rb250.Checked ? RadioDistance.NM250 : RadioDistance.NM500;
            Radio.IsDirty = true;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnTitleFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = _fontHolderTitle;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _fontHolderTitle = fontDialog1.Font;
                tbTitleFont.Font = new Font(fontDialog1.Font.FontFamily, tbTitleFont.Font.Size, fontDialog1.Font.Style, fontDialog1.Font.Unit, fontDialog1.Font.GdiCharSet);
                tbTitleFont.Text = fontDialog1.Font.Name;
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

        private void SpotifyControllerForm_Load(object sender, EventArgs e)
        {
            _fontHolderTitle = Radio.Font;
            _fontHolderArtist = Radio.ArtistFont;
            tbTitleFont.Font = new Font(Radio.Font.FontFamily, tbTitleFont.Font.Size, Radio.Font.Style, Radio.Font.Unit, Radio.Font.GdiCharSet);
            tbTitleFont.Text = Radio.Font.FontFamily.Name;
            tbArtistFont.Font = new Font(Radio.ArtistFont.FontFamily, tbArtistFont.Font.Size, Radio.ArtistFont.Style, Radio.ArtistFont.Unit, Radio.ArtistFont.GdiCharSet);
            tbArtistFont.Text = Radio.ArtistFont.FontFamily.Name;
            rb50.Checked = Radio.RadioDistance == RadioDistance.NM50;
            rb100.Checked = Radio.RadioDistance == RadioDistance.NM100;
            rb250.Checked = Radio.RadioDistance == RadioDistance.NM250;
            rb500.Checked = Radio.RadioDistance == RadioDistance.NM500;
            cbAutoPlay.Checked = Radio.AutoPlay;
            cbResume.Checked = Radio.Resume;
            cbPauseOtherMedia.Checked = Radio.PauseOtherMedia;
            btnFontColor.BackColor = Radio.FontColor;
        }

        private void btnArtistFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = _fontHolderArtist;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _fontHolderArtist = fontDialog1.Font;
                tbArtistFont.Font = new Font(fontDialog1.Font.FontFamily, tbArtistFont.Font.Size, fontDialog1.Font.Style, fontDialog1.Font.Unit, fontDialog1.Font.GdiCharSet);
                tbArtistFont.Text = fontDialog1.Font.Name;
            }
        }
    }
}
