namespace MushROMs.GenericEditor.PaletteEditor
{
    partial class CreatePaletteForm
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
            this.lblNumColors = new System.Windows.Forms.Label();
            this.nudNumColors = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkFromCopy = new System.Windows.Forms.CheckBox();
            this.gbxOptions = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumColors)).BeginInit();
            this.gbxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumColors
            // 
            this.lblNumColors.AutoSize = true;
            this.lblNumColors.Location = new System.Drawing.Point(6, 19);
            this.lblNumColors.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.lblNumColors.Name = "lblNumColors";
            this.lblNumColors.Size = new System.Drawing.Size(104, 13);
            this.lblNumColors.TabIndex = 0;
            this.lblNumColors.Text = "Number of colors: 0x";
            // 
            // nudNumColors
            // 
            this.nudNumColors.Hexadecimal = true;
            this.nudNumColors.Location = new System.Drawing.Point(110, 17);
            this.nudNumColors.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.nudNumColors.Maximum = new decimal(new int[] {
            8388608,
            0,
            0,
            0});
            this.nudNumColors.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumColors.Name = "nudNumColors";
            this.nudNumColors.Size = new System.Drawing.Size(92, 20);
            this.nudNumColors.TabIndex = 1;
            this.nudNumColors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNumColors.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(64, 84);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(145, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkFromCopy
            // 
            this.chkFromCopy.AutoSize = true;
            this.chkFromCopy.Location = new System.Drawing.Point(12, 61);
            this.chkFromCopy.Name = "chkFromCopy";
            this.chkFromCopy.Size = new System.Drawing.Size(188, 17);
            this.chkFromCopy.TabIndex = 1;
            this.chkFromCopy.Text = "Create new palette from copy data";
            this.chkFromCopy.UseVisualStyleBackColor = true;
            this.chkFromCopy.CheckedChanged += new System.EventHandler(this.chkFromCopy_CheckedChanged);
            // 
            // gbxOptions
            // 
            this.gbxOptions.Controls.Add(this.lblNumColors);
            this.gbxOptions.Controls.Add(this.nudNumColors);
            this.gbxOptions.Location = new System.Drawing.Point(12, 12);
            this.gbxOptions.Name = "gbxOptions";
            this.gbxOptions.Size = new System.Drawing.Size(208, 43);
            this.gbxOptions.TabIndex = 0;
            this.gbxOptions.TabStop = false;
            this.gbxOptions.Text = "Options";
            // 
            // CreatePaletteForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(232, 119);
            this.Controls.Add(this.gbxOptions);
            this.Controls.Add(this.chkFromCopy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatePaletteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Palette";
            ((System.ComponentModel.ISupportInitialize)(this.nudNumColors)).EndInit();
            this.gbxOptions.ResumeLayout(false);
            this.gbxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumColors;
        private System.Windows.Forms.NumericUpDown nudNumColors;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkFromCopy;
        private System.Windows.Forms.GroupBox gbxOptions;

    }
}