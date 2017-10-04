using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    public class EditorForm : Form
    {
        #region Events
        [Browsable(true)]   // This event is redone so it can be browsable in the designer.
        [Description("Occurs when the mouse wheel moves while the control has focus.")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public new event MouseEventHandler MouseWheel;
        #endregion

        #region Variables
        #region Editors
        private EditorControl primaryEditor;
        #endregion

        #region Key info
        private Keys[] overrideInputKeys;
        private Keys[] ignoreDialogKeys;
        #endregion
        #endregion

        #region Accessors
        #region Editors
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditorControl PrimaryEditor
        {
            get { return this.primaryEditor; }
            set { this.primaryEditor = value; }
        }
        #endregion

        #region Key info
        [Category("Editor")]
        [Description("Provides a collection of keys to override as input keys.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Keys[] OverrideInputKeys
        {
            get { return this.overrideInputKeys; }
            set { this.overrideInputKeys = value; }
        }

        [Category("Editor")]
        [Description("Provides a collection of keys to ignore as dialog keys.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Keys[] IgnoreDialogKeys
        {
            get { return this.ignoreDialogKeys; }
            set { this.ignoreDialogKeys = value; }
        }
        #endregion

        #region Sizing
        [Browsable(false)]
        public int CaptionHeight
        {
            get
            {
                switch (this.FormBorderStyle)
                {
                    case FormBorderStyle.None:
                        return 0;

                    case FormBorderStyle.FixedSingle:
                    case FormBorderStyle.Fixed3D:
                    case FormBorderStyle.FixedDialog:
                    case FormBorderStyle.Sizable:
                        return SystemInformation.CaptionHeight;

                    case FormBorderStyle.FixedToolWindow:
                    case FormBorderStyle.SizableToolWindow:
                        return SystemInformation.ToolWindowCaptionHeight;

                    default:
                        throw new InvalidEnumArgumentException("Not a valid form border style.");
                }
            }
        }

        [Browsable(true)]   // This accessor is redone so it can be browsable in the designer.
        [Description("The size of the client area of the form.")]
        public new Size ClientSize
        {
            get { return base.ClientSize; }
            set { base.ClientSize = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ClientWidth
        {
            get { return this.ClientSize.Width; }
            set { this.ClientSize = new Size(value, this.ClientHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ClientHeight
        {
            get { return this.ClientSize.Height; }
            set { this.ClientSize = new Size(this.ClientWidth, value); }
        }

        [Browsable(false)]
        public int BorderWidth
        {
            get { return this.BorderSize.Width; }
        }

        [Browsable(false)]
        public int BorderHeight
        {
            get { return this.BorderSize.Height; }
        }

        [Browsable(false)]
        public Size BorderSize
        {
            get
            {
                switch (this.FormBorderStyle)
                {
                    case FormBorderStyle.None:
                        return Size.Empty;

                    case FormBorderStyle.FixedSingle:
                    case FormBorderStyle.FixedDialog:
                    case FormBorderStyle.FixedToolWindow:
                        return SystemInformation.FixedFrameBorderSize;

                    case FormBorderStyle.Fixed3D:
                        return SystemInformation.Border3DSize;

                    case FormBorderStyle.Sizable:
                    case FormBorderStyle.SizableToolWindow:
                        return SystemInformation.FrameBorderSize;

                    default:
                        throw new InvalidEnumArgumentException("Not a valid form border style.");
                }
            }
        }

        [Browsable(false)]
        public int FullBorderWidth
        {
            get { return this.FullBorderSize.Width; }
        }

        [Browsable(false)]
        public int FullBorderHeight
        {
            get { return this.FullBorderSize.Height; }
        }

        [Browsable(false)]
        public Size FullBorderSize
        {
            get { return this.BorderSize + this.BorderSize + new Size(0, this.CaptionHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinimumWidth
        {
            get { return this.MinimumSize.Width; }
            set { this.MinimumSize = new Size(value, this.MinimumHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinimumHeight
        {
            get { return this.MinimumSize.Height; }
            set { this.MinimumSize = new Size(this.MinimumWidth, value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MinimumClientSize
        {
            get { return new Size(this.MinimumWidth - this.FullBorderWidth, this.MinimumHeight - this.FullBorderHeight); }
            set { this.MinimumSize = new Size(value.Width + this.FullBorderWidth, value.Height + this.FullBorderHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinimumClientWidth
        {
            get { return this.MinimumClientSize.Width; }
            set { this.MinimumClientSize = new Size(value, this.MinimumClientHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinimumClientHeight
        {
            get { return this.MinimumClientSize.Height; }
            set { this.MinimumClientSize = new Size(this.MinimumClientWidth, value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaximumWidth
        {
            get { return this.MaximumSize.Width; }
            set { this.MaximumSize = new Size(value, this.MaximumHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaximumHeight
        {
            get { return this.MaximumSize.Height; }
            set { this.MaximumSize = new Size(this.MaximumWidth, value); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MaximumClientSize
        {
            get { return new Size(this.MaximumWidth - this.FullBorderWidth, this.MaximumHeight - this.FullBorderHeight); }
            set { this.MaximumSize = new Size(value.Width + this.FullBorderWidth, value.Height + this.FullBorderHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaximumClientWidth
        {
            get { return this.MaximumClientSize.Width; }
            set { this.MaximumClientSize = new Size(value, this.MaximumClientHeight); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaximumClientHeight
        {
            get { return this.MaximumClientSize.Height; }
            set { this.MaximumClientSize = new Size(this.MaximumClientWidth, value); }
        }
        #endregion

        #region Primary control data
        #region Data size
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PixelWidth
        {
            get { return this.primaryEditor.PixelWidth; }
            set { this.primaryEditor.PixelWidth = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PixelHeight
        {
            get { return this.primaryEditor.PixelHeight; }
            set { this.primaryEditor.PixelHeight = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size PixelSize
        {
            get { return this.primaryEditor.PixelSize; }
            set { this.primaryEditor.PixelSize = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ZoomWidth
        {
            get { return this.primaryEditor.ZoomWidth; }
            set { this.primaryEditor.ZoomWidth = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ZoomHeight
        {
            get { return this.primaryEditor.ZoomHeight; }
            set { this.primaryEditor.ZoomHeight = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size ZoomSize
        {
            get { return this.primaryEditor.ZoomSize; }
            set { this.primaryEditor.ZoomSize = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellWidth
        {
            get { return this.primaryEditor.CellWidth; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellHeight
        {
            get { return this.primaryEditor.CellHeight; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size CellSize
        {
            get { return this.primaryEditor.CellSize; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DataLength
        {
            get { return this.primaryEditor.NumCells; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DataWidth
        {
            get { return this.primaryEditor.DataWidth; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DataHeight
        {
            get { return this.primaryEditor.DataHeight; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size DataSize
        {
            get { return this.primaryEditor.DataSize; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Remainder
        {
            get { return this.primaryEditor.Remainder; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Aligned
        {
            get { return this.primaryEditor.Aligned; }
        }
        #endregion

        #region View info
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ViewWidth
        {
            get { return this.primaryEditor.ViewWidth; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ViewHeight
        {
            get { return this.primaryEditor.ViewHeight; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size ViewSize
        {
            get { return this.primaryEditor.ViewSize; }
            set { this.primaryEditor.ViewSize = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VisibleCells
        {
            get { return this.primaryEditor.VisibleCells; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartIndex
        {
            get { return this.primaryEditor.StartIndex; }
            set { this.primaryEditor.StartIndex = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartX
        {
            get { return this.primaryEditor.StartX; }
            set { this.primaryEditor.StartX = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int StartY
        {
            get { return this.primaryEditor.StartY; }
            set { this.primaryEditor.StartY = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point StartCoordinates
        {
            get { return this.primaryEditor.StartCoordinates; }
            set { this.primaryEditor.StartCoordinates = value; }
        }

        [Browsable(false)]
        public int EndIndex
        {
            get { return this.primaryEditor.EndIndex; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EndX
        {
            get { return this.primaryEditor.EndX; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EndY
        {
            get { return this.primaryEditor.EndY; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point EndCoordinates
        {
            get { return this.primaryEditor.EndCoordinates; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ViewRectangle
        {
            get { return this.primaryEditor.ViewRectangle; }
            set { this.primaryEditor.ViewRectangle = value; }
        }
        #endregion

        #region Selection info
        #region Current
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentIndex
        {
            get { return this.primaryEditor.CurrentIndex; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentX
        {
            get { return this.primaryEditor.CurrentX; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentY
        {
            get { return this.primaryEditor.CurrentY; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point CurrentCoordinates
        {
            get { return this.primaryEditor.CurrentCoordinates; }
        }
        #endregion

        #region Previous
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PreviousIndex
        {
            get { return this.primaryEditor.PreviousIndex; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PreviousX
        {
            get { return this.primaryEditor.PreviousX; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PreviousY
        {
            get { return this.primaryEditor.PreviousY; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point PreviousCoordinates
        {
            get { return this.primaryEditor.PreviousCoordinates; }
        }
        #endregion

        #region Min
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinIndex
        {
            get { return this.primaryEditor.MinIndex; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinX
        {
            get { return this.primaryEditor.MinX; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinY
        {
            get { return this.primaryEditor.MinY; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point MinCoordinates
        {
            get { return this.primaryEditor.MinCoordinates; }
        }
        #endregion

        #region Max
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaxIndex
        {
            get { return this.primaryEditor.MaxIndex; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaxX
        {
            get { return this.primaryEditor.MaxX; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaxY
        {
            get { return this.primaryEditor.MaxY; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point MaxCoordinates
        {
            get { return this.primaryEditor.MaxCoordinates; }
        }
        #endregion

        #region Selection
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionWidth
        {
            get { return this.primaryEditor.SelectionWidth; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionHeight
        {
            get { return this.primaryEditor.SelectionHeight; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size SelecetionSize
        {
            get { return this.primaryEditor.SelectionSize; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle SelectionRectangle
        {
            get { return this.primaryEditor.SelectionRectangle; }
        }
        #endregion
        #endregion
        #endregion
        #endregion

        #region Initializers
        public EditorForm()
        {
            this.KeyPreview = true;

            base.MouseWheel += new MouseEventHandler(EditorForm_MouseWheel);
        }
        #endregion

        #region Event methods
        protected override bool IsInputKey(Keys keyData)
        {
            if (this.overrideInputKeys != null)
                for (int i = this.overrideInputKeys.Length; --i >= 0; )
                    if (this.overrideInputKeys[i] == keyData)
                        return true;

            return base.IsInputKey(keyData);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.ignoreDialogKeys != null)
                for (int i = this.ignoreDialogKeys.Length; --i >= 0; )
                    if (this.ignoreDialogKeys[i] == keyData)
                        return true;

            return base.ProcessDialogKey(keyData);
        }

        private void EditorForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (MouseWheel != null)
                MouseWheel(this, e);
        }
        #endregion
    }
}