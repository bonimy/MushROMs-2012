namespace MushROMs.Controls
{
    partial class GrayscaleForm
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
            this.gbxValues = new System.Windows.Forms.GroupBox();
            this.ttbBlue = new MushROMs.Controls.TextTrackBar();
            this.ttbGreen = new MushROMs.Controls.TextTrackBar();
            this.ttbRed = new MushROMs.Controls.TextTrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.gbxValues.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(185, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(266, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkPreview
            // 
            this.chkPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPreview.AutoSize = true;
            this.chkPreview.Checked = true;
            this.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreview.Enabled = false;
            this.chkPreview.Location = new System.Drawing.Point(12, 272);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(64, 17);
            this.chkPreview.TabIndex = 2;
            this.chkPreview.Text = "Preview";
            this.chkPreview.UseVisualStyleBackColor = true;
            this.chkPreview.Visible = false;
            // 
            // gbxValues
            // 
            this.gbxValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxValues.Controls.Add(this.ttbBlue);
            this.gbxValues.Controls.Add(this.ttbGreen);
            this.gbxValues.Controls.Add(this.ttbRed);
            this.gbxValues.Location = new System.Drawing.Point(12, 12);
            this.gbxValues.Name = "gbxValues";
            this.gbxValues.Size = new System.Drawing.Size(329, 250);
            this.gbxValues.TabIndex = 0;
            this.gbxValues.TabStop = false;
            // 
            // ttbBlue
            // 
            this.ttbBlue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ttbBlue.LabelText = "Blue:";
            this.ttbBlue.Location = new System.Drawing.Point(6, 173);
            this.ttbBlue.Maximum = 100;
            this.ttbBlue.MaxLength = 3;
            this.ttbBlue.Name = "ttbBlue";
            this.ttbBlue.Size = new System.Drawing.Size(317, 71);
            this.ttbBlue.TabIndex = 2;
            this.ttbBlue.TickFrequency = 5;
            this.ttbBlue.Value = 100;
            this.ttbBlue.ValueChanged += new System.EventHandler(this.ttb_ValueChanged);
            // 
            // ttbGreen
            // 
            this.ttbGreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ttbGreen.LabelText = "Green:";
            this.ttbGreen.Location = new System.Drawing.Point(6, 96);
            this.ttbGreen.Maximum = 100;
            this.ttbGreen.MaxLength = 3;
            this.ttbGreen.Name = "ttbGreen";
            this.ttbGreen.Size = new System.Drawing.Size(317, 71);
            this.ttbGreen.TabIndex = 1;
            this.ttbGreen.TickFrequency = 5;
            this.ttbGreen.Value = 100;
            this.ttbGreen.ValueChanged += new System.EventHandler(this.ttb_ValueChanged);
            // 
            // ttbRed
            // 
            this.ttbRed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ttbRed.LabelText = "Red:";
            this.ttbRed.Location = new System.Drawing.Point(6, 19);
            this.ttbRed.Maximum = 100;
            this.ttbRed.MaxLength = 3;
            this.ttbRed.Name = "ttbRed";
            this.ttbRed.Size = new System.Drawing.Size(317, 71);
            this.ttbRed.TabIndex = 0;
            this.ttbRed.TickFrequency = 5;
            this.ttbRed.Value = 100;
            this.ttbRed.ValueChanged += new System.EventHandler(this.ttb_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "&Luma";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnLuma_Click);
            // 
            // GrayscaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 303);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gbxValues);
            this.Controls.Add(this.chkPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GrayscaleForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Grayscale";
            this.gbxValues.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkPreview;
        private System.Windows.Forms.GroupBox gbxValues;
        private TextTrackBar ttbRed;
        private TextTrackBar ttbBlue;
        private TextTrackBar ttbGreen;
        private System.Windows.Forms.Button button1;
    }
}