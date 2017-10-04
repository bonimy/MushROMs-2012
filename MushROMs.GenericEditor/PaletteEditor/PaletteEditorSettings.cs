using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNESLibrary;

namespace MushROMs.GenericEditor.PaletteEditor
{
    public unsafe partial class PaletteEditorSettings : Form
    {
        public int DefaultRows
        {
            get { return (int)this.nudRows.Value; }
            set { this.nudRows.Value = value; }
        }

        public int DefaultColumns
        {
            get { return (int)this.nudColumns.Value; }
            set { this.nudColumns.Value = value; }
        }

        public PaletteZoomScales DefaultZoomSize
        {
            get { return (PaletteZoomScales)((this.cbxZoom.SelectedIndex + 1) * 8); }
            set { this.cbxZoom.SelectedIndex = ((int)value / 8) - 1; }
        }

        public Color DefaultBackColor1
        {
            get { return this.cpkBackColor1.SelectedColor; }
            set { this.cpkBackColor2.SelectedColor = value; }
        }

        public Color DefaultBackColor2
        {
            get { return this.cpkBackColor2.SelectedColor; }
            set { this.cpkBackColor2.SelectedColor = value; }
        }

        public PaletteBGSizes DefaultBGSize
        {
            get { return (PaletteBGSizes)(1 << this.cbxBackZoom.SelectedIndex); }
            set { this.cbxBackZoom.SelectedIndex = (int)Math.Log((int)value, 2); }
        }

        public int DashLength1
        {
            get { return (int)this.nudDashLength1.Value; }
            set { this.nudDashLength1.Value = value; }
        }

        public Color DashColor1
        {
            get { return this.cpkDashColor1.SelectedColor; }
            set { this.cpkDashColor1.SelectedColor = value; }
        }

        public int DashLength2
        {
            get { return (int)this.nudDashLength2.Value; }
            set { this.nudDashLength2.Value = value; }
        }

        public Color DashColor2
        {
            get { return this.cpkDashColor2.SelectedColor; }
            set { this.cpkDashColor2.SelectedColor = value; }
        }

        public Cursor EditCursor
        {
            get
            {
                switch (this.cbxCursor.SelectedIndex)
                {
                    case 0:
                        return Cursors.Arrow;
                    case 1:
                        return Cursors.Cross;
                    default:
                        return Cursors.No;
                }
            }
            set
            {
                if (value == Cursors.Arrow)
                    this.cbxCursor.SelectedIndex = 0;
                else if (value == Cursors.Cross)
                    this.cbxCursor.SelectedIndex = 1;
            }
        }

        public PaletteEditorSettings()
        {
            InitializeComponent();

            this.cpkBackColor1.SelectedColor = Color.White;
            this.cpkBackColor2.SelectedColor = Color.Gray;

            this.cbxZoom.SelectedIndex = 3;
            this.cbxBackZoom.SelectedIndex = 3;
        }

        private void drwBGExample_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.White, this.drwBGExample.ClientRectangle);
            g.FillRectangle(Brushes.Black, new Rectangle(8 - 1, 8 - 1, 32 + 3, 32 + 3));

            const int width = 32;
            const int height = width;
            int bgSize = 1 << this.cbxBackZoom.SelectedIndex;
            uint bgColor1 = PaletteForm.SystemToPCColor(this.cpkBackColor1.SelectedColor);
            uint bgColor2 = PaletteForm.SystemToPCColor(this.cpkBackColor2.SelectedColor);

            uint[,] data = new uint[height, width];
            fixed (uint* scan0 = data)
            {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        data[y, x] = ((x & (int)bgSize) ^ (y & (int)bgSize)) == 0 ? bgColor1 : bgColor2;

                g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), new Point(8, 8));
            }
        }

        private void cpkBackColor_ColorValueChanged(object sender, EventArgs e)
        {
            this.drwBGExample.Invalidate();
        }

        private void cbxBackZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drwBGExample.Invalidate();
        }
    }
}