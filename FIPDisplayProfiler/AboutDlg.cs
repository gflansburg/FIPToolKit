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
    public partial class AboutDlg : Form
    {
        private int fipIndex = 0;

        public AboutDlg()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fipIndex++;
            if(fipIndex > 5)
            {
                fipIndex = 0;
            }
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("FIPDisplayProfiler.Properties.Resources", typeof(global::FIPDisplayProfiler.Properties.Resources).Assembly);
            fipImage.Image = (System.Drawing.Bitmap)resourceManager.GetObject(string.Format("Fip{0}", fipIndex), System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}
