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
    public partial class SpotifyControllerForm : Form
    {
        public FIPSpotifyPlayerProperties SpotifyController { get; set; }

        private Font _fontHolderTitle;
        private Font _fontHolderArtist;

        public SpotifyControllerForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SpotifyController.Font = _fontHolderTitle;
            SpotifyController.ArtistFont = _fontHolderArtist;
            SpotifyController.FontColor = btnFontColor.BackColor;
            SpotifyController.ClientId = tbClientId.Text;
            SpotifyController.SecretId = tbSecretId.Text;
            SpotifyController.IsDirty = true;
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
            _fontHolderTitle = SpotifyController.Font;
            _fontHolderArtist = SpotifyController.ArtistFont;
            tbTitleFont.Font = new Font(SpotifyController.Font.FontFamily, tbTitleFont.Font.Size, SpotifyController.Font.Style, SpotifyController.Font.Unit, SpotifyController.Font.GdiCharSet);
            tbTitleFont.Text = SpotifyController.Font.FontFamily.Name;
            tbArtistFont.Font = new Font(SpotifyController.ArtistFont.FontFamily, tbArtistFont.Font.Size, SpotifyController.ArtistFont.Style, SpotifyController.ArtistFont.Unit, SpotifyController.ArtistFont.GdiCharSet);
            tbArtistFont.Text = SpotifyController.ArtistFont.FontFamily.Name;
            tbClientId.Text = SpotifyController.ClientId;
            tbSecretId.Text = SpotifyController.SecretId;
            btnFontColor.BackColor = SpotifyController.FontColor;
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

        private void lnkSpotifyDeveloper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SpotifyAPI.Web.Auth.AuthUtil.OpenBrowser("https://developer.spotify.com/dashboard/");
        }
    }
}
