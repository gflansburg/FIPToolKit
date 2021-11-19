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
    public enum AnalogClockTemplateTypes
    {
        Paris,
        Karachi,
        HongKong,
        Moscow,
        NewYork,
        LosAngeles,
        Chicago,
        London,
        Berlin,
        Tokyo,
        Sydney,
        Denver,
        Honolulu,
        Shanghai,
        CessnaClock1,
        CessnaClock2,
        CessnaAirspeed,
        CessnaAltimeter
    }

    public partial class AnalogClockTemplateDialog : Form
    {
        public AnalogClockTemplateTypes TemplateType { get; set; }

        public AnalogClockTemplateDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DeselectRadioButtonGroup(GroupBox groupBox)
        {
            foreach(RadioButton radioButton in groupBox.Controls)
            {
                radioButton.Checked = false;
            }
        }

        private void rbCessnaClock1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCessnaClock1.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.CessnaClock1;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbCities);
            }
        }

        private void rbCessnaClock2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCessnaClock2.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.CessnaClock2;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbCities);
            }
        }

        private void rbCessnaAirspeed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCessnaAirspeed.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.CessnaAirspeed;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbCities);
            }
        }

        private void rbCessnaAltimeter_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCessnaAltimeter.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.CessnaAltimeter;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbCities);
            }
        }

        private void rbParis_CheckedChanged(object sender, EventArgs e)
        {
            if (rbParis.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Paris;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbKarachi_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKarachi.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Karachi;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbHongKong_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHongKong.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.HongKong;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbMoscow_CheckedChanged(object sender, EventArgs e)
        {
            if(rbMoscow.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Moscow;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbNewYork_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNewYork.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.NewYork;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbLosAngeles_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLosAngeles.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.LosAngeles;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbChicago_CheckedChanged(object sender, EventArgs e)
        {
            if (rbChicago.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Chicago;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbLondon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLondon.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.London;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbBerlin_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBerlin.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Berlin;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbTokyo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTokyo.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Tokyo;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbSydney_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSydney.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Sydney;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbDenver_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDenver.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Denver;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbHonolulu_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHonolulu.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Honolulu;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }

        private void rbShanghai_CheckedChanged(object sender, EventArgs e)
        {
            if (rbShanghai.Checked)
            {
                TemplateType = AnalogClockTemplateTypes.Shanghai;
                btnOK.Enabled = true;
                DeselectRadioButtonGroup(gbAviation);
            }
        }
    }
}
