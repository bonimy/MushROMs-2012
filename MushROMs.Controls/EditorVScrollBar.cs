using System;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    public class EditorVScrollBar : VScrollBar
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
                this.ec = value;
                this.ec.StartIndexChanged += new EventHandler(ec_StartIndexChanged);
            }
        }

        private int Extra
        {
            get
            {
                switch (this.ec.VerticalScrollEnd)
                {
                    case ScrollEnd.Full:
                        return 0;
                    case ScrollEnd.Last:
                        return this.ec.ViewHeight - 1;
                    case ScrollEnd.None:
                        return this.ec.ViewHeight;
                    default:
                        throw new ArgumentException(ErrorScrollEnd);
                }
            }
        }

        public void SetProperties()
        {
            if (this.ec.VerticalScrollEnd == ScrollEnd.Full)
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
                int value = this.ec.StartIndex / this.ec.ViewWidth;
                int remainig = this.ec.NumCells - this.ec.EndIndex - 1;

                this.LargeChange = this.ec.ViewHeight;
                this.Maximum = this.ec.ViewHeight + value + (remainig / this.ec.ViewWidth) - 1 + this.Extra;
                this.Value = value;
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            if (this.ec != null)
                if (se.NewValue != se.OldValue)
                    this.ec.SetStartIndex(this.ec.StartIndex + this.ec.ViewWidth * (se.NewValue - se.OldValue));

            base.OnScroll(se);
        }

        private void ec_StartIndexChanged(object sender, EventArgs e)
        {
            SetValue();
        }
    }
}
