using System;
using System.Drawing;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNESLibrary;

namespace MushROMs.GenericEditor.PaletteEditor
{
    internal unsafe partial class CreatePaletteForm : Form
    {
        private const int DefaultNumColors = 0x100;

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public int NumColors
        {
            get { return (int)this.nudNumColors.Value; }
            set { this.nudNumColors.Value = value; }
        }

        public bool EnableCopyOption
        {
            get { return this.chkFromCopy.Enabled; }
            set { this.chkFromCopy.Enabled = value; }
        }

        public bool CopyFrom
        {
            get { return this.chkFromCopy.Enabled && this.chkFromCopy.Checked; }
            set { this.chkFromCopy.Checked = value; }
        }

        public CreatePaletteForm()
        {
            InitializeComponent();
            Reset();
        }

        public void Reset()
        {
            this.NumColors = DefaultNumColors;
        }

        private void chkFromCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.gbxOptions.Enabled = !chkFromCopy.Checked;
        }
    }
}