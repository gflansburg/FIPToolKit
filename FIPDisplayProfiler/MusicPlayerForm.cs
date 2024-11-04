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
    public partial class MusicPlayerForm : Form
    {
        public FIPMusicPlayerProperties MusicPlayer { get; set; }

        private Font _fontHolderTitle;
        private Font _fontHolderArtist;

        public MusicPlayerForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MusicPlayer.Font = _fontHolderTitle;
            MusicPlayer.ArtistFont = _fontHolderArtist;
            MusicPlayer.FontColor = btnFontColor.BackColor;
            MusicPlayer.Path = tbPath.Text;
            MusicPlayer.AutoPlay = cbAutoPlay.Checked;
            MusicPlayer.Resume = cbResume.Checked;
            MusicPlayer.IsDirty = true;
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
            _fontHolderTitle = MusicPlayer.Font;
            _fontHolderArtist = MusicPlayer.ArtistFont;
            tbTitleFont.Font = new Font(MusicPlayer.Font.FontFamily, tbTitleFont.Font.Size, MusicPlayer.Font.Style, MusicPlayer.Font.Unit, MusicPlayer.Font.GdiCharSet);
            tbTitleFont.Text = MusicPlayer.Font.FontFamily.Name;
            tbArtistFont.Font = new Font(MusicPlayer.ArtistFont.FontFamily, tbArtistFont.Font.Size, MusicPlayer.ArtistFont.Style, MusicPlayer.ArtistFont.Unit, MusicPlayer.ArtistFont.GdiCharSet);
            tbArtistFont.Text = MusicPlayer.ArtistFont.FontFamily.Name;
            tbPath.Text = MusicPlayer.Path;
            cbAutoPlay.Checked = MusicPlayer.AutoPlay;
            cbResume.Checked = MusicPlayer.Resume;
            btnFontColor.BackColor = MusicPlayer.FontColor;
            btnOK.Enabled = !string.IsNullOrEmpty(tbPath.Text);
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPath.Text))
            {
                folderBrowserDialog1.SelectedPath = tbPath.Text;
            }
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                tbPath.Text = folderBrowserDialog1.SelectedPath;
                btnOK.Enabled = !string.IsNullOrEmpty(tbPath.Text);
            }
        }
    }
}
