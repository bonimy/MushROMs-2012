namespace MushROMs.GenericEditor
{
    partial class GFXStatusForm
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
            this.drwPalette = new MushROMs.Controls.EditorControl();
            this.drwTile = new MushROMs.Controls.EditorControl();
            this.drwColor1 = new MushROMs.Controls.DrawControl();
            this.drwColor2 = new MushROMs.Controls.DrawControl();
            this.drwColor3 = new MushROMs.Controls.DrawControl();
            this.drwColor4 = new MushROMs.Controls.DrawControl();
            this.drwColor5 = new MushROMs.Controls.DrawControl();
            this.drwColor6 = new MushROMs.Controls.DrawControl();
            this.SuspendLayout();
            // 
            // drwPalette
            // 
            this.drwPalette.BackColor = System.Drawing.Color.Magenta;
            this.drwPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwPalette.Location = new System.Drawing.Point(9, 9);
            this.drwPalette.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.drwPalette.Name = "drwPalette";
            this.drwPalette.OverrideInputKeys = new System.Windows.Forms.Keys[0];
            this.drwPalette.TabIndex = 0;
            this.drwPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.drwPalette_Paint);
            this.drwPalette.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drwPalette_MouseClick);
            this.drwPalette.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwPalette_MouseMove);
            // 
            // drwTile
            // 
            this.drwTile.BackColor = System.Drawing.Color.Magenta;
            this.drwTile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwTile.Location = new System.Drawing.Point(145, 9);
            this.drwTile.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.drwTile.Name = "drwTile";
            this.drwTile.OverrideInputKeys = new System.Windows.Forms.Keys[0];
            this.drwTile.TabIndex = 1;
            this.drwTile.Paint += new System.Windows.Forms.PaintEventHandler(this.drwTile_Paint);
            this.drwTile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drwTile_MouseClick);
            this.drwTile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drwTile_MouseClick);
            this.drwTile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwTile_MouseMove);
            // 
            // drwColor1
            // 
            this.drwColor1.BackColor = System.Drawing.Color.Magenta;
            this.drwColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor1.Location = new System.Drawing.Point(9, 143);
            this.drwColor1.Margin = new System.Windows.Forms.Padding(0);
            this.drwColor1.Name = "drwColor1";
            this.drwColor1.Size = new System.Drawing.Size(18, 18);
            this.drwColor1.TabIndex = 2;
            this.drwColor1.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor2
            // 
            this.drwColor2.BackColor = System.Drawing.Color.Magenta;
            this.drwColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor2.Location = new System.Drawing.Point(27, 143);
            this.drwColor2.Margin = new System.Windows.Forms.Padding(0);
            this.drwColor2.Name = "drwColor2";
            this.drwColor2.Size = new System.Drawing.Size(18, 18);
            this.drwColor2.TabIndex = 3;
            this.drwColor2.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor3
            // 
            this.drwColor3.BackColor = System.Drawing.Color.Magenta;
            this.drwColor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor3.Location = new System.Drawing.Point(45, 143);
            this.drwColor3.Margin = new System.Windows.Forms.Padding(0);
            this.drwColor3.Name = "drwColor3";
            this.drwColor3.Size = new System.Drawing.Size(18, 18);
            this.drwColor3.TabIndex = 4;
            this.drwColor3.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor4
            // 
            this.drwColor4.BackColor = System.Drawing.Color.Magenta;
            this.drwColor4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor4.Location = new System.Drawing.Point(63, 143);
            this.drwColor4.Margin = new System.Windows.Forms.Padding(0);
            this.drwColor4.Name = "drwColor4";
            this.drwColor4.Size = new System.Drawing.Size(18, 18);
            this.drwColor4.TabIndex = 5;
            this.drwColor4.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor5
            // 
            this.drwColor5.BackColor = System.Drawing.Color.Magenta;
            this.drwColor5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor5.Location = new System.Drawing.Point(81, 143);
            this.drwColor5.Margin = new System.Windows.Forms.Padding(0);
            this.drwColor5.Name = "drwColor5";
            this.drwColor5.Size = new System.Drawing.Size(18, 18);
            this.drwColor5.TabIndex = 6;
            this.drwColor5.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // drwColor6
            // 
            this.drwColor6.BackColor = System.Drawing.Color.Magenta;
            this.drwColor6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColor6.Location = new System.Drawing.Point(99, 143);
            this.drwColor6.Margin = new System.Windows.Forms.Padding(0);
            this.drwColor6.Name = "drwColor6";
            this.drwColor6.Size = new System.Drawing.Size(18, 18);
            this.drwColor6.TabIndex = 7;
            this.drwColor6.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColor_Paint);
            // 
            // GFXStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 190);
            this.Controls.Add(this.drwColor6);
            this.Controls.Add(this.drwColor5);
            this.Controls.Add(this.drwColor4);
            this.Controls.Add(this.drwColor3);
            this.Controls.Add(this.drwColor2);
            this.Controls.Add(this.drwColor1);
            this.Controls.Add(this.drwTile);
            this.Controls.Add(this.drwPalette);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "GFXStatusForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GFXStatusForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.EditorControl drwPalette;
        private Controls.EditorControl drwTile;
        private Controls.DrawControl drwColor1;
        private Controls.DrawControl drwColor2;
        private Controls.DrawControl drwColor3;
        private Controls.DrawControl drwColor4;
        private Controls.DrawControl drwColor5;
        private Controls.DrawControl drwColor6;
    }
}