using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNESLibrary;

namespace MushROMs.SMB1
{
    public unsafe partial class PaletteEditor : EditorForm
    {
        private const string DefaultPath = @"\Palette\Default\Palette";
        private const string DefaultExt = ".bin";

        private const string InvalidSize = "Palette data is an invalid size and could not be read.";
        private const string InvalidNumbers = "Palette file has an invalid number of colors: ";
        private const string PaletteLoadError = "Palette load error";

        private const int MaxColumns = 0x10;
        private const int MaxRows = 0x10;
        private const int NumColors = MaxRows * MaxColumns;
        private const PaletteDataFormats DefaultFormat = PaletteDataFormats.BIN;
        private const int FallbackColor = 0xF800F8;     //Magenta
        private const int DefaultZoom = 0x10;

        private Palette palette;
        private SMB1Editor parent;
        private string path;
        private int zoom;

        public Palette Palette
        {
            get { return this.palette; }
        }

        public new SMB1Editor Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
                value.AddOwnedForm(this);
            }
        }

        private int Level
        {
            get { return this.parent.Level; }
        }

        private int ZoomX
        {
            get { return this.zoom; }
        }

        private int ZoomY
        {
            get { return this.zoom; }
        }

        public PaletteEditor(SMB1Editor parent)
        {
            InitializeComponent();

            this.Parent = parent;

            this.tsm8x.Tag = 8;
            this.tsm16x.Tag = 16;
            this.tsm24x.Tag = 24;
            this.tsm32x.Tag = 32;

            this.zoom = DefaultZoom;
        }

        public void LoadPalette()
        {
            string path = this.Parent.EditorDirectory + DefaultPath + this.Level.ToString("X2") + DefaultExt;
            if (!File.Exists(path))
            {
                this.palette = new Palette(NumColors, FallbackColor);
                return;
            }

            byte[] data = File.ReadAllBytes(path);
            if (!Palette.IsValidSize(DefaultFormat, data.Length))
            {
                MessageBox.Show(InvalidSize, PaletteLoadError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.palette = new Palette(NumColors, FallbackColor);
                return;
            }

            int num = Palette.GetNumColors(DefaultFormat, data.Length);
            if (num != NumColors)
            {
                MessageBox.Show(InvalidNumbers + num.ToString() + '/' + NumColors.ToString(), PaletteLoadError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.palette = new Palette(NumColors, FallbackColor);
                return;
            }

            this.palette = new Palette(data, DefaultFormat, 0, NumColors);
            this.path = path;
            Redraw();
        }

        public void Redraw()
        {
            this.drwPalette.Invalidate();
        }

        private void drwPalette_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int width = this.drwPalette.ClientWidth;
            int height = this.drwPalette.ClientHeight;
            int i1 = width * (ZoomY - 1);
            int i2 = ZoomX - (width * ZoomY);

            uint[,] data = new uint[height, width];
            fixed (uint* scan0 = data)
            {
                uint* src = this.palette.Colors;
                uint* dest = scan0;

                for (int y = MaxRows; --y >= 0; dest += i1, src += MaxColumns)
                    for (int x = 0; x < MaxColumns; ++x, dest += i2)
                        for (int i = ZoomY; --i >= 0; dest += width)
                            for (int j = ZoomX; --j >= 0; )
                                dest[j] = src[x];

                g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), Point.Empty);
            }
        }

        private void PaletteEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Parent.ShowPaletteEditor = false;
            }
        }

        public void Gradient(int i1, int i2)
        {
            int min = i1 < i2 ? i1 : i2;
            int max = i1 > i2 ? i1 : i2;
            int delta = max - min;
            if (delta < 2)
                return;

            uint* colors = this.palette.Colors;

            uint c1 = colors[min];
            int r1 = (int)(c1 >> 0x10) & 0xFF;
            int g1 = (int)(c1 >> 8) & 0xFF;
            int b1 = (int)(c1 & 0xFF);

            uint c2 = colors[max];
            int r2 = (int)(c2 >> 0x10) & 0xFF;
            int g2 = (int)(c2 >> 8) & 0xFF;
            int b2 = (int)(c2 & 0xFF);

            for (int i = delta; i >= 0; --i)
            {
                int r = ((r1 * (delta - i)) + (r2 * i)) / delta;
                int g = ((g1 * (delta - i)) + (g2 * i)) / delta;
                int b = ((b1 * (delta - i)) + (b2 * i)) / delta;

                r += 4;
                g += 4;
                b += 4;

                r = r > 0xFF ? 0xF8 : (r & 0xF8);
                g = g > 0xFF ? 0xF8 : (g & 0xF8);
                b = b > 0xFF ? 0xF8 : (b & 0xF8);

                colors[min + i] = (uint)((r << 0x10) | (g << 8) | b);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmZoom_Click(object sender, EventArgs e)
        {
            tsm24x.Checked =
            tsm8x.Checked =
            tsm16x.Checked =
            tsm32x.Checked = false;

            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            tsm.Checked = true;

            this.zoom = (int)(tsm).Tag;
            this.drwPalette.ClientSize = new Size(MaxColumns * this.ZoomX, MaxRows * this.ZoomY);
            Redraw();
        }
    }
}