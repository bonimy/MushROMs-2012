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
    public unsafe partial class GFXEditor : EditorForm
    {
        private const ushort NotSet = 0xFFFF;
        private const int ObjectScreens = 5;
        private const int SpriteScreens = 4;
        private const int AnimationScreens = 4;
        private const int PlayerScreens = 4;
        private const int TotalScreens = ObjectScreens + SpriteScreens + AnimationScreens + PlayerScreens;
        private const int EditableScreens = ObjectScreens + SpriteScreens;

        private const int MaxColumns = 0x10;
        private const int MaxRows = 0x10;
        private const int Zoom = 2;
        private const int TilesPerScreen = 0x80;

        private GFX gfx;
        private SMB1Editor parent;

        private ushort[] DefaultIndexes;
        private ushort[,] AllIndexes;
        private ushort[] Indexes;
        private int paletteIndex;
        
        public GFX GFX
        {
            get { return this.gfx; }
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

        private string DefaultPath
        {
            get { return this.Parent.EditorDirectory + @"\GFX\"; }
        }

        private int Level
        {
            get { return this.parent.Level; }
        }

        private Palette Palette
        {
            get { return this.parent.Palette; }
        }

        public GFXEditor(SMB1Editor parent)
        {
            InitializeComponent();

            this.Parent = parent;
            this.drwGFX.ClientSize = new Size(MaxColumns * Zoom * GFX.TileWidth, MaxRows * Zoom * GFX.TileHeight);

            this.paletteIndex = 0;
            this.gfx = new GFX(TotalScreens * TilesPerScreen);
            this.DefaultIndexes = new ushort[TotalScreens];
            this.Indexes = new ushort[TotalScreens];
            this.AllIndexes = new ushort[EditableScreens, SMB1Editor.MaxLevels];
        }

        public void GetDefaultIndexes()
        {
            string path = this.DefaultPath + @"Tables\Static.bin";

            if (!File.Exists(path))
                return;

            byte[] data = File.ReadAllBytes(path);
            if (data.Length != TotalScreens * sizeof(ushort))
                return;

            fixed (byte* ptr = data)
            fixed (ushort* dest = this.DefaultIndexes)
            {
                ushort* src = (ushort*)ptr;
                for (int i = TotalScreens; --i >= 0; )
                    dest[i] = src[i];
            }

            for (int i = TotalScreens; --i >= EditableScreens; )
                this.Indexes[i] = this.DefaultIndexes[i];
        }

        public void GetAllIndexes()
        {
            string dir = this.DefaultPath + @"Tables\Object";
            for (int i = ObjectScreens; --i >= 0; )
            {
                string path = dir + (i + 1).ToString() + ".bin";
                if (!File.Exists(path))
                    return;

                byte[] data = File.ReadAllBytes(path);
                if (data.Length != SMB1Editor.MaxLevels * sizeof(ushort))
                    return;

                fixed (byte* ptr = data)
                fixed (ushort* dest = &this.AllIndexes[i, 0])
                {
                    ushort* src = (ushort*)ptr;
                    for (int j = SMB1Editor.MaxLevels; --j >= 0; )
                        dest[j] = src[j];
                }
            }

            dir = this.DefaultPath + @"Tables\Sprite";
            for (int i = SpriteScreens; --i >= 0; )
            {
                string path = dir + (i + 1).ToString() + ".bin";
                if (!File.Exists(path))
                    return;

                byte[] data = File.ReadAllBytes(path);
                if (data.Length != SMB1Editor.MaxLevels * sizeof(ushort))
                    return;

                fixed (byte* ptr = data)
                fixed (ushort* dest = &this.AllIndexes[i + ObjectScreens, 0])
                {
                    ushort* src = (ushort*)ptr;
                    for (int j = SMB1Editor.MaxLevels; --j >= 0; )
                        dest[j] = src[j];
                }
            }
        }

        public void LoadGFX()
        {
            GetCurrentIndexes();
            for (int i = TotalScreens; --i >= 0; )
            {
                string path = this.DefaultPath + @"Default\GFX" + this.Indexes[i].ToString("X2") + ".lz2";

                if (!File.Exists(path))
                    return;
                LC.OpenRAMFile(path);
                byte[] data = LC.Decompress(CompressionFormats.LZ2);
                if (data == null)
                    return;
                if (GFX.GetDataSize(GraphicsTypes.SNES_4BPP, TilesPerScreen) != data.Length)
                    return;

                this.gfx.InsertData(data, GraphicsTypes.SNES_4BPP, 0, i * TilesPerScreen, TilesPerScreen);
            }
        }

        private void GetCurrentIndexes()
        {
            for (int i = EditableScreens; --i >= 0; )
            {
                this.Indexes[i] = this.AllIndexes[i, this.Level];
                if (this.Indexes[i] == NotSet)
                    this.Indexes[i] = this.DefaultIndexes[i];
            }
        }

        public void Redraw()
        {
            this.drwGFX.Invalidate();
        }

        private void drwGFX_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            const int width = MaxColumns * Zoom * GFX.TileWidth;
            const int height = MaxRows * Zoom * GFX.TileHeight;
            const int w = MaxColumns * GFX.TileSize;
            const int h = GFX.TileSize * (MaxColumns - 1);

            uint* scan0 = stackalloc uint[height * width];
            byte* gfx = this.gfx.Pixels;
            uint* palette = this.Palette.Colors + (this.paletteIndex << (int)(GraphicsTypes.SNES_4BPP));
            uint* dest = scan0;

            for (int row = MaxRows; --row >= 0; gfx += h)
                for (int y = GFX.TileHeight; --y >= 0; gfx += GFX.TileWidth)
                    for (int j = Zoom; --j >= 0; gfx -= w)
                        for (int column = MaxColumns; --column >= 0; gfx += GFX.TileSize)
                            for (int x = 0; x < GFX.TileWidth; ++x)
                                for (int i = Zoom; --i >= 0; ++dest)
                                    *dest = palette[gfx[x]];

            g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), Point.Empty);
        }

        private void GFXEditor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    --this.paletteIndex;
                    this.paletteIndex &= 0x07;
                    Redraw();
                    break;
                case Keys.Down:
                    ++this.paletteIndex;
                    this.paletteIndex &= 0x07;
                    Redraw();
                    break;
            }
        }
    }
}