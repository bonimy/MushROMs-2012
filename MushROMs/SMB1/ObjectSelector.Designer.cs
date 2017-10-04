namespace MushROMs.SMB1
{
    partial class ObjectSelector
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
                    this.Parent.ShowObjectSelector = false;
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
            this.cbxObjectType = new System.Windows.Forms.ComboBox();
            this.drwObject = new MushROMs.Controls.DrawControl();
            this.lbxObject = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // cbxObjectType
            // 
            this.cbxObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxObjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxObjectType.FormattingEnabled = true;
            this.cbxObjectType.Items.AddRange(new object[] {
            "Standard Objects",
            "Direct Map16",
            "Expandable Objects (Linear)",
            "Expandable Objects (Rectangular)",
            "Ground Objects",
            "Misc. Objects",
            "Castle Tileset Objects",
            "Scenery Objects"});
            this.cbxObjectType.Location = new System.Drawing.Point(0, 258);
            this.cbxObjectType.Name = "cbxObjectType";
            this.cbxObjectType.Size = new System.Drawing.Size(258, 24);
            this.cbxObjectType.TabIndex = 1;
            this.cbxObjectType.SelectedIndexChanged += new System.EventHandler(this.cbxObjectType_SelectedIndexChanged);
            // 
            // drwObject
            // 
            this.drwObject.BackColor = System.Drawing.Color.Magenta;
            this.drwObject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwObject.ItemCoordinates = new System.Drawing.Point(0, 0);
            this.drwObject.ItemX = 0;
            this.drwObject.ItemY = 0;
            this.drwObject.Location = new System.Drawing.Point(0, 0);
            this.drwObject.Name = "drwObject";
            this.drwObject.ShowMarker = false;
            this.drwObject.Size = new System.Drawing.Size(258, 258);
            this.drwObject.TabIndex = 0;
            this.drwObject.Paint += new System.Windows.Forms.PaintEventHandler(this.drwObject_Paint);
            // 
            // lbxObject
            // 
            this.lbxObject.FormattingEnabled = true;
            this.lbxObject.Location = new System.Drawing.Point(0, 282);
            this.lbxObject.Name = "lbxObject";
            this.lbxObject.Size = new System.Drawing.Size(258, 121);
            this.lbxObject.TabIndex = 2;
            this.lbxObject.SelectedIndexChanged += new System.EventHandler(this.lbxObject_SelectedIndexChanged);
            // 
            // ObjectSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 403);
            this.Controls.Add(this.lbxObject);
            this.Controls.Add(this.drwObject);
            this.Controls.Add(this.cbxObjectType);
            this.Name = "ObjectSelector";
            this.ShowInTaskbar = false;
            this.Text = "ObjectSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxObjectType;
        private Controls.DrawControl drwObject;
        private System.Windows.Forms.ListBox lbxObject;
    }
}