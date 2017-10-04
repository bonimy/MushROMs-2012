using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MushROMs.LunarCompress;
using MushROMs.Controls;
using MushROMs.SNESLibrary;
using MushROMs.SMB1.Level;

namespace MushROMs.SMB1
{
    public unsafe partial class ObjectSelector : EditorForm
    {
        private const int MaxColumns = 0x10;
        private const int MaxRows = 0x10;
        private const int TileWidth = Map16.TileWidth;
        private const int TileHeight = Map16.TileHeight;
        private const int TileSize = TileHeight * TileWidth;

        private LevelElements.Object select;
        private Map map;
        private int[] indexes;
        private int type;
        private SMB1Editor parent;

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

        private Palette Palette
        {
            get { return this.parent.Palette; }
        }

        private GFX GFX
        {
            get { return this.parent.GFX; }
        }

        private Map16 Map16
        {
            get { return this.parent.Map16; }
        }

        private LevelType LevelType
        {
            get { return this.parent.LevelType; }
        }

        public ObjectSelector(SMB1Editor parent)
        {
            InitializeComponent();

            this.Parent = parent;

            this.map = new Map(MaxColumns, MaxRows);
            this.indexes = new int[this.cbxObjectType.Items.Count];
            for (int i = indexes.Length; --i >= 0; )
                this.indexes[i] = 0;
            this.cbxObjectType.SelectedIndex = 0;
            this.lbxObject.SelectedIndex = 0;
        }

        private void WriteObject()
        {
            if (this.cbxObjectType.SelectedIndex == -1 || this.lbxObject.SelectedIndex == -1)
                return;

            int value = this.lbxObject.SelectedIndex;
            ushort* tiles = this.map.Tiles;

            for (int i = this.map.Size; --i >= 0; )
                tiles[i] = Map.NotSet;

            switch (cbxObjectType.SelectedIndex)
            {
                case 0:
                    if (value < 0x0C)
                    {
                        this.select.Data = ObjectType.SingleTile;
                        this.select.Value = value;
                    }
                    else
                    {
                        this.select.Data = ObjectType.StaticObject;
                        this.select.Value = value - 0x0C;
                    }
                    break;
                case 1:
                    {
                        int index = lbxObject.SelectedIndex * 0x100;
                        for (int i = 0x100; --i >= 0; )
                            tiles[i] = (ushort)(index + i);
                    }
                    Redraw();
                    return;
                case 2:
                    if (value < 3)
                    {
                        this.select.Data = ObjectType.Vertical;
                        this.select.Value = value;
                        this.select.Height = 5;
                    }
                    else
                    {
                        this.select.Data = ObjectType.Horizontal;
                        this.select.Value = value - 3;
                        this.select.Width = 5;
                    }
                    break;
                case 3:
                    if (value != 0x0F)
                    {
                        this.select.Data = ObjectType.Rectangular;
                        this.select.Value = value;
                        this.select.Width = 5;
                        this.select.Height = 5;
                    }
                    else
                    {
                        this.select.Data = ObjectType.LongHorizontal;
                        this.select.Value = 0;
                        this.select.Width = 0x10;
                    }
                    break;
                case 4:
                    this.select.Data = ObjectType.GroundObject;
                    this.select.Value = value;
                    this.select.Width = 5;
                    this.select.Height = 5;
                    break;
                case 5:
                    if (value < 2)
                    {
                        this.select.Data = ObjectType.HorizontalExtra;
                        this.select.Value = value;
                        this.select.Width = 5;
                    }
                    else if (value < 5)
                    {
                        this.select.Data = ObjectType.VerticalExtra;
                        this.select.Value = value - 2;
                        this.select.Height = 5;
                    }
                    else
                    {
                        this.select.Data = ObjectType.Staircase;
                        this.select.Value = 0;
                        this.select.Width = 9;
                    }
                    break;
                case 6:
                    this.select.Data = ObjectType.CastleTileset;
                    this.select.Value = value;
                    this.select.Height = 5;
                    break;
                case 7:
                    this.select.Data = ObjectType.StaticObjectExtra;
                    this.select.Value = value;
                    break;
            }
            //this.select.WriteObject(tiles, ObjectSelector.TileWidth, ObjectSelector.TileHeight, this.LevelType, null);
            Redraw();
        }

        public void Redraw()
        {
            this.drwObject.Invalidate();
        }

        private void drwObject_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            const int width = MaxColumns * TileWidth;
            const int height = MaxRows * TileHeight;

            uint* scan0 = stackalloc uint[height * width];
            ushort* tiles = this.map.Tiles;
            ushort** map16 = this.Map16.Tiles;
            byte* gfx = this.GFX.Pixels;
            uint* palette = this.Palette.Colors;

            for (int y = height, i = TileSize; (y -= TileHeight) >= 0; )
            {
                for (int x = width; (x -= TileWidth) >= 0; )
                {
                    ushort tile = tiles[--i];
                    if (tile != Map.NotSet)
                    {
                        LC.Render8x8(scan0, width, height, x, y, gfx, palette, map16[tile][0], Render8x8Flags.Draw);
                        LC.Render8x8(scan0, width, height, x, y + Map8.TileHeight, gfx, palette, map16[tile][1], Render8x8Flags.Draw);
                        LC.Render8x8(scan0, width, height, x + Map8.TileWidth, y, gfx, palette, map16[tile][2], Render8x8Flags.Draw);
                        LC.Render8x8(scan0, width, height, x + Map8.TileWidth, y + Map8.TileHeight, gfx, palette, map16[tile][3], Render8x8Flags.Draw);
                    }
                }
            }

            g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), Point.Empty);
        }

        private void cbxObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.indexes[this.type] = this.lbxObject.SelectedIndex;
            this.type = this.cbxObjectType.SelectedIndex;
            this.lbxObject.Items.Clear();
            this.lbxObject.Items.AddRange(ObjectSelector.ListItems[this.cbxObjectType.SelectedIndex]);
            this.lbxObject.SelectedIndex = this.indexes[this.type];
            WriteObject();
        }

        private void lbxObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            WriteObject();
        }
    }
}