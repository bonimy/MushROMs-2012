namespace MushROMs
{
    partial class NewProjectDialog
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
            this.gbxName = new System.Windows.Forms.GroupBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbxDirectory = new System.Windows.Forms.GroupBox();
            this.fbcDirectory = new MushROMs.Controls.FolderBrowserControl();
            this.gbxName.SuspendLayout();
            this.gbxDirectory.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxName
            // 
            this.gbxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxName.Controls.Add(this.tbxName);
            this.gbxName.Location = new System.Drawing.Point(12, 66);
            this.gbxName.Name = "gbxName";
            this.gbxName.Size = new System.Drawing.Size(199, 48);
            this.gbxName.TabIndex = 2;
            this.gbxName.TabStop = false;
            this.gbxName.Text = "Project Name";
            // 
            // tbxName
            // 
            this.tbxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxName.Location = new System.Drawing.Point(6, 19);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(187, 20);
            this.tbxName.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(298, 83);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(217, 83);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // gbxDirectory
            // 
            this.gbxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDirectory.Controls.Add(this.fbcDirectory);
            this.gbxDirectory.Location = new System.Drawing.Point(12, 12);
            this.gbxDirectory.Name = "gbxDirectory";
            this.gbxDirectory.Size = new System.Drawing.Size(361, 48);
            this.gbxDirectory.TabIndex = 1;
            this.gbxDirectory.TabStop = false;
            this.gbxDirectory.Text = "Project Directory";
            // 
            // fbcDirectory
            // 
            this.fbcDirectory.AllowManualEdit = true;
            this.fbcDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fbcDirectory.Description = "Select the primary directory of the project.";
            this.fbcDirectory.Location = new System.Drawing.Point(6, 19);
            this.fbcDirectory.Name = "fbcDirectory";
            this.fbcDirectory.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.fbcDirectory.SelectedPath = "";
            this.fbcDirectory.ShowNewFolderButton = true;
            this.fbcDirectory.Size = new System.Drawing.Size(349, 23);
            this.fbcDirectory.TabIndex = 0;
            // 
            // NewProjectDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(385, 125);
            this.Controls.Add(this.gbxDirectory);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New MushROMs Project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewProjectDialog_FormClosing);
            this.gbxName.ResumeLayout(false);
            this.gbxName.PerformLayout();
            this.gbxDirectory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbxDirectory;
        private Controls.FolderBrowserControl fbcDirectory;
        private System.Windows.Forms.TextBox tbxName;
    }
}