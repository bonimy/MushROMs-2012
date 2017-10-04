using System;
using System.IO;
using System.Windows.Forms;

namespace MushROMs
{
    public unsafe partial class NewProjectDialog : Form
    {
        public string ProjectDirectory
        {
            get { return this.fbcDirectory.Text; }
            set { this.fbcDirectory.Text = value; }
        }

        public string ProjectName
        {
            get { return this.tbxName.Text; }
            set { this.tbxName.Text = value; }
        }

        public NewProjectDialog()
        {
            InitializeComponent();
        }

        private void NewProjectDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.ProjectName == string.Empty)
                {
                    MessageBox.Show("Please provide a project name.", "Empty field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    char[] invalid = Path.GetInvalidFileNameChars();
                    for (int i = this.ProjectName.Length; --i >= 0 && !e.Cancel; )
                    {
                        for (int j = invalid.Length; --j >= 0; )
                        {
                            if (this.ProjectName[i] == invalid[j])
                            {
                                MessageBox.Show("A file path cannot contain the following characters:\n\t\\ / : * ? \" < > |", "Invalid path characters", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                e.Cancel = true;
                                break;
                            }
                        }
                    }
                }

                if (this.ProjectDirectory == string.Empty)
                {
                    MessageBox.Show("Please provide a project directory path.", "Empty field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else if (!Directory.Exists(this.ProjectDirectory))
                {
                    try
                    {
                        Directory.CreateDirectory(this.ProjectDirectory);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not create directory.\n" + ex.Message, "Invalid directory path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }

                if (!e.Cancel)
                {
                    if (File.Exists(Path.Combine(this.ProjectDirectory, this.ProjectName)))
                        if (MessageBox.Show("Project name already exists. Would you like to overwrite it?", "Overwrite file?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            e.Cancel = true;
                }
            }
        }
    }
}