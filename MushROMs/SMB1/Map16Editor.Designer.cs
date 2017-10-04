namespace MushROMs.SMB1
{
    partial class Map16Editor
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
                    this.Parent.ShowMap16Editor = false;
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
            this.drwMap16 = new MushROMs.Controls.DrawControl();
            this.SuspendLayout();
            // 
            // drwMap16
            // 
            this.drwMap16.BackColor = System.Drawing.Color.Magenta;
            this.drwMap16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwMap16.ItemCoordinates = new System.Drawing.Point(0, 0);
            this.drwMap16.ItemX = 0;
            this.drwMap16.ItemY = 0;
            this.drwMap16.Location = new System.Drawing.Point(0, 0);
            this.drwMap16.Name = "drwMap16";
            this.drwMap16.ShowMarker = false;
            this.drwMap16.Size = new System.Drawing.Size(258, 258);
            this.drwMap16.TabIndex = 0;
            this.drwMap16.Paint += new System.Windows.Forms.PaintEventHandler(this.drwMap16_Paint);
            // 
            // Map16Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 258);
            this.Controls.Add(this.drwMap16);
            this.Name = "Map16Editor";
            this.ShowInTaskbar = false;
            this.Text = "Map16Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DrawControl drwMap16;
    }
}