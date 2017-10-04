namespace MushROMs.SMB1
{
    partial class GFXEditor
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (this.Parent != null)
            {
                if (!this.Parent.Disposing)
                    this.Parent.ShowGFXEditor = false;
                else
                    base.Dispose(disposing);
            }
            else
                base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.drwGFX = new MushROMs.Controls.DrawControl();
            this.SuspendLayout();
            // 
            // drwGFX
            // 
            this.drwGFX.BackColor = System.Drawing.Color.Magenta;
            this.drwGFX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwGFX.ItemCoordinates = new System.Drawing.Point(0, 0);
            this.drwGFX.ItemX = 0;
            this.drwGFX.ItemY = 0;
            this.drwGFX.Location = new System.Drawing.Point(0, 0);
            this.drwGFX.Name = "drwGFX";
            this.drwGFX.ShowMarker = false;
            this.drwGFX.Size = new System.Drawing.Size(258, 258);
            this.drwGFX.TabIndex = 0;
            this.drwGFX.Paint += new System.Windows.Forms.PaintEventHandler(this.drwGFX_Paint);
            // 
            // GFXEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 258);
            this.Controls.Add(this.drwGFX);
            this.Name = "GFXEditor";
            this.ShowInTaskbar = false;
            this.Text = "GFXEditor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GFXEditor_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DrawControl drwGFX;
    }
}