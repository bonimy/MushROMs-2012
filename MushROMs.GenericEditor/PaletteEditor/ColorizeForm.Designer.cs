namespace MushROMs.Controls
{
    partial class ColorizeForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkPreview = new System.Windows.Forms.CheckBox();
            this.chkColorize = new System.Windows.Forms.CheckBox();
            this.gbxMaster = new System.Windows.Forms.GroupBox();
            this.ttbEffective = new MushROMs.Controls.TextTrackBar();
            this.ttbLightness = new MushROMs.Controls.TextTrackBar();
            this.ttbSaturation = new MushROMs.Controls.TextTrackBar();
            this.ttbHue = new MushROMs.Controls.TextTrackBar();
            this.btnReset = new System.Windows.Forms.Button();
            this.gbxMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(185, 345);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(266, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkPreview
            // 
            this.chkPreview.AutoSize = true;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Enabled = false;
            this.chkPreview.Location = new System.Drawing.Point(12, 372);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(64, 17);
            this.chkPreview.TabIndex = 2;
            this.chkPreview.Text = "Preview";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.Visible = false;
            this.chkPreview.CheckedChanged += new System.EventHandler(this.chkPreview_CheckedChanged);
            // 
            // chkColorize
            // 
            this.chkColorize.AutoSize = true;
            this.chkColorize.Location = new System.Drawing.Point(12, 349);
            this.chkColorize.Name = "chkColorize";
            this.chkColorize.Size = new System.Drawing.Size(63, 17);
            this.chkColorize.TabIndex = 1;
            this.chkColorize.Text = "Colorize";
            this.chkColorize.UseVisualStyleBackColor = true;
            this.chkColorize.CheckedChanged += new System.EventHandler(this.chkColorize_CheckedChanged);
            // 
            // gbxMaster
            // 
            this.gbxMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxMaster.Controls.Add(this.ttbEffective);
            this.gbxMaster.Controls.Add(this.ttbLightness);
            this.gbxMaster.Controls.Add(this.ttbSaturation);
            this.gbxMaster.Controls.Add(this.ttbHue);
            this.gbxMaster.Location = new System.Drawing.Point(12, 12);
            this.gbxMaster.Name = "gbxMaster";
            this.gbxMaster.Size = new System.Drawing.Size(329, 327);
            this.gbxMaster.TabIndex = 0;
            this.gbxMaster.TabStop = false;
            // 
            // ttbEffective
            // 
            this.ttbEffective.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ttbEffective.Enabled = false;
            this.ttbEffective.LabelText = "Effectiveness:";
            this.ttbEffective.Location = new System.Drawing.Point(6, 250);
            this.ttbEffective.Maximum = 100;
            this.ttbEffective.MaxLength = 3;
            this.ttbEffective.Name = "ttbEffective";
            this.ttbEffective.Size = new System.Drawing.Size(317, 71);
            this.ttbEffective.TabIndex = 3;
            this.ttbEffective.TickFrequency = 5;
            this.ttbEffective.Value = 100;
            this.ttbEffective.ValueChanged += new System.EventHandler(this.ttb_ValueChanged);
            // 
            // ttbLightness
            // 
            this.ttbLightness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ttbLightness.LabelText = "Lightness:";
            this.ttbLightness.LargeChange = 10;
            this.ttbLightness.Location = new System.Drawing.Point(6, 173);
            this.ttbLightness.Maximum = 100;
            this.ttbLightness.MaxLength = 4;
            this.ttbLightness.Minimum = -100;
            this.ttbLightness.Name = "ttbLightness";
            this.ttbLightness.Size = new System.Drawing.Size(317, 71);
            this.ttbLightness.TabIndex = 2;
            this.ttbLightness.TickFrequency = 10;
            this.ttbLightness.ValueChanged += new System.EventHandler(this.ttb_ValueChanged);
            // 
            // ttbSaturation
            // 
            this.ttbSaturation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ttbSaturation.LabelText = "Saturation:";
            this.ttbSaturation.LargeChange = 10;
            this.ttbSaturation.Location = new System.Drawing.Point(6, 96);
            this.ttbSaturation.Maximum = 100;
            this.ttbSaturation.MaxLength = 4;
            this.ttbSaturation.Minimum = -100;
            this.ttbSaturation.Name = "ttbSaturation";
            this.ttbSaturation.Size = new System.Drawing.Size(317, 71);
            this.ttbSaturation.TabIndex = 1;
            this.ttbSaturation.TickFrequency = 10;
            this.ttbSaturation.ValueChanged += new System.EventHandler(this.ttb_ValueChanged);
            // 
            // ttbHue
            // 
            this.ttbHue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ttbHue.LabelText = "Hue:";
            this.ttbHue.LargeChange = 18;
            this.ttbHue.Location = new System.Drawing.Point(6, 19);
            this.ttbHue.Maximum = 180;
            this.ttbHue.MaxLength = 4;
            this.ttbHue.Minimum = -180;
            this.ttbHue.Name = "ttbHue";
            this.ttbHue.Size = new System.Drawing.Size(317, 71);
            this.ttbHue.TabIndex = 0;
            this.ttbHue.TickFrequency = 18;
            this.ttbHue.ValueChanged += new System.EventHandler(this.ttb_ValueChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(81, 345);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ColorizeForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(353, 401);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.chkPreview);
            this.Controls.Add(this.chkColorize);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbxMaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorizeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colorize Palette";
            this.gbxMaster.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxMaster;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkColorize;
        private System.Windows.Forms.CheckBox chkPreview;
        private TextTrackBar ttbHue;
        private TextTrackBar ttbSaturation;
        private TextTrackBar ttbLightness;
        private TextTrackBar ttbEffective;
        private System.Windows.Forms.Button btnReset;
    }
}