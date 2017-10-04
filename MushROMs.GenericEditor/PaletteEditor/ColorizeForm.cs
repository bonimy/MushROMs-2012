using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    [DesignTimeVisible(false)]
    public partial class ColorizeForm : Form
    {
        public event EventHandler ColorValueChanged;

        private const int DefaultHue = 0;
        private const int DefaultSaturation = 0;
        private const int DefaultLuminosity = 0;
        private const int DefaultCHue = 0;
        private const int DefaultCSaturation = 25;
        private const int DefaultCLuminosity = 50;
        private const int DefaultCEffectiveness = 100;

        private int hue, sat, lum, cHue, cSat, cLum;
        private bool runEvent;

        public int Hue
        {
            get { return this.ttbHue.Value; }
            private set { this.ttbHue.Value = value; }
        }

        public int Saturation
        {
            get { return this.ttbSaturation.Value; }
            private set { this.ttbSaturation.Value = value; }
        }

        public int Luminosity
        {
            get { return this.ttbLightness.Value; }
            private set { this.ttbLightness.Value = value; }
        }

        public int Effectiveness
        {
            get { return this.ttbEffective.Value; }
            private set { this.ttbEffective.Value = value; }
        }

        public bool Colorize
        {
            get { return this.chkColorize.Checked; }
            private set { this.chkColorize.Checked = value; }
        }

        public bool Preview
        {
            get { return this.chkPreview.Checked; }
            set { this.chkPreview.Checked = value; }
        }

        public ColorizeForm()
        {
            InitializeComponent();

            this.hue = DefaultHue;
            this.sat = DefaultSaturation;
            this.lum = DefaultLuminosity;
            this.cHue = DefaultCHue;
            this.cSat = DefaultCSaturation;
            this.cLum = DefaultCLuminosity;

            ResetValues();

            this.runEvent = true;
        }

        private void ResetValues()
        {
            this.runEvent = false;

            if (this.Colorize)
            {
                this.Hue = DefaultCHue;
                this.Saturation = DefaultCSaturation;
                this.Luminosity = DefaultCLuminosity;
                this.Effectiveness = DefaultCEffectiveness;
            }
            else
            {
                this.Hue = DefaultHue;
                this.Saturation = DefaultSaturation;
                this.Luminosity = DefaultLuminosity;
            }

            this.runEvent = true;
            this.btnReset.Enabled = false;
            OnColorValueChanged(EventArgs.Empty);
        }

        private void SwitchValues()
        {
            this.ttbEffective.Enabled = this.Colorize;
            this.runEvent = false;   //Prevents OnColorValueChanged event.

            if (this.Colorize)
            {
                this.hue = this.Hue;
                this.sat = this.Saturation;
                this.lum = this.Luminosity;

                this.ttbHue.Minimum = 0;
                this.ttbHue.Maximum = 360;
                this.ttbSaturation.Minimum = this.ttbLightness.Minimum = 0;
                this.ttbSaturation.Maximum = this.ttbLightness.Maximum = 100;
                this.ttbSaturation.TickFrequency = this.ttbLightness.TickFrequency = 5;

                this.Hue = this.cHue;
                this.Saturation = this.cSat;
                this.Luminosity = this.cLum;
            }
            else
            {
                this.cHue = this.Hue;
                this.cSat = this.Saturation;
                this.cLum = this.Luminosity;

                this.ttbHue.Minimum = -180;
                this.ttbHue.Maximum = 180;
                this.ttbSaturation.Minimum = this.ttbLightness.Minimum = -100;
                this.ttbSaturation.Maximum = this.ttbLightness.Maximum = 100;
                this.ttbSaturation.TickFrequency = this.ttbLightness.TickFrequency = 10;

                this.Hue = this.hue;
                this.Saturation = this.sat;
                this.Luminosity = this.lum;
            }

            this.runEvent = true;
            OnColorValueChanged(EventArgs.Empty);
        }

        protected virtual void OnColorValueChanged(EventArgs e)
        {
            if (ColorValueChanged != null)
                ColorValueChanged(this, e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void chkColorize_CheckedChanged(object sender, EventArgs e)
        {
            SwitchValues();
        }

        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            OnColorValueChanged(e);
        }

        private void ttb_ValueChanged(object sender, EventArgs e)
        {
            this.btnReset.Enabled = true;
            if (this.runEvent)
                OnColorValueChanged(e);
        }
    }
}