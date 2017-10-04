using System;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    internal partial class GrayscaleForm : Form
    {
        public event EventHandler ColorValueChanged;

        private bool runEvent;

        public int Red
        {
            get { return this.ttbRed.Value; }
        }

        public int Green
        {
            get { return this.ttbGreen.Value; }
        }

        public int Blue
        {
            get { return this.ttbBlue.Value; }
        }

        public bool Preview
        {
            get { return this.chkPreview.Checked; }
        }

        public GrayscaleForm()
        {
            InitializeComponent();

            this.runEvent = true;
        }

        protected virtual void OnColorValueChanged(EventArgs e)
        {
            if (ColorValueChanged != null)
                ColorValueChanged(this, e);
        }

        private void ttb_ValueChanged(object sender, EventArgs e)
        {
            if (this.runEvent)
                OnColorValueChanged(e);
        }

        private void btnLuma_Click(object sender, EventArgs e)
        {
            this.runEvent = false;
            this.ttbRed.Value = (int)(ExpandedColor.LumaRedWeight * 100);
            this.ttbGreen.Value = (int)(ExpandedColor.LumaGreenWeight * 100);
            this.ttbBlue.Value = (int)(ExpandedColor.LumaBlueWeight * 100);
            this.runEvent = true;

            OnColorValueChanged(e);
        }
    }
}