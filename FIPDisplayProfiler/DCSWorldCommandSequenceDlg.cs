﻿using FIPToolKit.Models;
using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace FIPDisplayProfiler
{
    public partial class DCSWorldCommandSequenceDlg : Form
    {
        public FIPCommandSequenceButton Button { get; set; }

        private Font _fontHolder;
        private string _iconFilename;
 
        public DCSWorldCommandSequenceDlg()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Button.Label = tbName.Text;
            Button.Font = _fontHolder;
            Button.Color = btnFontColor.BackColor;
            Button.IconFilename = _iconFilename;
            Button.ReColor = cbReColor.Checked;
            Button.IsDirty = true;
            Button.Sequence.Clear();
            foreach(ListViewItem item in lvDCSWorldCommands.Items)
            {
                Button.Sequence.Add(item.Tag as FIPDCSWorldCommandButton);
            }
            DialogResult = DialogResult.OK;
            Close();
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
                if (!string.IsNullOrEmpty(_iconFilename))
                {
                    pbIcon.Image = cbReColor.Checked ? FIPToolKit.Drawing.ImageHelper.ChangeToColor(Image.FromFile(_iconFilename), btnFontColor.BackColor) : Image.FromFile(_iconFilename);
                }
            }
        }

        private void DCSWorldCommandSequenceDlg_Load(object sender, EventArgs e)
        {
            tbName.Text = Button.Label;
            _fontHolder = Button.Font;
            tbFont.Font = new Font(Button.Font.FontFamily, tbFont.Font.Size, Button.Font.Style, Button.Font.Unit, Button.Font.GdiCharSet);
            tbFont.Text = Button.Font.FontFamily.Name;
            btnFontColor.BackColor = Button.Color;
            _iconFilename = Button.IconFilename;
            pbIcon.Image = Button.Icon;
            cbReColor.Checked = Button.ReColor;
            foreach(FIPDCSWorldCommandButton button in Button.Sequence)
            {
                string[] arr = new string[2];
                arr[0] = Regex.Replace(button.Break.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ");
                arr[1] = button.ToString();
                ListViewItem item = new ListViewItem(arr);
                item.Tag = button;
                lvDCSWorldCommands.Items.Add(item);
            }
            if(lvDCSWorldCommands.Items.Count > 0)
            {
                lvDCSWorldCommands.Items[0].Selected = true;
            }
            btnOK.Enabled = (!string.IsNullOrEmpty(tbName.Text) && lvDCSWorldCommands.Items.Count > 0);
        }

        private void btnRemoveIcon_Click(object sender, EventArgs e)
        {
            pbIcon.Image = null;
            _iconFilename = String.Empty;
        }

        private void btnBrowseIcon_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff;*.ico|Jpeg Images|*.jpg;*.jpeg|Bitmap Images|*.bmp|PNG Images|*.png|GIF Images|*.gif|TIFF Images|*.tif;*.tiff|Icons|*.ico";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog1.FileName = _iconFilename;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                _iconFilename = openFileDialog1.FileName;
                pbIcon.Image = cbReColor.Checked ? FIPToolKit.Drawing.ImageHelper.ChangeToColor(Image.FromFile(_iconFilename), btnFontColor.BackColor) : Image.FromFile(_iconFilename);
            }
        }

        private void cbReColor_CheckedChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(_iconFilename))
            {
                pbIcon.Image = cbReColor.Checked ? FIPToolKit.Drawing.ImageHelper.ChangeToColor(Image.FromFile(_iconFilename), btnFontColor.BackColor) : Image.FromFile(_iconFilename);
            }
        }

        private void pbIcon_Click(object sender, EventArgs e)
        {
            btnBrowseIcon_Click(sender, e);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = GetSelectedItem;
            if (selectedItem != null)
            {
                int index = GetSelectedIndex;
                lvDCSWorldCommands.Items.Remove(selectedItem);
                lvDCSWorldCommands.Items.Insert(index - 1, selectedItem);
                selectedItem.Selected = true;
                btnDelete.Enabled = GetSelectedIndex != -1;
                btnMoveUp.Enabled = GetSelectedIndex > 0;
                btnMoveDown.Enabled = GetSelectedIndex < (lvDCSWorldCommands.Items.Count - 1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = GetSelectedItem;
            if(selectedItem != null)
            {
                if (MessageBox.Show(this, "Are you sure you wish to delete this command?", "Delete Command", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int index = GetSelectedIndex;
                    lvDCSWorldCommands.Items.Remove(selectedItem);
                    index = Math.Min(index, lvDCSWorldCommands.Items.Count - 1);
                    if (index != -1)
                    {
                        lvDCSWorldCommands.Items[index].Selected = true;
                    }
                    btnDelete.Enabled = GetSelectedIndex != -1;
                    btnMoveUp.Enabled = GetSelectedIndex > 0;
                    btnMoveDown.Enabled = GetSelectedIndex < (lvDCSWorldCommands.Items.Count - 1);
                    btnOK.Enabled = (!string.IsNullOrEmpty(tbName.Text) && lvDCSWorldCommands.Items.Count > 0);
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = GetSelectedItem;
            if (selectedItem != null)
            {
                int index = GetSelectedIndex;
                lvDCSWorldCommands.Items.Remove(selectedItem);
                lvDCSWorldCommands.Items.Insert(index + 1, selectedItem);
                selectedItem.Selected = true;
                btnDelete.Enabled = GetSelectedIndex != -1;
                btnMoveUp.Enabled = GetSelectedIndex > 0;
                btnMoveDown.Enabled = GetSelectedIndex < (lvDCSWorldCommands.Items.Count - 1);
            }
        }

        private ListViewItem GetSelectedItem
        {
            get
            {
                foreach(ListViewItem item in lvDCSWorldCommands.SelectedItems)
                {
                    return item;
                }
                return null;
            }
        }

        private int GetSelectedIndex
        {
            get
            {
                foreach(int index in lvDCSWorldCommands.SelectedIndices)
                {
                    return index;
                }
                return -1;
            }
        }

        private void btnAddDCSWorldCommand_Click(object sender, EventArgs e)
        {
            AddDCSWorldCommandDlg dlg = new AddDCSWorldCommandDlg()
            {
                Button = new FIPDCSWorldCommandButton()
            };
            if(dlg.ShowDialog(this) == DialogResult.OK)
            {
                string[] arr = new string[2];
                arr[0] = Regex.Replace(dlg.Button.Break.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ");
                arr[1] = dlg.Button.ToString();
                ListViewItem item = new ListViewItem(arr);
                item.Tag = dlg.Button;
                lvDCSWorldCommands.Items.Add(item);
                item.Selected = true;
                btnDelete.Enabled = GetSelectedIndex != -1;
                btnMoveUp.Enabled = GetSelectedIndex > 0;
                btnMoveDown.Enabled = GetSelectedIndex < (lvDCSWorldCommands.Items.Count - 1);
                btnOK.Enabled = (!string.IsNullOrEmpty(tbName.Text) && lvDCSWorldCommands.Items.Count > 0);
            }
        }

        private void lvDCSWorldCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = GetSelectedIndex != -1;
            btnMoveUp.Enabled = GetSelectedIndex > 0;
            btnMoveDown.Enabled = GetSelectedIndex < (lvDCSWorldCommands.Items.Count - 1);
        }

        private void lvDCSWorldCommands_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem selectedItem = GetSelectedItem;
            if (selectedItem != null)
            {
                AddDCSWorldCommandDlg dlg = new AddDCSWorldCommandDlg()
                {
                    Button = selectedItem.Tag as FIPDCSWorldCommandButton
                };
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    selectedItem.SubItems[0].Text = Regex.Replace(dlg.Button.Break.ToString(), "(\\B[A-Z])", " $1").Replace(" A ", " a ").Replace(" And ", " and ");
                    selectedItem.SubItems[1].Text = dlg.Button.ToString();
                    btnDelete.Enabled = GetSelectedIndex != -1;
                    btnMoveUp.Enabled = GetSelectedIndex > 0;
                    btnMoveDown.Enabled = GetSelectedIndex < (lvDCSWorldCommands.Items.Count - 1);
                }
            }
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnOK.Enabled = (!string.IsNullOrEmpty(tbName.Text) && lvDCSWorldCommands.Items.Count > 0);
        }

        private void lvDCSWorldCommands_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = lvDCSWorldCommands.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
