namespace MushROMs.GenericEditor
{
    partial class GFXEditor_old
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
            this.drwGFX = new MushROMs.Controls.EditorControl();
            this.SuspendLayout();
            // 
            // drwGFX
            // 
            this.drwGFX.BackColor = System.Drawing.Color.Magenta;
            this.drwGFX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwGFX.Location = new System.Drawing.Point(0, 0);
            this.drwGFX.Margin = new System.Windows.Forms.Padding(0);
            this.drwGFX.Name = "drwGFX";
            this.drwGFX.OverrideInputKeys = new System.Windows.Forms.Keys[0];
            this.drwGFX.TabIndex = 0;
            this.drwGFX.Paint += new System.Windows.Forms.PaintEventHandler(this.drwGFX_Paint);
            // 
            // GFXEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 258);
            this.Controls.Add(this.drwGFX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "GFXEditor";
            this.Text = "GFX Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.EditorControl drwGFX;
    }
}