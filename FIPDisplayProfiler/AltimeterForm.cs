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
        public FIPPage Altimeter { get; set; }

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
            tbName.Text = Altimeter.Name;
            tbFont.Font = new Font(Altimeter.Font.FontFamily, tbFont.Font.Size, Altimeter.Font.Style, Altimeter.Font.Unit, Altimeter.Font.GdiCharSet);
            tbFont.Text = Altimeter.Font.FontFamily.Name;
            btnFontColor.BackColor = Altimeter.FontColor;
            if(Altimeter.GetType() == typeof(FIPFSUIPCAltimeter))
            {
                cbDrawTenThousandsHand.Checked = ((FIPFSUIPCAltimeter)Altimeter).DrawTenThousandsHand;
                cbDrawThousandsHand.Checked = ((FIPFSUIPCAltimeter)Altimeter).DrawThousandsHand;
                cbDrawHundredsHand.Checked = ((FIPFSUIPCAltimeter)Altimeter).DrawHundredsHand;
                cbDrawNumerals.Checked = ((FIPFSUIPCAltimeter)Altimeter).DrawNumerals;
                cbDrawRim.Checked = ((FIPFSUIPCAltimeter)Altimeter).DrawRim;
                cbDrawTickMarks.Checked = ((FIPFSUIPCAltimeter)Altimeter).DrawFaceTicks;
                cbShowAltitudeStripes.Checked = ((FIPFSUIPCAltimeter)Altimeter).ShowAltitiudeStripes;
                cbShowKollsman.Checked = ((FIPFSUIPCAltimeter)Altimeter).ShowKollsmanWindow;
                btnFaceColorHigh.BackColor = ((FIPFSUIPCAltimeter)Altimeter).FaceColorHigh;
                btnFaceColorLow.BackColor = ((FIPFSUIPCAltimeter)Altimeter).FaceColorLow;
                btnNeedleColor.BackColor = ((FIPFSUIPCAltimeter)Altimeter).NeedleColor;
                btnRimColorInside.BackColor = ((FIPFSUIPCAltimeter)Altimeter).InnerRimColor;
                btnRimColorOutside.BackColor = ((FIPFSUIPCAltimeter)Altimeter).OuterRimColor;
                numFaceTickMarkLength.Value = ((FIPFSUIPCAltimeter)Altimeter).FaceTickSize.Height;
                numFaceTickMarkWidth.Value = ((FIPFSUIPCAltimeter)Altimeter).FaceTickSize.Width;
                numHundredsHandLengthOffset.Value = ((FIPFSUIPCAltimeter)Altimeter).HundredsHandLengthOffset;
                numRimWidth.Value = ((FIPFSUIPCAltimeter)Altimeter).RimWidth;
                numTenThousandsHandLengthOffset.Value = ((FIPFSUIPCAltimeter)Altimeter).TenThousandsHandLengthOffset;
                numThousandsHandLengthOffset.Value = ((FIPFSUIPCAltimeter)Altimeter).ThousandsHandLengthOffset;
                cbFaceGradientMode.SelectedIndex = IndexOfLinearGradientMode(((FIPFSUIPCAltimeter)Altimeter).FaceGradientMode);
                pbGaugeImage.Image = (((FIPFSUIPCAltimeter)Altimeter).GaugeImage != null ? new Bitmap(((FIPFSUIPCAltimeter)Altimeter).GaugeImage) : null);
                _gaugeImageFileName = ((FIPFSUIPCAltimeter)Altimeter).GaugeImageFilename;
                if (((FIPFSUIPCAltimeter)Altimeter).GaugeImage == null && !string.IsNullOrEmpty(((FIPFSUIPCAltimeter)Altimeter).GaugeImageFilename))
                {
                    pbGaugeImage.Image = new Bitmap(FIPToolKit.Drawing.ImageHelper.GetBitmapResource(((FIPFSUIPCAltimeter)Altimeter).GaugeImageFilename));
                }
            }
            else if(Altimeter.GetType() == typeof(FIPSimConnectAltimeter))
            {
                cbDrawTenThousandsHand.Checked = ((FIPSimConnectAltimeter)Altimeter).DrawTenThousandsHand;
                cbDrawThousandsHand.Checked = ((FIPSimConnectAltimeter)Altimeter).DrawThousandsHand;
                cbDrawHundredsHand.Checked = ((FIPSimConnectAltimeter)Altimeter).DrawHundredsHand;
                cbDrawNumerals.Checked = ((FIPSimConnectAltimeter)Altimeter).DrawNumerals;
                cbDrawRim.Checked = ((FIPSimConnectAltimeter)Altimeter).DrawRim;
                cbDrawTickMarks.Checked = ((FIPSimConnectAltimeter)Altimeter).DrawFaceTicks;
                cbShowAltitudeStripes.Checked = ((FIPSimConnectAltimeter)Altimeter).ShowAltitiudeStripes;
                cbShowKollsman.Checked = ((FIPSimConnectAltimeter)Altimeter).ShowKollsmanWindow;
                btnFaceColorHigh.BackColor = ((FIPSimConnectAltimeter)Altimeter).FaceColorHigh;
                btnFaceColorLow.BackColor = ((FIPSimConnectAltimeter)Altimeter).FaceColorLow;
                btnNeedleColor.BackColor = ((FIPSimConnectAltimeter)Altimeter).NeedleColor;
                btnRimColorInside.BackColor = ((FIPSimConnectAltimeter)Altimeter).InnerRimColor;
                btnRimColorOutside.BackColor = ((FIPSimConnectAltimeter)Altimeter).OuterRimColor;
                numFaceTickMarkLength.Value = ((FIPSimConnectAltimeter)Altimeter).FaceTickSize.Height;
                numFaceTickMarkWidth.Value = ((FIPSimConnectAltimeter)Altimeter).FaceTickSize.Width;
                numHundredsHandLengthOffset.Value = ((FIPSimConnectAltimeter)Altimeter).HundredsHandLengthOffset;
                numRimWidth.Value = ((FIPSimConnectAltimeter)Altimeter).RimWidth;
                numTenThousandsHandLengthOffset.Value = ((FIPSimConnectAltimeter)Altimeter).TenThousandsHandLengthOffset;
                numThousandsHandLengthOffset.Value = ((FIPSimConnectAltimeter)Altimeter).ThousandsHandLengthOffset;
                cbFaceGradientMode.SelectedIndex = IndexOfLinearGradientMode(((FIPSimConnectAltimeter)Altimeter).FaceGradientMode);
                pbGaugeImage.Image = (((FIPSimConnectAltimeter)Altimeter).GaugeImage != null ? new Bitmap(((FIPSimConnectAltimeter)Altimeter).GaugeImage) : null);
                _gaugeImageFileName = ((FIPSimConnectAltimeter)Altimeter).GaugeImageFilename;
                if (((FIPSimConnectAltimeter)Altimeter).GaugeImage == null && !string.IsNullOrEmpty(((FIPSimConnectAltimeter)Altimeter).GaugeImageFilename))
                {
                    pbGaugeImage.Image = new Bitmap(FIPToolKit.Drawing.ImageHelper.GetBitmapResource(((FIPSimConnectAltimeter)Altimeter).GaugeImageFilename));
                }
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
            btnOK.Enabled = !string.IsNullOrEmpty(tbName.Text);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Altimeter.Font = _fontHolder;
            Altimeter.FontColor = btnFontColor.BackColor;

            if (Altimeter.GetType() == typeof(FIPFSUIPCAltimeter))
            {
                ((FIPFSUIPCAltimeter)Altimeter).DrawTenThousandsHand = cbDrawTenThousandsHand.Checked;
                ((FIPFSUIPCAltimeter)Altimeter).DrawThousandsHand = cbDrawThousandsHand.Checked;
                ((FIPFSUIPCAltimeter)Altimeter).DrawHundredsHand = cbDrawHundredsHand.Checked;
                ((FIPFSUIPCAltimeter)Altimeter).DrawNumerals = cbDrawNumerals.Checked;
                ((FIPFSUIPCAltimeter)Altimeter).DrawRim = cbDrawRim.Checked;
                ((FIPFSUIPCAltimeter)Altimeter).DrawFaceTicks = cbDrawTickMarks.Checked;
                ((FIPFSUIPCAltimeter)Altimeter).ShowAltitiudeStripes = cbShowAltitudeStripes.Checked;
                ((FIPFSUIPCAltimeter)Altimeter).ShowKollsmanWindow = cbShowKollsman.Checked;
                ((FIPFSUIPCAltimeter)Altimeter).FaceColorHigh = btnFaceColorHigh.BackColor;
                ((FIPFSUIPCAltimeter)Altimeter).FaceColorLow = btnFaceColorLow.BackColor;
                ((FIPFSUIPCAltimeter)Altimeter).NeedleColor = btnNeedleColor.BackColor;
                ((FIPFSUIPCAltimeter)Altimeter).InnerRimColor = btnRimColorInside.BackColor;
                ((FIPFSUIPCAltimeter)Altimeter).OuterRimColor = btnRimColorOutside.BackColor;
                ((FIPFSUIPCAltimeter)Altimeter).FaceTickSize = new Size((int)numFaceTickMarkWidth.Value, (int)numFaceTickMarkLength.Value);
                ((FIPFSUIPCAltimeter)Altimeter).HundredsHandLengthOffset = (int)numHundredsHandLengthOffset.Value;
                ((FIPFSUIPCAltimeter)Altimeter).RimWidth = (int)numRimWidth.Value;
                ((FIPFSUIPCAltimeter)Altimeter).TenThousandsHandLengthOffset = (int)numTenThousandsHandLengthOffset.Value;
                ((FIPFSUIPCAltimeter)Altimeter).ThousandsHandLengthOffset = (int)numThousandsHandLengthOffset.Value;
                ((FIPFSUIPCAltimeter)Altimeter).FaceGradientMode = (cbFaceGradientMode.SelectedItem as AltimeterLinearGradientMode).LinearGradientMode;
                ((FIPFSUIPCAltimeter)Altimeter).GaugeImage = pbGaugeImage.Image != null ? new Bitmap(pbGaugeImage.Image) : null;
                ((FIPFSUIPCAltimeter)Altimeter).GaugeImageFilename = _gaugeImageFileName;
            }
            else if (Altimeter.GetType() == typeof(FIPSimConnectAltimeter))
            {
                ((FIPSimConnectAltimeter)Altimeter).DrawTenThousandsHand = cbDrawTenThousandsHand.Checked;
                ((FIPSimConnectAltimeter)Altimeter).DrawThousandsHand = cbDrawThousandsHand.Checked;
                ((FIPSimConnectAltimeter)Altimeter).DrawHundredsHand = cbDrawHundredsHand.Checked;
                ((FIPSimConnectAltimeter)Altimeter).DrawNumerals = cbDrawNumerals.Checked;
                ((FIPSimConnectAltimeter)Altimeter).DrawRim = cbDrawRim.Checked;
                ((FIPSimConnectAltimeter)Altimeter).DrawFaceTicks = cbDrawTickMarks.Checked;
                ((FIPSimConnectAltimeter)Altimeter).ShowAltitiudeStripes = cbShowAltitudeStripes.Checked;
                ((FIPSimConnectAltimeter)Altimeter).ShowKollsmanWindow = cbShowKollsman.Checked;
                ((FIPSimConnectAltimeter)Altimeter).FaceColorHigh = btnFaceColorHigh.BackColor;
                ((FIPSimConnectAltimeter)Altimeter).FaceColorLow = btnFaceColorLow.BackColor;
                ((FIPSimConnectAltimeter)Altimeter).NeedleColor = btnNeedleColor.BackColor;
                ((FIPSimConnectAltimeter)Altimeter).InnerRimColor = btnRimColorInside.BackColor;
                ((FIPSimConnectAltimeter)Altimeter).OuterRimColor = btnRimColorOutside.BackColor;
                ((FIPSimConnectAltimeter)Altimeter).FaceTickSize = new Size((int)numFaceTickMarkWidth.Value, (int)numFaceTickMarkLength.Value);
                ((FIPSimConnectAltimeter)Altimeter).HundredsHandLengthOffset = (int)numHundredsHandLengthOffset.Value;
                ((FIPSimConnectAltimeter)Altimeter).RimWidth = (int)numRimWidth.Value;
                ((FIPSimConnectAltimeter)Altimeter).TenThousandsHandLengthOffset = (int)numTenThousandsHandLengthOffset.Value;
                ((FIPSimConnectAltimeter)Altimeter).ThousandsHandLengthOffset = (int)numThousandsHandLengthOffset.Value;
                ((FIPSimConnectAltimeter)Altimeter).FaceGradientMode = (cbFaceGradientMode.SelectedItem as AltimeterLinearGradientMode).LinearGradientMode;
                ((FIPSimConnectAltimeter)Altimeter).GaugeImage = pbGaugeImage.Image != null ? new Bitmap(pbGaugeImage.Image) : null;
                ((FIPSimConnectAltimeter)Altimeter).GaugeImageFilename = _gaugeImageFileName;
            }
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

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbName.Text);
        }

        private void pbGaugeImage_Click(object sender, EventArgs e)
        {
            btnBrowse_Click(sender, e);
        }
    }
}
