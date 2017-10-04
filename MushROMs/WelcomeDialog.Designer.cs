namespace MushROMs
{
    partial class WelcomeDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxROM = new System.Windows.Forms.GroupBox();
            this.ofcROM = new MushROMs.Controls.OpenFileControl();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkBackup = new System.Windows.Forms.CheckBox();
            this.gbxROM.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxROM
            // 
            this.gbxROM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxROM.Controls.Add(this.ofcROM);
            this.gbxROM.Location = new System.Drawing.Point(13, 13);
            this.gbxROM.Name = "gbxROM";
            this.gbxROM.Size = new System.Drawing.Size(367, 48);
            this.gbxROM.TabIndex = 0;
            this.gbxROM.TabStop = false;
            this.gbxROM.Text = "Base U.S. Super Mario All-Stars ROM";
            // 
            // ofcROM
            // 
            this.ofcROM.AddExtension = true;
            this.ofcROM.AllowManualEdit = true;
            this.ofcROM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ofcROM.CheckFileExists = true;
            this.ofcROM.CheckPathExists = true;
            this.ofcROM.DefaultExt = "smc";
            this.ofcROM.DereferenceLinks = true;
            this.ofcROM.DialogAutoUpgradeEnabled = true;
            this.ofcROM.DialogTitle = "Select the base Super Mario All-Stars ROM that MushROMs will use.";
            this.ofcROM.FileName = "";
            this.ofcROM.Filter = "SNES ROM Images|*.smc;*.sfc;*.fig|All Files|*.*";
            this.ofcROM.FilterIndex = 1;
            this.ofcROM.InitialDirectory = "";
            this.ofcROM.Location = new System.Drawing.Point(6, 19);
            this.ofcROM.Multiselect = false;
            this.ofcROM.Name = "ofcROM";
            this.ofcROM.RestoreDirectory = false;
            this.ofcROM.ShowDialogHelp = false;
            this.ofcROM.ShowReadOnly = false;
            this.ofcROM.Size = new System.Drawing.Size(355, 23);
            this.ofcROM.SupportMultiDottedExtension = false;
            this.ofcROM.TabIndex = 0;
            this.ofcROM.ValidateNames = true;
            this.ofcROM.TextChanged += new System.EventHandler(this.ofcROM_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(224, 67);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(305, 67);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkBackup
            // 
            this.chkBackup.AutoSize = true;
            this.chkBackup.Checked = true;
            this.chkBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBackup.Location = new System.Drawing.Point(13, 71);
            this.chkBackup.Name = "chkBackup";
            this.chkBackup.Size = new System.Drawing.Size(200, 17);
            this.chkBackup.TabIndex = 1;
            this.chkBackup.Text = "Backup ROM to application directory";
            this.chkBackup.UseVisualStyleBackColor = true;
            // 
            // WelcomeDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(392, 102);
            this.Controls.Add(this.chkBackup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbxROM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to MushROMs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Welcome_FormClosing);
            this.gbxROM.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxROM;
        private Controls.OpenFileControl ofcROM;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkBackup;

    }
}