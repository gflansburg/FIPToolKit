using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIPDisplayProfiler
{
    public class ToolStripSubMenu : ToolStripMenuItem
    {
        private bool isHovering = false;
        public ToolStripSubMenu() : base()
        {
            this.MouseEnter += MyToolStripMenuItem_MouseEnter;
            this.MouseLeave += MyToolStripMenuItem_MouseLeave;
        }

        public ToolStripSubMenu(string t) : base(t)
        {
            this.MouseEnter += MyToolStripMenuItem_MouseEnter;
            this.MouseLeave += MyToolStripMenuItem_MouseLeave;
        }

        private void MyToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            isHovering = false;
        }

        private void MyToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            isHovering = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(isHovering)
            {
                e.Graphics.FillRectangle(SystemBrushes.Window, e.ClipRectangle);
            }
            Rectangle glyphRect = new Rectangle(e.ClipRectangle.Width - 20, e.ClipRectangle.Y, 16, e.ClipRectangle.Height);
            ControlPaint.DrawMenuGlyph(e.Graphics, glyphRect, MenuGlyph.Arrow);
            if (isHovering)
            {
                Color newColor = Color.FromArgb(64, SystemColors.MenuHighlight);
                using (SolidBrush brush = new SolidBrush(newColor))
                {
                    Rectangle borderRect = new Rectangle(2, 0, e.ClipRectangle.Width - 4, e.ClipRectangle.Height - 1);
                    e.Graphics.FillRectangle(brush, borderRect);
                    e.Graphics.DrawRectangle(SystemPens.MenuHighlight, borderRect);
                }
            }
            if(Image != null)
            {
                Rectangle destRect = new Rectangle(4, (e.ClipRectangle.Height - 16) / 2, 16, 16);
                e.Graphics.DrawImage(Image, destRect, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel);
            }
            using (SolidBrush brush = new SolidBrush(this.ForeColor))
            {
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                Rectangle textRect = new Rectangle(32, e.ClipRectangle.Y, e.ClipRectangle.Width - 32, e.ClipRectangle.Height);
                e.Graphics.DrawString(this.Text, this.Font, brush, textRect, format);
            }
        }
    }
}
