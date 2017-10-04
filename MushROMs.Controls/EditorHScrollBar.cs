using System;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    public class EditorHScrollBar : HScrollBar
    {
        private const string ErrorScrollEnd = "Invalid Scroll End value.";

        private EditorControl ec;

        public EditorControl EditControl
        {
            get
            {
                return this.ec;
            }
            set
            {
                if (this.ec != null)
                    this.ec.StartIndexChanged -= ec_StartIndexChanged;
                if ((this.ec = value) != null)
                    this.ec.StartIndexChanged += new EventHandler(ec_StartIndexChanged);
            }
        }

        private int Extra
        {
            get
            {
                switch (this.ec.HorizontalScrollEnd)
                {
                    case ScrollEnd.Full:
                        return 0;
                    case ScrollEnd.Last:
                        return this.ec.ViewWidth - 2;
                    case ScrollEnd.None:
                        return this.ec.ViewWidth - 1;
                    default:
                        throw new ArgumentException(ErrorScrollEnd);
                }
            }
        }

        public void SetProperties()
        {
            if (this.ec.HorizontalScrollEnd == ScrollEnd.Full)
            {
                int range = this.ec.NumCells - this.ec.VisibleCells + this.Extra;
                this.Enabled = this.Visible = range > 0;
            }
            else
                this.Enabled = true;
            SetValue();

            this.ec.SetStartIndex(this.ec.StartIndex);
        }

        public void SetValue()
        {
            if (this.Enabled)
            {
                this.Maximum = this.ec.ViewWidth + this.Extra;
                this.LargeChange = this.ec.ViewWidth;
                this.Value = this.ec.StartIndex % this.ec.ViewWidth;
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            if (this.ec != null)
                if (se.NewValue != se.OldValue)
                    this.ec.SetStartIndex(this.ec.StartIndex + (se.NewValue - se.OldValue));

            base.OnScroll(se);
        }

        private void ec_StartIndexChanged(object sender, EventArgs e)
        {
            SetValue();
        }
    }
}