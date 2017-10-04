namespace MushROMs.GenericEditor.PaletteEditor
{
    partial class PaletteParent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaletteParent));
            this.tlsMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tssMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmFindAndReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGoto = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInvert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHorizontalGradient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVerticalGradient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmColorize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditBackColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLumaGrayscale = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmWeightedGrayscale = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCustomize = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tlsMain.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlsMain
            // 
            this.tlsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.tsbSaveAll,
            this.toolStripSeparator6,
            this.tsbCut,
            this.tsbCopy,
            this.tsbPaste,
            this.toolStripSeparator7,
            this.tsbHelp});
            this.tlsMain.Location = new System.Drawing.Point(0, 24);
            this.tlsMain.Name = "tlsMain";
            this.tlsMain.Size = new System.Drawing.Size(984, 25);
            this.tlsMain.TabIndex = 5;
            this.tlsMain.Text = "...";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "&New";
            this.tsbNew.Click += new System.EventHandler(this.tsmNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "&Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "&Save";
            this.tsbSave.Click += new System.EventHandler(this.tsmSave_Click);
            // 
            // tsbSaveAll
            // 
            this.tsbSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAll.Enabled = false;
            this.tsbSaveAll.Image = global::MushROMs.GenericEditor.Properties.Resources.saveAll;
            this.tsbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAll.Name = "tsbSaveAll";
            this.tsbSaveAll.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAll.Text = "Save All";
            this.tsbSaveAll.ToolTipText = "Save all unsaved palettes";
            this.tsbSaveAll.Click += new System.EventHandler(this.tsmSaveAll_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Enabled = false;
            this.tsbCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbCut.Image")));
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(23, 22);
            this.tsbCut.Text = "C&ut";
            this.tsbCut.Click += new System.EventHandler(this.tsmCut_Click);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Enabled = false;
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbCopy.Text = "&Copy";
            this.tsbCopy.Click += new System.EventHandler(this.tsmCopy_Click);
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Enabled = false;
            this.tsbPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbPaste.Image")));
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbPaste.Text = "&Paste";
            this.tsbPaste.Click += new System.EventHandler(this.tsmPaste_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Enabled = false;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "He&lp";
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssMain});
            this.stsMain.Location = new System.Drawing.Point(0, 540);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(984, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.TabIndex = 3;
            this.stsMain.Text = "Status...";
            // 
            // tssMain
            // 
            this.tssMain.Name = "tssMain";
            this.tssMain.Size = new System.Drawing.Size(70, 17);
            this.tssMain.Text = "Editor ready";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmEdit,
            this.tsmTools,
            this.optionsToolStripMenuItem,
            this.tsmHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(984, 24);
            this.mnuMain.TabIndex = 2;
            this.mnuMain.Text = "Menu...";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNew,
            this.tsmOpen,
            this.tsmRecentFiles,
            this.toolStripSeparator,
            this.tsmSave,
            this.tsmSaveAs,
            this.tsmSaveAll,
            this.toolStripSeparator1,
            this.tsmExit});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(37, 20);
            this.tsmFile.Text = "&File";
            // 
            // tsmNew
            // 
            this.tsmNew.Image = ((System.Drawing.Image)(resources.GetObject("tsmNew.Image")));
            this.tsmNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmNew.Name = "tsmNew";
            this.tsmNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmNew.Size = new System.Drawing.Size(187, 22);
            this.tsmNew.Text = "&New";
            this.tsmNew.ToolTipText = "Create a new palette.";
            this.tsmNew.Click += new System.EventHandler(this.tsmNew_Click);
            // 
            // tsmOpen
            // 
            this.tsmOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsmOpen.Image")));
            this.tsmOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmOpen.Size = new System.Drawing.Size(187, 22);
            this.tsmOpen.Text = "&Open";
            this.tsmOpen.ToolTipText = "Open an existing palette from a file.";
            this.tsmOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // tsmRecentFiles
            // 
            this.tsmRecentFiles.Enabled = false;
            this.tsmRecentFiles.Name = "tsmRecentFiles";
            this.tsmRecentFiles.Size = new System.Drawing.Size(187, 22);
            this.tsmRecentFiles.Text = "Open &Recent";
            this.tsmRecentFiles.ToolTipText = "Open a recently opened file";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(184, 6);
            // 
            // tsmSave
            // 
            this.tsmSave.Enabled = false;
            this.tsmSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmSave.Image")));
            this.tsmSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(187, 22);
            this.tsmSave.Text = "&Save";
            this.tsmSave.ToolTipText = "Save the current palette";
            this.tsmSave.Click += new System.EventHandler(this.tsmSave_Click);
            // 
            // tsmSaveAs
            // 
            this.tsmSaveAs.Enabled = false;
            this.tsmSaveAs.Name = "tsmSaveAs";
            this.tsmSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.tsmSaveAs.Size = new System.Drawing.Size(187, 22);
            this.tsmSaveAs.Text = "Save &As";
            this.tsmSaveAs.ToolTipText = "Save the current palette to a new file location.";
            this.tsmSaveAs.Click += new System.EventHandler(this.tsmSaveAs_Click);
            // 
            // tsmSaveAll
            // 
            this.tsmSaveAll.Enabled = false;
            this.tsmSaveAll.Image = global::MushROMs.GenericEditor.Properties.Resources.saveAll;
            this.tsmSaveAll.Name = "tsmSaveAll";
            this.tsmSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.tsmSaveAll.Size = new System.Drawing.Size(187, 22);
            this.tsmSaveAll.Text = "Save All";
            this.tsmSaveAll.ToolTipText = "Save all unsaved palettes.";
            this.tsmSaveAll.Click += new System.EventHandler(this.tsmSaveAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(187, 22);
            this.tsmExit.Text = "E&xit";
            this.tsmExit.ToolTipText = "Exit the application.";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // tsmEdit
            // 
            this.tsmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUndo,
            this.tsmRedo,
            this.toolStripSeparator3,
            this.tsmCut,
            this.tsmCopy,
            this.tsmPaste,
            this.tsmDelete,
            this.toolStripSeparator4,
            this.tsmSelectAll,
            this.toolStripSeparator2,
            this.tsmFindAndReplace,
            this.tsmGoto});
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(39, 20);
            this.tsmEdit.Text = "&Edit";
            // 
            // tsmUndo
            // 
            this.tsmUndo.Enabled = false;
            this.tsmUndo.Name = "tsmUndo";
            this.tsmUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmUndo.Size = new System.Drawing.Size(164, 22);
            this.tsmUndo.Text = "&Undo";
            this.tsmUndo.ToolTipText = "Undo last action.";
            this.tsmUndo.Click += new System.EventHandler(this.tsmUndo_Click);
            // 
            // tsmRedo
            // 
            this.tsmRedo.Enabled = false;
            this.tsmRedo.Name = "tsmRedo";
            this.tsmRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.tsmRedo.Size = new System.Drawing.Size(164, 22);
            this.tsmRedo.Text = "&Redo";
            this.tsmRedo.ToolTipText = "Redo action.";
            this.tsmRedo.Click += new System.EventHandler(this.tsmRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // tsmCut
            // 
            this.tsmCut.Enabled = false;
            this.tsmCut.Image = ((System.Drawing.Image)(resources.GetObject("tsmCut.Image")));
            this.tsmCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmCut.Name = "tsmCut";
            this.tsmCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmCut.Size = new System.Drawing.Size(164, 22);
            this.tsmCut.Text = "Cu&t";
            this.tsmCut.ToolTipText = "Cut palette selection.";
            this.tsmCut.Click += new System.EventHandler(this.tsmCut_Click);
            // 
            // tsmCopy
            // 
            this.tsmCopy.Enabled = false;
            this.tsmCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsmCopy.Image")));
            this.tsmCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.ShortcutKeyDisplayString = "Ctrl+C";
            this.tsmCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmCopy.Size = new System.Drawing.Size(164, 22);
            this.tsmCopy.Text = "&Copy";
            this.tsmCopy.ToolTipText = "Copy palette selection.";
            this.tsmCopy.Click += new System.EventHandler(this.tsmCopy_Click);
            // 
            // tsmPaste
            // 
            this.tsmPaste.Enabled = false;
            this.tsmPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsmPaste.Image")));
            this.tsmPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmPaste.Name = "tsmPaste";
            this.tsmPaste.ShortcutKeyDisplayString = "Ctrl+V";
            this.tsmPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmPaste.Size = new System.Drawing.Size(164, 22);
            this.tsmPaste.Text = "&Paste";
            this.tsmPaste.ToolTipText = "Paste palette selection (current width of editor may be a factor).";
            this.tsmPaste.Click += new System.EventHandler(this.tsmPaste_Click);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Enabled = false;
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmDelete.Size = new System.Drawing.Size(164, 22);
            this.tsmDelete.Text = "&Delete";
            this.tsmDelete.ToolTipText = "Black out selection.";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
            // 
            // tsmSelectAll
            // 
            this.tsmSelectAll.Enabled = false;
            this.tsmSelectAll.Name = "tsmSelectAll";
            this.tsmSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmSelectAll.Size = new System.Drawing.Size(164, 22);
            this.tsmSelectAll.Text = "Select &All";
            this.tsmSelectAll.ToolTipText = "Select all colors in palette.";
            this.tsmSelectAll.Click += new System.EventHandler(this.tsmSelectAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // tsmFindAndReplace
            // 
            this.tsmFindAndReplace.Enabled = false;
            this.tsmFindAndReplace.Name = "tsmFindAndReplace";
            this.tsmFindAndReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tsmFindAndReplace.Size = new System.Drawing.Size(164, 22);
            this.tsmFindAndReplace.Text = "&Find";
            this.tsmFindAndReplace.ToolTipText = "Find color sequences in the palette.";
            this.tsmFindAndReplace.Click += new System.EventHandler(this.tsmFindAndReplace_Click);
            // 
            // tsmGoto
            // 
            this.tsmGoto.Enabled = false;
            this.tsmGoto.Name = "tsmGoto";
            this.tsmGoto.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.tsmGoto.Size = new System.Drawing.Size(164, 22);
            this.tsmGoto.Text = "&Go To ...";
            this.tsmGoto.ToolTipText = "Go to address (only valid for ROMs).";
            this.tsmGoto.Click += new System.EventHandler(this.tsmGoto_Click);
            // 
            // tsmTools
            // 
            this.tsmTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmInvert,
            this.tsmHorizontalGradient,
            this.tsmVerticalGradient,
            this.tsmColorize,
            this.tsmEditBackColor,
            this.tsmLumaGrayscale,
            this.tsmWeightedGrayscale});
            this.tsmTools.Name = "tsmTools";
            this.tsmTools.Size = new System.Drawing.Size(48, 20);
            this.tsmTools.Text = "&Tools";
            // 
            // tsmInvert
            // 
            this.tsmInvert.Enabled = false;
            this.tsmInvert.Name = "tsmInvert";
            this.tsmInvert.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsmInvert.Size = new System.Drawing.Size(223, 22);
            this.tsmInvert.Text = "&Invert";
            this.tsmInvert.ToolTipText = "Invert the colors.";
            this.tsmInvert.Click += new System.EventHandler(this.tsmInvert_Click);
            // 
            // tsmHorizontalGradient
            // 
            this.tsmHorizontalGradient.Enabled = false;
            this.tsmHorizontalGradient.Name = "tsmHorizontalGradient";
            this.tsmHorizontalGradient.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.tsmHorizontalGradient.Size = new System.Drawing.Size(223, 22);
            this.tsmHorizontalGradient.Text = "&Horizontal Gradient";
            this.tsmHorizontalGradient.ToolTipText = "Apply a horizontal gradient effect.";
            this.tsmHorizontalGradient.Click += new System.EventHandler(this.tsmHorizontalGradient_Click);
            // 
            // tsmVerticalGradient
            // 
            this.tsmVerticalGradient.Enabled = false;
            this.tsmVerticalGradient.Name = "tsmVerticalGradient";
            this.tsmVerticalGradient.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.tsmVerticalGradient.Size = new System.Drawing.Size(223, 22);
            this.tsmVerticalGradient.Text = "&Vertical Gradient";
            this.tsmVerticalGradient.ToolTipText = "Apply a vertical gradient effect.";
            this.tsmVerticalGradient.Click += new System.EventHandler(this.tsmVerticalGradient_Click);
            // 
            // tsmColorize
            // 
            this.tsmColorize.Enabled = false;
            this.tsmColorize.Name = "tsmColorize";
            this.tsmColorize.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.tsmColorize.Size = new System.Drawing.Size(223, 22);
            this.tsmColorize.Text = "&Colorize";
            this.tsmColorize.ToolTipText = "Colorize the selection.";
            this.tsmColorize.Click += new System.EventHandler(this.tsmColorize_Click);
            // 
            // tsmEditBackColor
            // 
            this.tsmEditBackColor.Enabled = false;
            this.tsmEditBackColor.Name = "tsmEditBackColor";
            this.tsmEditBackColor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.tsmEditBackColor.Size = new System.Drawing.Size(223, 22);
            this.tsmEditBackColor.Text = "Edit &Back Color";
            this.tsmEditBackColor.ToolTipText = "Edit back color (only valid for .mw3 files).";
            this.tsmEditBackColor.Click += new System.EventHandler(this.tsmEditBackColor_Click);
            // 
            // tsmLumaGrayscale
            // 
            this.tsmLumaGrayscale.Enabled = false;
            this.tsmLumaGrayscale.Name = "tsmLumaGrayscale";
            this.tsmLumaGrayscale.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.tsmLumaGrayscale.Size = new System.Drawing.Size(223, 22);
            this.tsmLumaGrayscale.Text = "&Luma Grayscale";
            this.tsmLumaGrayscale.ToolTipText = "Apply a luma-weighted RGB grayscale. This effect looks better than the standard g" +
    "rayscale usually.";
            this.tsmLumaGrayscale.Click += new System.EventHandler(this.tsmLumaGrayscale_Click);
            // 
            // tsmWeightedGrayscale
            // 
            this.tsmWeightedGrayscale.Enabled = false;
            this.tsmWeightedGrayscale.Name = "tsmWeightedGrayscale";
            this.tsmWeightedGrayscale.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tsmWeightedGrayscale.Size = new System.Drawing.Size(223, 22);
            this.tsmWeightedGrayscale.Text = "&Weighted Grayscale";
            this.tsmWeightedGrayscale.ToolTipText = "Apply a custom grayscale with user-defined RGB component weights.";
            this.tsmWeightedGrayscale.Click += new System.EventHandler(this.tsmWeightedGrayscale_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCustomize,
            this.resetAllSettingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // tsmCustomize
            // 
            this.tsmCustomize.Name = "tsmCustomize";
            this.tsmCustomize.Size = new System.Drawing.Size(164, 22);
            this.tsmCustomize.Text = "&Customize";
            this.tsmCustomize.ToolTipText = "Customize settings for the editor.";
            this.tsmCustomize.Click += new System.EventHandler(this.tsmCustomize_Click);
            // 
            // resetAllSettingsToolStripMenuItem
            // 
            this.resetAllSettingsToolStripMenuItem.Name = "resetAllSettingsToolStripMenuItem";
            this.resetAllSettingsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.resetAllSettingsToolStripMenuItem.Text = "&Reset All Settings";
            this.resetAllSettingsToolStripMenuItem.ToolTipText = "Reset all settings back to default.";
            this.resetAllSettingsToolStripMenuItem.Click += new System.EventHandler(this.ResetSettings_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAbout});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(44, 20);
            this.tsmHelp.Text = "&Help";
            // 
            // tsmAbout
            // 
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(116, 22);
            this.tsmAbout.Text = "&About...";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAbout_Click);
            // 
            // PaletteParent
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.tlsMain);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.mnuMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "PaletteParent";
            this.Text = "Palette Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PaletteParent_FormClosing);
            this.MdiChildActivate += new System.EventHandler(this.PaletteParent_MdiChildActivate);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PaletteParent_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PaletteParent_DragEnter);
            this.tlsMain.ResumeLayout(false);
            this.tlsMain.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmNew;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmCut;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmTools;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.ToolStripStatusLabel tssMain;
        private System.Windows.Forms.ToolStrip tlsMain;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmInvert;
        private System.Windows.Forms.ToolStripMenuItem tsmColorize;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveAll;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmEditBackColor;
        private System.Windows.Forms.ToolStripMenuItem tsmCustomize;
        private System.Windows.Forms.ToolStripMenuItem tsmLumaGrayscale;
        private System.Windows.Forms.ToolStripMenuItem resetAllSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmRecentFiles;
        private System.Windows.Forms.ToolStripMenuItem tsmWeightedGrayscale;
        private System.Windows.Forms.ToolStripMenuItem tsmHorizontalGradient;
        private System.Windows.Forms.ToolStripMenuItem tsmVerticalGradient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmFindAndReplace;
        private System.Windows.Forms.ToolStripMenuItem tsmGoto;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripButton tsbSaveAll;
    }
}