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

        public string VideoName
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(tbFilename.Text);
            }
        }

        public string Filename
        {
            get
            {
                return tbFilename.Text;
            }
        }

        public Font PlayerFont
        {
            get
            {
                return _fontHolder;
            }
        }

        public Color FontColor
        {
            get
            {
                return btnFontColor.BackColor;
            }
        }

        public bool MaintainAspectRatio
        {
            get
            {
                return chkMaintainAspectRadio.Checked;
            }
        }

        public bool PortraitMode
        {
            get
            {
                return chkPortraitMode.Checked;
            }
        }

        public bool ShowControls
        {
            get
            {
                return chkShowControls.Checked;
            }
        }

        public bool ResumePlayback
        {
            get
            {
                return chkResumePlayback.Checked;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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
                btnOK.Enabled = !string.IsNullOrEmpty(tbFilename.Text);
            }
        }

        private void VideoPlayerForm_Load(object sender, EventArgs e)
        {
            tbFilename.Text = VideoPlayer.Filename;
            _fontHolder = VideoPlayer.Font;
            tbFont.Font = new Font(VideoPlayer.Font.FontFamily, tbFont.Font.Size, VideoPlayer.Font.Style, VideoPlayer.Font.Unit, VideoPlayer.Font.GdiCharSet);
            tbFont.Text = VideoPlayer.Font.FontFamily.Name;
            btnFontColor.BackColor = VideoPlayer.FontColor;
            chkMaintainAspectRadio.Checked = VideoPlayer.MaintainAspectRatio;
            chkPortraitMode.Checked = VideoPlayer.PortraitMode;
            chkShowControls.Checked = VideoPlayer.ShowControls;
            chkResumePlayback.Checked = VideoPlayer.ResumePlayback;
            btnOK.Enabled = !string.IsNullOrEmpty(tbFilename.Text);
        }
    }
}
