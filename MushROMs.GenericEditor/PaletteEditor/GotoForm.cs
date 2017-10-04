using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MushROMs.GenericEditor.PaletteEditor
{
    internal partial class GotoForm : Form
    {
        private const string ErrorAddressFomatUnknown = "Invalid address format selected.";
        private const string ErrorStartPositionUnknown = "Invalid start position selected.";

        private bool odd;
        private int dataSize;
        private int startIndex;
        private int currentIndex;

        public int Address
        {
            get { return this.ntbAddress.Value; }
            set { this.ntbAddress.Value = value; }
        }

        public GotoAddressFormats AddressFormat
        {
            get
            {
                if (this.rdbPC.Checked)
                    return GotoAddressFormats.PC;
                else if (this.rdbSNES.Checked)
                    return GotoAddressFormats.SNES;
                else
                {
                    this.rdbPC.Checked = true;
                    return GotoAddressFormats.PC;
                }
            }
            set
            {
                switch (value)
                {
                    case GotoAddressFormats.PC:
                        this.rdbPC.Checked = true;
                        break;
                    case GotoAddressFormats.SNES:
                        this.rdbSNES.Checked = true;
                        break;
                    default:
                        throw new InvalidEnumArgumentException(ErrorAddressFomatUnknown);
                }
            }
        }

        public GotoStartPositions GotoStartPosition
        {
            get
            {
                if (this.rdbBeginning.Checked)
                    return GotoStartPositions.BeginningOfFile;
                else if (this.rdbCurrent.Checked)
                    return GotoStartPositions.CurrentPosition;
                else if (this.rdbFirst.Checked)
                    return GotoStartPositions.FirstIndex;
                else
                {
                    this.rdbBeginning.Checked = true;
                    return GotoStartPositions.BeginningOfFile;
                }
            }
            set
            {
                switch (value)
                {
                    case GotoStartPositions.BeginningOfFile:
                        this.rdbBeginning.Checked = true;
                        break;
                    case GotoStartPositions.CurrentPosition:
                        this.rdbCurrent.Checked = true;
                        break;
                    case GotoStartPositions.FirstIndex:
                        this.rdbFirst.Checked = true;
                        break;
                    default:
                        throw new InvalidEnumArgumentException(ErrorStartPositionUnknown);
                }
            }
        }

        public GotoForm()
        {
            InitializeComponent();

            this.Address = GotoSettings.Default.Address;
            this.AddressFormat = GotoSettings.Default.AddressMode;
        }

        public DialogResult ShowDialog(int dataSize, int startIndex, int currentIndex, bool odd)
        {
            this.dataSize = dataSize;
            this.startIndex = startIndex;
            this.currentIndex = currentIndex;
            this.odd = odd;
            return this.ShowDialog();
        }
        public DialogResult ShowDialog(IWin32Window owner, int dataSize, int startIndex, int currentIndex, bool odd)
        {
            this.dataSize = dataSize;
            this.startIndex = startIndex;
            this.currentIndex = currentIndex;
            this.odd = odd;
            return this.ShowDialog(owner);
        }

        private void rdbBeginning_CheckedChanged(object sender, EventArgs e)
        {
            rdbPC.Enabled = rdbSNES.Enabled = rdbBeginning.Checked;
        }

        private void GotoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.GotoStartPosition == GotoStartPositions.BeginningOfFile)
                {
                    GotoSettings.Default.Address = this.Address;
                    GotoSettings.Default.AddressMode = this.AddressFormat;
                    GotoSettings.Default.Save();
                }
            }
        }

        private void GotoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                int max = this.Address;
                if (this.GotoStartPosition == GotoStartPositions.CurrentPosition)
                    max += this.currentIndex;
                else if (this.GotoStartPosition == GotoStartPositions.FirstIndex)
                    max += this.startIndex;
                max <<= 1;
                if (this.odd)
                    max += 1;

                if (max >= this.dataSize)
                {
                    MessageBox.Show("Address cannot exceed file size.", PaletteParent.DialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
        }

        public enum GotoAddressFormats
        {
            PC,
            SNES
        }

        public enum GotoStartPositions
        {
            BeginningOfFile,
            FirstIndex,
            CurrentPosition
        }
    }
}