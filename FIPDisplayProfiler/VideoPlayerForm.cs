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
    public partial class VideoPlayerForm : Form
    {
        public FIPVideoPlayer VideoPlayer { get; set; }

        private Font _fontHolder;

        public VideoPlayerForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            VideoPlayer.Name = System.IO.Path.GetFileNameWithoutExtension(tbFilename.Text);
            VideoPlayer.Filename = tbFilename.Text;
            VideoPlayer.Font = _fontHolder;
            VideoPlayer.FontColor = btnFontColor.BackColor;
            VideoPlayer.IsDirty = true;
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                tbFilename.Text = openFileDialog1.FileName;
                btnOK.Enabled = !String.IsNullOrEmpty(tbFilename.Text);
            }
        }

        private void VideoPlayerForm_Load(object sender, EventArgs e)
        {
            tbFilename.Text = VideoPlayer.Filename;
            _fontHolder = VideoPlayer.Font;
            tbFont.Font = new Font(VideoPlayer.Font.FontFamily, tbFont.Font.Size, VideoPlayer.Font.Style, VideoPlayer.Font.Unit, VideoPlayer.Font.GdiCharSet);
            tbFont.Text = VideoPlayer.Font.FontFamily.Name;
            btnFontColor.BackColor = VideoPlayer.FontColor;
            btnOK.Enabled = !String.IsNullOrEmpty(tbFilename.Text);
        }
    }
}
