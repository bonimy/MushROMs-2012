using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.LunarCompress;
using MushROMs.Unmanaged;
using MushROMs.GenericEditor.Properties;

namespace MushROMs.GenericEditor.GFXEditor
{
    public unsafe partial class GFXForm : EditorForm
    {
        #region Constant and readonly fields
        #region Default properties
        private const int MinColumns = 4;
        private const int MinRows = 4;
        private const int MaxColumns = 0x40;
        private const int MaxRows = 0x40;

        public const int SNESPaletteWidth = SNESEditor.PaletteWidth;
        public const int SNESPaletteHeight = SNESEditor.PaletteHeight;
        public const int SNESPaletteSize = SNESEditor.PaletteSize;

        public const int GFXTileWidth = SNESEditor.GFXTileWidth;
        public const int GFXTileHeight = SNESEditor.GFXTileHeight;
        public const int GFXTileSize = SNESEditor.GFXTileSize;
        #endregion

        #region File extension strings
        public const string ExtensionBIN = SNESEditor.ExtensionBIN;
        public const string ExtensionS9X_0to9 = SNESEditor.ExtensionS9X_0to9;
        public const string ExtensionZST = SNESEditor.ExtensionZST;
        public const string ExtensionZST_0to9 = SNESEditor.ExtensionZST_0to9;
        public const string ExtensionZST_10to99 = SNESEditor.ExtensionZST_10to99;
        public const string AltExtensionZST = SNESEditor.AltExtensionZST;
        public const string ExtensionSMC = SNESEditor.ExtensionSMC;
        public const string ExtensionSFC = SNESEditor.ExtensionSFC;
        public const string ExtensionSWC = SNESEditor.ExtensionSWC;
        public const string ExtensionFIG = SNESEditor.ExtensionFIG;
        #endregion

        #region Formatting info
        public const string UnsavedNotification = SNESEditor.UnsavedNotification;
        #endregion
        #endregion

        #region Variables
        #region File data
        private string fp;
        private string fn;
        private byte[] fData;

        private static int untitled = 1;
        #endregion

        #region GFX data
        private byte* bppPixels;
        private GraphicsTypes gfxType;

        private UndoRedo<CopyData> undoRedo;
        #endregion

        #region Image info
        private int dash;
        private EventWatch dashTime;

        private int tileDash;
        private int paletteDash;
        #endregion
        #endregion

        #region Accessors
        #region ParentInteraction
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new GFXParent MdiParent
        {
            get { return (GFXParent)base.MdiParent; }
            set { base.MdiParent = value; }
        }
        #endregion

        #region File
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FilePath
        {
            get { return this.fp; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FileName
        {
            get { return this.fn; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Unsaved
        {
            get { return this.undoRedo.Unsaved; }
        }
        #endregion

        #region GFX data
        public GraphicsTypes GraphicsType
        {
            get { return this.gfxType; }
        }

        public int TileDataSize
        {
            get { return this.DataLength * BPP(this.gfxType); }
        }
        #endregion

        public int Zoom
        {
            get { return this.edcGFX.ZoomWidth; }
        }

        public bool GFXEditorActive
        {
            get { return this.ActiveControl == this.spcMain && this.spcMain.ActiveControl == this.edcGFX; }
        }
        #endregion

        #region Initializers
        public GFXForm()
        {
            InitializeComponent();

            this.PrimaryEditor = this.edcGFX;

            this.undoRedo = new UndoRedo<CopyData>();
            /*
            this.undoRedo.Undo += new EventHandler(undoRedo_Undo);
            this.undoRedo.Redo += new EventHandler(undoRedo_Redo);
            this.undoRedo.LastUndo += new EventHandler(undoRedo_LastUndo);
            this.undoRedo.LastRedo += new EventHandler(undoRedo_LastRedo);
            */

            this.edcPalette.ResetData(sizeof(uint) * SNESPaletteSize);
            fixed (byte* ptr = Resources.DefaultPalette)
            {
                ushort* src = (ushort*)ptr;
                ushort* dest = (ushort*)this.edcPalette.Data;
                for (int i = SNESPaletteSize; --i >= 0; )
                    dest[i] = src[i];
            }
            this.edcPalette.ResetBitmapPixels();
            WritePalettePixelData();

            this.dashTime = new EventWatch(Program.DefaultDashWait);
            Program.Animator.AddWatch(ref this.dashTime);
            this.dashTime.Elapsed += new ElapsedEventHandler(dashTime_Elapsed);

            this.Text = "ERROR: GFX uninitialized.";
            this.Enabled = false;

            this.fData = File.ReadAllBytes(@"Z:\Libraries\Dropbox\Private\Emulation\Super Mario All-Stars\disassembly\a\b02\DATA_02C000.bin");
            CreateNew(GraphicsTypes.SNES_8BPP, 0x80);
            this.bppPixels = (byte*)this.edcGFX.Data;
            for (int i = 0x20 * 8 * 8; --i >= 0; )
                this.bppPixels[i] = this.fData[i + (0 * 0x80 * 8 * 8)];
            this.gfxType = GraphicsTypes.SNES_8BPP;
            WriteGFXPixelData();
        }

        public void CreateNew(GraphicsTypes gfxType, int numTiles)
        {
            CreateEmptyFileData();

            SetNumTiles(gfxType, numTiles);
            byte* tiles = GetTile();
            for (int i = GetDataSize(gfxType, numTiles); --i >= 0; )
                tiles[i] = 0;

            Initialize();
        }

        private void CreateEmptyFileData()
        {
            this.fp = string.Empty;
            //SetPaletteDataFormat(PaletteSettings.Default.DefaultFormat);
            this.fn = "Untitled" + (GFXForm.untitled++).ToString() + GFXForm.ExtensionBIN;
        }

        private void SetNumTiles(GraphicsTypes gfxType, int numTiles)
        {
            this.edcGFX.SetDataLength(numTiles);
            this.edcGFX.SetDataWidth(GFXSettings.Default.DefaultViewWidth);
            this.edcGFX.ResetData(sizeof(byte) * GetDataSize(gfxType, numTiles));
            this.edcGFX.StartIndex = 0;
        }

        private void Initialize()
        {
            this.Enabled = true;

            this.edcGFX.SetViewSize(GFXSettings.Default.DefaultViewWidth, GFXSettings.Default.DefaultViewHeight);
            this.edcGFX.ResetMarker();

            this.cbxGFXZoom.SelectedIndex = ((int)GFXSettings.Default.DefaultGFXZoom - 1);
            SetUnsavedText();
        }
        #endregion

        private void SetUnsavedText()
        {
            this.Text = this.fn + (this.Unsaved ? UnsavedNotification : string.Empty);
        }

        private byte* GetTile()
        {
            return GetTile(this.edcGFX.CurrentIndex, this.gfxType);
        }

        private byte* GetTile(int index)
        {
            return GetTile(index, this.gfxType);
        }

        private byte* GetTile(int index, GraphicsTypes gfxType)
        {
            return (byte*)this.edcGFX.Data + (8 * index * BPP(gfxType));
        }

        private void WriteGFXPixelData()
        {
            this.edcGFX.ResetBitmapPixels();

            int numColors = 1 << BPP(this.gfxType);
            IntPtr ptr_pixels = Pointer.CreatePointer(sizeof(byte) * this.DataLength * GFXTileSize);
            IntPtr ptr_colors = Pointer.CreatePointer(sizeof(uint) * numColors);
            ushort* palette = (ushort*)this.edcPalette.Data;
            uint* dest = (uint*)this.edcGFX.Scan0;

            byte* pixels = (byte*)ptr_pixels;
            uint* colors = (uint*)ptr_colors;
            LC.CreatePixelMap(this.bppPixels, pixels, this.DataLength, this.gfxType);
            for (int i = numColors; --i >= 0; )
                colors[i] = LC.SNEStoPCRGB(palette[i]);
            uint color = colors[*pixels];

            int width = this.edcGFX.ClientWidth;
            int cwidth = this.edcGFX.CellWidth;
            int cheight = this.edcGFX.CellHeight;
            int numTiles = this.DataLength - this.StartIndex;
            int zoom = this.Zoom;
            int i1 = width * (cheight - 1);
            int i2 = cheight * width - cwidth;
            int i3 = width * zoom - cwidth;
            int i4 = zoom * (width - 1);

            for (int y = 0; y < this.ViewHeight; y++, dest += i1)
                for (int x = 0; x < this.ViewWidth; x++, dest -= i2)
                    for (int h = 0; h < GFXTileHeight; h++, dest += i3)
                        for (int w = 0; w < GFXTileWidth; w++, dest -= i4, color = colors[*(++pixels)])
                            for (int i = zoom; --i >= 0; dest += width)
                                for (int j = 0; j < zoom; j++)
                                    dest[j] = color;

            this.edcGFX.WriteBitmapPixels();

            Pointer.FreePointer(ptr_colors);
            Pointer.FreePointer(ptr_pixels);
        }

        public void WritePalettePixelData()
        {
            ushort* src = (ushort*)this.edcPalette.Data;
            uint* dest = (uint*)this.edcPalette.Scan0;
            uint color = LC.SNEStoPCRGB(*src);

            int rows = this.edcPalette.ViewHeight;
            int columns = this.edcPalette.ViewWidth;
            int zoomW = this.edcPalette.ZoomWidth;
            int zoomH = this.edcPalette.ZoomHeight;
            int width = this.edcPalette.ClientWidth;
            int height = this.edcPalette.ClientHeight;
            int i1 = width * (zoomW - 1);
            int i2 = zoomW * (width - 1);

            for (int y = rows; --y >= 0; dest += i1, src += columns)
                for (int x = 0; x < columns; dest -= i2, color = LC.SNEStoPCRGB(src[++x]))
                    for (int i = zoomH; --i >= 0; dest += width)
                        for (int j = zoomW; --j >= 0; )
                            dest[j] = color;

            this.edcPalette.WriteBitmapPixels();
        }

        private void cbxGFXZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int zoom = this.cbxGFXZoom.SelectedIndex + 1;
            int w = this.edcGFX.PixelWidth * this.edcGFX.ViewWidth * zoom;
            int h = this.edcGFX.PixelHeight * this.edcGFX.ViewHeight * zoom;
            int deltaW = w - this.edcGFX.ClientWidth;
            int deltaH = h - this.edcGFX.ClientHeight;
            this.Size = new Size(this.Width + deltaW, this.Height + deltaH);
            this.edcGFX.ZoomSize = new Size(zoom, zoom);
            WriteGFXPixelData();
        }

        private void cbxTileZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int zoom = 8 * (this.cbxTileZoom.SelectedIndex + 1);
            int w = this.edcTile.PixelWidth * this.edcTile.ViewWidth * zoom;
            int h = this.edcTile.PixelHeight * this.edcTile.ViewHeight * zoom;
            int deltaW = w - this.edcTile.ClientWidth;
            int deltaH = h - this.edcTile.ClientHeight;
            this.spcMain.FixedPanel = FixedPanel.Panel1;
            this.Size = new Size(this.Width + deltaW, this.Height + deltaH);
            this.edcTile.ZoomSize = new Size(zoom, zoom);
            this.spcMain.FixedPanel = FixedPanel.Panel2;
            WriteTilePixelData();
        }

        private void dashTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.GFXEditorActive)
            {
                this.dash += 1;
                this.edcGFX.Invalidate();
            }
        }

        #region Static methods
        public static int BPP(GraphicsTypes gfxType)
        {
            return (int)gfxType & 0x0F;
        }

        public static bool IsValidSize(GraphicsTypes gfxType, int size)
        {
            return (size & BPP(gfxType)) == 0;
        }

        public static int GetNumTiles(GraphicsTypes gfxType, int size)
        {
            return size / BPP(gfxType);
        }

        public static int GetDataSize(GraphicsTypes gfxType, int numTiles)
        {
            return numTiles * GFXTileHeight * BPP(gfxType);
        }
        #endregion

        private struct CopyData
        {

        }

        private void edcGFX_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            float DashLength1 = GFXSettings.Default.DefaultDashLength1;
            float DashLength2 = GFXSettings.Default.DefaultDashLength2;
            Pen p1 = new Pen(Color.FromArgb(0xFF, GFXSettings.Default.DefaultDashColor1), 1);
            p1.DashStyle = DashStyle.Dot;
            p1.DashOffset = this.dash;

            Pen p2 = new Pen(Color.FromArgb(0xFF, GFXSettings.Default.DefaultDashColor2), 1);
            p2.DashStyle = DashStyle.Dot;
            p2.DashOffset = 1 + this.dash;

            int zoom = this.Zoom * this.edcGFX.PixelWidth;

            Rectangle r = new Rectangle(zoom * this.CurrentX + 2, zoom * this.CurrentY + 2, zoom - 5, zoom - 5);
            if (this.CurrentIndex != this.PreviousIndex)
            {
                g.DrawRectangle(p1, r);
                g.DrawRectangle(p2, r);
            }

            int width = this.SelectionWidth;
            int height = this.SelectionHeight;
            r = new Rectangle(this.MinX * zoom, this.MinY * zoom, (width * zoom) - 1, (height * zoom) - 1);

            p1.DashStyle = DashStyle.Custom;
            p1.DashPattern = new float[] { DashLength1, DashLength2 };
            p1.DashOffset = this.dash;

            p2.DashStyle = DashStyle.Custom;
            p2.DashPattern = new float[] { DashLength2, DashLength1 };
            p2.DashOffset = this.dash - DashLength1;

            g.DrawRectangle(p1, r);
            g.DrawRectangle(p2, r);
        }

        private void edcGFX_SizeChanged(object sender, EventArgs e)
        {
            WriteGFXPixelData();
        }

        private void edcTile_SelectedCellMouseClick(object sender, MouseEventArgs e)
        {
        }

        private void WriteTilePixelData()
        {
            this.edcTile.ResetBitmapPixels();

            int numColors = 1 << BPP(this.gfxType);
            IntPtr ptr_pixels = Pointer.CreatePointer(sizeof(byte) * GFXTileSize);
            IntPtr ptr_colors = Pointer.CreatePointer(sizeof(uint) * numColors);
            ushort* palette = (ushort*)this.edcPalette.Data;
            uint* dest = (uint*)this.edcTile.Scan0;

            byte* pixels = (byte*)ptr_pixels;
            uint* colors = (uint*)ptr_colors;
            LC.CreatePixelMap((byte*)this.edcTile.Data, pixels, 1, this.gfxType);
            for (int i = numColors; --i >= 0; )
                colors[i] = LC.SNEStoPCRGB(palette[i]);
            uint color = colors[*pixels];

            int width = this.edcTile.ClientWidth;
            int cwidth = this.edcTile.CellWidth;
            int cheight = this.edcTile.CellHeight;
            int zoom = this.edcTile.ZoomWidth;
            int i3 = width * (zoom - 1);
            int i4 = zoom * (width - 1);

            for (int h = 0; h < GFXTileHeight; h++, dest += i3)
                for (int w = 0; w < GFXTileWidth; w++, dest -= i4, color = colors[*(++pixels)])
                    for (int i = zoom; --i >= 0; dest += width)
                        for (int j = 0; j < zoom; j++)
                            dest[j] = color;

            this.edcTile.WriteBitmapPixels();

            Pointer.FreePointer(ptr_colors);
            Pointer.FreePointer(ptr_pixels);
        }

        private void edcGFX_SelectedCellMouseClick(object sender, MouseEventArgs e)
        {
            this.edcTile.ResetData(sizeof(byte) * GFXTileSize);
            byte* src = GetTile();
            byte* dest = (byte*)this.edcTile.Data;
            for (int i = GFXTileSize; --i >= 0; )
                dest[i] = src[i];
            WriteTilePixelData();
            this.edcTile.Invalidate();
        }
    }

    public enum GFXZoomScales
    {
        Zoom1x = 1,
        Zoom2x = 2,
        Zoom3x = 3,
        Zoom4x = 4
    }

    public enum GFXTileZoomScales
    {
        Zoom8x = 8,
        Zoom16x = 16,
        Zoom24x = 24,
        Zoom32x = 32
    }
}
