namespace MushROMs.Controls
{
    partial class TextTrackBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.trkValue = new System.Windows.Forms.TrackBar();
            this.ntbValue = new MushROMs.Controls.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trkValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(0, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(28, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Text";
            // 
            // trkValue
            // 
            this.trkValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkValue.Location = new System.Drawing.Point(0, 26);
            this.trkValue.Name = "trkValue";
            this.trkValue.Size = new System.Drawing.Size(339, 45);
            this.trkValue.TabIndex = 1;
            this.trkValue.ValueChanged += new System.EventHandler(this.trkValue_ValueChanged);
            // 
            // ntbValue
            // 
            this.ntbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ntbValue.Location = new System.Drawing.Point(289, 0);
            this.ntbValue.Name = "ntbValue";
            this.ntbValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ntbValue.Size = new System.Drawing.Size(50, 20);
            this.ntbValue.TabIndex = 2;
            this.ntbValue.Text = "0";
            this.ntbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntbValue.ValueChanged += new System.EventHandler(this.ntbValue_ValueChanged);
            // 
            // TextTrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ntbValue);
            this.Controls.Add(this.trkValue);
            this.Controls.Add(this.lblName);
            this.Name = "TextTrackBar";
            this.Size = new System.Drawing.Size(339, 71);
            ((System.ComponentModel.ISupportInitialize)(this.trkValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TrackBar trkValue;
        private NumericTextBox ntbValue;
    }
}
