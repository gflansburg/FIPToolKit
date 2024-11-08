using FIPToolKit.Models;
using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public partial class VideoPlayerForm : Form
    {
        public FIPVideoPlayerProperties VideoPlayer { get; set; }

        private Font _fontHolderTitle;
        private Font _fontHolderSubtitle;

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
                return _fontHolderTitle;
            }
        }

        public Font SubtitleFont
        {
            get
            {
                return _fontHolderSubtitle;
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

        public bool PauseOtherMedia
        {
            get
            {
                return cbPauseOtherMedia.Checked;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            VideoPlayer.Name = VideoName;
            VideoPlayer.Filename = Filename;
            VideoPlayer.Font = PlayerFont;
            VideoPlayer.SubtitleFont = _fontHolderSubtitle;
            VideoPlayer.FontColor = FontColor;
            VideoPlayer.MaintainAspectRatio = MaintainAspectRatio;
            VideoPlayer.PauseOtherMedia = PauseOtherMedia;
            VideoPlayer.IsDirty = true;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = _fontHolderTitle;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _fontHolderTitle = fontDialog1.Font;
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
            if (string.IsNullOrEmpty(btnBrowse.Text))
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                openFileDialog1.FileName = string.Empty;
            }
            else
            {
                string path = Path.GetDirectoryName(tbFilename.Text);
                openFileDialog1.InitialDirectory = Directory.Exists(path) ? path : Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                openFileDialog1.FileName = Path.GetFileName(tbFilename.Text);
            }
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                tbFilename.Text = openFileDialog1.FileName;
                btnOK.Enabled = !string.IsNullOrEmpty(tbFilename.Text);
            }
        }

        private void VideoPlayerForm_Load(object sender, EventArgs e)
        {
            tbFilename.Text = VideoPlayer.Filename;
            _fontHolderTitle = VideoPlayer.Font;
            _fontHolderSubtitle = VideoPlayer.SubtitleFont;
            tbFont.Font = new Font(VideoPlayer.Font.FontFamily, tbFont.Font.Size, VideoPlayer.Font.Style, VideoPlayer.Font.Unit, VideoPlayer.Font.GdiCharSet);
            tbFont.Text = VideoPlayer.Font.FontFamily.Name;
            tbSubtitleFont.Font = new Font(VideoPlayer.SubtitleFont.FontFamily, tbSubtitleFont.Font.Size, VideoPlayer.SubtitleFont.Style, VideoPlayer.SubtitleFont.Unit, VideoPlayer.SubtitleFont.GdiCharSet);
            tbSubtitleFont.Text = VideoPlayer.SubtitleFont.FontFamily.Name;
            btnFontColor.BackColor = VideoPlayer.FontColor;
            chkMaintainAspectRadio.Checked = VideoPlayer.MaintainAspectRatio;
            chkPortraitMode.Checked = VideoPlayer.PortraitMode;
            chkShowControls.Checked = VideoPlayer.ShowControls;
            chkResumePlayback.Checked = VideoPlayer.ResumePlayback;
            cbPauseOtherMedia.Checked = VideoPlayer.PauseOtherMedia;
            btnOK.Enabled = !string.IsNullOrEmpty(tbFilename.Text);
        }

        private void btnSubtitleFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = _fontHolderSubtitle;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _fontHolderSubtitle = fontDialog1.Font;
                tbSubtitleFont.Font = new Font(fontDialog1.Font.FontFamily, tbSubtitleFont.Font.Size, fontDialog1.Font.Style, fontDialog1.Font.Unit, fontDialog1.Font.GdiCharSet);
                tbSubtitleFont.Text = fontDialog1.Font.Name;
            }
        }
    }
}
