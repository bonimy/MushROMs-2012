using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [Description("A text box that only accepts numbered values.")]
    public class NumericTextBox : TextBox
    {
        public event EventHandler ValueChanged;

        private bool hex;
        private bool neg;
        private int value;

        [Category("Editor")]
        [DefaultValue(false)]
        [Description("Determines whether the control reads hexadecimal values or decimal.")]
        public bool Hexadecimal
        {
            get { return this.hex; }
            set
            {
                this.hex = value;
                this.Text = this.value.ToString(value ? "X" : string.Empty);
            }
        }

        [Category("Editor")]
        [DefaultValue(true)]
        [Description("Determines whether negative numbers are valid input.")]
        public bool AllowNegative
        {
            get { return this.neg; }
            set
            {
                this.neg = value;
                if (!value && this.value < 0)
                {
                    this.value *= -1;
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Editor")]
        [DefaultValue(0)]
        [Description("The value written to the text box.")]
        public int Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                this.Text = value.ToString(hex ? "X" : string.Empty);
                OnValueChanged(EventArgs.Empty);
            }
        }

        [Category("Behavior")]
        [DefaultValue(CharacterCasing.Upper)]
        [Description("Indicates if all characters should be left alone or converted to uppercase or lowercase.")]
        public new CharacterCasing CharacterCasing
        {
            get { return base.CharacterCasing; }
            set { base.CharacterCasing = value; }
        }

        public NumericTextBox()
        {
            this.hex = false;
            this.neg = true;
            this.value = 0;
            this.Text = "0";
            this.CharacterCasing = CharacterCasing.Upper;
        }

        protected void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.Handled)
                return;

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                return;
            else if (neg && e.KeyChar == '-')
                return;
            else if (hex && e.KeyChar >= 'a' && e.KeyChar <= 'f')
                return;
            else if (hex && e.KeyChar >= 'A' && e.KeyChar <= 'F')
                return;
            else if (e.KeyChar == '\b')
                return;
            else
                e.Handled = true;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            int old = this.value;
            int.TryParse(this.Text, this.hex ? NumberStyles.AllowHexSpecifier : this.neg ? NumberStyles.AllowLeadingSign : NumberStyles.None, CultureInfo.InvariantCulture, out this.value);
            if (this.value != old)
                OnValueChanged(EventArgs.Empty);

            base.OnTextChanged(e);
        }
    }
}