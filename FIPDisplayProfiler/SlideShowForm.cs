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
    public partial class SlideShowForm : Form
    {
        public FIPSlideShow SlideShow { get; set; }

        private Font _fontHolder;

        public SlideShowForm()
        {
            InitializeComponent();
        }

        private void btnNewPicture_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                int index = lbImages.Items.Add(openFileDialog1.FileName);
                lbImages.SelectedIndex = index;
                btnOK.Enabled = !String.IsNullOrEmpty(tbName.Text) && lbImages.Items.Count > 0;
            }
        }

        private void lbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbImages.SelectedItem != null)
            {
                pbImage.Image = new Bitmap(lbImages.SelectedItem.ToString());
            }
            else
            {
                pbImage.Image = null;
            }
            btnDelete.Enabled = lbImages.SelectedIndex != -1;
            btnMoveUp.Enabled = lbImages.SelectedIndex > 0;
            btnMoveDown.Enabled = lbImages.SelectedIndex < (lbImages.Items.Count - 1);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int index = lbImages.SelectedIndex;
            string fileName = lbImages.SelectedItem as string;
            lbImages.Items.RemoveAt(index);
            lbImages.Items.Insert(index - 1, fileName);
            lbImages.SelectedIndex = index - 1;
            btnDelete.Enabled = lbImages.SelectedIndex != -1;
            btnMoveUp.Enabled = lbImages.SelectedIndex > 0;
            btnMoveDown.Enabled = lbImages.SelectedIndex < (lbImages.Items.Count - 1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = lbImages.SelectedIndex;
            if (index != -1)
            {
                if (MessageBox.Show(this, "Are you sure you wish to delete this picture?", "Delete Picture", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    lbImages.Items.RemoveAt(index);
                    if (lbImages.Items.Count > 0)
                    {
                        lbImages.SelectedIndex = Math.Min(index, lbImages.Items.Count - 1);
                    }
                    btnDelete.Enabled = lbImages.SelectedIndex != -1;
                    btnMoveUp.Enabled = lbImages.SelectedIndex > 0;
                    btnMoveDown.Enabled = lbImages.SelectedIndex < (lbImages.Items.Count - 1);
                    btnOK.Enabled = !String.IsNullOrEmpty(tbName.Text) && lbImages.Items.Count > 0;
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int index = lbImages.SelectedIndex;
            string fileName = lbImages.SelectedItem as string;
            lbImages.Items.RemoveAt(index);
            lbImages.Items.Insert(index + 1, fileName);
            lbImages.SelectedIndex = index + 1;
            btnDelete.Enabled = lbImages.SelectedIndex != -1;
            btnMoveUp.Enabled = lbImages.SelectedIndex > 0;
            btnMoveDown.Enabled = lbImages.SelectedIndex < (lbImages.Items.Count - 1);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SlideShow.Name = tbName.Text;
            SlideShow.Delay = (int)(numDelayInSeconds.Value * 1000);
            SlideShow.Images.Clear();
            foreach(string fileName in lbImages.Items)
            {
                SlideShow.Images.Add(fileName);
            }
            SlideShow.Font = _fontHolder;
            SlideShow.FontColor = btnFontColor.BackColor;
            SlideShow.IsDirty = true;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SlideShowForm_Load(object sender, EventArgs e)
        {
            foreach(string fileName in SlideShow.Images)
            {
                lbImages.Items.Add(fileName);
            }
            if(lbImages.Items.Count > 0)
            {
                lbImages.SelectedIndex = 0;
            }
            numDelayInSeconds.Value = SlideShow.Delay / 1000;
            tbName.Text = SlideShow.Name;
            _fontHolder = SlideShow.Font;
            tbFont.Font = new Font(SlideShow.Font.FontFamily, tbFont.Font.Size, SlideShow.Font.Style, SlideShow.Font.Unit, SlideShow.Font.GdiCharSet);
            tbFont.Text = SlideShow.Font.FontFamily.Name;
            btnFontColor.BackColor = SlideShow.FontColor;
            btnDelete.Enabled = lbImages.SelectedIndex != -1;
            btnMoveUp.Enabled = lbImages.SelectedIndex > 0;
            btnMoveDown.Enabled = lbImages.SelectedIndex < (lbImages.Items.Count - 1);
            btnOK.Enabled = !String.IsNullOrEmpty(tbName.Text) && lbImages.Items.Count > 0;
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

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnOK.Enabled = !String.IsNullOrEmpty(tbName.Text) && lbImages.Items.Count > 0;
        }
    }
}
