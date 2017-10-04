using System;
using System.IO;
using System.Windows.Forms;

namespace MushROMs
{
    public partial class WelcomeDialog : Form
    {
        private const string BackupDirectory = "Backup";
        private const string BackupName = "Super Mario All-Stars (U) [!].smc";

        public string ROMPath
        {
            get { return this.ofcROM.Text; }
            set { this.ofcROM.Text = value; }
        }

        public WelcomeDialog()
        {
            InitializeComponent();
        }

        private void ofcROM_TextChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = File.Exists(this.ofcROM.Text);
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK && this.chkBackup.Checked)
            {
                string backup = Path.Combine(BackupDirectory, BackupName);
                Directory.CreateDirectory(BackupDirectory);
                if (File.Exists(backup))
                    File.Delete(backup);
                File.Copy(this.ROMPath, backup);
                this.ROMPath = backup;
            }
        }
    }
}