/* A control designed for Super Nintendo graphics editing.
 * Built by spel werdz rite.
 * http://www.smwcentral.net/?p=profile&id=1191
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MushROMs.Unmanaged;

namespace MushROMs.Controls
{
    public class EditorControl : DrawControl
    {
        #region Constant and readonly fields
        private const int DefaultCellWidth = 8;
        private const int DefaultCellHeight = DefaultCellWidth;

        private const int DefaultZoomWidth = 2;
        private const int DefaultZoomHeight = DefaultZoomWidth;

        private const int DefaultDataWidth = 0x10;
        private const int DefaultDataHeight = DefaultDataWidth;
        private const int DefaultDataSize = DefaultDataHeight * DefaultDataWidth;
        private const int DefaultRemainder = DefaultDataSize % DefaultDataWidth;

        private const int DefaultColumns = DefaultDataWidth;
        private const int DefaultRows = DefaultDataHeight;
        private const int DefaultCells = DefaultRows * DefaultColumns;

        private const bool DefaultDrawImage = true;

        private const bool DefaultUpdateMouseOnChange = false;
        private const bool DefaultUpdateMouseInRange = false;

        private const bool DefaultValidSelect = true;
        private const bool DefaultAllowMultiselect = true;
        private const MouseButtons DefaultSelectButton = MouseButtons.Left;

        private static readonly Keys[] DefaultOverrideKeys = { };

        public static readonly Point MouseOutOfRange = new Point(-1, -1);
        #endregion

        #region Events
        [Category("Editor")]
        [Description("Occurs when the pixel size of the data changes.")]
        public event EventHandler PixelSizeChanged;

        [Category("Editor")]
        [Description("Occurs when the length of the data changes.")]
        public event EventHandler DataLengthChanged;

        [Category("Editor")]
        [Description("Occurs when the width and/or the height of the data changes.")]
        public event EventHandler DataSizeChanged;

        [Category("Editor")]
        [Description("Occurs when the zoom value changes.")]
        public event EventHandler ZoomSizeChanged;

        [Category("Editor")]
        [Description("Occurs when the number of visible columns or rows changes.")]
        public event EventHandler ViewSizeChanged;

        [Category("Editor")]
        [Description("Occurs when the selected coordinates in the control changes.")]
        public event EventHandler EditRegionChanged;

        [Category("Editor")]
        [Description("Occurs when the start index realligns to a different column.")]
        public event EventHandler StartIndexAlignmentChanged;

        [Category("Editor")]
        [Description("Occurs when the start index in the control changes.")]
        public event EventHandler StartIndexChanged;

        [Category("Editor")]
        [Description("Occurs when the selected cell is clicked with the mouse.")]
        public event MouseEventHandler SelectedCellMouseClick;

        [Category("Editor")]
        [Description("Occurs when the selected cell is double-clicked with the mouse.")]
        public event MouseEventHandler SelectedCellMouseDoubleClick;

        [Browsable(true)]   // This event is redone so it can be browsable in the designer.
        [Description("Occurs when the mouse wheel moves while the control has focus.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event MouseEventHandler MouseWheel;
        #endregion

        #region Variables
        #region Data selection info
        private int pixelW;
        private int pixelH;
        private int zoomW;
        private int zoomH;

        private int width;
        private int height;
        private int length;
        private int memSize;
        private int remainder;
        private int columns;
        private int rows;
        private int cells;

        private bool validSelect;
        private bool multiselect;
        private bool hold;

        private EditorVScrollBar vScrollBar;
        private EditorHScrollBar hScrollBar;

        private ScrollEnd vScrollEnd;
        private ScrollEnd hScrollEnd;

        private int firstIndex;
        private bool sameIndex;
        private IndexPoint start;
        private IndexPoint end;
        private IndexPoint current;
        private IndexPoint previous;
        private IndexPoint min;
        private IndexPoint max;
        #endregion

        #region Imaging
        private IntPtr data;
        
        private Bitmap image;
        private IntPtr scan0;
        private bool drawImage;
        #endregion

        #region Mouse Data
        private MouseButtons selectButton;

        private Point currentMousePoint;
        private Point previousMousePoint;
        private MouseButtons currentMouseButtons;
        private MouseButtons previousMouseButtons;

        private bool processMouseOnChange;
        private bool processMouseInRange;
        #endregion

        #region Key data
        private Keys currentKeys;
        private Keys previousKeys;
        private Keys modifierKeys;

        private Keys[] overrideInputKeys;
        #endregion
        #endregion

        #region Accessors
        #region Data selection info
        #region Data size
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PixelWidth
        {
            get { return this.pixelW; }
            set { SetPixelSize(value, this.pixelH); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PixelHeight
        {
            get { return this.pixelH; }
            set { SetPixelSize(this.pixelW, value); }
        }

        [Browsable(true)]
        [Category("Editor Sizing")]
        [Description("The vertical and horizontal size of a single data cell.")]
        public Size PixelSize
        {
            get { return new Size(this.pixelW, this.pixelH); }
            set { SetPixelSize(value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ZoomWidth
        {
            get { return this.zoomW; }
            set { SetZoomSize(value, this.zoomH); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ZoomHeight
        {
            get { return this.zoomH; }
            set { SetZoomSize(this.zoomW, value); }
        }

        [Browsable(true)]
        [Category("Editor Sizing")]
        [Description("The vertical and horizontal zoom factor.")]
        public Size ZoomSize
        {
            get { return new Size(this.zoomW, this.zoomH); }
            set { SetZoomSize(value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellWidth
        {
            get { return this.zoomW * this.pixelW; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellHeight
        {
            get { return this.zoomH * this.pixelH; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size CellSize
        {
            get { return new Size(this.CellWidth, this.CellHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int NumCells
        {
            get { return this.length; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DataWidth
        {
            get { return this.width; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DataHeight
        {
            get { return this.height; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size DataSize
        {
            get { return new Size(this.width, this.height); }
            set { SetDataSize(value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Remainder
        {
            get { return this.remainder; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Aligned
        {
            get { return this.remainder == 0; }
        }
        #endregion

        #region View
        [Browsable(false)]
        public int ViewWidth
        {
            get { return this.columns; }
        }

        [Browsable(false)]
        public int ViewHeight
        {
            get { return this.rows; }
        }

        [Browsable(true)]
        [Category("Editor Sizing")]
        [Description("The number of columns and rows visible in the editor.")]
        public Size ViewSize
        {
            get { return new Size(this.columns, this.rows); }
            set { SetViewSize(value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VisibleCells
        {
            get { return this.cells; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartIndex
        {
            get { return this.start.Index; }
            set { SetStartIndex(value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartX
        {
            get { return this.start.X; }
            set { SetStartIndex(GetIndex(0, value, this.StartY)); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartY
        {
            get { return this.start.Y; }
            set { SetStartIndex(GetIndex(0, this.StartX, value)); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point StartCoordinates
        {
            get { return this.start.Coordinates; }
            set { SetStartIndex(GetIndex(0, value)); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EndIndex
        {
            get { return this.end.Index; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EndX
        {
            get { return this.end.X; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EndY
        {
            get { return this.end.Y; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point EndCoordinates
        {
            get { return this.end.Coordinates; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ViewRectangle
        {
            get { return new Rectangle(this.StartCoordinates, this.ViewSize); }
            set { this.StartCoordinates = value.Location; this.ViewSize = value.Size; }
        }
        #endregion

        #region Selecting info
        [Browsable(true)]
        [Category("Editor")]
        [DefaultValue(DefaultValidSelect)]
        [Description("A value indicating whether only a cell inside the valid region can be selected.")]
        public bool ValidSelect
        {
            get { return this.validSelect; }
            set { this.validSelect = value; }
        }

        [Browsable(true)]
        [Category("Editor")]
        [DefaultValue(DefaultAllowMultiselect)]
        [Description("A value indicating whether ranged selections will be permitted within the control.")]
        public bool AllowMultiselect
        {
            get { return this.multiselect; }
            set { if (!(this.multiselect = value)) SetCurrentIndex(this.CurrentIndex); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Hold
        {
            get { return this.hold; }
            set { this.hold = value; }
        }
        #endregion

        #region Scroll
        [Browsable(true)]
        [Category("Scroll")]
        [Description("The vertical scroll bar for this control.")]
        public EditorVScrollBar VScrollBar
        {
            get { return this.vScrollBar; }
            set { this.vScrollBar = value; }
        }

        [Browsable(true)]
        [Category("Scroll")]
        [Description("The horizontal scroll bar for this control.")]
        public EditorHScrollBar HScrollBar
        {
            get { return this.hScrollBar; }
            set { this.hScrollBar = value; }
        }

        [Browsable(true)]
        [Category("Scroll")]
        [DefaultValue(ScrollEnd.Full)]
        [Description("Determines the vertical scroll stop style ate the bottom of the edit control.")]
        public ScrollEnd VerticalScrollEnd
        {
            get { return this.vScrollEnd; }
            set { this.vScrollEnd = value; }
        }

        [Browsable(true)]
        [Category("Scroll")]
        [DefaultValue(ScrollEnd.Full)]
        [Description("Determines the horizontal scroll stop style ate the right of the edit control.")]
        public ScrollEnd HorizontalScrollEnd
        {
            get { return this.hScrollEnd; }
            set { this.hScrollEnd = value; }
        }
        #endregion

        #region Current
        [Browsable(false)]
        public int CurrentIndex
        {
            get { return this.current.Index; }
        }

        [Browsable(false)]
        public int CurrentX
        {
            get { return this.current.X; }
        }

        [Browsable(false)]
        public int CurrentY
        {
            get { return this.current.Y; }
        }

        [Browsable(false)]
        public Point CurrentCoordinates
        {
            get { return this.current.Coordinates; }
        }
        #endregion

        #region Previous
        [Browsable(false)]
        public int PreviousIndex
        {
            get { return this.previous.Index; }
        }

        [Browsable(false)]
        public int PreviousX
        {
            get { return this.previous.X; }
        }

        [Browsable(false)]
        public int PreviousY
        {
            get { return this.previous.Y; }
        }

        [Browsable(false)]
        public Point PreviousCoordinates
        {
            get { return this.previous.Coordinates; }
        }
        #endregion

        #region Min
        [Browsable(false)]
        public int MinIndex
        {
            get { return this.min.Index; }
        }

        [Browsable(false)]
        public int MinX
        {
            get { return this.min.X; }
        }

        [Browsable(false)]
        public int MinY
        {
            get { return this.min.Y; }
        }

        [Browsable(false)]
        public Point MinCoordinates
        {
            get { return this.min.Coordinates; }
        }
        #endregion

        #region Max
        [Browsable(false)]
        public int MaxIndex
        {
            get { return this.max.Index; }
        }

        [Browsable(false)]
        public int MaxX
        {
            get { return this.max.X; }
        }

        [Browsable(false)]
        public int MaxY
        {
            get { return this.max.Y; }
        }

        [Browsable(false)]
        public Point MaxCoordinates
        {
            get { return this.max.Coordinates; }
        }
        #endregion

        #region Selection
        [Browsable(false)]
        public int SelectionWidth
        {
            get { return this.MaxX - this.MinX + 1; }
        }

        [Browsable(false)]
        public int SelectionHeight
        {
            get { return this.MaxY - this.MinY + 1; }
        }

        [Browsable(false)]
        public Size SelectionSize
        {
            get { return new Size(this.SelectionWidth, this.SelectionHeight); }
        }

        [Browsable(false)]
        public Rectangle SelectionRectangle
        {
            get { return new Rectangle(this.MinCoordinates, this.SelectionSize); }
        }
        #endregion
        #endregion

        #region Imaging
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IntPtr Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MemorySize
        {
            get { return this.memSize; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IntPtr Scan0
        {
            get { return this.scan0; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Bitmap Image
        {
            get { return this.image; }
        }

        [Browsable(true)]
        [Category("Editor")]
        [DefaultValue(DefaultDrawImage)]
        [Description("Determines whether to draw the prerendered image before the paint event.")]
        public bool DrawImage
        {
            get { return this.drawImage; }
            set { this.drawImage = value; this.Invalidate(); }
        }
        #endregion

        #region Mouse Data
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point CurrentMousePoint
        {
            get { return this.currentMousePoint; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point PreviousMousePoint
        {
            get { return this.previousMousePoint; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MouseButtons CurrentMouseButtons
        {
            get { return this.currentMouseButtons; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MouseButtons PreviousMouseButtons
        {
            get { return this.previousMouseButtons; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool LeftMouseButtonHeld
        {
            get { return MouseButtonIsHeld(MouseButtons.Left); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool RightMouseButtonHeld
        {
            get { return MouseButtonIsHeld(MouseButtons.Right); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MiddleMouseButtonHeld
        {
            get { return MouseButtonIsHeld(MouseButtons.Middle); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool X1MouseButtonHeld
        {
            get { return MouseButtonIsHeld(MouseButtons.XButton1); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool X2MouseButtonHeld
        {
            get { return MouseButtonIsHeld(MouseButtons.XButton2); }
        }

        [Browsable(true)]
        [Category("Editor")]
        [DefaultValue(DefaultSelectButton)]
        [Description("The mouse button needed to update the selected coordinates in the control.")]
        public MouseButtons SelectButton
        {
            get { return this.selectButton; }
            set { this.selectButton = value; }
        }

        [Category("Editor")]
        [DefaultValue(DefaultUpdateMouseOnChange)]
        [Description("A value indicating whether the MouseMove event will only be called when the mouse coordinates change position.")]
        public bool ProcessMouseOnChange
        {
            get { return this.processMouseOnChange; }
            set { this.processMouseOnChange = value; }
        }

        [Category("Editor")]
        [DefaultValue(DefaultUpdateMouseInRange)]
        [Description("A value indicating whether the MouseMove event will only be called when the mouse is in the control's region.")]
        public bool ProcessMouseInRange
        {
            get { return this.processMouseInRange; }
            set { this.processMouseInRange = value; }
        }
        #endregion

        #region Key Data
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Keys CurrentKeys
        {
            get { return this.currentKeys; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Keys PreviousKeys
        {
            get { return this.previousKeys; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Keys ModifierKeys
        {
            get { return this.modifierKeys; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ControlKeyHeld
        {
            get { return (this.modifierKeys & Keys.Control) == Keys.Control; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShiftKeyHeld
        {
            get { return (this.modifierKeys & Keys.Shift) == Keys.Shift; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AltKeyHeld
        {
            get { return (this.modifierKeys & Keys.Alt) == Keys.Alt; }
        }

        [Category("Editor")]
        [Description("Provides a collection of keys to override as input keys.")]
        public Keys[] OverrideInputKeys
        {
            get { return this.overrideInputKeys; }
            set { this.overrideInputKeys = value; }
        }
        #endregion

        #region Form size data
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size
        {
            get { return base.Size; }
        }

        public new int Width
        {
            get { return base.Width; }
        }

        public new int Height
        {
            get { return base.Height; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size ClientSize
        {
            get { return base.ClientSize; }
        }

        public new int ClientWidth
        {
            get { return this.ClientSize.Width; }
        }

        public new int ClientHeight
        {
            get { return this.ClientSize.Height; }
        }
        #endregion
        #endregion

        #region Initializers
        public EditorControl()
        {
            this.data =
            this.scan0 = IntPtr.Zero;
            this.drawImage = DefaultDrawImage;

            this.pixelW = DefaultCellWidth;
            this.pixelH = DefaultCellHeight;
            this.zoomW = DefaultZoomWidth;
            this.zoomH = DefaultZoomHeight;

            this.width = DefaultDataWidth;
            this.height = DefaultDataHeight;
            this.length = DefaultDataSize;
            this.remainder = DefaultRemainder;
            this.columns = DefaultColumns;
            this.rows = DefaultRows;
            this.cells = DefaultCells;

            this.start =
            this.current =
            this.previous =
            this.min =
            this.max = new IndexPoint();
            SetEndIndex();

            SetClientSize();

            this.validSelect = DefaultValidSelect;
            this.multiselect = DefaultAllowMultiselect;

            this.selectButton = DefaultSelectButton;

            this.currentMousePoint =
            this.previousMousePoint = MouseOutOfRange;

            this.processMouseOnChange = DefaultUpdateMouseOnChange;
            this.processMouseInRange = DefaultUpdateMouseInRange;

            this.currentKeys =
            this.previousKeys = Keys.None;
            this.modifierKeys = Keys.None;
            this.overrideInputKeys = DefaultOverrideKeys;

            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.Selectable, true);

            base.MouseWheel += new MouseEventHandler(DrawControl_MouseWheel);
        }
        #endregion

        #region Methods
        #region Editor methods
        #region Set methods
        public void ResetData(int size)
        {
            Pointer.FreePointer(this.data);
            this.data = Pointer.CreatePointer(size);
        }

        public void ResetBitmapPixels()
        {
            this.image = null;
            Pointer.FreePointer(this.scan0);
            this.scan0 = Pointer.CreatePointer(sizeof(uint) * this.ClientWidth * this.ClientHeight);
        }

        public void WriteBitmapPixels()
        {
            WriteBitmapPixels(false);
        }

        public void WriteBitmapPixels(bool alpha)
        {
            this.image = new Bitmap(this.ClientWidth, this.ClientHeight, this.ClientWidth * 4, alpha ? PixelFormat.Format32bppArgb : PixelFormat.Format32bppRgb, this.scan0);
        }

        public void SetPixelSize(int size)
        {
            SetPixelSize(size, size);
        }

        public void SetPixelSize(Size size)
        {
            SetPixelSize(size.Width, size.Height);
        }

        public void SetPixelSize(int pixelW, int pixelH)
        {
            if (pixelW == this.pixelW && pixelH == this.pixelH)
                return;

            this.pixelW = pixelW;
            this.pixelH = pixelH;

            OnPixelSizeChanged(EventArgs.Empty);
        }

        public void SetZoomSize(int zoom)
        {
            SetZoomSize(zoom, zoom);
        }

        public void SetZoomSize(Size zoom)
        {
            SetZoomSize(zoom.Width, zoom.Height);
        }

        public void SetZoomSize(int zoomW, int zoomH)
        {
            if (zoomW == this.zoomW && zoomH == this.zoomH)
                return;

            this.zoomW = zoomW;
            this.zoomH = zoomH;

            OnZoomSizeChanged(EventArgs.Empty);
        }

        public void SetDataLength(int length)
        {
            if (length == this.length)
                return;

            this.length = length;
            SetDataWidth(this.width, true);
            ResetMarker();

            OnDataLengthChanged(EventArgs.Empty);
        }

        public void SetDataWidth(int width)
        {
            SetDataWidth(width, false);
        }

        private void SetDataWidth(int width, bool force)
        {
            if (this.length == 0 || width == 0 || (width == this.width && !force))
                return;

            this.width = width;
            this.height = this.length / width;
            this.remainder = this.length % width;
            SetEndIndex();

            if (!force)
                SetEditRegion(this.CurrentIndex, this.CurrentIndex, true);

            OnDataSizeChanged(EventArgs.Empty);
        }

        public void SetDataSize(Size size)
        {
            SetDataSize(size.Width, size.Height);
        }

        public void SetDataSize(int width, int height)
        {
            if (width == this.DataWidth && height == this.DataHeight)
                return;

            this.width = width;
            this.height = height;

            int oldLength = this.length;
            this.length = height * width;
            this.remainder = 0;
            SetEndIndex();

            OnDataSizeChanged(EventArgs.Empty);

            if (oldLength != this.length)
                OnDataLengthChanged(EventArgs.Empty);
        }

        public void SetViewSize(Size size)
        {
            SetViewSize(size.Width, size.Height);
        }

        public void SetViewSize(int columns, int rows)
        {
            if (columns == this.columns && rows == this.rows)
                return;

            this.columns = columns;
            this.rows = rows;
            this.cells = rows * columns;
            SetEndIndex();

            OnViewSizeChanged(EventArgs.Empty);
        }

        public void ResetMarker()
        {
            SetEditRegion(0, 0);
            SetStartIndex(0);
        }

        public void SetStartIndex(int startX, int startY)
        {
            SetStartIndex(GetIndex(0, this.width, startX, startY));
        }

        public void SetStartIndex(int startIndex)
        {
            if (startIndex < 0)
                startIndex = 0;
            if (startIndex == this.StartIndex)
                return;

            IndexPoint previous = this.previous;
            bool realigned = this.StartIndex % this.columns != startIndex % columns;
            if (realigned)
                previous = this.current;

            this.start = new IndexPoint(0, this.width, startIndex);
            SetEndIndex();

            SetEditRegion(this.current, previous, true);

            OnStartIndexChanged(EventArgs.Empty);
            if (realigned)
                OnStartIndexAlignmentChanged(EventArgs.Empty);
        }

        private void SetEndIndex()
        {
            this.end = new IndexPoint(this.StartIndex, this.width, this.columns - 1, this.rows - 1);
        }

        public void SetCurrentIndex(int currentX, int currentY)
        {
            SetCurrentIndex(new IndexPoint(this.StartIndex, this.width, currentX, currentY));
        }

        public void SetCurrentIndex(int currentIndex)
        {
            SetCurrentIndex(new IndexPoint(this.StartIndex, this.width, currentIndex));
        }

        private void SetCurrentIndex(IndexPoint current)
        {
            this.hold &= this.multiselect;
            SetEditRegion(current, this.hold ? previous : current);
        }

        public void SetEditRegion(int currentX, int currentY, int previousX, int previousY)
        {
            SetEditRegion(new IndexPoint(this.StartIndex, this.width, currentX, currentY), new IndexPoint(this.StartIndex, this.width, previousX, previousY));
        }

        public void SetEditRegion(int currentIndex, int previousIndex)
        {
            SetEditRegion(currentIndex, previousIndex, false);
        }

        protected void SetEditRegion(int currentIndex, int previousIndex, bool force)
        {
            SetEditRegion(new IndexPoint(this.StartIndex, this.width, currentIndex), new IndexPoint(this.StartIndex, this.width, previousIndex), force);
        }

        private void SetEditRegion(IndexPoint current, IndexPoint previous)
        {
            SetEditRegion(current, previous, false);
        }

        private void SetEditRegion(IndexPoint current, IndexPoint previous, bool force)
        {
            bool editRegionChanged = current.Index != this.CurrentIndex || previous.Index != this.PreviousIndex;
            if (!editRegionChanged && !force)
                return;

            if (this.validSelect && (!IsInValidRange(current) || !IsInValidRange(previous)))
                return;

            this.current = new IndexPoint(this.StartIndex, this.width, current.Index);
            this.previous = new IndexPoint(this.StartIndex, this.width, previous.Index);

            int minX = this.CurrentX < this.PreviousX ? this.CurrentX : this.PreviousX;
            int minY = this.CurrentY < this.PreviousY ? this.CurrentY : this.PreviousY;
            int maxX = this.CurrentX > this.PreviousX ? this.CurrentX : this.PreviousX;
            int maxY = this.CurrentY > this.PreviousY ? this.CurrentY : this.PreviousY;

            this.min = new IndexPoint(this.StartIndex, this.width, minX, minY);
            this.max = new IndexPoint(this.StartIndex, this.width, maxX, maxY);

            if (editRegionChanged)
                OnEditRegionChanged(EventArgs.Empty);
        }

        private void SetClientSize()
        {
            base.ClientSize = new Size(this.pixelW * this.zoomW * this.columns, this.pixelH * this.zoomH * this.rows);
        }
        #endregion

        #region Get methods
        public int GetIndex(Point coordinates)
        {
            return GetIndex(coordinates.X, coordinates.Y);
        }

        public int GetIndex(int startIndex, Point coordinates)
        {
            return GetIndex(startIndex, coordinates.X, coordinates.Y);
        }

        public static int GetIndex(int startIndex, int width, Point coordinates)
        {
            return GetIndex(startIndex, width, coordinates.X, coordinates.Y);
        }

        public int GetIndex(int x, int y)
        {
            return GetIndex(this.StartIndex, x, y);
        }

        public int GetIndex(int startIndex, int x, int y)
        {
            return GetIndex(startIndex, this.width, x, y);
        }

        public static int GetIndex(int startIndex, int width, int x, int y)
        {
            return startIndex + (y * width) + x;
        }

        public Point GetCoordinates(int index)
        {
            return GetCoordinates(this.StartIndex, index);
        }

        public Point GetCoordinates(int startIndex, int index)
        {
            return GetCoordinates(startIndex, this.width, index);
        }

        public static Point GetCoordinates(int startIndex, int width, int index)
        {
            int x, y;
            GetCoordinates(startIndex, width, index, out x, out y);
            return new Point(x, y);
        }

        public void GetCoordinates(int index, out int x, out int y)
        {
            GetCoordinates(this.StartIndex, index, out x, out y);
        }

        public void GetCoordinates(int startIndex, int index, out int x, out int y)
        {
            GetCoordinates(startIndex, this.width, index, out x, out y);
        }

        public static void GetCoordinates(int startIndex, int width, int index, out int x, out int y)
        {
            x = (index - startIndex) % width;
            y = (index - startIndex) / width;
            for (; x < 0; x += width, --y) ;
        }

        public bool IsInViewRange(int index)
        {
            int x, y;
            GetCoordinates(index, out x, out y);
            return IsInViewRange(x, y);
        }

        public bool IsInViewRange(int x, int y)
        {
            return x >= 0 && x < this.columns && y >= 0 && y < this.rows;
        }

        public bool IsInValidRange(int x, int y)
        {
            return IsInValidRange(new IndexPoint(this.StartIndex, this.width, x, y));
        }

        public bool IsInValidRange(int index)
        {
            return IsInValidRange(new IndexPoint(this.StartIndex, this.width, index));
        }

        private bool IsInValidRange(IndexPoint point)
        {
            if (point.X >= this.width || point.X + this.StartIndex < this.width - this.columns)
                return false;
            if (point.Y == this.height && point.X >= this.remainder)
                return false;
            if (point.Y > this.height)
                return false;
            return point.Index >= 0 && point.Index < this.length;
        }

        public bool IsUniquePoint()
        {
            return this.CurrentIndex == this.PreviousIndex;
        }

        public bool IsInBoundary(Point point)
        {
            return IsInBoundary(point, this.ClientRectangle);
        }

        public static bool IsInBoundary(Point point, Rectangle r)
        {
            return point.X >= r.X && point.X < r.X + r.Width &&
                point.Y >= r.Y && point.Y < r.Y + r.Height;
        }

        public bool MouseButtonIsHeld(MouseButtons mouseButtons)
        {
            return (this.currentMouseButtons & mouseButtons) == mouseButtons;
        }

        public bool KeyIsHeld(Keys keys)
        {
            return (this.currentKeys & keys) == keys;
        }
        #endregion
        #endregion

        #region Virtual and override methods
        #region Custom modifiers
        protected virtual void OnPixelSizeChanged(EventArgs e)
        {
            SetClientSize();
            if (PixelSizeChanged != null)
                PixelSizeChanged(this, e);
            this.Invalidate();
        }

        protected virtual void OnDataLengthChanged(EventArgs e)
        {
            if (DataLengthChanged != null)
                DataLengthChanged(this, e);
            this.Invalidate();
        }

        protected virtual void OnDataSizeChanged(EventArgs e)
        {
            if (DataSizeChanged != null)
                DataSizeChanged(this, e);
            this.Invalidate();
        }

        protected virtual void OnZoomSizeChanged(EventArgs e)
        {
            SetClientSize();
            if (ZoomSizeChanged != null)
                ZoomSizeChanged(this, e);
            this.Invalidate();
        }

        protected virtual void OnViewSizeChanged(EventArgs e)
        {
            SetClientSize();
            if (ViewSizeChanged != null)
                ViewSizeChanged(this, e);
            this.Invalidate();
        }

        protected virtual void OnEditRegionChanged(EventArgs e)
        {
            if (EditRegionChanged != null)
                EditRegionChanged(this, e);
            this.Invalidate();
        }

        protected virtual void OnStartIndexChanged(EventArgs e)
        {
            if (StartIndexChanged != null)
                StartIndexChanged(this, e);
            this.Invalidate();
        }

        protected virtual void OnStartIndexAlignmentChanged(EventArgs e)
        {
            if (StartIndexAlignmentChanged != null)
                StartIndexAlignmentChanged(this, e);
        }

        protected virtual void OnSelectedCellMouseClick(MouseEventArgs e)
        {
            if (SelectedCellMouseClick != null)
                SelectedCellMouseClick(this, e);
        }

        protected virtual void OnSelectedCellMouseDoubleClick(MouseEventArgs e)
        {
            if (SelectedCellMouseDoubleClick != null)
                SelectedCellMouseDoubleClick(this, e);
        }
        #endregion

        #region Imaging
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.drawImage && this.image != null && this.Enabled)
                e.Graphics.DrawImageUnscaled(this.image, Point.Empty);
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.drawImage)
            {
                this.image = null;
                this.Invalidate();
            }
            base.OnResize(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Pointer.FreePointer(this.scan0);
                Pointer.FreePointer(this.data);
                this.scan0 =
                this.data = IntPtr.Zero;
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Key processing
        protected virtual bool OnProcessArrowKeyCommands(Keys keyData)
        {
            if (!this.ShiftKeyHeld && !this.AltKeyHeld)
            {
                int currentX = this.CurrentX;
                int currentY = this.CurrentY;
                switch (keyData)
                {
                    case Keys.Left:
                        currentX--;
                        break;
                    case Keys.Right:
                        currentX++;
                        break;
                    case Keys.Up:
                        currentY--;
                        break;
                    case Keys.Down:
                        currentY++;
                        break;
                    default:
                        break;
                }

                if (currentX != this.CurrentX || currentY != this.CurrentY)
                {
                    bool dummy = this.hold;
                    this.hold = this.ControlKeyHeld;
                    SetCurrentIndex(currentX, currentY);
                    this.hold = dummy;
                    return true;
                }
            }

            if (this.modifierKeys == Keys.Shift)
            {
                switch (keyData)
                {
                    case Keys.Left:
                        SetStartIndex(this.StartIndex - 1);
                        return true;
                    case Keys.Right:
                        SetStartIndex(this.StartIndex + 1);
                        return true;
                    case Keys.Up:
                        SetStartIndex(this.StartIndex - this.width);
                        return true;
                    case Keys.Down:
                        SetStartIndex(this.StartIndex + this.width);
                        return true;
                    default:
                        break;
                }
            }

            if (!this.ControlKeyHeld && !this.AltKeyHeld)
            {
                switch (keyData)
                {
                    case Keys.Home:
                        SetStartIndex(this.ShiftKeyHeld ? 0 : ((this.StartIndex / this.width) * this.width));
                        return true;
                    case Keys.End:
                        SetStartIndex(this.ShiftKeyHeld ? this.length : (((this.StartIndex / this.width) * this.width) + this.width - this.columns));
                        return true;
                    case Keys.PageDown:
                        SetStartIndex(this.StartIndex + (this.rows * this.width) * (this.ShiftKeyHeld ? 0x10 : 1));
                        return true;
                    case Keys.PageUp:
                        SetStartIndex(this.StartIndex - (this.rows * this.width) * (this.ShiftKeyHeld ? 0x10 : 1));
                        return true;
                    default:
                        break;
                }
            }
            return false;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                case Keys.Control | Keys.Left:
                case Keys.Control | Keys.Right:
                case Keys.Control | Keys.Up:
                case Keys.Control | Keys.Down:
                    return true;
            }

            if (this.overrideInputKeys != null)
                for (int i = this.overrideInputKeys.Length; --i >= 0; )
                    if (this.overrideInputKeys[i] == keyData)
                        return true;

            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.previousKeys = this.currentKeys;
            this.currentKeys = e.KeyCode;
            this.modifierKeys = e.Modifiers;

            OnProcessArrowKeyCommands(e.KeyCode);

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.previousKeys = this.currentKeys;
            this.currentKeys &= ~e.KeyCode;
            this.modifierKeys = e.Modifiers;

            base.OnKeyUp(e);
        }
        #endregion

        #region Mouse processing
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.processMouseOnChange && this.currentMousePoint == e.Location)
                return;

            if (this.processMouseInRange && !IsInBoundary(e.Location))
                return;

            this.previousMousePoint = this.currentMousePoint;
            this.currentMousePoint = e.Location;

            if (e.Button == this.selectButton || this.selectButton == MouseButtons.None)
            {
                SetCurrentIndex(e.X / this.CellWidth, e.Y / this.CellHeight);
                this.sameIndex &= this.firstIndex == this.CurrentIndex && IsUniquePoint();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.previousMousePoint = this.currentMousePoint;
            this.currentMousePoint = MouseOutOfRange;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.previousMouseButtons = this.currentMouseButtons;
            this.currentMouseButtons = e.Button;

            if (e.Button == this.selectButton)
            {
                this.firstIndex = this.CurrentIndex;
                this.sameIndex = IsUniquePoint();
                SetCurrentIndex(e.X / this.CellWidth, (e.Y / this.CellHeight));
                this.sameIndex &= this.firstIndex == this.CurrentIndex && IsUniquePoint();
                this.hold = true;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.previousMouseButtons = this.currentMouseButtons;
            this.currentMouseButtons &= ~e.Button;

            this.hold = false;
            this.sameIndex = false;

            base.OnMouseUp(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (this.sameIndex)
                OnSelectedCellMouseClick(e);

            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (this.sameIndex)
                OnSelectedCellMouseDoubleClick(e);

            base.OnMouseDoubleClick(e);
        }
        #endregion
        #endregion

        #region Events
        private void DrawControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (MouseWheel != null)
                MouseWheel(this, e);
        }
        #endregion
        #endregion

        #region Structures
        private struct IndexPoint
        {
            private int index;
            private int x;
            private int y;

            public int Index
            {
                get { return this.index; }
                set { this.index = value; }
            }

            public int X
            {
                get { return this.x; }
                set { this.x = value; }
            }

            public int Y
            {
                get { return this.y; }
                set { this.y = value; }
            }

            public Point Coordinates
            {
                get { return new Point(this.x, this.y); }
            }

            public IndexPoint(int startIndex, int width, int index)
            {
                this.index = index;
                this.x = (index - startIndex) % width;
                this.y = (index - startIndex) / width;
                if (this.x < 0)
                { this.x += width; --this.y; }
            }

            public IndexPoint(int startIndex, int width, int x, int y)
            {
                this.index = startIndex + (y * width) + x;
                this.x = x;
                this.y = y;
            }
        }
        #endregion
    }

    public enum ScrollEnd
    {
        Full,
        Last,
        None
    }
}