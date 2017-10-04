using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    [DefaultEvent("Paint")]
    [DefaultProperty("ClientSize")]
    [Description("Provides a control with predefined settings for rendering images.")]
    public class DrawControl : UserControl
    {
        public event EventHandler BorderStyleChanged;

        [Browsable(true)]   // This accessor is redone so it can be browsable in the designer.
        [Category("Editor")]
        [DefaultValue(true)]
        [Description("A value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new bool DoubleBuffered
        {
            get { return base.DoubleBuffered; }
            set { base.DoubleBuffered = value; }
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderWidth
        {
            get { return this.BorderSize.Width; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderHeight
        {
            get { return this.BorderSize.Height; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size BorderSize
        {
            get
            {
                switch (this.BorderStyle)
                {
                    case BorderStyle.None:
                        return Size.Empty;

                    case BorderStyle.FixedSingle:
                        return SystemInformation.BorderSize;

                    case BorderStyle.Fixed3D:
                        return SystemInformation.Border3DSize;

                    default:
                        throw new InvalidEnumArgumentException("Not a valid border style.");
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FullBorderWidth
        {
            get { return this.FullBorderSize.Width; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FullBorderHeight
        {
            get { return this.FullBorderSize.Height; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size FullBorderSize
        {
            get { return this.BorderSize + this.BorderSize; }
        }

        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; OnBorderStyleChanged(EventArgs.Empty); }
        }

        public DrawControl()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Magenta;     // A nice, annoying color to remind us that this control is intended to be drawn on.
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = Padding.Empty;    // Most draw controls are placed with no margins desired.

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected virtual void OnBorderStyleChanged(EventArgs e)
        {
            if (BorderStyleChanged != null)
                BorderStyleChanged(this, e);
        }
    }
}