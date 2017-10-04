namespace MushROMs.GenericEditor.GFXEditor
{
    partial class GFXForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.gbxGFXZoom = new System.Windows.Forms.GroupBox();
            this.cbxGFXZoom = new System.Windows.Forms.ComboBox();
            this.edcGFX = new MushROMs.Controls.EditorControl();
            this.spcSub = new System.Windows.Forms.SplitContainer();
            this.gbxTileZoom = new System.Windows.Forms.GroupBox();
            this.cbxTileZoom = new System.Windows.Forms.ComboBox();
            this.edcTile = new MushROMs.Controls.EditorControl();
            this.btnUseDefaultPalette = new System.Windows.Forms.Button();
            this.edcPalette = new MushROMs.Controls.EditorControl();
            this.statusStrip1.SuspendLayout();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.gbxGFXZoom.SuspendLayout();
            this.spcSub.Panel1.SuspendLayout();
            this.spcSub.Panel2.SuspendLayout();
            this.spcSub.SuspendLayout();
            this.gbxTileZoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 262);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(947, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabel1.Text = "Editor Ready";
            // 
            // spcMain
            // 
            this.spcMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spcMain.IsSplitterFixed = true;
            this.spcMain.Location = new System.Drawing.Point(0, 0);
            this.spcMain.Name = "spcMain";
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.Controls.Add(this.gbxGFXZoom);
            this.spcMain.Panel1.Controls.Add(this.edcGFX);
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.Controls.Add(this.spcSub);
            this.spcMain.Size = new System.Drawing.Size(947, 262);
            this.spcMain.SplitterDistance = 336;
            this.spcMain.TabIndex = 2;
            // 
            // gbxGFXZoom
            // 
            this.gbxGFXZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxGFXZoom.Controls.Add(this.cbxGFXZoom);
            this.gbxGFXZoom.Location = new System.Drawing.Point(261, 3);
            this.gbxGFXZoom.Name = "gbxGFXZoom";
            this.gbxGFXZoom.Size = new System.Drawing.Size(56, 44);
            this.gbxGFXZoom.TabIndex = 22;
            this.gbxGFXZoom.TabStop = false;
            this.gbxGFXZoom.Text = "Zoom";
            // 
            // cbxGFXZoom
            // 
            this.cbxGFXZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGFXZoom.FormattingEnabled = true;
            this.cbxGFXZoom.Items.AddRange(new object[] {
            "1x",
            "2x",
            "3x",
            "4x"});
            this.cbxGFXZoom.Location = new System.Drawing.Point(6, 16);
            this.cbxGFXZoom.Name = "cbxGFXZoom";
            this.cbxGFXZoom.Size = new System.Drawing.Size(44, 21);
            this.cbxGFXZoom.TabIndex = 0;
            this.cbxGFXZoom.SelectedIndexChanged += new System.EventHandler(this.cbxGFXZoom_SelectedIndexChanged);
            // 
            // edcGFX
            // 
            this.edcGFX.BackColor = System.Drawing.Color.Magenta;
            this.edcGFX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edcGFX.Location = new System.Drawing.Point(0, 0);
            this.edcGFX.Margin = new System.Windows.Forms.Padding(0);
            this.edcGFX.Name = "edcGFX";
            this.edcGFX.OverrideInputKeys = new System.Windows.Forms.Keys[0];
            this.edcGFX.PixelSize = new System.Drawing.Size(8, 8);
            this.edcGFX.ProcessMouseOnChange = true;
            this.edcGFX.TabIndex = 0;
            this.edcGFX.ViewSize = new System.Drawing.Size(16, 16);
            this.edcGFX.ZoomSize = new System.Drawing.Size(2, 2);
            this.edcGFX.SelectedCellMouseClick += new System.Windows.Forms.MouseEventHandler(this.edcGFX_SelectedCellMouseClick);
            this.edcGFX.SelectedCellMouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.edcGFX_SelectedCellMouseClick);
            this.edcGFX.SizeChanged += new System.EventHandler(this.edcGFX_SizeChanged);
            this.edcGFX.Paint += new System.Windows.Forms.PaintEventHandler(this.edcGFX_Paint);
            // 
            // spcSub
            // 
            this.spcSub.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spcSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcSub.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spcSub.IsSplitterFixed = true;
            this.spcSub.Location = new System.Drawing.Point(0, 0);
            this.spcSub.Name = "spcSub";
            // 
            // spcSub.Panel1
            // 
            this.spcSub.Panel1.Controls.Add(this.gbxTileZoom);
            this.spcSub.Panel1.Controls.Add(this.edcTile);
            // 
            // spcSub.Panel2
            // 
            this.spcSub.Panel2.Controls.Add(this.btnUseDefaultPalette);
            this.spcSub.Panel2.Controls.Add(this.edcPalette);
            this.spcSub.Size = new System.Drawing.Size(607, 262);
            this.spcSub.SplitterDistance = 279;
            this.spcSub.TabIndex = 0;
            // 
            // gbxTileZoom
            // 
            this.gbxTileZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxTileZoom.Controls.Add(this.cbxTileZoom);
            this.gbxTileZoom.Location = new System.Drawing.Point(153, 3);
            this.gbxTileZoom.Name = "gbxTileZoom";
            this.gbxTileZoom.Size = new System.Drawing.Size(56, 44);
            this.gbxTileZoom.TabIndex = 23;
            this.gbxTileZoom.TabStop = false;
            this.gbxTileZoom.Text = "Zoom";
            // 
            // cbxTileZoom
            // 
            this.cbxTileZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTileZoom.FormattingEnabled = true;
            this.cbxTileZoom.Items.AddRange(new object[] {
            "8x",
            "16x",
            "24x",
            "32x"});
            this.cbxTileZoom.Location = new System.Drawing.Point(6, 16);
            this.cbxTileZoom.Name = "cbxTileZoom";
            this.cbxTileZoom.Size = new System.Drawing.Size(44, 21);
            this.cbxTileZoom.TabIndex = 0;
            this.cbxTileZoom.SelectedIndexChanged += new System.EventHandler(this.cbxTileZoom_SelectedIndexChanged);
            // 
            // edcTile
            // 
            this.edcTile.BackColor = System.Drawing.Color.Magenta;
            this.edcTile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edcTile.Location = new System.Drawing.Point(0, 0);
            this.edcTile.Margin = new System.Windows.Forms.Padding(0);
            this.edcTile.Name = "edcTile";
            this.edcTile.OverrideInputKeys = new System.Windows.Forms.Keys[0];
            this.edcTile.PixelSize = new System.Drawing.Size(1, 1);
            this.edcTile.TabIndex = 0;
            this.edcTile.ViewSize = new System.Drawing.Size(8, 8);
            this.edcTile.ZoomSize = new System.Drawing.Size(16, 16);
            this.edcTile.SelectedCellMouseClick += new System.Windows.Forms.MouseEventHandler(this.edcTile_SelectedCellMouseClick);
            // 
            // btnUseDefaultPalette
            // 
            this.btnUseDefaultPalette.Location = new System.Drawing.Point(4, 232);
            this.btnUseDefaultPalette.Name = "btnUseDefaultPalette";
            this.btnUseDefaultPalette.Size = new System.Drawing.Size(112, 23);
            this.btnUseDefaultPalette.TabIndex = 1;
            this.btnUseDefaultPalette.Text = "Use default palette";
            this.btnUseDefaultPalette.UseVisualStyleBackColor = true;
            // 
            // edcPalette
            // 
            this.edcPalette.BackColor = System.Drawing.Color.Magenta;
            this.edcPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edcPalette.Location = new System.Drawing.Point(0, 0);
            this.edcPalette.Margin = new System.Windows.Forms.Padding(0);
            this.edcPalette.Name = "edcPalette";
            this.edcPalette.OverrideInputKeys = new System.Windows.Forms.Keys[0];
            this.edcPalette.PixelSize = new System.Drawing.Size(1, 1);
            this.edcPalette.TabIndex = 0;
            this.edcPalette.ViewSize = new System.Drawing.Size(16, 16);
            this.edcPalette.ZoomSize = new System.Drawing.Size(8, 8);
            // 
            // GFXForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 284);
            this.Controls.Add(this.spcMain);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.Name = "GFXForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GFXForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel2.ResumeLayout(false);
            this.spcMain.ResumeLayout(false);
            this.gbxGFXZoom.ResumeLayout(false);
            this.spcSub.Panel1.ResumeLayout(false);
            this.spcSub.Panel2.ResumeLayout(false);
            this.spcSub.ResumeLayout(false);
            this.gbxTileZoom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer spcMain;
        private System.Windows.Forms.SplitContainer spcSub;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Controls.EditorControl edcGFX;
        private System.Windows.Forms.GroupBox gbxGFXZoom;
        private System.Windows.Forms.ComboBox cbxGFXZoom;
        private System.Windows.Forms.GroupBox gbxTileZoom;
        private System.Windows.Forms.ComboBox cbxTileZoom;
        private Controls.EditorControl edcTile;
        private Controls.EditorControl edcPalette;
        private System.Windows.Forms.Button btnUseDefaultPalette;
    }
}