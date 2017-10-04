namespace MushROMs.GenericEditor.PaletteEditor
{
    partial class FindReplaceForm
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
            this.drwColors = new MushROMs.Controls.EditorControl();
            this.nudNumColors = new System.Windows.Forms.NumericUpDown();
            this.gbxFind = new System.Windows.Forms.GroupBox();
            this.gbxDirections = new System.Windows.Forms.GroupBox();
            this.rdbDown = new System.Windows.Forms.RadioButton();
            this.rdbUp = new System.Windows.Forms.RadioButton();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumColors)).BeginInit();
            this.gbxFind.SuspendLayout();
            this.gbxDirections.SuspendLayout();
            this.SuspendLayout();
            // 
            // drwColors
            // 
            this.drwColors.BackColor = System.Drawing.Color.Magenta;
            this.drwColors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drwColors.Location = new System.Drawing.Point(6, 19);
            this.drwColors.Margin = new System.Windows.Forms.Padding(0);
            this.drwColors.Name = "drwColors";
            this.drwColors.OverrideInputKeys = new System.Windows.Forms.Keys[0];
            this.drwColors.TabIndex = 0;
            this.drwColors.Paint += new System.Windows.Forms.PaintEventHandler(this.drwColors_Paint);
            this.drwColors.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drwColors_MouseClick);
            // 
            // nudNumColors
            // 
            this.nudNumColors.Location = new System.Drawing.Point(267, 19);
            this.nudNumColors.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudNumColors.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumColors.Name = "nudNumColors";
            this.nudNumColors.Size = new System.Drawing.Size(48, 20);
            this.nudNumColors.TabIndex = 1;
            this.nudNumColors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNumColors.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumColors.ValueChanged += new System.EventHandler(this.nudNumColors_ValueChanged);
            // 
            // gbxFind
            // 
            this.gbxFind.Controls.Add(this.nudNumColors);
            this.gbxFind.Controls.Add(this.drwColors);
            this.gbxFind.Location = new System.Drawing.Point(12, 12);
            this.gbxFind.Name = "gbxFind";
            this.gbxFind.Size = new System.Drawing.Size(321, 45);
            this.gbxFind.TabIndex = 2;
            this.gbxFind.TabStop = false;
            this.gbxFind.Text = "Find";
            // 
            // gbxDirections
            // 
            this.gbxDirections.Controls.Add(this.rdbDown);
            this.gbxDirections.Controls.Add(this.rdbUp);
            this.gbxDirections.Enabled = false;
            this.gbxDirections.Location = new System.Drawing.Point(177, 63);
            this.gbxDirections.Name = "gbxDirections";
            this.gbxDirections.Size = new System.Drawing.Size(156, 42);
            this.gbxDirections.TabIndex = 3;
            this.gbxDirections.TabStop = false;
            this.gbxDirections.Text = "Direction";
            // 
            // rdbDown
            // 
            this.rdbDown.AutoSize = true;
            this.rdbDown.Checked = true;
            this.rdbDown.Location = new System.Drawing.Point(97, 19);
            this.rdbDown.Name = "rdbDown";
            this.rdbDown.Size = new System.Drawing.Size(53, 17);
            this.rdbDown.TabIndex = 1;
            this.rdbDown.TabStop = true;
            this.rdbDown.Text = "Down";
            this.rdbDown.UseVisualStyleBackColor = true;
            // 
            // rdbUp
            // 
            this.rdbUp.AutoSize = true;
            this.rdbUp.Location = new System.Drawing.Point(6, 19);
            this.rdbUp.Name = "rdbUp";
            this.rdbUp.Size = new System.Drawing.Size(39, 17);
            this.rdbUp.TabIndex = 0;
            this.rdbUp.Text = "Up";
            this.rdbUp.UseVisualStyleBackColor = true;
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(177, 111);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(75, 23);
            this.btnFindNext.TabIndex = 4;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(258, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FindReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 146);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.gbxDirections);
            this.Controls.Add(this.gbxFind);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindReplaceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Find";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindReplaceForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudNumColors)).EndInit();
            this.gbxFind.ResumeLayout(false);
            this.gbxDirections.ResumeLayout(false);
            this.gbxDirections.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.EditorControl drwColors;
        private System.Windows.Forms.NumericUpDown nudNumColors;
        private System.Windows.Forms.GroupBox gbxFind;
        private System.Windows.Forms.GroupBox gbxDirections;
        private System.Windows.Forms.RadioButton rdbDown;
        private System.Windows.Forms.RadioButton rdbUp;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Button btnCancel;

    }
}