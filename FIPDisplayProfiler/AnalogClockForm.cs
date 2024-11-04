using FIPToolKit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public partial class AnalogClockForm : Form
    {
        public FIPAnalogClockProperties AnalogClock { get; set; }

        private Font _fontHolder;

        private string _faceImageFileName;

        private class ClockTickStyle
        {
            public FIPAnalogClock.TickStyle TickStyle { get; set; }
            public string Name { get; set; }
        }

        private class ClockSmoothingMode
        {
            public SmoothingMode SmoothingMode { get; set; }
            public string Name { get; set; }
        }

        private class ClockTextRenderingHint
        {
            public TextRenderingHint TextRenderingHint { get; set; }
            public string Name { get; set; }
        }

        private class ClockLinearGradientMode
        {
            public LinearGradientMode LinearGradientMode { get; set; }
            public string Name { get; set; }
        }

        private class ClockLineCap
        {
            public LineCap LineCap { get; set; }
            public string Name { get; set; }
        }

        private class ClockFaceTickStyle
        {
            public FIPAnalogClock.FaceTickStyles FaceTickStyle { get; set; }
            public string Name { get; set; }
        }

        private class ClockNumeralType
        {
            public FIPAnalogClock.NumeralTypes NumeralType { get; set; }
            //public string CultureCode { get; set; }
            public string Name { get; set; }
        }

        public AnalogClockForm()
        {
            InitializeComponent();
            /*cbNumeralType.Items.Add(new ClockNumeralType()
            {
                Name = "Roman Numerals (Classic)",
                CultureCode = "en",
                NumeralType = FIPAnalogClock.NumeralTypes.RomanClassic
            });
            cbNumeralType.Items.Add(new ClockNumeralType()
            {
                Name = "Roman Numerals (Clock)",
                CultureCode = "en",
                NumeralType = FIPAnalogClock.NumeralTypes.RomanClock
            });
            foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                if (!string.IsNullOrEmpty(cultureInfo.Name))
                {
                    cbNumeralType.Items.Add(new ClockNumeralType()
                    {
                        Name = cultureInfo.EnglishName,
                        CultureCode = cultureInfo.Name,
                        NumeralType = FIPAnalogClock.NumeralTypes.Default
                    });
                }
            }*/
            foreach (FIPAnalogClock.NumeralTypes numeralType in (FIPAnalogClock.NumeralTypes[])Enum.GetValues(typeof(FIPAnalogClock.NumeralTypes)))
            {
                cbNumeralType.Items.Add(new ClockNumeralType()
                {
                    NumeralType = numeralType,
                    Name = (numeralType == FIPAnalogClock.NumeralTypes.ArabicWestern ? "Western (Hindu/Arabic)" : Regex.Replace(numeralType.ToString(), "(\\B[A-Z])", " $1"))
                });
            }
            foreach (TimeZoneInfo timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                cbTimeZone.Items.Add(timeZone);
            }
            foreach (FIPAnalogClock.TickStyle tickStyle in (FIPAnalogClock.TickStyle[])Enum.GetValues(typeof(FIPAnalogClock.TickStyle)))
            {
                ClockTickStyle clockTickStyle = new ClockTickStyle()
                {
                    TickStyle = tickStyle,
                    Name = Regex.Replace(tickStyle.ToString(), "(\\B[A-Z])", " $1")
                };
                cbSecondHandTickStyle.Items.Add(clockTickStyle);
                cbMinuteHandTickStyle.Items.Add(clockTickStyle);
            }
            foreach (SmoothingMode smoothingMode in (SmoothingMode[])Enum.GetValues(typeof(SmoothingMode)))
            {
                cbSmoothingMode.Items.Add(new ClockSmoothingMode()
                {
                    SmoothingMode = smoothingMode,
                    Name = Regex.Replace(smoothingMode.ToString(), "(\\B[A-Z])", " $1")
                });
            }
            foreach (LineCap lineCap in (LineCap[])Enum.GetValues(typeof(LineCap)))
            {
                cbSecondHandEndCap.Items.Add(new ClockLineCap()
                {
                    LineCap = lineCap,
                    Name = Regex.Replace(lineCap.ToString(), "(\\B[A-Z])", " $1")
                });
            }
            foreach (TextRenderingHint textRenderingHint in (TextRenderingHint[])Enum.GetValues(typeof(TextRenderingHint)))
            {
                cbTextRenderingHint.Items.Add(new ClockTextRenderingHint()
                {
                    TextRenderingHint = textRenderingHint,
                    Name = Regex.Replace(textRenderingHint.ToString(), "(\\B[A-Z])", " $1")
                });
            }
            foreach (LinearGradientMode linearGradientMode in (LinearGradientMode[])Enum.GetValues(typeof(LinearGradientMode)))
            {
                ClockLinearGradientMode clockLinearGradientMode = new ClockLinearGradientMode()
                {
                    LinearGradientMode = linearGradientMode,
                    Name = Regex.Replace(linearGradientMode.ToString(), "(\\B[A-Z])", " $1")
                };
                cbFaceGradientMode.Items.Add(clockLinearGradientMode);
                cbRimGradientMode.Items.Add(clockLinearGradientMode);
            }
            foreach (FIPAnalogClock.FaceTickStyles faceTickStyle in (FIPAnalogClock.FaceTickStyles[])Enum.GetValues(typeof(FIPAnalogClock.FaceTickStyles)))
            {
                cbFaceTickStyle.Items.Add(new ClockFaceTickStyle()
                {
                    FaceTickStyle = faceTickStyle,
                    Name = Regex.Replace(faceTickStyle.ToString(), "(\\B[A-Z])", " $1")
                });
            }
        }

        private int IndexOfTimeZone(string id)
        {
            for(int i = 0; i < cbTimeZone.Items.Count; i++)
            {
                TimeZoneInfo timeZone = cbTimeZone.Items[i] as TimeZoneInfo;
                if (timeZone.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfTickStyle(ComboBox cb, FIPAnalogClock.TickStyle tickStyle)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                ClockTickStyle clockTickStyle = cb.Items[i] as ClockTickStyle;
                if (clockTickStyle.TickStyle == tickStyle)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfLinearGradientMode(ComboBox cb, LinearGradientMode linearGradientMode)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                ClockLinearGradientMode clockLinearGradientMode = cb.Items[i] as ClockLinearGradientMode;
                if (clockLinearGradientMode.LinearGradientMode == linearGradientMode)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfLineCap(LineCap lineCap)
        {
            for (int i = 0; i < cbSecondHandEndCap.Items.Count; i++)
            {
                ClockLineCap clockLineCap = cbSecondHandEndCap.Items[i] as ClockLineCap;
                if (clockLineCap.LineCap == lineCap)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfSmoothingMode(SmoothingMode smoothingMode)
        {
            for (int i = 0; i < cbSmoothingMode.Items.Count; i++)
            {
                ClockSmoothingMode clockSmoothingMode = cbSmoothingMode.Items[i] as ClockSmoothingMode;
                if (clockSmoothingMode.SmoothingMode == smoothingMode)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfTextRenderingHint(TextRenderingHint textRenderingHint)
        {
            for (int i = 0; i < cbTextRenderingHint.Items.Count; i++)
            {
                ClockTextRenderingHint clockTextRenderingHint = cbTextRenderingHint.Items[i] as ClockTextRenderingHint;
                if (clockTextRenderingHint.TextRenderingHint == textRenderingHint)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfNumeralType(FIPAnalogClock.NumeralTypes numeralType/*, string cultureCode*/)
        {
            for (int i = 0; i < cbNumeralType.Items.Count; i++)
            {
                ClockNumeralType clockNumeralType = cbNumeralType.Items[i] as ClockNumeralType;
                //if (clockNumeralType.NumeralType == numeralType && clockNumeralType.CultureCode.Equals(cultureCode, StringComparison.OrdinalIgnoreCase))
                if (clockNumeralType.NumeralType == numeralType)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfFaceTickStyle(FIPAnalogClock.FaceTickStyles faceTickStyle)
        {
            for (int i = 0; i < cbFaceTickStyle.Items.Count; i++)
            {
                ClockFaceTickStyle clockFaceTickStyle = cbFaceTickStyle.Items[i] as ClockFaceTickStyle;
                if (clockFaceTickStyle.FaceTickStyle == faceTickStyle)
                {
                    return i;
                }
            }
            return -1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AnalogClock.Name = tbName.Text;
            AnalogClock.DrawCaption = cbDrawCaption.Checked;
            AnalogClock.CaptionColor = btnCaptionColor.BackColor;
            AnalogClock.TimeZone = ((TimeZoneInfo)cbTimeZone.SelectedItem).Id;
            AnalogClock.Font = _fontHolder;
            AnalogClock.SecondHandTickStyle = ((ClockTickStyle)cbSecondHandTickStyle.SelectedItem).TickStyle;
            AnalogClock.MinuteHandTickStyle = ((ClockTickStyle)cbMinuteHandTickStyle.SelectedItem).TickStyle;
            AnalogClock.DrawNumerals = cbDrawNumerals.Checked;
            AnalogClock.FontColor = btnNumeralColor.BackColor;
            AnalogClock.FaceImage = pbFaceImage.Image != null ? new Bitmap(pbFaceImage.Image) : null;
            AnalogClock.FaceImageFilename = _faceImageFileName;
            AnalogClock.DrawRim = cbDrawRim.Checked;
            AnalogClock.DrawDropShadow = cbDrawDropShadow.Checked;
            AnalogClock.DropShadowColor = btnDropShadowColor.BackColor;
            AnalogClock.DrawHourHandShadow = cbDrawHourHandShadow.Checked;
            AnalogClock.HourHandDropShadowColor = btnHourHandDropShadowColor.BackColor;
            AnalogClock.DrawMinuteHandShadow = cbDrawMinuteHandShadow.Checked;
            AnalogClock.MinuteHandDropShadowColor = btnMinuteHandDropShadowColor.BackColor;
            AnalogClock.DrawSecondHandShadow = cbDrawSecondHandShadow.Checked;
            AnalogClock.SecondHandDropShadowColor = btnSecondHandDropShadowColor.BackColor;
            AnalogClock.FaceColorHigh = btnFaceColorHigh.BackColor;
            AnalogClock.FaceColorLow = btnFaceColorLow.BackColor;
            AnalogClock.RimColorHigh = btnRimColorHigh.BackColor;
            AnalogClock.RimColorLow = btnRimColorLow.BackColor;
            AnalogClock.DrawHourHand = cbDrawHourHand.Checked;
            AnalogClock.HourHandColor = btnHourHandColor.BackColor;
            AnalogClock.DrawMinuteHand = cbDrawMinuteHand.Checked;
            AnalogClock.MinuteHandColor = btnMinuteHandColor.BackColor;
            AnalogClock.DrawSecondHand = cbDrawSecondHand.Checked;
            AnalogClock.SecondHandColor = btnSecondHandColor.BackColor;
            AnalogClock.HourHandLengthOffset = (int)numHourHandLengthOffset.Value;
            AnalogClock.MinuteHandLengthOffset = (int)numMinuteHandLengthOffset.Value;
            AnalogClock.SecondHandLengthOffset = (int)numSecondHandLengthOffset.Value;
            AnalogClock.FaceTickSize = new Size((int)numFaceTickMarkWidth.Value, (int)numFaceTickMarkLength.Value);
            AnalogClock.SmoothingMode = ((ClockSmoothingMode)cbSmoothingMode.SelectedItem).SmoothingMode;
            AnalogClock.TextRenderingHint = ((ClockTextRenderingHint)cbTextRenderingHint.SelectedItem).TextRenderingHint;
            AnalogClock.DropShadowOffset = new Point((int)numDropShadowOffsetX.Value, (int)numDropShadowOffsetY.Value);
            AnalogClock.FaceGradientMode = ((ClockLinearGradientMode)cbFaceGradientMode.SelectedItem).LinearGradientMode;
            AnalogClock.RimGradientMode = ((ClockLinearGradientMode)cbRimGradientMode.SelectedItem).LinearGradientMode;
            AnalogClock.SecondHandEndCap = ((ClockLineCap)cbSecondHandEndCap.SelectedItem).LineCap;
            AnalogClock.NumeralType = ((ClockNumeralType)cbNumeralType.SelectedItem).NumeralType;
            //AnalogClock.CultureCode = ((ClockNumeralType)cbNumeralType.SelectedItem).CultureCode;
            AnalogClock.FaceTickStyle = ((ClockFaceTickStyle)cbFaceTickStyle.SelectedItem).FaceTickStyle;
            AnalogClock.DrawFaceTicks = cbDrawTickMarks.Checked;
            AnalogClock.RimWidth = (int)numRimWidth.Value;
            AnalogClock.TwentyFourHour = cbTwentyFourHourClock.Checked;
            AnalogClock.IsDirty = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AnalogClockForm_Load(object sender, EventArgs e)
        {
            LoadAnalogClock(AnalogClock);
            btnOK.Enabled = !string.IsNullOrEmpty(tbName.Text);
        }

        private void cbDrawCaption_CheckedChanged(object sender, EventArgs e)
        {
            btnCaptionColor.Enabled = cbDrawCaption.Checked;
        }

        private void btnCaptionColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnCaptionColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnCaptionColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = _fontHolder;
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _fontHolder = fontDialog1.Font;
                tbFont.Font = new Font(fontDialog1.Font.FontFamily, tbFont.Font.Size, fontDialog1.Font.Style, fontDialog1.Font.Unit, fontDialog1.Font.GdiCharSet);
                tbFont.Text = fontDialog1.Font.Name;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog1.FileName = _faceImageFileName;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                pbFaceImage.Image = Image.FromFile(openFileDialog1.FileName);
                _faceImageFileName = openFileDialog1.FileName;
            }
        }

        private void cbDrawDropShadow_CheckedChanged(object sender, EventArgs e)
        {
            btnDropShadowColor.Enabled = cbDrawDropShadow.Checked;
            numDropShadowOffsetX.Enabled = cbDrawDropShadow.Checked;
            numDropShadowOffsetY.Enabled = cbDrawDropShadow.Checked;
        }

        private void btnDrowShadowColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnDropShadowColor.BackColor;
            if(colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnDropShadowColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void cbDrawHourHandShadow_CheckedChanged(object sender, EventArgs e)
        {
            btnHourHandDropShadowColor.Enabled = cbDrawHourHandShadow.Checked;
        }

        private void btnHourHandDropShadowColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnHourHandDropShadowColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnHourHandDropShadowColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void cbDrawMinuteHandShadow_CheckedChanged(object sender, EventArgs e)
        {
            btnMinuteHandDropShadowColor.Enabled = cbDrawMinuteHandShadow.Checked;
        }

        private void btnMinuteHandDropShadowColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnMinuteHandDropShadowColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnMinuteHandDropShadowColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void cbDrawSecondHandShadow_CheckedChanged(object sender, EventArgs e)
        {
            btnSecondHandDropShadowColor.Enabled = cbDrawSecondHandShadow.Checked;
        }

        private void btnSecondHandDropShadowColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnSecondHandDropShadowColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnSecondHandDropShadowColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
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

        private void btnRimColorHigh_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnRimColorHigh.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnRimColorHigh.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void btnRimColorLow_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnRimColorLow.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnRimColorLow.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void cbDrawHourHand_CheckedChanged(object sender, EventArgs e)
        {
            btnHourHandColor.Enabled = cbDrawHourHand.Checked;
            numHourHandLengthOffset.Enabled = cbDrawHourHand.Checked;
        }

        private void btnHourHandColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnHourHandColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnHourHandColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void cbDrawMinuteHand_CheckedChanged(object sender, EventArgs e)
        {
            btnMinuteHandColor.Enabled = cbDrawMinuteHand.Checked;
            cbMinuteHandTickStyle.Enabled = cbDrawMinuteHand.Checked;
            numMinuteHandLengthOffset.Enabled = cbDrawMinuteHand.Checked;
        }

        private void btnMinuteHandColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnMinuteHandColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnMinuteHandColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void cbDrawSecondHand_CheckedChanged(object sender, EventArgs e)
        {
            btnSecondHandColor.Enabled = cbDrawSecondHand.Checked;
            cbSecondHandEndCap.Enabled = cbDrawSecondHand.Checked;
            cbSecondHandTickStyle.Enabled = cbDrawSecondHand.Checked;
            numSecondHandLengthOffset.Enabled = cbDrawSecondHand.Checked;
        }

        private void btnSecondHandColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnSecondHandColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnSecondHandColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void cbDrawNumerals_CheckedChanged(object sender, EventArgs e)
        {
            btnNumeralColor.Enabled = cbDrawNumerals.Checked;
            cbNumeralType.Enabled = cbDrawNumerals.Checked;
        }

        private void btnNumeralColor_Click(object sender, EventArgs e)
        {
            colorDialog1.CustomColors = Properties.Settings.Default.CustomColors;
            colorDialog1.Color = btnNumeralColor.BackColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                btnNumeralColor.BackColor = colorDialog1.Color;
                Properties.Settings.Default.CustomColors = colorDialog1.CustomColors;
                Properties.Settings.Default.Save();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            pbFaceImage.Image = null;
            _faceImageFileName = String.Empty;
        }

        private void cbDrawRim_CheckedChanged(object sender, EventArgs e)
        {
            numRimWidth.Enabled = cbDrawRim.Checked;
            cbRimGradientMode.Enabled = cbDrawTickMarks.Checked || cbDrawRim.Checked;
            btnRimColorHigh.Enabled = cbDrawTickMarks.Checked || cbDrawRim.Checked;
            btnRimColorLow.Enabled = cbDrawTickMarks.Checked || cbDrawRim.Checked;
        }

        private void cbDrawTickMarks_CheckedChanged(object sender, EventArgs e)
        {
            numFaceTickMarkLength.Enabled = cbDrawTickMarks.Checked;
            numFaceTickMarkWidth.Enabled = cbDrawTickMarks.Checked;
            cbFaceTickStyle.Enabled = cbDrawTickMarks.Checked;
            cbRimGradientMode.Enabled =  cbDrawTickMarks.Checked || cbDrawRim.Checked;
            btnRimColorHigh.Enabled = cbDrawTickMarks.Checked || cbDrawRim.Checked;
            btnRimColorLow.Enabled = cbDrawTickMarks.Checked || cbDrawRim.Checked;
        }

        private void LoadAnalogClock(FIPAnalogClockProperties analogClock)
        {
            tbName.Text = analogClock.Name;
            cbDrawCaption.Checked = analogClock.DrawCaption;
            btnCaptionColor.BackColor = analogClock.CaptionColor;
            cbTimeZone.SelectedIndex = IndexOfTimeZone(analogClock.TimeZone);
            _fontHolder = analogClock.Font;
            tbFont.Font = new Font(analogClock.Font.FontFamily, tbFont.Font.Size, analogClock.Font.Style, analogClock.Font.Unit, analogClock.Font.GdiCharSet);
            tbFont.Text = analogClock.Font.FontFamily.Name;
            cbSecondHandTickStyle.SelectedIndex = IndexOfTickStyle(cbSecondHandTickStyle, analogClock.SecondHandTickStyle);
            cbMinuteHandTickStyle.SelectedIndex = IndexOfTickStyle(cbMinuteHandTickStyle, analogClock.MinuteHandTickStyle);
            cbSecondHandEndCap.SelectedIndex = IndexOfLineCap(analogClock.SecondHandEndCap);
            cbNumeralType.SelectedIndex = IndexOfNumeralType(analogClock.NumeralType/*, analogClock.CultureCode*/);
            cbFaceTickStyle.SelectedIndex = IndexOfFaceTickStyle(analogClock.FaceTickStyle);
            cbDrawTickMarks.Checked = analogClock.DrawFaceTicks;
            cbDrawNumerals.Checked = analogClock.DrawNumerals;
            btnNumeralColor.BackColor = analogClock.FontColor;
            pbFaceImage.Image = (analogClock.FaceImage != null ? new Bitmap(analogClock.FaceImage) : null);
            _faceImageFileName = analogClock.FaceImageFilename;
            if (analogClock.FaceImage == null && !string.IsNullOrEmpty(analogClock.FaceImageFilename))
            {
                pbFaceImage.Image = new Bitmap(FIPToolKit.Drawing.ImageHelper.GetBitmapResource(analogClock.FaceImageFilename));
            }
            cbDrawRim.Checked = analogClock.DrawRim;
            cbDrawDropShadow.Checked = analogClock.DrawDropShadow;
            btnDropShadowColor.BackColor = analogClock.DropShadowColor;
            cbDrawHourHandShadow.Checked = analogClock.DrawHourHandShadow;
            btnHourHandDropShadowColor.BackColor = analogClock.HourHandDropShadowColor;
            cbDrawMinuteHandShadow.Checked = analogClock.DrawMinuteHandShadow;
            btnMinuteHandDropShadowColor.BackColor = analogClock.MinuteHandDropShadowColor;
            cbDrawSecondHandShadow.Checked = analogClock.DrawSecondHandShadow;
            btnSecondHandDropShadowColor.BackColor = analogClock.SecondHandDropShadowColor;
            btnFaceColorHigh.BackColor = analogClock.FaceColorHigh;
            btnFaceColorLow.BackColor = analogClock.FaceColorLow;
            btnRimColorHigh.BackColor = analogClock.RimColorHigh;
            btnRimColorLow.BackColor = analogClock.RimColorLow;
            cbDrawHourHand.Checked = analogClock.DrawHourHand;
            btnHourHandColor.BackColor = analogClock.HourHandColor;
            cbDrawMinuteHand.Checked = analogClock.DrawMinuteHand;
            btnMinuteHandColor.BackColor = analogClock.MinuteHandColor;
            cbDrawSecondHand.Checked = analogClock.DrawSecondHand;
            btnSecondHandColor.BackColor = analogClock.SecondHandColor;
            cbSmoothingMode.SelectedIndex = IndexOfSmoothingMode(analogClock.SmoothingMode);
            cbTextRenderingHint.SelectedIndex = IndexOfTextRenderingHint(analogClock.TextRenderingHint);
            cbFaceGradientMode.SelectedIndex = IndexOfLinearGradientMode(cbFaceGradientMode, analogClock.FaceGradientMode);
            cbRimGradientMode.SelectedIndex = IndexOfLinearGradientMode(cbRimGradientMode, analogClock.RimGradientMode);
            numDropShadowOffsetX.Value = analogClock.DropShadowOffset.X;
            numDropShadowOffsetY.Value = analogClock.DropShadowOffset.Y;
            numRimWidth.Value = analogClock.RimWidth;
            numFaceTickMarkLength.Value = analogClock.FaceTickSize.Height;
            numFaceTickMarkWidth.Value = analogClock.FaceTickSize.Width;
            numHourHandLengthOffset.Value = analogClock.HourHandLengthOffset;
            numMinuteHandLengthOffset.Value = analogClock.MinuteHandLengthOffset;
            numSecondHandLengthOffset.Value = analogClock.SecondHandLengthOffset;
            cbTwentyFourHourClock.Checked = analogClock.TwentyFourHour;
            numDropShadowOffsetX.Enabled = cbDrawDropShadow.Checked;
            numDropShadowOffsetY.Enabled = cbDrawDropShadow.Checked;
            btnDropShadowColor.Enabled = cbDrawDropShadow.Checked;
            btnCaptionColor.Enabled = cbDrawCaption.Checked;
            btnHourHandDropShadowColor.Enabled = cbDrawHourHandShadow.Checked;
            btnMinuteHandDropShadowColor.Enabled = cbDrawMinuteHandShadow.Checked;
            btnSecondHandDropShadowColor.Enabled = cbDrawSecondHandShadow.Checked;
            btnHourHandColor.Enabled = cbDrawHourHand.Checked;
            numHourHandLengthOffset.Enabled = cbDrawHourHand.Checked;
            btnMinuteHandColor.Enabled = cbDrawMinuteHand.Checked;
            numMinuteHandLengthOffset.Enabled = cbDrawMinuteHand.Checked;
            cbMinuteHandTickStyle.Enabled = cbDrawMinuteHand.Checked;
            btnSecondHandColor.Enabled = cbDrawSecondHand.Checked;
            numSecondHandLengthOffset.Enabled = cbDrawSecondHand.Checked;
            cbSecondHandEndCap.Enabled = cbDrawSecondHand.Checked;
            cbSecondHandTickStyle.Enabled = cbDrawSecondHand.Checked;
            btnNumeralColor.Enabled = cbDrawNumerals.Checked;
            cbNumeralType.Enabled = cbDrawNumerals.Checked;
            numFaceTickMarkLength.Enabled = cbDrawTickMarks.Checked;
            numFaceTickMarkWidth.Enabled = cbDrawTickMarks.Checked;
            cbFaceTickStyle.Enabled = cbDrawTickMarks.Enabled;
            cbRimGradientMode.Enabled = cbDrawTickMarks.Checked || cbDrawRim.Checked;
            btnRimColorHigh.Enabled = cbDrawTickMarks.Checked || cbDrawRim.Checked;
            btnRimColorLow.Enabled = cbDrawTickMarks.Checked || cbDrawRim.Checked;
            numRimWidth.Enabled = cbDrawRim.Checked;
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            AnalogClockTemplateDialog dlg = new AnalogClockTemplateDialog();
            if(dlg.ShowDialog(this) == DialogResult.OK)
            {
                FIPAnalogClockProperties template = null;
                switch(dlg.TemplateType)
                {
                    case AnalogClockTemplateTypes.CessnaAirspeed:
                        template = FIPAnalogClock.FIPAnalogClockCessnaAirspeed;
                        break;
                    case AnalogClockTemplateTypes.CessnaAltimeter:
                        template = FIPAnalogClock.FIPAnalogClockCessnaAltimeter;
                        break;
                    case AnalogClockTemplateTypes.CessnaClock1:
                        template = FIPAnalogClock.FIPAnalogClockCessnaClock1;
                        break;
                    case AnalogClockTemplateTypes.CessnaClock2:
                        template = FIPAnalogClock.FIPAnalogClockCessnaClock2;
                        break;
                    case AnalogClockTemplateTypes.HongKong:
                        template = FIPAnalogClock.FIPAnalogClockHongKong;
                        break;
                    case AnalogClockTemplateTypes.Karachi:
                        template = FIPAnalogClock.FIPAnalogClockKarachi;
                        break;
                    case AnalogClockTemplateTypes.Paris:
                        template = FIPAnalogClock.FIPAnalogClockParis;
                        break;
                    case AnalogClockTemplateTypes.Moscow:
                        template = FIPAnalogClock.FIPAnalogClockMoscow;
                        break;
                    case AnalogClockTemplateTypes.NewYork:
                        template = FIPAnalogClock.FIPAnalogClockNewYork;
                        break;
                    case AnalogClockTemplateTypes.LosAngeles:
                        template = FIPAnalogClock.FIPAnalogClockLosAngeles;
                        break;
                    case AnalogClockTemplateTypes.Sydney:
                        template = FIPAnalogClock.FIPAnalogClockSydney;
                        break;
                    case AnalogClockTemplateTypes.London:
                        template = FIPAnalogClock.FIPAnalogClockLondon;
                        break;
                    case AnalogClockTemplateTypes.Chicago:
                        template = FIPAnalogClock.FIPAnalogClockChicago;
                        break;
                    case AnalogClockTemplateTypes.Berlin:
                        template = FIPAnalogClock.FIPAnalogClockBerlin;
                        break;
                    case AnalogClockTemplateTypes.Tokyo:
                        template = FIPAnalogClock.FIPAnalogClockTokyo;
                        break;
                    case AnalogClockTemplateTypes.Denver:
                        template = FIPAnalogClock.FIPAnalogClockDenver;
                        break;
                    case AnalogClockTemplateTypes.Shanghai:
                        template = FIPAnalogClock.FIPAnalogClockShanghai;
                        break;
                    case AnalogClockTemplateTypes.Honolulu:
                        template = FIPAnalogClock.FIPAnalogClockHonolulu;
                        break;
                }
                LoadAnalogClock(template);
            }
        }

        private void pbFaceImage_Click(object sender, EventArgs e)
        {
            btnBrowse_Click(sender, e);
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbName.Text);
        }
    }
}
