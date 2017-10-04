using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    internal partial class UnsavedForm : Form
    {
        internal List<string> Files
        {
            get
            {
                List<string> files = new List<string>();
                for (int i = 0; i < this.lbxFiles.Items.Count; i++)
                    files.Add((string)this.lbxFiles.Items[i]);
                return files;
            }
            set
            {
                this.lbxFiles.Items.Clear();
                this.lbxFiles.Items.AddRange(value.ToArray());
            }
        }

        internal UnsavedForm()
        {
            InitializeComponent();
        }
    }
}
