namespace MushROMs.GenericEditor.PaletteEditor
{
    partial class GotoForm
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
            this.gbxAddress = new System.Windows.Forms.GroupBox();
            this.ntbAddress = new MushROMs.Controls.NumericTextBox();
            this.rdbSNES = new System.Windows.Forms.RadioButton();
            this.rdbPC = new System.Windows.Forms.RadioButton();
            this.gbxOptions = new System.Windows.Forms.GroupBox();
            this.rdbCurrent = new System.Windows.Forms.RadioButton();
            this.rdbFirst = new System.Windows.Forms.RadioButton();
            this.rdbBeginning = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxAddress.SuspendLayout();
            this.gbxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxAddress
            // 
            this.gbxAddress.Controls.Add(this.ntbAddress);
            this.gbxAddress.Controls.Add(this.rdbSNES);
            this.gbxAddress.Controls.Add(this.rdbPC);
            this.gbxAddress.Location = new System.Drawing.Point(12, 12);
            this.gbxAddress.Name = "gbxAddress";
            this.gbxAddress.Size = new System.Drawing.Size(112, 88);
            this.gbxAddress.TabIndex = 0;
            this.gbxAddress.TabStop = false;
            this.gbxAddress.Text = "Go to address";
            // 
            // ntbAddress
            // 
            this.ntbAddress.AllowNegative = false;
            this.ntbAddress.Hexadecimal = true;
            this.ntbAddress.Location = new System.Drawing.Point(6, 19);
            this.ntbAddress.MaxLength = 6;
            this.ntbAddress.Name = "ntbAddress";
            this.ntbAddress.Size = new System.Drawing.Size(100, 20);
            this.ntbAddress.TabIndex = 0;
            this.ntbAddress.Text = "0";
            // 
            // rdbSNES
            // 
            this.rdbSNES.AutoSize = true;
            this.rdbSNES.Enabled = false;
            this.rdbSNES.Location = new System.Drawing.Point(51, 65);
            this.rdbSNES.Name = "rdbSNES";
            this.rdbSNES.Size = new System.Drawing.Size(54, 17);
            this.rdbSNES.TabIndex = 2;
            this.rdbSNES.Text = "SNES";
            this.rdbSNES.UseVisualStyleBackColor = true;
            // 
            // rdbPC
            // 
            this.rdbPC.AutoSize = true;
            this.rdbPC.Checked = true;
            this.rdbPC.Enabled = false;
            this.rdbPC.Location = new System.Drawing.Point(6, 65);
            this.rdbPC.Name = "rdbPC";
            this.rdbPC.Size = new System.Drawing.Size(39, 17);
            this.rdbPC.TabIndex = 1;
            this.rdbPC.TabStop = true;
            this.rdbPC.Text = "PC";
            this.rdbPC.UseVisualStyleBackColor = true;
            // 
            // gbxOptions
            // 
            this.gbxOptions.Controls.Add(this.rdbCurrent);
            this.gbxOptions.Controls.Add(this.rdbFirst);
            this.gbxOptions.Controls.Add(this.rdbBeginning);
            this.gbxOptions.Location = new System.Drawing.Point(130, 12);
            this.gbxOptions.Name = "gbxOptions";
            this.gbxOptions.Size = new System.Drawing.Size(111, 88);
            this.gbxOptions.TabIndex = 1;
            this.gbxOptions.TabStop = false;
            this.gbxOptions.Text = "Start from ...";
            // 
            // rdbCurrent
            // 
            this.rdbCurrent.AutoSize = true;
            this.rdbCurrent.Location = new System.Drawing.Point(6, 42);
            this.rdbCurrent.Name = "rdbCurrent";
            this.rdbCurrent.Size = new System.Drawing.Size(97, 17);
            this.rdbCurrent.TabIndex = 2;
            this.rdbCurrent.Text = "current position";
            this.rdbCurrent.UseVisualStyleBackColor = true;
            // 
            // rdbFirst
            // 
            this.rdbFirst.AutoSize = true;
            this.rdbFirst.Location = new System.Drawing.Point(6, 65);
            this.rdbFirst.Name = "rdbFirst";
            this.rdbFirst.Size = new System.Drawing.Size(69, 17);
            this.rdbFirst.TabIndex = 1;
            this.rdbFirst.Text = "first index";
            this.rdbFirst.UseVisualStyleBackColor = true;
            // 
            // rdbBeginning
            // 
            this.rdbBeginning.AutoSize = true;
            this.rdbBeginning.Checked = true;
            this.rdbBeginning.Location = new System.Drawing.Point(6, 19);
            this.rdbBeginning.Name = "rdbBeginning";
            this.rdbBeginning.Size = new System.Drawing.Size(99, 17);
            this.rdbBeginning.TabIndex = 0;
            this.rdbBeginning.TabStop = true;
            this.rdbBeginning.Text = "beginning of file";
            this.rdbBeginning.UseVisualStyleBackColor = true;
            this.rdbBeginning.CheckedChanged += new System.EventHandler(this.rdbBeginning_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(85, 106);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Go";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(166, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // GotoForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(253, 141);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbxOptions);
            this.Controls.Add(this.gbxAddress);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GotoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Go To Address";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GotoForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GotoForm_FormClosed);
            this.gbxAddress.ResumeLayout(false);
            this.gbxAddress.PerformLayout();
            this.gbxOptions.ResumeLayout(false);
            this.gbxOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxAddress;
        private System.Windows.Forms.GroupBox gbxOptions;
        private System.Windows.Forms.RadioButton rdbSNES;
        private System.Windows.Forms.RadioButton rdbPC;
        private Controls.NumericTextBox ntbAddress;
        private System.Windows.Forms.RadioButton rdbCurrent;
        private System.Windows.Forms.RadioButton rdbFirst;
        private System.Windows.Forms.RadioButton rdbBeginning;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}