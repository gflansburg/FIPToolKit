using FIPToolKit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public partial class AltimeterForm : Form
    {
        public FIPAltimeterProperties Altimeter { get; set; }

        private Font _fontHolder;

        private string _gaugeImageFileName;

        private class AltimeterLinearGradientMode
        {
            public LinearGradientMode LinearGradientMode { get; set; }
            public string Name { get; set; }
        }

        public AltimeterForm()
        {
            InitializeComponent();
            foreach (LinearGradientMode linearGradientMode in (LinearGradientMode[])Enum.GetValues(typeof(LinearGradientMode)))
            {
                cbFaceGradientMode.Items.Add(new AltimeterLinearGradientMode()
                {
                    LinearGradientMode = linearGradientMode,
                    Name = Regex.Replace(linearGradientMode.ToString(), "(\\B[A-Z])", " $1")
                });
            }
        }

        private int IndexOfLinearGradientMode(LinearGradientMode linearGradientMode)
        {
            for (int i = 0; i < cbFaceGradientMode.Items.Count; i++)
            {
                AltimeterLinearGradientMode altimeterLinearGradientMode = cbFaceGradientMode.Items[i] as AltimeterLinearGradientMode;
                if (altimeterLinearGradientMode.LinearGradientMode == linearGradientMode)
                {
                    return i;
                }
            }
            return -1;
        }

        private void FSUIPCAltimeterForm_Load(object sender, EventArgs e)
        {
            _fontHolder = Altimeter.Font;
            tbFont.Font = new Font(Altimeter.Font.FontFamily, tbFont.Font.Size, Altimeter.Font.Style, Altimeter.Font.Unit, Altimeter.Font.GdiCharSet);
            tbFont.Text = Altimeter.Font.FontFamily.Name;
            btnFontColor.BackColor = Altimeter.FontColor;
            cbDrawTenThousandsHand.Checked = Altimeter.DrawTenThousandsHand;
            cbDrawThousandsHand.Checked = Altimeter.DrawThousandsHand;
            cbDrawHundredsHand.Checked = Altimeter.DrawHundredsHand;
            cbDrawNumerals.Checked = Altimeter.DrawNumerals;
            cbDrawRim.Checked = Altimeter.DrawRim;
            cbDrawTickMarks.Checked = Altimeter.DrawFaceTicks;
            cbShowAltitudeStripes.Checked = Altimeter.ShowAltitiudeStripes;
            cbShowKollsman.Checked = Altimeter.ShowKollsmanWindow;
            btnFaceColorHigh.BackColor = Altimeter.FaceColorHigh;
            btnFaceColorLow.BackColor = Altimeter.FaceColorLow;
            btnNeedleColor.BackColor = Altimeter.NeedleColor;
            btnRimColorInside.BackColor = Altimeter.InnerRimColor;
            btnRimColorOutside.BackColor = Altimeter.OuterRimColor;
            numFaceTickMarkLength.Value = Altimeter.FaceTickSize.Height;
            numFaceTickMarkWidth.Value = Altimeter.FaceTickSize.Width;
            numHundredsHandLengthOffset.Value = Altimeter.HundredsHandLengthOffset;
            numRimWidth.Value = Altimeter.RimWidth;
            numTenThousandsHandLengthOffset.Value = Altimeter.TenThousandsHandLengthOffset;
            numThousandsHandLengthOffset.Value = Altimeter.ThousandsHandLengthOffset;
            cbFaceGradientMode.SelectedIndex = IndexOfLinearGradientMode(Altimeter.FaceGradientMode);
            pbGaugeImage.Image = (Altimeter.GaugeImage != null ? new Bitmap(Altimeter.GaugeImage) : null);
            _gaugeImageFileName = Altimeter.GaugeImageFilename;
            if (Altimeter.GaugeImage == null && !string.IsNullOrEmpty(Altimeter.GaugeImageFilename))
            {
                pbGaugeImage.Image = new Bitmap(FIPToolKit.Drawing.ImageHelper.GetBitmapResource(Altimeter.GaugeImageFilename));
            }
            numFaceTickMarkLength.Enabled = cbDrawTickMarks.Checked;
            numFaceTickMarkWidth.Enabled = cbDrawTickMarks.Checked;
            btnRimColorInside.Enabled = cbDrawRim.Checked;
            btnRimColorOutside.Enabled = cbDrawRim.Checked;
            numRimWidth.Enabled = cbDrawRim.Checked;
            numTenThousandsHandLengthOffset.Enabled = cbDrawTenThousandsHand.Checked;
            btnNeedleColor.Enabled = cbDrawTenThousandsHand.Checked || cbDrawThousandsHand.Checked || cbDrawHundredsHand.Checked;
            numThousandsHandLengthOffset.Enabled = cbDrawThousandsHand.Checked;
            btnNeedleColor.Enabled = cbDrawTenThousandsHand.Checked || cbDrawThousandsHand.Checked || cbDrawHundredsHand.Checked;
            numHundredsHandLengthOffset.Enabled = cbDrawHundredsHand.Checked;
            btnNeedleColor.Enabled = cbDrawTenThousandsHand.Checked || cbDrawThousandsHand.Checked || cbDrawHundredsHand.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Altimeter.Font = _fontHolder;
            Altimeter.FontColor = btnFontColor.BackColor;

            Altimeter.DrawTenThousandsHand = cbDrawTenThousandsHand.Checked;
            Altimeter.DrawThousandsHand = cbDrawThousandsHand.Checked;
            Altimeter.DrawHundredsHand = cbDrawHundredsHand.Checked;
            Altimeter.DrawNumerals = cbDrawNumerals.Checked;
            Altimeter.DrawRim = cbDrawRim.Checked;
            Altimeter.DrawFaceTicks = cbDrawTickMarks.Checked;
            Altimeter.ShowAltitiudeStripes = cbShowAltitudeStripes.Checked;
            Altimeter.ShowKollsmanWindow = cbShowKollsman.Checked;
            Altimeter.FaceColorHigh = btnFaceColorHigh.BackColor;
            Altimeter.FaceColorLow = btnFaceColorLow.BackColor;
            Altimeter.NeedleColor = btnNeedleColor.BackColor;
            Altimeter.InnerRimColor = btnRimColorInside.BackColor;
            Altimeter.OuterRimColor = btnRimColorOutside.BackColor;
            Altimeter.FaceTickSize = new Size((int)numFaceTickMarkWidth.Value, (int)numFaceTickMarkLength.Value);
            Altimeter.HundredsHandLengthOffset = (int)numHundredsHandLengthOffset.Value;
            Altimeter.RimWidth = (int)numRimWidth.Value;
            Altimeter.TenThousandsHandLengthOffset = (int)numTenThousandsHandLengthOffset.Value;
            Altimeter.ThousandsHandLengthOffset = (int)numThousandsHandLengthOffset.Value;
            Altimeter.FaceGradientMode = (cbFaceGradientMode.SelectedItem as AltimeterLinearGradientMode).LinearGradientMode;
            Altimeter.GaugeImage = pbGaugeImage.Image != null ? new Bitmap(pbGaugeImage.Image) : null;
            Altimeter.GaugeImageFilename = _gaugeImageFileName;
            Altimeter.IsDirty = true;
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

        private void cbDrawTickMarks_CheckedChanged(object sender, EventArgs e)
        {
            numFaceTickMarkLength.Enabled = cbDrawTickMarks.Checked;
            numFaceTickMarkWidth.Enabled = cbDrawTickMarks.Checked;
        }

        private void cbDrawRim_CheckedChanged(object sender, EventArgs e)
        {
            btnRimColorInside.Enabled = cbDrawRim.Checked;
            btnRimColorOutside.Enabled = cbDrawRim.Checked;
            numRimWidth.Enabled = cbDrawRim.Checked;
        }

        private void cbDrawTenThousandsHand_CheckedChanged(object sender, EventArgs e)
        {
            numTenThousandsHandLengthOffset.Enabled = cbDrawTenThousandsHand.Checked;
            btnNeedleColor.Enabled = cbDrawTenThousandsHand.Checked || cbDrawThousandsHand.Checked || cbDrawHundredsHand.Checked;
        }

        private void cbDrawThousandsHand_CheckedChanged(object sender, EventArgs e)
        {
            numThousandsHandLengthOffset.Enabled = cbDrawThousandsHand.Checked;
            btnNeedleColor.Enabled = cbDrawTenThousandsHand.Checked || cbDrawThousandsHand.Checked || cbDrawHundredsHand.Checked;
        }

        private void cbDrawHundredsHand_CheckedChanged(object sender, EventArgs e)
        {
            numHundredsHandLengthOffset.Enabled = cbDrawHundredsHand.Checked;
            btnNeedleColor.Enabled = cbDrawTenThousandsHand.Checked || cbDrawThousandsHand.Checked || cbDrawHundredsHand.Checked;
        }

        private void btnNeedleColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnNeedleColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnNeedleColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void btnRimColorOutside_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnRimColorOutside.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnRimColorOutside.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void btnRimColorInside_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnRimColorInside.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnRimColorInside.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog1.FileName = _gaugeImageFileName;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                pbGaugeImage.Image = Image.FromFile(openFileDialog1.FileName);
                _gaugeImageFileName = openFileDialog1.FileName;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            pbGaugeImage.Image = null;
            _gaugeImageFileName = String.Empty;
        }

        private void btnFaceColorHigh_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnFaceColorHigh.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnFaceColorHigh.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void btnFaceColorLow_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnFaceColorLow.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnFaceColorLow.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void pbGaugeImage_Click(object sender, EventArgs e)
        {
            btnBrowse_Click(sender, e);
        }
    }
}
