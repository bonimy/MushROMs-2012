using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MushROMs.LunarCompress;
using MushROMs.Controls;
using MushROMs.SNESLibrary;
using MushROMs.GenericEditor.PaletteEditor;

namespace MushROMs.GenericEditor
{
    public unsafe partial class GFXEditor_old : EditorForm
    {
        public const int MaxColumns = 0x10;
        public const int MaxRows = 0x10;
        public const int MaxTiles = MaxRows * MaxColumns;
        public const GraphicsTypes DefaultGraphicsType = GraphicsTypes.SNES_4BPP;
        public const PaletteZoomScales DefaultZoom = PaletteZoomScales.Zoom32x;

        //[Description("Occurs when the selected coordinates in the control changes.")]
        //public event CoordinatesEventHandler CoordinatesChanged;

        private GFX gfx;

        [Browsable(false)]
        public GFX GFX
        {
            get { return this.gfx; }
        }

        public GFXEditor_old(int numColors)
        {
            InitializeComponent();

            this.gfx = new GFX(numColors);
        }

        private void drwGFX_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int width = this.drwGFX.ClientWidth;
            int height = this.drwGFX.ClientHeight;

            uint[,] data = new uint[height, width];
            fixed (uint* scan0 = data)
            {
                for (int y = height; --y >= 0; )
                    for (int x = width; --x >= 0; )
                        data[y, x] = (uint)((x ^ y) << (x ^ y));

                g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)scan0), Point.Empty);
            }
        }
    }

    public enum GFXZoomScales
    {
        Zoom1x = 1,
        Zoom2x = 2,
        Zoom3x = 3,
        Zoom4x = 4
    }
}