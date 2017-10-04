using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    [DefaultEvent("ColorValueChanged")]
    [DefaultProperty("SelectedColor")]
    [Description("Provides a control for representing and manipulating a color value.")]
    public unsafe class ColorPicker : DrawControl
    {
        private const int DefaultClientWidth = 0x10;
        private const int DefaultClientHeight = DefaultClientWidth;

        [Category("Editor")]
        [Description("Occurs when the selected color value of the control changes.")]
        public event EventHandler ColorValueChanged;

        private Color selectedColor;

        [Category("Editor")]
        [DefaultValue("Black")]
        [Description("The selected color for the control.")]
        public Color SelectedColor
        {
            get { return this.selectedColor; }
            set { SetSelectedColor(value); }
        }

        public ColorPicker()
        {
            this.ClientWidth = DefaultClientWidth;
            this.ClientHeight = DefaultClientHeight;

            this.selectedColor = Color.Black;
            this.Invalidate();
        }

        private void SetSelectedColor(Color value)
        {
            this.selectedColor = value;
            OnColorValueChanged(EventArgs.Empty);
        }

        protected override void OnClick(EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.FullOpen = true;
            dlg.Color = this.selectedColor;
            if (dlg.ShowDialog() == DialogResult.OK)
                SetSelectedColor(dlg.Color);

            base.OnClick(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnEnabledChanged(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == ' ' && !e.Handled)
                OnClick(EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int width = this.ClientWidth;
            int height = this.ClientHeight;
            int size = width * height;
            uint color = (uint)this.selectedColor.ToArgb();
            Graphics g = e.Graphics;

            if (!this.Enabled)
            {
                ExpandedColor x = this.selectedColor;
                color = (uint)x.LumaGrayScale().ToArgb();
            }

            uint* pixels = stackalloc uint[size];
            for (int i = size; --i >= 0; )
                pixels[i] = color;
            g.DrawImageUnscaled(new Bitmap(width, height, width * 4, PixelFormat.Format32bppRgb, (IntPtr)pixels), Point.Empty);

            if (this.Focused)
            {
                Pen p1 = new Pen(Color.Black, 1);
                p1.DashStyle = DashStyle.Dot;
                Pen p2 = new Pen(Color.White, 1);
                p2.DashStyle = DashStyle.Dot;
                p2.DashOffset = 1;
                Rectangle r = new Rectangle(0, 0, width - 1, height - 1);
                g.DrawRectangle(p1, r);
                g.DrawRectangle(p2, r);
            }

            base.OnPaint(e);
        }

        protected virtual void OnColorValueChanged(EventArgs e)
        {
            this.Invalidate();
            if (ColorValueChanged != null)
                ColorValueChanged(this, e);
        }
    }
}