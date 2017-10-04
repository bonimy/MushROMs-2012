using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MushROMs.Controls
{
    /// <summary>
    /// Represents a Windows track bar accompanied with a text box showing the current value.
    /// </summary>
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [DefaultBindingProperty("Value")]
    [Designer(typeof(TrackBarDesigner))] //"System.Windows.Forms.Design.TrackBarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    public partial class TextTrackBar : UserControl
    {
        [Category("Property Changed")]
        [Description("Occurs when the AutoSize property has changed.")]
        public new event EventHandler AutoSizeChanged;

        [Category("Behavior")]
        [Description("Occurs when either a mouse or keyboard action moves the scroll box.")]
        public new event EventHandler Scroll;

        [Category("Action")]
        [Description("Occurs when the value of the control changes.")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the
        /// scroll box on the track bar.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// The assigned value is less than the value of <see cref="Minimum"/>.
        ///  -or- The assigned value is greater than the value of <see cref="Maximum"/>.
        /// </exception>
        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("The position of the slider.")]
        public int Value
        {
            get { return this.trkValue.Value; }
            set { this.trkValue.Value = value; }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the <see cref="TextTrackBar.Value"/>
        /// property when the scroll box is moved a small distance.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// The assigned value is less than 0.
        /// </exception>
        [Category("Appearance")]
        [DefaultValue(1)]
        [Description("The number of positions the slider moves in response to keyboard input (arrow keys).")]
        public int SmallChange
        {
            get { return this.trkValue.SmallChange; }
            set { this.trkValue.SmallChange = value; }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the <see cref="TextTrackBar.Value"/>
        /// property when the scroll box is moved a large distance.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// The assigned value is less than 0.
        /// </exception>
        [Category("Behavior")]
        [DefaultValue(5)]
        [Description("The number of positions the slider moves in response to mouse clicks or the PAGE UP and PAGE DOWN keys.")]
        public int LargeChange
        {
            get { return this.trkValue.LargeChange; }
            set { this.trkValue.LargeChange = value; }
        }

        /// <summary>
        /// Gets or sets the lower limit of the range this <see cref="TextTrackBar.Value"/>
        /// is working with.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("The minimum position for the position of the slider of the trackbar.")]
        public int Minimum
        {
            get { return this.trkValue.Minimum; }
            set { this.trkValue.Minimum = value; }
        }

        /// <summary>
        /// Gets or sets the upper limit of the range this <see cref="TextTrackBar.Value"/>
        /// is working with.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(10)]
        [Description("The maximum position for the position of the slider of the trackbar.")]
        public int Maximum
        {
            get { return this.trkValue.Maximum; }
            set { this.trkValue.Maximum = value; }
        }

        /// <summary>
        /// Gets or sets a value that specifies the delta between ticks drawn on the
        /// control.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(1)]
        [Description("The number of positions between tick marks.")]
        public int TickFrequency
        {
            get { return this.trkValue.TickFrequency; }
            set { this.trkValue.TickFrequency = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating how to display the tick marks on the track
        /// bar.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">
        /// The assigned value is not a valid <see cref="TickStyle"/>.
        /// </exception>
        [Category("Appearance")]
        [DefaultValue(TickStyle.BottomRight)]
        [Description("Indicates where the ticks appear on the TrackBar.")]
        public TickStyle TickStyle
        {
            get { return this.trkValue.TickStyle; }
            set { this.trkValue.TickStyle = value; }
        }

        [Category("Appearance")]
        [DefaultValue(Orientation.Horizontal)]
        [Description("The orientation of the control.")]
        [Localizable(true)]
        public Orientation Orientation
        {
            get { return this.trkValue.Orientation; }
            set { this.trkValue.Orientation = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the height or width of the track
        /// bar is being automatically sized.
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Indicates whether the control wil resize itself automatically based on a computation of the default scroll bar dimensions.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new bool AutoSize
        {
            get { return this.trkValue.AutoSize; }
            set { this.trkValue.AutoSize = base.AutoSize = value; }
        }
        
        [Browsable(true)]
        [Category("Appearance")]
        public string LabelText
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters the user can type or paste
        /// into the text box control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value assigned to the property is less than 0.
        /// </exception>
        [Category("Behavior")]
        [DefaultValue(32767)]
        [Description("Specifies the maximum number of characters that can be entered into the edit control.")]
        public int MaxLength
        {
            get { return this.ntbValue.MaxLength; }
            set { this.ntbValue.MaxLength = value; }
        }

        /// <summary>
        /// Initializes a new instant of the <see cref="TextTrackBar"/> class.
        /// </summary>
        public TextTrackBar()
        {
            InitializeComponent();

            this.LabelText = "TextTrackBar";
            this.Value = this.Minimum;
            this.trkValue.AutoSizeChanged += new EventHandler(trkValue_AutoSizeChanged);
            this.trkValue.Scroll += new EventHandler(trkValue_Scroll);
        }

        /// <summary>
        /// Overrides the <see cref="Control.SetBoundsCore"/> to enfore autoSize
        /// </summary>
        /// <param name="x">
        /// The new <see cref="Control.Left"/> property value of the control.
        /// </param>
        /// <param name="y">
        /// The new <see cref="Control.Right"/> property value of the control.
        /// </param>
        /// <param name="width">
        /// The new <see cref="Control.Width"/> property value of the control.
        /// </param>
        /// <param name="height">
        /// The new <see cref="Control.Height"/> property value of the control.
        /// </param>
        /// <param name="specified">
        /// A bitwise combination of the <see cref="BoundsSpecified"/> values.
        /// </param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, 71, specified);
        }

        /// <summary>
        /// Raises the <see cref="ValueChanged"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Scroll"/> event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected virtual void OnScroll(EventArgs e)
        {
            if (Scroll != null)
                Scroll(this, e);
        }

        /// <summary>
        /// Sets the minimum and maximum values for a <see cref="TextTrackBar.Value"/>.
        /// </summary>
        /// <param name="minValue">
        /// The lower limit of the range of the track bar.
        /// </param>
        /// <param name="maxValue">
        /// The upper limit of the range of the track bar.
        /// </param>
        public void SetRange(int minValue, int maxValue)
        {
            this.trkValue.SetRange(minValue, maxValue);
        }

        private void trkValue_ValueChanged(object sender, EventArgs e)
        {
            this.ntbValue.Value = this.trkValue.Value;
            OnValueChanged(EventArgs.Empty);
        }

        private void trkValue_AutoSizeChanged(object sender, EventArgs e)
        {
            if (AutoSizeChanged != null)
                AutoSizeChanged(this, e);
        }

        private void trkValue_Scroll(object sender, EventArgs e)
        {
            OnScroll(e);
        }

        private void ntbValue_ValueChanged(object sender, EventArgs e)
        {
            if (this.ntbValue.Value >= this.trkValue.Minimum && this.ntbValue.Value <= this.trkValue.Maximum)
                this.trkValue.Value = this.ntbValue.Value;
        }
    }
}