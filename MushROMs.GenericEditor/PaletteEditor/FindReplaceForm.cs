using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using MushROMs.SNESLibrary;

namespace MushROMs.GenericEditor.PaletteEditor
{
    public unsafe partial class FindReplaceForm : Form
    {
        private const int MaxSearchSize = 0x10;
        private const int Zoom = (int)PaletteZoomScales.Zoom16x;

        public event EventHandler FindNext;

        private uint[] colors;

        public uint[] Colors
        {
            get { return this.colors; }
        }

        public int SearchSize
        {
            get { return (int)this.nudNumColors.Value; }
            set { this.nudNumColors.Value = value; this.drwColors.Invalidate(); }
        }

        public FindDirections FindDirection
        {
            get
            {
                if (this.rdbUp.Checked)
                    return FindDirections.Up;
                else if (this.rdbDown.Checked)
                    return FindDirections.Down;
                else
                {
                    this.rdbDown.Checked = true;
                    return FindDirections.Down;
                }
            }
            set
            {
                if (value == FindDirections.Up)
                    this.rdbUp.Checked = true;
                else if (value == FindDirections.Down)
                    this.rdbDown.Checked = true;
                else throw new InvalidEnumArgumentException();
            }
        }

        public FindReplaceForm()
        {
            InitializeComponent();
            //this.drwColors.ClientSize = new Size(MaxSearchSize * Zoom, Zoom);
            this.colors = new uint[MaxSearchSize];
            for (int i = MaxSearchSize; --i >= 0; )
                this.colors[i] = 0;
        }

        private string DumpString(uint color)
        {
            return (color >> 3).ToString("x6");
        }

        protected virtual void OnFindNext(EventArgs e)
        {
            if (FindNext != null)
                FindNext(this, e);
        }

        private void nudNumColors_ValueChanged(object sender, EventArgs e)
        {
            this.drwColors.Invalidate();
        }

        private void drwColors_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int width = this.drwColors.ClientWidth;
            int height = this.drwColors.ClientHeight;
            int i2 = Zoom * (width - 1);

            uint[,] data = new uint[height, width];
            fixed (uint* scan0 = data)
            {
                int bgSize = (int)PaletteSettings.Default.DefaultBGSize;
                uint bgColor1 = PaletteForm.SystemToPCColor(PaletteSettings.Default.DefaultBGColor1);
                uint bgColor2 = PaletteForm.SystemToPCColor(PaletteSettings.Default.DefaultBGColor2);

                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        data[y, x] = ((x & bgSize) ^ (y & bgSize)) == 0 ? bgColor1 : bgColor2;

                uint* dest = scan0;
                int numColors = SearchSize;

                for (int x = 0; x < numColors; ++x, dest -= i2)
                    for (int i = Zoom; --i >= 0; dest += width)
                        for (int j = Zoom; --j >= 0; )
                            dest[j] = this.colors[x];

                g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), Point.Empty);
            }
        }

        private void drwColors_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X / Zoom;
                if (x + 1 > this.nudNumColors.Value)
                    return;

                ColorDialog dlg = new ColorDialog();
                dlg.FullOpen = true;
                dlg.Color = PaletteForm.PCToSystemColor(this.colors[x]);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.colors[x] = PaletteForm.SystemToPCColor(dlg.Color);
                    this.drwColors.Invalidate();
                }
            }
        }

        private void FindReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            OnFindNext(EventArgs.Empty);
        }

        public enum FindDirections
        {
            Up,
            Down
        }
    }
}