using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNESLibrary;
using MushROMs.LunarCompress;

namespace MushROMs.SMB1
{
    public unsafe partial class Map16Editor : EditorForm
    {
        private const int NumTiles = 0x1000;
        private const int MaxColumns = 0x10;
        private const int MaxRows = 0x10;
        private const int MaxTiles = MaxRows * MaxColumns;

        private Map16 map16;
        private ushort[] actsLike;
        private SMB1Editor parent;
        private uint backColor;

        public Map16 Map16
        {
            get { return this.map16; }
        }

        public ushort[] ActsLike
        {
            get { return this.actsLike; }
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

        public Palette Palette
        {
            get { return this.parent.Palette; }
        }

        public GFX GFX
        {
            get { return this.parent.GFX; }
        }

        public string DefaultPath
        {
            get { return this.Parent.EditorDirectory + @"\Map16\Default\"; }
        }

        private int Level
        {
            get { return this.parent.Level; }
        }

        public Map16Editor(SMB1Editor parent)
        {
            InitializeComponent();

            this.Parent = parent;
            this.drwMap16.ClientSize = new Size(Map16.TileWidth * MaxColumns, Map16.TileHeight * MaxRows);
            this.backColor = Palette.SystemToPCColor(Color.Gray);
            this.actsLike = new ushort[Map16Editor.NumTiles];
        }

        public void LoadMap16()
        {
            string path = this.DefaultPath + "Map16G.bin";
            if (!File.Exists(path))
                return;
            byte[] data = File.ReadAllBytes(path);
            if (Map16.GetDataSize(NumTiles) != data.Length)
                return;

            this.map16 = new Map16(data, 0, NumTiles);

            path = this.DefaultPath + "Map16.bin";
            if (!File.Exists(path))
                return;
            data = File.ReadAllBytes(path);
            if (data.Length != Map16Editor.NumTiles * sizeof(ushort))
                return;

            fixed (byte* ptr = data)
            fixed (ushort* dest = this.actsLike)
            {
                ushort* src = (ushort*)ptr;
                for (int i = Map16Editor.NumTiles; --i >= 0; )
                    dest[i] = src[i];
            }
        }

        public void Redraw()
        {
            this.drwMap16.Invalidate();
        }

        private void drwMap16_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            const int width = MaxColumns * Map16.TileWidth;
            const int height = MaxRows * Map16.TileHeight;

            uint* scan0 = stackalloc uint[height * width];
            ushort** map16 = this.map16.Tiles;
            byte* gfx = this.GFX.Pixels;
            uint* palette = this.Palette.Colors;

            for (int i = height * width; --i >= 0; )
                scan0[i] = this.backColor;

            for (int y = height, i = MaxTiles; (y -= Map16.TileHeight) >= 0; )
            {
                for (int x = width; (x -= Map16.TileWidth) >= 0; )
                {
                    LC.Render8x8(scan0, width, height, x, y, gfx, palette, map16[--i][0], Render8x8Flags.Draw);
                    LC.Render8x8(scan0, width, height, x, y + Map8.TileHeight, gfx, palette, map16[i][1], Render8x8Flags.Draw);
                    LC.Render8x8(scan0, width, height, x + Map8.TileWidth, y, gfx, palette, map16[i][2], Render8x8Flags.Draw);
                    LC.Render8x8(scan0, width, height, x + Map8.TileWidth, y + Map8.TileHeight, gfx, palette, map16[i][3], Render8x8Flags.Draw);
                }
            }

            g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, new IntPtr(scan0)), Point.Empty);
        }
    }
}