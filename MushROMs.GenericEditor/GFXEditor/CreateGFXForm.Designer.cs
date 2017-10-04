namespace MushROMs.GenericEditor.GFXEditor
{
    partial class CreateGFXForm
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
            this.gbxOptions = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudNumTiles = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxGraphicsType = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTiles)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxOptions
            // 
            this.gbxOptions.Controls.Add(this.cbxGraphicsType);
            this.gbxOptions.Controls.Add(this.label2);
            this.gbxOptions.Controls.Add(this.nudNumTiles);
            this.gbxOptions.Controls.Add(this.label1);
            this.gbxOptions.Location = new System.Drawing.Point(13, 13);
            this.gbxOptions.Name = "gbxOptions";
            this.gbxOptions.Size = new System.Drawing.Size(235, 71);
            this.gbxOptions.TabIndex = 0;
            this.gbxOptions.TabStop = false;
            this.gbxOptions.Text = "Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Tiles: 0x";
            // 
            // nudNumTiles
            // 
            this.nudNumTiles.Hexadecimal = true;
            this.nudNumTiles.Location = new System.Drawing.Point(108, 18);
            this.nudNumTiles.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.nudNumTiles.Maximum = new decimal(new int[] {
            8388608,
            0,
            0,
            0});
            this.nudNumTiles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumTiles.Name = "nudNumTiles";
            this.nudNumTiles.Size = new System.Drawing.Size(92, 20);
            this.nudNumTiles.TabIndex = 2;
            this.nudNumTiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNumTiles.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "GraphicsFormat: ";
            // 
            // comboBox1
            // 
            this.cbxGraphicsType.FormattingEnabled = true;
            this.cbxGraphicsType.Items.AddRange(new object[] {
            "SNES 1BPP",
            "SNES 2BPP",
            "SNES 3BPP",
            "SNES 4BPP",
            "SNES 5BPP",
            "SNES 6BPP",
            "SNES 7BPP",
            "SNES 8BPP",
            "Mode 7 8BPP",
            "GBA 4BPP"});
            this.cbxGraphicsType.Location = new System.Drawing.Point(108, 44);
            this.cbxGraphicsType.Name = "comboBox1";
            this.cbxGraphicsType.Size = new System.Drawing.Size(121, 21);
            this.cbxGraphicsType.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(92, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(173, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // CreateGFXForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(260, 125);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbxOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateGFXForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Create New GFX";
            this.gbxOptions.ResumeLayout(false);
            this.gbxOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumTiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxGraphicsType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudNumTiles;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}