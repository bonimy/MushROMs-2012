/* A Palette Editor for Super Nintendo graphics editing.
 * Built by spel werdz rite.
 * http://www.smwcentral.net/?p=profile&id=1191
 * 
 * Lunar Compress.dll built by FuSoYa, Defender of Realm.
 * Used with permission.
 * http://fusoya.eludevisibility.org/
 * http://fusoya.eludevisibility.org/lc/index.html
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.LunarCompress;
using MushROMs.SNESLibrary;

namespace MushROMs.GenericEditor.PaletteEditor
{
    public unsafe partial class PaletteForm : EditorForm
    {
        #region Constants and readonly fields
        #region Default properties
        private const int MinColumns = 4;
        private const int MinRows = 4;
        private const int MaxColumns = 0x40;
        private const int MaxRows = 0x40;

        public const int SNESPaletteWidth = SNESEditor.PaletteWidth;
        public const int SNESPaletteHeight = SNESEditor.PaletteHeight;
        public const int SNESPaletteSize = SNESEditor.PaletteSize;
        #endregion

        #region File extension strings
        public const string ExtensionTPL = SNESEditor.ExtensionTPL;
        public const string ExtensionMW3 = SNESEditor.ExtensionMW3;
        public const string ExtensionPAL = SNESEditor.ExtensionPAL;
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
        public const string ExtensionDMP = SNESEditor.ExtensionDMP;
        #endregion

        #region Formatting info
        public const string UnsavedNotification = SNESEditor.UnsavedNotification;

        private readonly int StatusWidth;
        private readonly int StatusHeight;

        private const uint TPLHeader = ((byte)'T' << 0x00) | ((byte)'P' << 0x08) | ((byte)'L' << 0x10) | ((byte)2 << 0x18);
        private const string S9XHeader151 = "#!snes9x";
        private const string S9XHeader153 = "#!s9xsnp";
        private const int S9XV153 = 153;
        private const int S9XV151 = 151;
        private const int S9XSize153 = 0x11D073;
        private const int S9XSize151 = 0x11C670;
        private const int S9XOffset151 = 0xBE;
        private const int S9XOffset153 = 0xB9;
        private const int S9XPathSizeAddress = 0x12;
        private const int S9XPathDigits = 6;
        private const string ZSTHeader = "ZSNES Save State File V143";
        private const int ZSTOffset = 0x618;
        #endregion

        #region Status strings
        private const string TextColumns = "Columns";
        private const string TextRows = "Rows";
        private const string TextStartIndex = "Starting Index";
        private const string TextPCAddress = "PC Address";
        private const string TextWidth = "Width";
        private const string TextHeight = "Height";

        private const string StatusZoomChanged = "Zoom value changed successfully.";
        private const string StatusSelectionLost = "Selection lost when columns per row changes.";
        private const string StatusBackColorEnabled = "Background color editing enabled. Note this is saved only to .MW3 files.";
        private const string StatusBackColorDisabled = "Background color editing disabled.";
        private const string StatusColorChangeApplied = "Color change successful.";
        private const string StatusGradientApplied = "Gradient applied successfully.";
        private const string StatusColorizeApplied = "Colorization applied successfully.";
        private const string StatusColorizeCanceled = "Colorization cancelled.";
        private const string StatusColorizePreview = "Previewing colorization.";
        private const string StatusInvertApplied = "Color inversion applied successfully.";
        private const string StatusLumaApplied = "Luma-weighted grayscale applied successfully.";
        private const string StatusCustomGrayApplied = "Custom grayscale applied successfully.";
        private const string StatusCustomGrayCanceled = "Custom grayscale canceled.";
        private const string StatusCustomGrayPreview = "Previewing custom grayscale.";
        #endregion

        #region Error and warning strings
        public const string ErrorInvalidDataSize = "Size of data is not a valid size for the specified format.";
        public const string ErrorDataTooSmall = "Size of data is not large enough to support specified number of colors.";
        public const string ErrorNumColors = "Number of colors must be greater than zero.";
        public const string ErrorTPLFormat = "The file does not have the proper header for a SNES Tile Layer Pro Palette file.";
        public const string ErrorS9XFormat = "SNES9x save state is an unknown format.";
        private const string ErrorSNESFile = "Cannot save palette to SNES file.";
        private const string ErrorS9XVersion = "Unknown SNES9x version.";
        private const string ErrorSaveStateFile = "Cannot save a save state as a new file.";
        private const string ErrorSaveStateSize = "Cannot get size for save state files.";
        public const string ErrorSaveStateData = "Cannot write palette data to a new byte array in a save state format. Try writing to a referenced array.";
        public const string ErrorSaveStatenumColors = "There must be exactly 256 colors to write for save state formats.";
        private const string ErrorDataFormatUnknown = "The palette format is unknown or not supported.";
        private const string WarningSelectAllSNES = "Cannot select all colors for ROMs. Most values are not valid colors, so this option would be unwise.";
        #endregion
        #endregion

        #region Events
        [Browsable(false)]
        public event EventHandler DataModified;

        [Browsable(false)]
        public event EventHandler LastUndo;

        [Browsable(false)]
        public event EventHandler LastRedo;
        #endregion

        #region Variables
        #region File data
        private string fp;
        private string fn;
        private byte[] fData;

        private static int untitled = 1;
        #endregion

        #region Palette data
        private PaletteDataFormats dataFormat;
        private PaletteZoomScales zoomScale;
        private ushort* colors;

        private UndoRedo<CopyData> undoRedo;

        private int address;
        private int odd;
        #endregion

        #region Search info
        /*
        private int lastSearchIndex;
        private int currentSearchIndex;
        private bool searchLooped;
         * */
        #endregion

        #region Modifier dialogs
        private ColorizeForm hslForm;
        private GrayscaleForm gryForm;
        private CopyData selectionBase;
        private CopyData selectionChanged;
        #endregion

        #region Image info
        private EventWatch dashTime;
        private int dash;
        #endregion
        #endregion

        #region Accessors
        #region Parent interaction
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PaletteParent MdiParent
        {
            get { return (PaletteParent)base.MdiParent; }
            set { base.MdiParent = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ParentStatus
        {
            set { if (this.MdiParent != null) this.MdiParent.Status = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EditorActive
        {
            get { return this.ActiveControl == this.edcPalette; }
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

        #region Palette info
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaletteDataFormats PaletteDataFormat
        {
            get { return this.dataFormat; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort PaletteBackColor
        {
            get { return 0; }
            set {  }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ushort EditColor
        {
            get { return 0; }
            set {  }
        }
        #endregion

        #region Sizing info
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PaletteZoomScales PaletteZoomScale
        {
            get { return this.zoomScale; }
            set { this.edcPalette.SetZoomSize((int)value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Zoom
        {
            get { return (int)this.zoomScale; }
        }
        #endregion
        #endregion

        #region Initializers
        /* Initially had separate initializers, but switched to one with create methods
         * to test interfacing different editors. Not sure what to think for the time being.
         * I guess with this way, the user can create a new palette for the same form. That
         * might come in handy for more dedicated editors
         */

        public PaletteForm()
        {
            InitializeComponent();

            this.PrimaryEditor = this.edcPalette;

            this.undoRedo = new UndoRedo<CopyData>();
            this.undoRedo.Undo += new EventHandler(undoRedo_Undo);
            this.undoRedo.Redo += new EventHandler(undoRedo_Redo);
            this.undoRedo.LastUndo += new EventHandler(undoRedo_LastUndo);
            this.undoRedo.LastRedo += new EventHandler(undoRedo_LastRedo);

            this.StatusWidth = this.ClientWidth - this.edcPalette.ClientWidth;
            this.StatusHeight = this.ClientHeight - this.edcPalette.ClientWidth;

            this.dashTime = new EventWatch(Program.DefaultDashWait);
            Program.Animator.AddWatch(ref this.dashTime);
            this.dashTime.Elapsed += new ElapsedEventHandler(dashTime_Elapsed);

            this.Text = "ERROR: Palette uninitialized.";
            this.Enabled = false;
        }

        public void CreateNew(int numColors)
        {
            CreateEmptyFileData();

            SetNumColors(numColors);
            for (int i = numColors; --i >= 0; )
                this.colors[i] = 0;

            Initialize();
        }

        public void CreateCopy(CopyData data)
        {
            CreateEmptyFileData();

            SetNumColors(data.Height * data.Width);
            fixed (ushort* src = data.Colors)
                for (int i = this.DataLength; --i >= 0; )
                    this.colors[i] = src[i];

            Initialize();
        }

        private void CreateEmptyFileData()
        {
            this.undoRedo.ForceUnsaved = true;
            this.fp = string.Empty;
            SetPaletteDataFormat(PaletteSettings.Default.DefaultFormat);
            this.fn = "Untitled" + (PaletteForm.untitled++).ToString() + PaletteForm.GetExtension(this.dataFormat);
        }

        public void Open(string path)
        {
            this.undoRedo.ForceUnsaved = false;
            this.fp = path;
            this.fn = Path.GetFileName(path);
            this.fData = Program.GZipDecompress(path);

            fixed (byte* ptr = this.fData)
                LoadPaletteData(ptr, this.fData.Length, GetFormat(path));

            Initialize();
        }

        private void SetNumColors(int numColors)
        {
            if (numColors == 0)
                throw new ArgumentException(ErrorNumColors);

            this.edcPalette.SetDataLength(numColors);
            this.edcPalette.SetDataWidth(PaletteSettings.Default.DefaultViewWidth);
            this.edcPalette.ResetData(sizeof(ushort) * numColors);
            this.colors = (ushort*)this.edcPalette.Data;
        }

        private void LoadPaletteData(byte* data, int size, PaletteDataFormats format)
        {
            if (!IsValidSize(format, size))
                throw new ArgumentException(ErrorInvalidDataSize);

            SetNumColors(GetNumColors(format, size));
            SetPaletteDataFormat(format);

            ushort* src = (ushort*)data;
            switch (format)
            {
                case PaletteDataFormats.TPL:
                    if (*((int*)data) != TPLHeader)
                        throw new ArgumentException(ErrorTPLFormat);
                    data += 4;
                    break;

                case PaletteDataFormats.PAL:
                    for (int i = this.DataLength, x = this.DataLength * 3; --i >= 0; )
                    {
                        uint color = (uint)data[--x];
                        color |= (uint)data[--x] << 8;
                        color |= (uint)data[--x] << 0x10;
                        this.colors[i] = LC.PCtoSNESRGB(color);
                    }
                    return;

                case PaletteDataFormats.MW3:
                    this.PaletteBackColor = src[this.DataLength];
                    break;

                case PaletteDataFormats.S9X:
                    int version = S9XVersion((IntPtr)data);

                    int len = 0;
                    if (!int.TryParse(new string((sbyte*)data, S9XPathSizeAddress, S9XPathDigits), out len))
                        throw new ArgumentException(ErrorS9XFormat);

                    if (version == S9XV151)
                    {
                        if (size != S9XSize151 + len)
                            throw new ArgumentException(ErrorS9XFormat);
                        data += S9XOffset151 + len;
                    }
                    else if (version == S9XV153)
                    {
                        if (size != S9XSize153 + len)
                            throw new ArgumentException(ErrorS9XFormat);
                        data += S9XOffset153 + len;
                    }

                    for (int i = this.DataLength, x = this.DataLength << 1; --i >= 0; )
                        this.colors[i] = (ushort)(data[--x] | (data[--x] << 8));
                    return;

                case PaletteDataFormats.ZST:
                    data += ZSTOffset;
                    break;

                case PaletteDataFormats.BIN:
                    break;

                case PaletteDataFormats.SNES:
                    data += size & ROM.LoBankSize;  // Ignore header
                    break;

                default:
                    throw new InvalidEnumArgumentException(ErrorDataFormatUnknown);
            }

            for (int i = this.DataLength; --i >= 0; )
                this.colors[i] = src[i];
        }

        private void Initialize()
        {
            this.Enabled = true;

            this.undoRedo.Reset(this.undoRedo.ForceUnsaved);

            /*
            this.lastSearchIndex = -1;
            this.currentSearchIndex = -1;
            this.searchLooped = false;
             * */

            this.edcPalette.SetViewSize(PaletteSettings.Default.DefaultViewWidth, PaletteSettings.Default.DefaultViewHeight);
            this.edcPalette.ResetMarker();

            this.cbxZoom.SelectedIndex = ((int)PaletteSettings.Default.DefaultZoom / 8) - 1;
            this.evsPalette.SetProperties();
            this.ehsPalette.SetProperties();
            SetUnsavedText();
        }
        #endregion

        #region Methods
        #region Property modifiers
        private void SetPaletteDataFormat(PaletteDataFormats value)
        {
            this.dataFormat = value;
            this.gbxROMViewing.Visible = value == PaletteDataFormats.SNES;
            if (!this.gbxROMViewing.Visible && this.Enabled)
                SetOdd(false);
            this.chkBackColor.Checked = value == PaletteDataFormats.MW3;
        }

        private void SetOdd(bool value)
        {
            this.odd = value ? 1 : 0;
            this.colors = (ushort*)((byte*)this.edcPalette.Data + odd);

            int current = this.CurrentIndex;
            int previous = this.PreviousIndex;
            int start = this.StartIndex;
            this.edcPalette.SetDataLength(this.edcPalette.NumCells + 1 - 2 * this.odd);
            this.edcPalette.SetStartIndex(start);
            this.edcPalette.SetEditRegion(current, previous);

            Redraw();
            UpdateStatus();
        }

        private void SetUnsavedText()
        {
            this.Text = this.fn + (this.Unsaved ? UnsavedNotification : string.Empty);
        }

        public void SetEditorCursor(Cursor cursor)
        {
            this.edcPalette.Cursor = cursor;
        }

        private void UpdateStatus()
        {
            uint color = LC.SNEStoPCRGB(this.colors[this.CurrentIndex]);
            this.lblPcValue.Text = "0x" + color.ToString("X6");
            this.lblSnesValue.Text = "0x" + LC.PCtoSNESRGB(color).ToString("X4");
            this.lblRedValue.Text = ((color >> 0x10) & 0xF8).ToString();
            this.lblGreenValue.Text = ((color >> 8) & 0xF8).ToString();
            this.lblBlueValue.Text = (color & 0xF8).ToString();

            StringBuilder sb = new StringBuilder();
            if (this.dataFormat != PaletteDataFormats.SNES)
            {
                sb.Append(TextStartIndex);
                sb.Append(": 0x");
                sb.Append(this.MinIndex.ToString("X"));
            }
            else
            {
                sb.Append(TextPCAddress);
                sb.Append(": 0x");
                sb.Append(this.address.ToString("X"));
            }
            sb.Append(", ");
            sb.Append(TextWidth);
            sb.Append(": 0x");
            sb.Append(this.edcPalette.SelectionWidth.ToString("X"));
            sb.Append(", ");
            sb.Append(TextHeight);
            sb.Append(": 0x");
            sb.Append(this.edcPalette.SelectionHeight.ToString("X"));
            this.tssMain.Text = sb.ToString();
        }

        private void SetMinMaxSize()
        {
            this.MinimumClientSize = new Size((MinColumns * this.Zoom) + this.StatusWidth, (MinRows * this.Zoom) + this.StatusHeight);
            this.MaximumClientSize = new Size((MaxColumns * this.Zoom) + this.StatusWidth, (MaxRows * this.Zoom) + this.StatusHeight);
        }

        private void SetFormSize()
        {
            this.ClientSize = new Size(this.edcPalette.ClientWidth + this.StatusWidth, this.edcPalette.ClientHeight + this.StatusHeight);
        }

        public void Redraw()
        {
            Redraw(true);
        }

        private void Redraw(bool write)
        {
            if (write)
                WritePixels();
            this.edcPalette.Invalidate();
        }

        private void WritePixels()
        {
            this.edcPalette.ResetBitmapPixels();

            ushort* src = (ushort*)this.colors + this.StartIndex;
            uint* dest = (uint*)this.edcPalette.Scan0;
            int numColors = this.DataLength - this.StartIndex;
            uint color = numColors >= 0 ? LC.SNEStoPCRGB(*src) : 0;

            int zoom = this.Zoom;
            int width = this.edcPalette.ClientWidth;
            int height = this.edcPalette.ClientHeight;
            int i1 = width * (zoom - 1);
            int i2 = zoom * (width - 1);

            int bgSize = (int)PaletteSettings.Default.DefaultBGSize;
            uint bgColor1 = PaletteForm.SystemToPCColor(PaletteSettings.Default.DefaultBGColor1);
            uint bgColor2 = PaletteForm.SystemToPCColor(PaletteSettings.Default.DefaultBGColor2);

            //This needs to be optimized for speed.
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    dest[(y * width) + x] = ((x & bgSize) ^ (y & bgSize)) == 0 ? bgColor1 : bgColor2;

            for (int y = this.ViewHeight, colors = numColors; --y >= 0; dest += i1, src += this.ViewWidth)
                for (int x = 0; x < this.ViewWidth && --colors >= 0; ++x, dest -= i2, color = LC.SNEStoPCRGB(src[x]))
                    for (int i = zoom; --i >= 0; dest += width)
                        for (int j = zoom; --j >= 0; )
                            dest[j] = color;

            this.edcPalette.WriteBitmapPixels();
        }

        private void WritePixels(CopyData data)
        {
            if (data.MinIndex > this.EndIndex || data.MaxIndex < this.StartIndex)
                return;

            int width = this.edcPalette.ClientWidth;
            int height = this.edcPalette.ClientHeight;

            /*
             * Almost positive we can use the original scan0 pointer. The program doesn't seem to
             * ever change the bitmap's pixel data location. But to be safe, we'll stick to this method.
             */
            BitmapData bmpData = this.edcPalette.Image.LockBits(new Rectangle(Point.Empty, this.edcPalette.Image.Size), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            uint* scan0 = (uint*)bmpData.Scan0;

            int zoom = this.Zoom;
            int startIndex = data.MinIndex;
            int columns = data.Columns;
            int x, y;
            for (int h = data.Height; --h >= 0; )
            {
                for (int w = data.Width; --w >= 0; )
                {
                    this.edcPalette.GetCoordinates(EditorControl.GetIndex(startIndex, columns, w, h), out x, out y);

                    if (!this.edcPalette.IsInViewRange(x, y))
                        continue;

                    uint* dest = scan0 + (y * zoom * width) + (x * zoom);
                    uint color = LC.SNEStoPCRGB(data.Colors[h, w]);

                    if (*dest == color)
                        continue;

                    for (int i = zoom; --i >= 0; dest += width)
                        for (int j = zoom; --j >= 0; )
                            dest[j] = color;
                }
            }

            this.edcPalette.Image.UnlockBits(bmpData);
        }

        private void drwPalette_Paint(object sender, PaintEventArgs e)
        {
            if (!this.Enabled)
                return;

            Graphics g = e.Graphics;

            float DashLength1 = PaletteSettings.Default.DefaultDashLength1;
            float DashLength2 = PaletteSettings.Default.DefaultDashLength2;
            Pen p1 = new Pen(Color.FromArgb(0xFF, PaletteSettings.Default.DefaultDashColor1), 1);
            p1.DashStyle = DashStyle.Dot;
            p1.DashOffset = this.dash;

            Pen p2 = new Pen(Color.FromArgb(0xFF, PaletteSettings.Default.DefaultDashColor2), 1);
            p2.DashStyle = DashStyle.Dot;
            p2.DashOffset = 1 + this.dash;

            int zoom = this.Zoom;

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

        private void PaletteEditor_Resize(object sender, EventArgs e)
        {
            int columns = (this.ClientWidth - this.StatusWidth) / this.Zoom;
            int rows = (this.ClientHeight - this.StatusHeight) / this.Zoom;

            this.edcPalette.SetViewSize(columns, rows);
        }

        private void PaletteEditor_ResizeEnd(object sender, EventArgs e)
        {
            SetFormSize();
        }

        private void drwPalette_EditRegionChanged(object sender, EventArgs e)
        {
            if (this.dataFormat == PaletteDataFormats.SNES)
                this.address = (this.CurrentIndex << 1) + this.odd + (this.fData.Length & ROM.LoBankSize);

            UpdateStatus();
        }

        private void drwPalette_ZoomSizeChanged(object sender, EventArgs e)
        {
            this.zoomScale = (PaletteZoomScales)this.edcPalette.ZoomWidth;
            SetMinMaxSize();
            SetFormSize();
            Redraw();
            this.ParentStatus = StatusZoomChanged;
        }

        private void drwPalette_ViewSizeChanged(object sender, EventArgs e)
        {
            this.edcPalette.SetDataWidth(this.ViewWidth);
            this.evsPalette.SetProperties();
            Redraw();
            this.tssMain.Text = TextColumns + ": 0x" + this.ViewWidth.ToString("X") + ", " + TextRows + ": 0x" + this.ViewHeight.ToString("X");
        }

        private void drwPalette_StartIndexChanged(object sender, EventArgs e)
        {
            Redraw();
        }

        private void drwPalette_DataSizeChanged(object sender, EventArgs e)
        {
            this.ParentStatus = StatusSelectionLost;
        }

        private void dashTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.EditorActive)
            {
                this.dash += 1;
                Redraw(false);
            }
        }
        #endregion

        #region Save methods
        public void Save()
        {
            Save(this.fp);
        }

        public void Save(string path)
        {
            PaletteDataFormats format = PaletteForm.GetFormat(path);
            switch (format)
            {
                case PaletteDataFormats.S9X:
                case PaletteDataFormats.ZST:
                    if (File.Exists(path))
                        this.fData = File.ReadAllBytes(path);
                    else if (this.dataFormat != format)
                        throw new ArgumentException(ErrorSaveStateFile);
                    break;
                case PaletteDataFormats.SNES:
                    if (this.dataFormat != PaletteDataFormats.SNES)
                        throw new ArgumentException(ErrorSNESFile);
                    break;
                default:
                    this.fData = new byte[GetFormatSize(format, this.DataLength)];
                    break;
            }
            SetPaletteDataFormat(format);

            if (format == PaletteDataFormats.S9X)
                Program.GZipDecompress(ref this.fData);

            fixed (byte* ptr = this.fData)
                WritePaletteData(ptr, this.fData.Length, format);

            if (format == PaletteDataFormats.S9X)
                Program.GZipCompress(ref this.fData);

            File.WriteAllBytes(path, this.fData);
            this.fp = path;
            this.fn = Path.GetFileName(path);

            this.undoRedo.SetSaveIndex();
            SetUnsavedText();
        }

        private void WritePaletteData(byte* data, int size, PaletteDataFormats format)
        {
            int numColors = GetNumColors(format, size);
            ushort* src = (ushort*)this.edcPalette.Data;
            ushort* dest = (ushort*)data;

            switch (format)
            {
                case PaletteDataFormats.S9X:
                    int version = S9XVersion((IntPtr)data);
                    
                    int len = 0;
                    if (!int.TryParse(new string((sbyte*)data, S9XPathSizeAddress, S9XPathDigits), out len))
                        throw new ArgumentException(ErrorS9XFormat);
                    
                    if (version == S9XV151)
                    {
                        if (size != S9XSize151 + len)
                            throw new ArgumentException(ErrorS9XFormat);
                        data += S9XOffset151 + len;
                    }
                    else if (version == S9XV153)
                    {
                        if (size != S9XSize153 + len)
                            throw new ArgumentException(ErrorS9XFormat);
                        data += S9XOffset153 + len;
                    }

                    for (int i = numColors, x = numColors << 1; --i >= 0; )
                    {
                        ushort color = src[i];
                        data[--x] = (byte)color;
                        data[--x] = (byte)(color >> 8);
                    }
                    return;

                case PaletteDataFormats.PAL:
                    for (int i = numColors, x = numColors * 3; --i >= 0; )
                    {
                        uint color = LC.SNEStoPCRGB(src[i]);
                        data[--x] = (byte)color;
                        data[--x] = (byte)(color >> 8);
                        data[--x] = (byte)(color >> 0x10);
                    }
                    return;

                case PaletteDataFormats.TPL:
                    *((uint*)data) = TPLHeader;
                    data += 4;
                    break;

                case PaletteDataFormats.MW3:
                    dest[numColors] = this.PaletteBackColor;
                    break;

                case PaletteDataFormats.ZST:
                    data += ZSTOffset;
                    break;
                case PaletteDataFormats.BIN:
                    break;

                case PaletteDataFormats.SNES:
                    data += size % ROM.HeaderSize;
                    break;

                default:
                    throw new InvalidEnumArgumentException(ErrorDataFormatUnknown);
            }

            for (int i = numColors; --i >= 0; )
                dest[i] = src[i];
        }
        #endregion

        #region Undo/Redo methods
        private void AddUndoData()
        {
            this.undoRedo.AddUndoData(Copy());
        }

        private void AddRedoData()
        {
            this.undoRedo.AddRedoData(Copy());
        }

        public void Undo()
        {
            this.undoRedo.UndoChange();
        }

        private void undoRedo_Undo(object sender, EventArgs e)
        {
            ModifyPalette(this.undoRedo.CurrentUndoData, false);
        }

        public void Redo()
        {
            this.undoRedo.RedoChange();
        }

        private void undoRedo_Redo(object sender, EventArgs e)
        {
            ModifyPalette(this.undoRedo.CurrentRedoData, false);
        }

        private void undoRedo_LastUndo(object sender, EventArgs e)
        {
            OnLastUndo(e);
        }

        private void undoRedo_LastRedo(object sender, EventArgs e)
        {
            OnLastRedo(e);
        }

        protected virtual void OnLastUndo(EventArgs e)
        {
            if (LastUndo != null)
                LastUndo(this, e);
        }

        protected virtual void OnLastRedo(EventArgs e)
        {
            if (LastRedo != null)
                LastRedo(this, e);
        }
        #endregion

        #region Basic edit methods
        public CopyData Cut()
        {
            CopyData data = Copy();
            DeleteSelection();
            return data;
        }

        public CopyData Copy()
        {
            return Copy(this.MinIndex, this.ViewWidth, this.SelectionWidth, this.SelectionHeight);
        }

        public CopyData Copy(int minIndex, int columns, int width, int height)
        {
            return Copy(minIndex, columns, width, height, this.colors + minIndex);
        }

        public CopyData Copy(int minIndex, int columns, int width, int height, ushort* data)
        {
            return new CopyData(minIndex, columns, width, height, data);
        }

        public void Paste(CopyData data)
        {
            data.Columns = this.ViewWidth;
            data.Width = data.Columns < data.Width ? data.Columns : data.Width;
            data.MinIndex = this.MinIndex;
            ModifyPalette(data, true, true);
        }

        public void DeleteSelection()
        {
            DeleteSelection(this.MinIndex, this.ViewWidth, this.SelectionWidth, this.SelectionHeight);
        }

        public void DeleteSelection(int minIndex, int columns, int width, int height)
        {
            ModifyPalette(new CopyData(minIndex, columns, width, height));
        }

        public void SelectAll()
        {
            if (this.PaletteDataFormat == PaletteDataFormats.SNES)
            {
                MessageBox.Show(WarningSelectAllSNES, PaletteParent.DialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.edcPalette.SetEditRegion(this.DataLength - 1, 0);
        }

        public void GoTo()
        {
            GotoForm dlg = new GotoForm();
            if (dlg.ShowDialog(this.DataLength, this.StartIndex, this.CurrentIndex, this.odd == 1) == DialogResult.OK)
            {
                int address = dlg.Address;
                if (dlg.AddressFormat == GotoForm.GotoAddressFormats.SNES)
                    address = LC.PCtoSNES(address, ROMTypes.LoROM, this.fData.Length % 0x8000 == 0x200);
                switch (dlg.GotoStartPosition)
                {
                    case GotoForm.GotoStartPositions.BeginningOfFile:
                        if ((this.odd == 1) != (address % 2 == 1))
                            chkInvertData.Checked ^= true;
                        address >>= 1;
                        break;
                    case GotoForm.GotoStartPositions.CurrentPosition:
                        address += this.CurrentIndex;
                        break;
                    case GotoForm.GotoStartPositions.FirstIndex:
                        address += this.StartIndex;
                        break;
                }
                GoToAddress(address);
            }
        }

        public void GoToAddress(int address)
        {
            this.edcPalette.SetEditRegion(address, address);
            if (this.StartIndex > this.CurrentIndex || this.StartIndex + this.VisibleCells - 1 < this.CurrentIndex)
                this.edcPalette.SetStartIndex(address);
            else
                this.edcPalette.SetStartIndex(this.StartIndex);
        }
        /*
        public void FindNext(uint[] colors, int size, FindReplaceForm.FindDirections direction)
        {
            fixed (uint* search = colors)
            {
                if (this.dataFormat != PaletteDataFormats.SNES)
                {
                    if (this.lastSearchIndex == -1)
                        this.currentSearchIndex = this.lastSearchIndex = this.CurrentIndex;

                    bool loop = searchLooped;
                    int address = FindNext(search, size, this.palette, this.currentSearchIndex, direction, ref loop);
                    if (address != -1)
                    {
                        if (!this.searchLooped)
                            this.searchLooped = loop;
                        this.currentSearchIndex = address + 1;
                        if (this.currentSearchIndex >= this.lastSearchIndex && this.searchLooped)
                        {
                            this.searchLooped = false;
                            this.lastSearchIndex = this.currentSearchIndex;
                            if (MessageBox.Show("No more matches. Start from beginning?", PaletteParent.DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                this.lastSearchIndex = -1;
                                return;
                            }
                        }
                        GoToAddress(address);
                        return;
                    }
                }
                else
                {
                    if (this.lastSearchIndex == -1)
                        this.currentSearchIndex = this.lastSearchIndex = this.address;

                    bool loop0 = this.searchLooped, loop1 = this.searchLooped;
                    int odd = this.lastSearchIndex % 2 == 0 ? 0 : 1;
                    int address0 = 0 + 2 * FindNext(search, size, this.evenPalette, (this.currentSearchIndex >> 1) + odd, direction, ref loop0);
                    int address1 = 1 + 2 * FindNext(search, size, this.oddPalette, this.currentSearchIndex >> 1, direction, ref loop1);

                    if (direction == FindReplaceForm.FindDirections.Down)
                    {
                        if (address0 >= 0 && (address1 < 0 || (address1 >= 0 && ((address0 < address1 && ((!loop0 && !loop1) || (loop0 && loop1))) || (!loop0 && loop1)))))
                        {
                            if (!this.searchLooped)
                                this.searchLooped = loop0;
                            this.currentSearchIndex = address0 + 1;
                            if (this.currentSearchIndex >= this.lastSearchIndex && this.searchLooped)
                            {
                                this.searchLooped = false;
                                this.lastSearchIndex = this.currentSearchIndex;
                                if (MessageBox.Show("No more matches. Start from beginning?", PaletteParent.DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                {
                                    this.lastSearchIndex = -1;
                                    return;
                                }
                            }
                            this.chkInvertData.Checked = false;
                            GoToAddress(address0 >> 1);
                            return;
                        }
                        else if (address1 >= 0 && (address0 < 0 || (address0 >= 0 && ((address1 < address0 && ((!loop0 && !loop1) || (loop0 && loop1))) || (loop0 && !loop1)))))
                        {
                            if (!this.searchLooped)
                                this.searchLooped = loop1;
                            this.currentSearchIndex = address1 + 1;
                            if (this.currentSearchIndex >= this.lastSearchIndex && this.searchLooped)
                            {
                                this.searchLooped = false;
                                this.lastSearchIndex = this.currentSearchIndex;
                                if (MessageBox.Show("No more matches. Start from beginning?", PaletteParent.DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                {
                                    this.lastSearchIndex = -1;
                                    return;
                                }
                            }
                            this.chkInvertData.Checked = true;
                            GoToAddress(address1 >> 1);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Unknown search case appeared. Please debug!");
                            return;
                        }
                    }
                }
            }

            MessageBox.Show("Could not find search criteria.");
        }

        private int FindNext(uint* search, int searchSize, Palette palette, int index, FindReplaceForm.FindDirections direction, ref bool loop)
        {
            int address = -1;
            if (direction == FindReplaceForm.FindDirections.Down)
            {
                if ((address = FindNextDown(search, searchSize, palette.Colors, index, palette.numColors)) == -1)
                {
                    address = FindNextDown(search, searchSize, palette.Colors, 0, index);
                    loop = true;
                }
            }
            else if ((address = FindNextUp(search, searchSize, palette.Colors, index, 0)) == -1)
            {
                address = FindNextUp(search, searchSize, palette.Colors, palette.numColors, index);
                loop = true;
            }
            return address;
        }

        private int FindNextUp(uint* search, int size, uint* src, int start, int end)
        {
            int j;
            for (int i = start; --i >= end; )
            {
                for (j = size; --j >= 0; --i)
                    if (search[j] != src[i])
                        break;
                if (j == 0)
                    return i;
            }
            return -1;
        }

        private int FindNextDown(uint* search, int size, uint* src, int start, int end)
        {
            int j;
            for (int i = start; i < end; i++)
            {
                for (j = 0; j < size; j++, i++)
                    if (search[j] != src[i])
                        break;
                if (j == size)
                    return i - j;
            }
            return -1;
        }
         * */

        #endregion

        #region Palette modifiers
        private void ModifyPalette(CopyData data)
        {
            ModifyPalette(data, true);
        }

        private void ModifyPalette(CopyData data, bool addUndoRedo)
        {
            ModifyPalette(data, addUndoRedo, true);
        }

        private void ModifyPalette(CopyData data, bool addUndoRedo, bool redraw)
        {
            if (addUndoRedo)
            {
                this.undoRedo.AddUndoData(Copy(data.MinIndex, data.Columns, data.Width, data.Height, this.colors + data.MinIndex));
                this.undoRedo.AddRedoData(data);
            }

            data.WriteCopyData(this.colors, this.DataLength);

            if (!redraw)
                return;

            WritePixels(data);

            if (!addUndoRedo)
            {
                SetUnsavedText();
                UpdateStatus();
                Redraw(false);
            }
            else
                OnDataModified(EventArgs.Empty);
        }

        protected virtual void OnDataModified(EventArgs e)
        {
            SetUnsavedText();
            UpdateStatus();
            Redraw(false);

            if (DataModified != null)
                DataModified(this, e);
        }
        #endregion

        #region Advanced edit methods
        private void EditPaletteColor()
        {
            EditPaletteColor(this.CurrentIndex);
        }

        private void EditPaletteColor(int index)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.FullOpen = true;
            dlg.Color = SNESToSystemColor(this.colors[index]);
            if (dlg.ShowDialog() == DialogResult.OK)
                EditPaletteColor(index, SystemToSNESColor(dlg.Color));
        }

        private void EditPaletteColor(int index, ushort color)
        {
            ModifyPalette(new CopyData(index, this.ViewWidth, 1, 1, &color));
            this.ParentStatus = StatusColorChangeApplied;
        }

        public void Gradient(bool vertical)
        {
            if ((vertical ? this.SelectionHeight : this.SelectionWidth) < 2)
                return;

            CopyData data = Copy();
            int last = (vertical ? data.Height : data.Width) - 1;
            for (int j = vertical ? data.Width : data.Height; --j >= 0; )
            {
                Color c1 = SNESToSystemColor(data.Colors[vertical ? 0 : j, vertical ? j : 0]);
                Color c2 = SNESToSystemColor(data.Colors[vertical ? last : j, vertical ? j : last]);

                for (int i = vertical ? data.Height : data.Width; --i >= 0; )
                    data.Colors[vertical ? i : j, vertical ? j : i] = LC.PCtoSNESRGB((uint)((((c1.R * (last - i)) + (c2.R * i)) / last) << 0x10 |
                                                                                            (((c1.G * (last - i)) + (c2.G * i)) / last) << 8 |
                                                                                            (((c1.B * (last - i)) + (c2.B * i)) / last)));
            }
            ModifyPalette(data);
            this.ParentStatus = StatusGradientApplied;
        }

        public void Colorize()
        {
            this.selectionBase = Copy();
            this.selectionChanged = Copy();

            this.hslForm = new ColorizeForm();
            this.hslForm.ColorValueChanged += new EventHandler(hsl_ColorValueChanged);

            if (this.hslForm.ShowDialog() == DialogResult.OK)
            {
                ColorizeSelection(true);
                this.ParentStatus = StatusColorizeApplied;
            }
            else
            {
                ModifyPalette(this.selectionBase, false);
                this.ParentStatus = StatusColorizeCanceled;
            }
        }

        private void hsl_ColorValueChanged(object sender, EventArgs e)
        {
            if (this.hslForm.Preview)
            {
                ColorizeSelection(false);
                this.ParentStatus = StatusColorizePreview;
            }
        }

        private void ColorizeSelection(bool addUndoRedo)
        {
            int h = this.hslForm.Hue;
            int s = this.hslForm.Saturation;
            int l = this.hslForm.Luminosity;
            int ce = this.hslForm.Effectiveness;
            bool colorize = this.hslForm.Colorize;

            ModifyPalette(this.selectionBase, false, false);

            for (int y = this.selectionBase.Height; --y >= 0; )
            {
                for (int x = this.selectionBase.Width; --x >= 0; )
                {
                    ExpandedColor xColor = SNESToSystemColor(this.selectionBase.Colors[y, x]);
                    xColor = colorize ? xColor.Colorize(h, s, l, ce) : xColor.Modify(h, s, l);
                    this.selectionChanged.Colors[y, x] = SystemToSNESColor((Color)xColor);
                }
            }

            ModifyPalette(this.selectionChanged, addUndoRedo);
        }

        public void Invert()
        {
            CopyData data = Copy();
            for (int y = data.Height; --y >= 0; )
            {
                for (int x = data.Width; --x >= 0; )
                {
                    ExpandedColor c = ExpandedColor.FromArgb((int)LC.SNEStoPCRGB(data.Colors[y, x]));
                    data.Colors[y, x] = SystemToSNESColor((Color)c.Invert());
                }
            }
            ModifyPalette(data);
            this.ParentStatus = StatusInvertApplied;
        }

        public void LumaGrayscale()
        {
            CopyData data = Copy();
            for (int y = data.Height; --y >= 0; )
            {
                for (int x = data.Width; --x >= 0; )
                {
                    ExpandedColor c = ExpandedColor.FromArgb((int)LC.SNEStoPCRGB(data.Colors[y, x]));
                    data.Colors[y, x] = SystemToSNESColor((Color)c.LumaGrayScale());
                }
            }
            ModifyPalette(data);
            this.ParentStatus = StatusLumaApplied;
        }

        public void WeightedGrayScale()
        {
            this.selectionBase = Copy();
            this.selectionChanged = Copy();

            this.gryForm = new GrayscaleForm();
            this.gryForm.ColorValueChanged += new EventHandler(gryForm_ColorValueChanged);
            if (this.gryForm.Preview)
                WeightedGrayscaleSelection(false);

            if (this.gryForm.ShowDialog() == DialogResult.OK)
            {
                WeightedGrayscaleSelection(true);
                this.ParentStatus = StatusCustomGrayApplied;
            }
            else
            {
                ModifyPalette(this.selectionBase, false);
                this.ParentStatus = StatusCustomGrayCanceled;
            }
        }

        private void gryForm_ColorValueChanged(object sender, EventArgs e)
        {
            if (this.gryForm.Preview)
            {
                WeightedGrayscaleSelection(false);
                this.ParentStatus = StatusCustomGrayPreview;
            }
        }

        private void WeightedGrayscaleSelection(bool addUndoRedo)
        {
            int r = this.gryForm.Red;
            int g = this.gryForm.Green;
            int b = this.gryForm.Blue;
            int sum = r + g + b;

            ModifyPalette(this.selectionBase, false, false);

            for (int y = this.selectionBase.Height; --y >= 0; )
            {
                for (int x = this.selectionBase.Width; --x >= 0; )
                {
                    ExpandedColor xColor = SNESToSystemColor(this.selectionBase.Colors[y, x]);
                    xColor = xColor.WeightedGrayScale(r, g, b);
                    this.selectionChanged.Colors[y, x] = SystemToSNESColor((Color)xColor);
                }
            }

            ModifyPalette(this.selectionChanged, addUndoRedo);
        }
        #endregion

        #region UI events
        private void cbxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.edcPalette.SetZoomSize((this.cbxZoom.SelectedIndex + 1) * 8);
        }

        private void chkBackColor_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBackColor.Checked)
                this.ParentStatus = StatusBackColorEnabled;
            else
                this.ParentStatus = StatusBackColorDisabled;
        }

        private void chkInvertData_CheckedChanged(object sender, EventArgs e)
        {
            SetOdd(this.chkInvertData.Checked);
            if (this.odd == 1)
                this.ParentStatus = "Viewing SNES palette data at odd index.";
            else
                this.ParentStatus = "Viewing SNES palette data at even index.";
        }

        private void PaletteForm_MouseWheel(object sender, MouseEventArgs e)
        {
            int multplier = this.ViewWidth * (e.Delta / SystemInformation.MouseWheelScrollDelta);
            if ((this.modifiers & ~Keys.Alt) == Keys.Control)
                multplier *= this.ViewHeight;
            else if ((this.modifiers & ~Keys.Alt) == Keys.Shift)
                multplier *= (this.ViewHeight << 4);
            else if ((this.modifiers & ~Keys.Alt) == (Keys.Shift | Keys.Control))
                multplier *= (this.ViewHeight << 6);

            this.edcPalette.SetStartIndex(this.StartIndex - multplier);
        }

        private void drwPalette_SelectedCellMouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    EditPaletteColor();
                    break;
                case MouseButtons.Right:
                    this.EditColor = this.colors[this.CurrentIndex];
                    break;
                case MouseButtons.Middle:
                    EditPaletteColor(this.CurrentIndex, this.EditColor);
                    break;
            }
        }

        private void drwPalette_SelectedCellMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                EditPaletteColor();
        }

        private void drwPalette_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && !e.Control && !e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        if (this.edcPalette.IsUniquePoint())
                            EditPaletteColor();
                        break;
                    case Keys.D3:
                        if (this.edcPalette.IsUniquePoint())
                            this.EditColor = this.colors[this.CurrentIndex];
                        break;
                    case Keys.D2:
                        if (this.edcPalette.IsUniquePoint())
                            EditPaletteColor(this.CurrentIndex, this.EditColor);
                        break;
                }
            }
        }

        private Keys modifiers = Keys.None;

        private void PaletteForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.modifiers = e.Modifiers;
        }

        private void PaletteForm_KeyUp(object sender, KeyEventArgs e)
        {
            this.modifiers = ~e.Modifiers;
        }
        #endregion
        #endregion

        #region Static methods
        public static PaletteDataFormats GetFormat(string path)
        {
            if (path == string.Empty)
                return PaletteSettings.Default.DefaultFormat;

            string ext = Path.GetExtension(path).ToLower();
            if (ext == ExtensionTPL)
                return PaletteDataFormats.TPL;
            if (ext == ExtensionMW3)
                return PaletteDataFormats.MW3;
            if (ext == ExtensionPAL)
                return PaletteDataFormats.PAL;
            if (ext == ExtensionSMC || ext == ExtensionSFC || ext == ExtensionSWC || ext == ExtensionFIG)
                return PaletteDataFormats.SNES;
            if (ext.StartsWith(ExtensionS9X_0to9) && ext[3] >= '0' && ext[3] <= '9')
                return PaletteDataFormats.S9X;
            if (ext == ExtensionZST)
                return PaletteDataFormats.ZST;
            if (ext == AltExtensionZST)
                return PaletteDataFormats.ZST;
            if (ext.StartsWith(ExtensionZST_0to9) && ext[3] >= '0' && ext[3] <= '9')
                return PaletteDataFormats.ZST;
            if (ext.StartsWith(ExtensionZST_10to99) && ext[2] >= '1' && ext[2] <= '9' && ext[3] >= '0' && ext[3] <= '9')
                return PaletteDataFormats.ZST;
            return PaletteDataFormats.BIN;
        }

        public static string GetExtension(PaletteDataFormats format)
        {
            switch (format)
            {
                case PaletteDataFormats.TPL:
                    return ExtensionTPL;
                case PaletteDataFormats.MW3:
                    return ExtensionMW3;
                case PaletteDataFormats.PAL:
                    return ExtensionPAL;
                case PaletteDataFormats.BIN:
                    return ExtensionBIN;
                case PaletteDataFormats.SNES:
                    return ExtensionSMC;
                case PaletteDataFormats.ZST:
                    return ExtensionZST;
                case PaletteDataFormats.S9X:
                    return ExtensionS9X_0to9 + '0';
                default:
                    throw new InvalidEnumArgumentException(ErrorDataFormatUnknown);
            }
        }

        public static bool IsValidSize(PaletteDataFormats format, int size)
        {
            switch (format)
            {
                case PaletteDataFormats.TPL:
                    return size >= 4 && (size - 4) % 2 == 0;
                case PaletteDataFormats.PAL:
                    return size % 3 == 0;
                case PaletteDataFormats.MW3:
                    return size >= 2 && (size - 2) % 2 == 0;
                case PaletteDataFormats.BIN:
                    return size % 2 == 0;
                case PaletteDataFormats.SNES:
                    return (size & ~ROM.HeaderSize) % ROM.LoBankSize == 0;
                case PaletteDataFormats.ZST:
                    return size >= ZSTOffset + (SNESPaletteSize * 2);
                case PaletteDataFormats.S9X:
                    return size >= S9XSize151 || size >= S9XSize153;
                default:
                    throw new InvalidEnumArgumentException(ErrorDataFormatUnknown);
            }
        }

        public static int GetNumColors(PaletteDataFormats format, int size)
        {
            switch (format)
            {
                case PaletteDataFormats.TPL:
                    return (size - 4) >> 1;
                case PaletteDataFormats.PAL:
                    return size / 3;
                case PaletteDataFormats.MW3:
                    return (size - 2) >> 1;
                case PaletteDataFormats.BIN:
                    return size >> 1;
                case PaletteDataFormats.SNES:
                    return (size & ~ROM.HeaderSize) >> 1;
                case PaletteDataFormats.S9X:
                case PaletteDataFormats.ZST:
                    return SNESPaletteSize;
                default:
                    throw new InvalidEnumArgumentException(ErrorDataFormatUnknown);
            }
        }

        public static int GetFormatSize(PaletteDataFormats format, int numColors)
        {
            switch (format)
            {
                case PaletteDataFormats.TPL:
                    return numColors * 2 + 4;
                case PaletteDataFormats.PAL:
                    return numColors * 3;
                case PaletteDataFormats.MW3:
                    return numColors * 2 + 2;
                case PaletteDataFormats.BIN:
                case PaletteDataFormats.SNES:
                    return numColors * 2;
                case PaletteDataFormats.ZST:
                case PaletteDataFormats.S9X:
                    throw new InvalidEnumArgumentException(ErrorSaveStateSize);
                default:
                    throw new InvalidEnumArgumentException(ErrorDataFormatUnknown);
            }
        }

        public static int S9XVersion(IntPtr data)
        {
            if (new string((sbyte*)data, 0, S9XHeader151.Length) == S9XHeader151)
                return S9XV151;
            else if (new string((sbyte*)data, 0, S9XHeader153.Length) == S9XHeader153)
                return S9XV153;
            throw new ArgumentException(ErrorS9XFormat);
        }

        public static int S9XPaletteOffset(int version)
        {
            switch (version)
            {
                case S9XV151:
                    return S9XOffset151;
                case S9XV153:
                    return S9XOffset153;
                default:
                    throw new ArgumentException(ErrorS9XVersion);
            }
        }

        public static uint RoundColorValue(uint color)
        {
            // Get the RGB components individually. We add 4 so rounding is based around the midpoint.
            uint r = (color & 0xFF0000) + 0x040000;
            uint g = (color & 0x00FF00) + 0x000400;
            uint b = (color & 0x0000FF) + 0x000004;

            // Round the component to the nearest valid value. Do not let exceed 255.
            r = r > 0xFF0000 ? 0xF80000 : r & 0xF80000;
            g = g > 0x00FF00 ? 0x00F800 : g & 0x00F800;
            b = b > 0x0000FF ? 0x0000F8 : b & 0x0000F8;

            // Return composition of all rounded components.
            return r | g | b;
        }

        public static Color RoundColorValue(Color color)
        {
            // Get the RGB components individually. We add 4 so rounding is based around the midpoint.
            int r = color.R + 4;
            int g = color.G + 4;
            int b = color.B + 4;

            // Round the component to the nearest valid value. Do not let exceed 255.
            r = r > 0xFF ? 0xF8 : r & 0xF8;
            g = g > 0xFF ? 0xF8 : g & 0xF8;
            b = b > 0xFF ? 0xF8 : b & 0xF8;

            // Return composition of all rounded components.
            return Color.FromArgb(r, g, b);
        }

        public static uint SystemToPCColor(Color color)
        {
            return RoundColorValue((uint)color.ToArgb());
        }

        public static Color PCToSystemColor(uint color)
        {
            return Color.FromArgb((int)RoundColorValue(color));
        }

        public static ushort SystemToSNESColor(Color color)
        {
            return LC.PCtoSNESRGB(SystemToPCColor(color));
        }

        public static Color SNESToSystemColor(ushort color)
        {
            return PCToSystemColor(LC.SNEStoPCRGB(color));
        }
        #endregion

        #region Structures
        public class CopyData
        {
            #region Variables
            private ushort[,] colors;
            private int columns;
            private int minIndex;
            private int height;
            private int width;
            #endregion

            #region Accessors
            public ushort[,] Colors
            {
                get { return this.colors; }
            }

            public int Columns
            {
                get { return this.columns; }
                set { this.columns = value; }
            }

            public int MinIndex
            {
                get { return this.minIndex; }
                set { this.minIndex = value; }
            }

            public int MaxIndex
            {
                get { return this.minIndex + (this.columns * (this.height - 1)) + this.width; }
            }

            public int Height
            {
                get { return this.height; }
                set { this.height = value; }
            }

            public int Width
            {
                get { return this.width; }
                set { this.width = value; }
            }
            #endregion

            #region Initializers
            public CopyData(int minIndex, int columns, int width, int height)
            {
                this.columns = columns;
                this.minIndex = minIndex;
                this.width = width;
                this.height = height;
                this.colors = new ushort[this.height, this.width];
            }

            public CopyData(int minIndex, int columns, int width, int height, ushort* data)
            {
                this.columns = columns;
                this.minIndex = minIndex;
                this.width = width;
                this.height = height;
                this.colors = new ushort[this.height, this.width];

                for (int i = this.height; --i >= 0; )
                    for (int j = this.width; --j >= 0; )
                        this.colors[i, j] = data[(i * columns) + j];
            }
            #endregion

            #region Methods
            public void WriteCopyData(ushort* data, int maxSize)
            {
                WriteCopyData(data, maxSize, this.minIndex, this.Columns);
            }

            public void WriteCopyData(ushort* data, int maxSize, int minIndex, int columns)
            {
                data += minIndex;
                maxSize -= minIndex;
                int w = this.width;
                if (columns != this.columns)
                    w = w < columns ? w : columns;
                for (int i = this.height; --i >= 0; )
                    for (int j = w; --j >= 0; )
                        if ((i * columns) + j < maxSize)
                            data[(i * columns) + j] = this.colors[i, j];
            }
            #endregion
        }
        #endregion
    }

    #region Enumerations
    public enum PaletteDataFormats
    {
        None = -1,
        TPL = 0,
        PAL = 1,
        MW3 = 2,
        BIN = 3,
        S9X = 4,
        ZST = 5,
        SNES = 6
    }

    public enum PaletteZoomScales
    {
        Zoom8x = 8,
        Zoom16x = 16,
        Zoom24x = 24,
        Zoom32x = 32
    }

    public enum PaletteBGSizes
    {
        Size1x = 1,
        Size2x = 2,
        Size4x = 4,
        Size8x = 8,
    }
    #endregion
}