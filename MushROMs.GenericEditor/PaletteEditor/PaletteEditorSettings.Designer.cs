namespace MushROMs.GenericEditor.PaletteEditor
{
    partial class PaletteEditorSettings
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
            this.gbxSizing = new System.Windows.Forms.GroupBox();
            this.cbxZoom = new System.Windows.Forms.ComboBox();
            this.lblZoomSize = new System.Windows.Forms.Label();
            this.nudColumns = new System.Windows.Forms.NumericUpDown();
            this.nudRows = new System.Windows.Forms.NumericUpDown();
            this.lblNumColumns = new System.Windows.Forms.Label();
            this.lblNumRows = new System.Windows.Forms.Label();
            this.pnlPalette = new System.Windows.Forms.Panel();
            this.gbxCursor = new System.Windows.Forms.GroupBox();
            this.cbxCursor = new System.Windows.Forms.ComboBox();
            this.gbxMarker = new System.Windows.Forms.GroupBox();
            this.lblDashColor2 = new System.Windows.Forms.Label();
            this.lblDashColor1 = new System.Windows.Forms.Label();
            this.cpkDashColor2 = new MushROMs.Controls.ColorPicker();
            this.cpkDashColor1 = new MushROMs.Controls.ColorPicker();
            this.nudDashLength2 = new System.Windows.Forms.NumericUpDown();
            this.nudDashLength1 = new System.Windows.Forms.NumericUpDown();
            this.lblDashLength2 = new System.Windows.Forms.Label();
            this.lblDashLength1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbxBackground = new System.Windows.Forms.GroupBox();
            this.cpkBackColor2 = new MushROMs.Controls.ColorPicker();
            this.cpkBackColor1 = new MushROMs.Controls.ColorPicker();
            this.drwBGExample = new MushROMs.Controls.DrawControl();
            this.cbxBackZoom = new System.Windows.Forms.ComboBox();
            this.lblBackZoomSize = new System.Windows.Forms.Label();
            this.lblBackColor2 = new System.Windows.Forms.Label();
            this.lblBackColor1 = new System.Windows.Forms.Label();
            this.gbxSizing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).BeginInit();
            this.pnlPalette.SuspendLayout();
            this.gbxCursor.SuspendLayout();
            this.gbxMarker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDashLength2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDashLength1)).BeginInit();
            this.gbxBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxSizing
            // 
            this.gbxSizing.Controls.Add(this.cbxZoom);
            this.gbxSizing.Controls.Add(this.lblZoomSize);
            this.gbxSizing.Controls.Add(this.nudColumns);
            this.gbxSizing.Controls.Add(this.nudRows);
            this.gbxSizing.Controls.Add(this.lblNumColumns);
            this.gbxSizing.Controls.Add(this.lblNumRows);
            this.gbxSizing.Location = new System.Drawing.Point(12, 12);
            this.gbxSizing.Name = "gbxSizing";
            this.gbxSizing.Size = new System.Drawing.Size(199, 98);
            this.gbxSizing.TabIndex = 0;
            this.gbxSizing.TabStop = false;
            this.gbxSizing.Text = "Sizing Options";
            // 
            // cbxZoom
            // 
            this.cbxZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxZoom.FormattingEnabled = true;
            this.cbxZoom.Items.AddRange(new object[] {
            "8x",
            "16x",
            "24x",
            "32x"});
            this.cbxZoom.Location = new System.Drawing.Point(148, 71);
            this.cbxZoom.MaxDropDownItems = 4;
            this.cbxZoom.Name = "cbxZoom";
            this.cbxZoom.Size = new System.Drawing.Size(45, 21);
            this.cbxZoom.TabIndex = 2;
            // 
            // lblZoomSize
            // 
            this.lblZoomSize.AutoSize = true;
            this.lblZoomSize.Location = new System.Drawing.Point(6, 74);
            this.lblZoomSize.Name = "lblZoomSize";
            this.lblZoomSize.Size = new System.Drawing.Size(94, 13);
            this.lblZoomSize.TabIndex = 0;
            this.lblZoomSize.Text = "Default Zoom Size";
            // 
            // nudColumns
            // 
            this.nudColumns.Location = new System.Drawing.Point(148, 45);
            this.nudColumns.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudColumns.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudColumns.Name = "nudColumns";
            this.nudColumns.Size = new System.Drawing.Size(45, 20);
            this.nudColumns.TabIndex = 1;
            this.nudColumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudColumns.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // nudRows
            // 
            this.nudRows.Location = new System.Drawing.Point(148, 18);
            this.nudRows.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudRows.Name = "nudRows";
            this.nudRows.Size = new System.Drawing.Size(45, 20);
            this.nudRows.TabIndex = 0;
            this.nudRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudRows.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // lblNumColumns
            // 
            this.lblNumColumns.AutoSize = true;
            this.lblNumColumns.Location = new System.Drawing.Point(6, 47);
            this.lblNumColumns.Name = "lblNumColumns";
            this.lblNumColumns.Size = new System.Drawing.Size(136, 13);
            this.lblNumColumns.TabIndex = 0;
            this.lblNumColumns.Text = "Default Number of Columns";
            // 
            // lblNumRows
            // 
            this.lblNumRows.AutoSize = true;
            this.lblNumRows.Location = new System.Drawing.Point(6, 20);
            this.lblNumRows.Name = "lblNumRows";
            this.lblNumRows.Size = new System.Drawing.Size(123, 13);
            this.lblNumRows.TabIndex = 0;
            this.lblNumRows.Text = "Default Number of Rows";
            // 
            // pnlPalette
            // 
            this.pnlPalette.Controls.Add(this.gbxCursor);
            this.pnlPalette.Controls.Add(this.gbxMarker);
            this.pnlPalette.Controls.Add(this.btnCancel);
            this.pnlPalette.Controls.Add(this.btnOK);
            this.pnlPalette.Controls.Add(this.gbxBackground);
            this.pnlPalette.Controls.Add(this.gbxSizing);
            this.pnlPalette.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPalette.Location = new System.Drawing.Point(0, 0);
            this.pnlPalette.Name = "pnlPalette";
            this.pnlPalette.Size = new System.Drawing.Size(385, 225);
            this.pnlPalette.TabIndex = 1;
            // 
            // gbxCursor
            // 
            this.gbxCursor.Controls.Add(this.cbxCursor);
            this.gbxCursor.Location = new System.Drawing.Point(217, 132);
            this.gbxCursor.Name = "gbxCursor";
            this.gbxCursor.Size = new System.Drawing.Size(156, 46);
            this.gbxCursor.TabIndex = 5;
            this.gbxCursor.TabStop = false;
            this.gbxCursor.Text = "Cursor";
            // 
            // cbxCursor
            // 
            this.cbxCursor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCursor.FormattingEnabled = true;
            this.cbxCursor.Items.AddRange(new object[] {
            "Arrow",
            "Cross"});
            this.cbxCursor.Location = new System.Drawing.Point(6, 19);
            this.cbxCursor.Name = "cbxCursor";
            this.cbxCursor.Size = new System.Drawing.Size(144, 21);
            this.cbxCursor.TabIndex = 0;
            // 
            // gbxMarker
            // 
            this.gbxMarker.Controls.Add(this.lblDashColor2);
            this.gbxMarker.Controls.Add(this.lblDashColor1);
            this.gbxMarker.Controls.Add(this.cpkDashColor2);
            this.gbxMarker.Controls.Add(this.cpkDashColor1);
            this.gbxMarker.Controls.Add(this.nudDashLength2);
            this.gbxMarker.Controls.Add(this.nudDashLength1);
            this.gbxMarker.Controls.Add(this.lblDashLength2);
            this.gbxMarker.Controls.Add(this.lblDashLength1);
            this.gbxMarker.Location = new System.Drawing.Point(217, 12);
            this.gbxMarker.Name = "gbxMarker";
            this.gbxMarker.Size = new System.Drawing.Size(156, 113);
            this.gbxMarker.TabIndex = 2;
            this.gbxMarker.TabStop = false;
            this.gbxMarker.Text = "Marker Options";
            // 
            // lblDashColor2
            // 
            this.lblDashColor2.AutoSize = true;
            this.lblDashColor2.Location = new System.Drawing.Point(6, 90);
            this.lblDashColor2.Name = "lblDashColor2";
            this.lblDashColor2.Size = new System.Drawing.Size(68, 13);
            this.lblDashColor2.TabIndex = 6;
            this.lblDashColor2.Text = "Dash Color 2";
            // 
            // lblDashColor1
            // 
            this.lblDashColor1.AutoSize = true;
            this.lblDashColor1.Location = new System.Drawing.Point(6, 40);
            this.lblDashColor1.Name = "lblDashColor1";
            this.lblDashColor1.Size = new System.Drawing.Size(68, 13);
            this.lblDashColor1.TabIndex = 5;
            this.lblDashColor1.Text = "Dash Color 1";
            // 
            // cpkDashColor2
            // 
            this.cpkDashColor2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cpkDashColor2.BackColor = System.Drawing.Color.Magenta;
            this.cpkDashColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpkDashColor2.Location = new System.Drawing.Point(128, 90);
            this.cpkDashColor2.Margin = new System.Windows.Forms.Padding(0);
            this.cpkDashColor2.Name = "cpkDashColor2";
            this.cpkDashColor2.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpkDashColor2.Size = new System.Drawing.Size(18, 18);
            this.cpkDashColor2.TabIndex = 4;
            // 
            // cpkDashColor1
            // 
            this.cpkDashColor1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cpkDashColor1.BackColor = System.Drawing.Color.Magenta;
            this.cpkDashColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpkDashColor1.Location = new System.Drawing.Point(128, 40);
            this.cpkDashColor1.Margin = new System.Windows.Forms.Padding(0);
            this.cpkDashColor1.Name = "cpkDashColor1";
            this.cpkDashColor1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpkDashColor1.Size = new System.Drawing.Size(18, 18);
            this.cpkDashColor1.TabIndex = 2;
            // 
            // nudDashLength2
            // 
            this.nudDashLength2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDashLength2.Location = new System.Drawing.Point(101, 64);
            this.nudDashLength2.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudDashLength2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDashLength2.Name = "nudDashLength2";
            this.nudDashLength2.Size = new System.Drawing.Size(45, 20);
            this.nudDashLength2.TabIndex = 3;
            this.nudDashLength2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDashLength2.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // nudDashLength1
            // 
            this.nudDashLength1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDashLength1.Location = new System.Drawing.Point(101, 14);
            this.nudDashLength1.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudDashLength1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDashLength1.Name = "nudDashLength1";
            this.nudDashLength1.Size = new System.Drawing.Size(45, 20);
            this.nudDashLength1.TabIndex = 1;
            this.nudDashLength1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDashLength1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // lblDashLength2
            // 
            this.lblDashLength2.AutoSize = true;
            this.lblDashLength2.Location = new System.Drawing.Point(6, 66);
            this.lblDashLength2.Name = "lblDashLength2";
            this.lblDashLength2.Size = new System.Drawing.Size(77, 13);
            this.lblDashLength2.TabIndex = 0;
            this.lblDashLength2.Text = "Dash Length 2";
            // 
            // lblDashLength1
            // 
            this.lblDashLength1.AutoSize = true;
            this.lblDashLength1.Location = new System.Drawing.Point(6, 16);
            this.lblDashLength1.Name = "lblDashLength1";
            this.lblDashLength1.Size = new System.Drawing.Size(77, 13);
            this.lblDashLength1.TabIndex = 0;
            this.lblDashLength1.Text = "Dash Length 1";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(298, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(217, 190);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // gbxBackground
            // 
            this.gbxBackground.Controls.Add(this.cpkBackColor2);
            this.gbxBackground.Controls.Add(this.cpkBackColor1);
            this.gbxBackground.Controls.Add(this.drwBGExample);
            this.gbxBackground.Controls.Add(this.cbxBackZoom);
            this.gbxBackground.Controls.Add(this.lblBackZoomSize);
            this.gbxBackground.Controls.Add(this.lblBackColor2);
            this.gbxBackground.Controls.Add(this.lblBackColor1);
            this.gbxBackground.Location = new System.Drawing.Point(12, 116);
            this.gbxBackground.Name = "gbxBackground";
            this.gbxBackground.Size = new System.Drawing.Size(199, 97);
            this.gbxBackground.TabIndex = 1;
            this.gbxBackground.TabStop = false;
            this.gbxBackground.Text = "Background Options";
            // 
            // cpkBackColor2
            // 
            this.cpkBackColor2.BackColor = System.Drawing.Color.Magenta;
            this.cpkBackColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpkBackColor2.Location = new System.Drawing.Point(104, 46);
            this.cpkBackColor2.Margin = new System.Windows.Forms.Padding(0);
            this.cpkBackColor2.Name = "cpkBackColor2";
            this.cpkBackColor2.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpkBackColor2.Size = new System.Drawing.Size(18, 18);
            this.cpkBackColor2.TabIndex = 1;
            this.cpkBackColor2.ColorValueChanged += new System.EventHandler(this.cpkBackColor_ColorValueChanged);
            // 
            // cpkBackColor1
            // 
            this.cpkBackColor1.BackColor = System.Drawing.Color.Magenta;
            this.cpkBackColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpkBackColor1.Location = new System.Drawing.Point(104, 19);
            this.cpkBackColor1.Margin = new System.Windows.Forms.Padding(0);
            this.cpkBackColor1.Name = "cpkBackColor1";
            this.cpkBackColor1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cpkBackColor1.Size = new System.Drawing.Size(18, 18);
            this.cpkBackColor1.TabIndex = 0;
            this.cpkBackColor1.ColorValueChanged += new System.EventHandler(this.cpkBackColor_ColorValueChanged);
            // 
            // drwBGExample
            // 
            this.drwBGExample.BackColor = System.Drawing.Color.Magenta;
            this.drwBGExample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwBGExample.Location = new System.Drawing.Point(131, 19);
            this.drwBGExample.Margin = new System.Windows.Forms.Padding(0);
            this.drwBGExample.Name = "drwBGExample";
            this.drwBGExample.Size = new System.Drawing.Size(50, 50);
            this.drwBGExample.TabIndex = 0;
            this.drwBGExample.Paint += new System.Windows.Forms.PaintEventHandler(this.drwBGExample_Paint);
            // 
            // cbxBackZoom
            // 
            this.cbxBackZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBackZoom.FormattingEnabled = true;
            this.cbxBackZoom.Items.AddRange(new object[] {
            "1x",
            "2x",
            "4x",
            "8x"});
            this.cbxBackZoom.Location = new System.Drawing.Point(77, 70);
            this.cbxBackZoom.MaxDropDownItems = 4;
            this.cbxBackZoom.Name = "cbxBackZoom";
            this.cbxBackZoom.Size = new System.Drawing.Size(45, 21);
            this.cbxBackZoom.TabIndex = 2;
            this.cbxBackZoom.SelectedIndexChanged += new System.EventHandler(this.cbxBackZoom_SelectedIndexChanged);
            // 
            // lblBackZoomSize
            // 
            this.lblBackZoomSize.AutoSize = true;
            this.lblBackZoomSize.Location = new System.Drawing.Point(6, 73);
            this.lblBackZoomSize.Name = "lblBackZoomSize";
            this.lblBackZoomSize.Size = new System.Drawing.Size(57, 13);
            this.lblBackZoomSize.TabIndex = 3;
            this.lblBackZoomSize.Text = "Zoom Size";
            // 
            // lblBackColor2
            // 
            this.lblBackColor2.AutoSize = true;
            this.lblBackColor2.Location = new System.Drawing.Point(6, 46);
            this.lblBackColor2.Name = "lblBackColor2";
            this.lblBackColor2.Size = new System.Drawing.Size(68, 13);
            this.lblBackColor2.TabIndex = 0;
            this.lblBackColor2.Text = "Back Color 2";
            // 
            // lblBackColor1
            // 
            this.lblBackColor1.AutoSize = true;
            this.lblBackColor1.Location = new System.Drawing.Point(6, 19);
            this.lblBackColor1.Name = "lblBackColor1";
            this.lblBackColor1.Size = new System.Drawing.Size(68, 13);
            this.lblBackColor1.TabIndex = 0;
            this.lblBackColor1.Text = "Back Color 1";
            // 
            // PaletteEditorSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(385, 225);
            this.Controls.Add(this.pnlPalette);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteEditorSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Customization Settings";
            this.gbxSizing.ResumeLayout(false);
            this.gbxSizing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).EndInit();
            this.pnlPalette.ResumeLayout(false);
            this.gbxCursor.ResumeLayout(false);
            this.gbxMarker.ResumeLayout(false);
            this.gbxMarker.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDashLength2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDashLength1)).EndInit();
            this.gbxBackground.ResumeLayout(false);
            this.gbxBackground.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxSizing;
        private System.Windows.Forms.NumericUpDown nudColumns;
        private System.Windows.Forms.NumericUpDown nudRows;
        private System.Windows.Forms.Label lblNumColumns;
        private System.Windows.Forms.Label lblNumRows;
        private System.Windows.Forms.Panel pnlPalette;
        private System.Windows.Forms.ComboBox cbxZoom;
        private System.Windows.Forms.Label lblZoomSize;
        private System.Windows.Forms.GroupBox gbxBackground;
        private System.Windows.Forms.ComboBox cbxBackZoom;
        private System.Windows.Forms.Label lblBackZoomSize;
        private System.Windows.Forms.Label lblBackColor2;
        private System.Windows.Forms.Label lblBackColor1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Controls.DrawControl drwBGExample;
        private Controls.ColorPicker cpkBackColor1;
        private Controls.ColorPicker cpkBackColor2;
        private System.Windows.Forms.GroupBox gbxMarker;
        private System.Windows.Forms.Label lblDashColor2;
        private System.Windows.Forms.Label lblDashColor1;
        private Controls.ColorPicker cpkDashColor2;
        private Controls.ColorPicker cpkDashColor1;
        private System.Windows.Forms.NumericUpDown nudDashLength2;
        private System.Windows.Forms.NumericUpDown nudDashLength1;
        private System.Windows.Forms.Label lblDashLength2;
        private System.Windows.Forms.Label lblDashLength1;
        private System.Windows.Forms.GroupBox gbxCursor;
        private System.Windows.Forms.ComboBox cbxCursor;
    }
}