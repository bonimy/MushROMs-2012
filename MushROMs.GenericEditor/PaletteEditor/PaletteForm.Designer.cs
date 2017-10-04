namespace MushROMs.GenericEditor.PaletteEditor
{
    partial class PaletteForm
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
            this.edcPalette = new MushROMs.Controls.EditorControl();
            this.lblGreenValue = new System.Windows.Forms.Label();
            this.lblBlueValue = new System.Windows.Forms.Label();
            this.lblRedValue = new System.Windows.Forms.Label();
            this.lblSnesValue = new System.Windows.Forms.Label();
            this.lblSnesValueText = new System.Windows.Forms.Label();
            this.lblPcValue = new System.Windows.Forms.Label();
            this.lblPcValueText = new System.Windows.Forms.Label();
            this.cbxZoom = new System.Windows.Forms.ComboBox();
            this.gbxZoom = new System.Windows.Forms.GroupBox();
            this.gbxSelectedColor = new System.Windows.Forms.GroupBox();
            this.gbxEditColor = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxBackColor = new System.Windows.Forms.GroupBox();
            this.chkBackColor = new System.Windows.Forms.CheckBox();
            this.chkInvertData = new System.Windows.Forms.CheckBox();
            this.gbxROMViewing = new System.Windows.Forms.GroupBox();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tssMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.evsPalette = new MushROMs.Controls.EditorVScrollBar();
            this.ehsPalette = new MushROMs.Controls.EditorHScrollBar();
            this.gbxZoom.SuspendLayout();
            this.gbxSelectedColor.SuspendLayout();
            this.gbxEditColor.SuspendLayout();
            this.gbxBackColor.SuspendLayout();
            this.gbxROMViewing.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // edcPalette
            // 
            this.edcPalette.BackColor = System.Drawing.Color.Magenta;
            this.edcPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edcPalette.HorizontalScrollEnd = MushROMs.Controls.ScrollEnd.Last;
            this.edcPalette.HScrollBar = null;
            this.edcPalette.Location = new System.Drawing.Point(0, 0);
            this.edcPalette.Margin = new System.Windows.Forms.Padding(0);
            this.edcPalette.Name = "edcPalette";
            this.edcPalette.OverrideInputKeys = new System.Windows.Forms.Keys[0];
            this.edcPalette.PixelSize = new System.Drawing.Size(1, 1);
            this.edcPalette.ProcessMouseOnChange = true;
            this.edcPalette.TabIndex = 0;
            this.edcPalette.VerticalScrollEnd = MushROMs.Controls.ScrollEnd.Last;
            this.edcPalette.ViewSize = new System.Drawing.Size(16, 16);
            this.edcPalette.VScrollBar = null;
            this.edcPalette.ZoomSize = new System.Drawing.Size(16, 16);
            this.edcPalette.DataSizeChanged += new System.EventHandler(this.drwPalette_DataSizeChanged);
            this.edcPalette.ZoomSizeChanged += new System.EventHandler(this.drwPalette_ZoomSizeChanged);
            this.edcPalette.ViewSizeChanged += new System.EventHandler(this.drwPalette_ViewSizeChanged);
            this.edcPalette.EditRegionChanged += new System.EventHandler(this.drwPalette_EditRegionChanged);
            this.edcPalette.StartIndexChanged += new System.EventHandler(this.drwPalette_StartIndexChanged);
            this.edcPalette.SelectedCellMouseClick += new System.Windows.Forms.MouseEventHandler(this.drwPalette_SelectedCellMouseClick);
            this.edcPalette.SelectedCellMouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.drwPalette_SelectedCellMouseDoubleClick);
            this.edcPalette.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PaletteForm_MouseWheel);
            this.edcPalette.Paint += new System.Windows.Forms.PaintEventHandler(this.drwPalette_Paint);
            this.edcPalette.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drwPalette_KeyDown);
            // 
            // lblGreenValue
            // 
            this.lblGreenValue.AutoSize = true;
            this.lblGreenValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblGreenValue.Location = new System.Drawing.Point(62, 57);
            this.lblGreenValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblGreenValue.Name = "lblGreenValue";
            this.lblGreenValue.Size = new System.Drawing.Size(13, 13);
            this.lblGreenValue.TabIndex = 0;
            this.lblGreenValue.Text = "0";
            // 
            // lblBlueValue
            // 
            this.lblBlueValue.AutoSize = true;
            this.lblBlueValue.ForeColor = System.Drawing.Color.Blue;
            this.lblBlueValue.Location = new System.Drawing.Point(117, 57);
            this.lblBlueValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblBlueValue.Name = "lblBlueValue";
            this.lblBlueValue.Size = new System.Drawing.Size(13, 13);
            this.lblBlueValue.TabIndex = 0;
            this.lblBlueValue.Text = "0";
            // 
            // lblRedValue
            // 
            this.lblRedValue.AutoSize = true;
            this.lblRedValue.ForeColor = System.Drawing.Color.Red;
            this.lblRedValue.Location = new System.Drawing.Point(6, 57);
            this.lblRedValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblRedValue.Name = "lblRedValue";
            this.lblRedValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRedValue.Size = new System.Drawing.Size(13, 13);
            this.lblRedValue.TabIndex = 0;
            this.lblRedValue.Text = "0";
            // 
            // lblSnesValue
            // 
            this.lblSnesValue.AutoSize = true;
            this.lblSnesValue.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSnesValue.Location = new System.Drawing.Point(81, 38);
            this.lblSnesValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblSnesValue.Name = "lblSnesValue";
            this.lblSnesValue.Size = new System.Drawing.Size(49, 14);
            this.lblSnesValue.TabIndex = 0;
            this.lblSnesValue.Text = "0x0000";
            // 
            // lblSnesValueText
            // 
            this.lblSnesValueText.AutoSize = true;
            this.lblSnesValueText.Location = new System.Drawing.Point(6, 38);
            this.lblSnesValueText.Margin = new System.Windows.Forms.Padding(3);
            this.lblSnesValueText.Name = "lblSnesValueText";
            this.lblSnesValueText.Size = new System.Drawing.Size(69, 13);
            this.lblSnesValueText.TabIndex = 0;
            this.lblSnesValueText.Text = "SNES Value:";
            // 
            // lblPcValue
            // 
            this.lblPcValue.AutoSize = true;
            this.lblPcValue.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPcValue.Location = new System.Drawing.Point(81, 19);
            this.lblPcValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblPcValue.Name = "lblPcValue";
            this.lblPcValue.Size = new System.Drawing.Size(63, 14);
            this.lblPcValue.TabIndex = 0;
            this.lblPcValue.Text = "0x000000";
            // 
            // lblPcValueText
            // 
            this.lblPcValueText.AutoSize = true;
            this.lblPcValueText.Location = new System.Drawing.Point(6, 19);
            this.lblPcValueText.Margin = new System.Windows.Forms.Padding(3);
            this.lblPcValueText.Name = "lblPcValueText";
            this.lblPcValueText.Size = new System.Drawing.Size(54, 13);
            this.lblPcValueText.TabIndex = 0;
            this.lblPcValueText.Text = "PC Value:";
            // 
            // cbxZoom
            // 
            this.cbxZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxZoom.FormattingEnabled = true;
            this.cbxZoom.ItemHeight = 13;
            this.cbxZoom.Items.AddRange(new object[] {
            "8x",
            "16x",
            "24x",
            "32x"});
            this.cbxZoom.Location = new System.Drawing.Point(6, 16);
            this.cbxZoom.Name = "cbxZoom";
            this.cbxZoom.Size = new System.Drawing.Size(44, 21);
            this.cbxZoom.TabIndex = 0;
            this.cbxZoom.SelectedIndexChanged += new System.EventHandler(this.cbxZoom_SelectedIndexChanged);
            // 
            // gbxZoom
            // 
            this.gbxZoom.Controls.Add(this.cbxZoom);
            this.gbxZoom.Location = new System.Drawing.Point(97, 81);
            this.gbxZoom.Name = "gbxZoom";
            this.gbxZoom.Size = new System.Drawing.Size(56, 44);
            this.gbxZoom.TabIndex = 2;
            this.gbxZoom.TabStop = false;
            this.gbxZoom.Text = "Zoom";
            // 
            // gbxSelectedColor
            // 
            this.gbxSelectedColor.Controls.Add(this.lblPcValueText);
            this.gbxSelectedColor.Controls.Add(this.lblPcValue);
            this.gbxSelectedColor.Controls.Add(this.lblSnesValueText);
            this.gbxSelectedColor.Controls.Add(this.lblSnesValue);
            this.gbxSelectedColor.Controls.Add(this.lblGreenValue);
            this.gbxSelectedColor.Controls.Add(this.lblRedValue);
            this.gbxSelectedColor.Controls.Add(this.lblBlueValue);
            this.gbxSelectedColor.Location = new System.Drawing.Point(3, 3);
            this.gbxSelectedColor.Name = "gbxSelectedColor";
            this.gbxSelectedColor.Size = new System.Drawing.Size(150, 76);
            this.gbxSelectedColor.TabIndex = 0;
            this.gbxSelectedColor.TabStop = false;
            this.gbxSelectedColor.Text = "Selected color";
            // 
            // gbxEditColor
            // 
            this.gbxEditColor.Controls.Add(this.label1);
            this.gbxEditColor.Location = new System.Drawing.Point(3, 81);
            this.gbxEditColor.Name = "gbxEditColor";
            this.gbxEditColor.Size = new System.Drawing.Size(88, 44);
            this.gbxEditColor.TabIndex = 1;
            this.gbxEditColor.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Edit Color:";
            // 
            // gbxBackColor
            // 
            this.gbxBackColor.Controls.Add(this.chkBackColor);
            this.gbxBackColor.Location = new System.Drawing.Point(159, 3);
            this.gbxBackColor.Name = "gbxBackColor";
            this.gbxBackColor.Size = new System.Drawing.Size(92, 76);
            this.gbxBackColor.TabIndex = 3;
            this.gbxBackColor.TabStop = false;
            this.gbxBackColor.Text = "Back Color";
            // 
            // chkBackColor
            // 
            this.chkBackColor.AutoSize = true;
            this.chkBackColor.Location = new System.Drawing.Point(6, 19);
            this.chkBackColor.Name = "chkBackColor";
            this.chkBackColor.Size = new System.Drawing.Size(80, 17);
            this.chkBackColor.TabIndex = 0;
            this.chkBackColor.Text = "Enable Edit";
            this.chkBackColor.UseVisualStyleBackColor = true;
            this.chkBackColor.CheckedChanged += new System.EventHandler(this.chkBackColor_CheckedChanged);
            // 
            // chkInvertData
            // 
            this.chkInvertData.AutoSize = true;
            this.chkInvertData.Location = new System.Drawing.Point(6, 18);
            this.chkInvertData.Name = "chkInvertData";
            this.chkInvertData.Size = new System.Drawing.Size(79, 17);
            this.chkInvertData.TabIndex = 0;
            this.chkInvertData.Text = "Invert Data";
            this.chkInvertData.UseVisualStyleBackColor = true;
            this.chkInvertData.CheckedChanged += new System.EventHandler(this.chkInvertData_CheckedChanged);
            // 
            // gbxROMViewing
            // 
            this.gbxROMViewing.Controls.Add(this.chkInvertData);
            this.gbxROMViewing.Location = new System.Drawing.Point(159, 81);
            this.gbxROMViewing.Name = "gbxROMViewing";
            this.gbxROMViewing.Size = new System.Drawing.Size(92, 44);
            this.gbxROMViewing.TabIndex = 4;
            this.gbxROMViewing.TabStop = false;
            this.gbxROMViewing.Text = "ROM Viewing";
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 275);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(528, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 3;
            this.stsMain.Text = "...";
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(73, 17);
            this.tssMain.Text = "Editor Ready";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatus.Controls.Add(this.gbxSelectedColor);
            this.pnlStatus.Controls.Add(this.gbxZoom);
            this.pnlStatus.Controls.Add(this.gbxROMViewing);
            this.pnlStatus.Controls.Add(this.gbxEditColor);
            this.pnlStatus.Controls.Add(this.gbxBackColor);
            this.pnlStatus.Location = new System.Drawing.Point(275, 0);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(253, 128);
            this.pnlStatus.TabIndex = 2;
            // 
            // evsPalette
            // 
            this.evsPalette.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.evsPalette.EditControl = this.edcPalette;
            this.evsPalette.Enabled = false;
            this.evsPalette.LargeChange = 1;
            this.evsPalette.Location = new System.Drawing.Point(258, 0);
            this.evsPalette.Maximum = 0;
            this.evsPalette.Name = "evsPalette";
            this.evsPalette.Size = new System.Drawing.Size(17, 254);
            this.evsPalette.TabIndex = 1;
            // 
            // ehsPalette
            // 
            this.ehsPalette.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ehsPalette.EditControl = this.edcPalette;
            this.ehsPalette.LargeChange = 1;
            this.ehsPalette.Location = new System.Drawing.Point(0, 258);
            this.ehsPalette.Maximum = 0;
            this.ehsPalette.Name = "ehsPalette";
            this.ehsPalette.Size = new System.Drawing.Size(258, 17);
            this.ehsPalette.TabIndex = 4;
            // 
            // PaletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 297);
            this.Controls.Add(this.ehsPalette);
            this.Controls.Add(this.evsPalette);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.edcPalette);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.Name = "PaletteForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResizeEnd += new System.EventHandler(this.PaletteEditor_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaletteForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PaletteForm_KeyUp);
            this.Resize += new System.EventHandler(this.PaletteEditor_Resize);
            this.gbxZoom.ResumeLayout(false);
            this.gbxSelectedColor.ResumeLayout(false);
            this.gbxSelectedColor.PerformLayout();
            this.gbxEditColor.ResumeLayout(false);
            this.gbxEditColor.PerformLayout();
            this.gbxBackColor.ResumeLayout(false);
            this.gbxBackColor.PerformLayout();
            this.gbxROMViewing.ResumeLayout(false);
            this.gbxROMViewing.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.EditorControl edcPalette;
        private System.Windows.Forms.Label lblGreenValue;
        private System.Windows.Forms.Label lblBlueValue;
        private System.Windows.Forms.Label lblRedValue;
        private System.Windows.Forms.Label lblSnesValue;
        private System.Windows.Forms.Label lblSnesValueText;
        private System.Windows.Forms.Label lblPcValue;
        private System.Windows.Forms.Label lblPcValueText;
        private System.Windows.Forms.ComboBox cbxZoom;
        private System.Windows.Forms.GroupBox gbxZoom;
        private System.Windows.Forms.GroupBox gbxSelectedColor;
        private System.Windows.Forms.GroupBox gbxEditColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxBackColor;
        private System.Windows.Forms.CheckBox chkBackColor;
        private System.Windows.Forms.CheckBox chkInvertData;
        private System.Windows.Forms.GroupBox gbxROMViewing;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private System.Windows.Forms.Panel pnlStatus;
        private Controls.EditorVScrollBar evsPalette;
        private Controls.EditorHScrollBar ehsPalette;
    }
}