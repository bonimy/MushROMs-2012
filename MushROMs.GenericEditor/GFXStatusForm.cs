using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.LunarCompress;
using MushROMs.SNESLibrary;
using MushROMs.GenericEditor.Properties;

namespace MushROMs.GenericEditor
{
    public unsafe partial class GFXStatusForm : MenulessForm
    {
        private const int TileZoom = 1 << 4;
        const int PaletteWidth = 1 << 7;
        const int PaletteHeight = PaletteWidth;

        Palette palette;
        GFX gfx;
        GraphicsTypes gfxType;

        private DrawControl[] drwColors;
        private int[] indexes;

        private int dash;
        private double dashTimer;
        private System.Timers.Timer animator;

        [Browsable(false)]
        private int DashOffset
        {
            get
            {
                return this.dash;
            }
            set
            {
                this.dash = value;
                this.dashTimer %= SNESEditor.DefaultDashWait;
            }
        }

        public GFXStatusForm()
        {
            InitializeComponent();

            this.palette = new Palette(0x100);
            this.gfx = new GFX(1);
            this.gfxType = GraphicsTypes.SNES_4BPP;

            this.indexes = new int[6];
            this.drwColors = new DrawControl[6];

            this.drwColors[0] = this.drwColor1;
            this.drwColors[1] = this.drwColor2;
            this.drwColors[2] = this.drwColor3;
            this.drwColors[3] = this.drwColor4;
            this.drwColors[4] = this.drwColor5;
            this.drwColors[5] = this.drwColor6;

            for (int i = this.indexes.Length; --i >= 0; )
                this.drwColors[i].Tag = this.indexes[i] = i;

            this.dashTimer = 0;
            this.animator = new System.Timers.Timer();
            this.animator.Interval = 1000 / 60;
            this.animator.Elapsed += new System.Timers.ElapsedEventHandler(animator_Elapsed);
            this.animator.Enabled = true;
        }

        private int GetPaletteIndex()
        {
            return GetPaletteIndex(0, 0);
        }

        private int GetPaletteIndex(int x, int y)
        {
            int bpp = (int)this.gfxType & 0x0F;
            int columns = 1 << ((bpp + 1) >> 1);
            int rows = 1 << (bpp >> 1);

            return (rows * y) + x;
        }

        private void animator_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.dashTimer += ((System.Timers.Timer)sender).Interval;
            if (this.dashTimer > SNESEditor.DefaultDashWait)
                this.DashOffset += 1;
        }

        private void drwPalette_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int bpp = (int)this.gfxType & 0x0F;
            int zoomX = PaletteWidth >> ((bpp + 1) >> 1);
            int zoomY = PaletteHeight >> (bpp >> 1);
            int columns = 1 << ((bpp + 1) >> 1);
            int rows = 1 << (bpp >> 1);

            int i1 = PaletteWidth * (zoomY - 1);
            int i2 = zoomX - (PaletteWidth * zoomY);

            uint* scan0 = stackalloc uint[PaletteHeight * PaletteWidth];
            uint* src = this.palette.Colors;
            uint* dest = scan0;
            for (int y = rows, colors = this.palette.NumColors; --y >= 0 && colors >= 0; dest += i1, src += columns)
                for (int x = 0; x < columns && --colors >= 0; ++x, dest += i2)
                    for (int i = zoomY; --i >= 0; dest += PaletteWidth)
                        for (int j = zoomX; --j >= 0; )
                            dest[j] = src[x];

            g.DrawImageUnscaled(new Bitmap(PaletteWidth, PaletteHeight, PaletteWidth * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), Point.Empty);

            const float DashLength = 4;
            Pen p1 = new Pen(Color.Black, 1);
            p1.DashStyle = DashStyle.Custom;
            p1.DashPattern = new float[] { DashLength, DashLength };
            p1.DashOffset = this.DashOffset;

            Pen p2 = new Pen(Color.White, 1);
            p2.DashStyle = DashStyle.Custom;
            p2.DashPattern = new float[] { DashLength, DashLength };
            p2.DashOffset = this.DashOffset + DashLength;
        }

        private void drwTile_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            const int width = GFX.TileWidth * TileZoom;
            const int height = GFX.TileHeight * TileZoom;

            uint* scan0 = stackalloc uint[height * width];
            byte** gfx = *this.gfx.Tiles;
            uint* palette = this.palette.Colors;

            for (int y = 0; y < GFX.TileHeight; y++)
            {
                uint* pixels = scan0 + (GFX.TileWidth * TileZoom * TileZoom * y);
                for (int x = 0; x < GFX.TileWidth; x++, pixels += TileZoom)
                    for (int i = TileZoom; --i >= 0; )
                        for (int j = TileZoom; --j >= 0; )
                            pixels[(i * width) + j] = palette[gfx[y][x]];
            }

            g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), Point.Empty);

            const float DashLength = 4;
            Pen p1 = new Pen(Color.Black, 1);
            p1.DashStyle = DashStyle.Custom;
            p1.DashPattern = new float[] { DashLength, DashLength };
            p1.DashOffset = this.DashOffset;

            Pen p2 = new Pen(Color.White, 1);
            p2.DashStyle = DashStyle.Custom;
            p2.DashPattern = new float[] { DashLength, DashLength };
            p2.DashOffset = this.DashOffset + DashLength;
        }

        private void drwColor_Paint(object sender, PaintEventArgs e)
        {
            DrawControl color = (DrawControl)sender;
            int index = this.indexes[(int)color.Tag];

            Graphics g = e.Graphics;

            int width = color.ClientWidth;
            int height = color.ClientHeight;

            uint[,] pixels = new uint[height, width];
            fixed (uint* scan0 = pixels)
            {
                for (int i = height * width; --i >= 0; )
                    scan0[i] = this.palette.Colors[index];
                g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), Point.Empty);
            }
        }

        private void drwPalette_MouseMove(object sender, MouseEventArgs e)
        {
            int bpp = (int)this.gfxType & 0x0F;
            int zoomX = PaletteWidth >> ((bpp + 1) >> 1);
            int zoomY = PaletteHeight >> (bpp >> 1);
        }

        private void drwTile_MouseMove(object sender, MouseEventArgs e)
        {
            drwTile_MouseClick(sender, e);
        }

        private void GFXStatusForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Shift && !e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                    case Keys.A:
                    case Keys.S:
                    case Keys.D:
                        {
                            int x = 0;
                            int y = 0;

                            int bpp = (int)this.gfxType & 0x0F;
                            int columns = 1 << ((bpp + 1) >> 1);
                            int rows = 1 << (bpp >> 1);

                            switch (e.KeyCode)
                            {
                                case Keys.A:
                                    if (x > 0)
                                        x--;
                                    break;
                                case Keys.D:
                                    if (x + 1 < columns)
                                        x++;
                                    break;
                                case Keys.W:
                                    if (y > 0)
                                        y--;
                                    break;
                                case Keys.S:
                                    if (y + 1 < columns)
                                        y++;
                                    break;
                            }

                        }
                        return;
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Down:
                    case Keys.Up:
                        {
                            int x = 0;
                            int y = 0;

                            switch (e.KeyCode)
                            {
                                case Keys.Left:
                                    if (x > 0)
                                        x--;
                                    break;
                                case Keys.Right:
                                    if (x < GFX.TileWidth - 1)
                                        x++;
                                    break;
                                case Keys.Up:
                                    if (y > 0)
                                        y--;
                                    break;
                                case Keys.Down:
                                    if (y < GFX.TileHeight - 1)
                                        y++;
                                    break;
                            }

                        }
                        return;
                }
            }
        }

        private void drwTile_MouseClick(object sender, MouseEventArgs e)
        {


            this.drwTile.Invalidate();
        }

        private void drwPalette_MouseClick(object sender, MouseEventArgs e)
        {
            int index = 0;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    index = 0;
                    break;
                case MouseButtons.Right:
                    index = 1;
                    break;
                case MouseButtons.Middle:
                    index = 2;
                    break;
                case MouseButtons.XButton1:
                    index = 3;
                    break;
                case MouseButtons.XButton2:
                    index = 4;
                    break;
                default:
                    return;
            }

            this.indexes[index] = GetPaletteIndex();
            this.drwColors[index].Invalidate();
        }
    }
}