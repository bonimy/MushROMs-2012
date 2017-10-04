using System;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.GenericEditor.PaletteEditor;
using MushROMs.GenericEditor.GFXEditor;
using MushROMs.SNESLibrary;

namespace MushROMs.GenericEditor
{
    public partial class SNESEditor : EditorForm
    {
        #region Constant and readonly fields
        #region Default Properties
        public const int PaletteWidth = 0x10;
        public const int PaletteHeight = 0x10;
        public const int PaletteSize = PaletteHeight * PaletteWidth;

        public const int GFXTileWidth = 8;
        public const int GFXTileHeight = 8;
        public const int GFXTileSize = GFXTileHeight * GFXTileWidth;
        #endregion

        #region File extension strings
        public const string ExtensionTPL = ".tpl";
        public const string ExtensionMW3 = ".mw3";
        public const string ExtensionPAL = ".pal";
        public const string ExtensionBIN = ".bin";
        public const string ExtensionS9X_0to9 = ".00";
        public const string ExtensionZST = ".zst";
        public const string ExtensionZST_0to9 = ".zs";
        public const string ExtensionZST_10to99 = ".z";
        public const string AltExtensionZST = ".zss";
        public const string ExtensionSMC = ".smc";
        public const string ExtensionSFC = ".sfc";
        public const string ExtensionSWC = ".swc";
        public const string ExtensionFIG = ".fig";
        public const string ExtensionDMP = ".txt";
        #endregion

        #region Formatting info
        public const string UnsavedNotification = "*";
        #endregion
        #endregion

        private const bool DefaultAnimate = true;
        private const FPSModes DefaultFPSMode = FPSModes.NTSC;
        private const FrameReductions DefaultFrameReduction = FrameReductions.None;
        public const double DefaultDashWait = 400;

        PaletteParent paletteEditor;
        GFXParent gfxEditor;

        public PaletteParent PaletteEditor
        {
            get { return this.paletteEditor; }
        }

        public GFXParent GFXEditor
        {
            get { return this.gfxEditor; }
        }

        public SNESEditor(string[] args)
        {
            InitializeComponent();

            this.paletteEditor = new PaletteParent(args);
            this.paletteEditor.SNESEditor = this;
            this.paletteEditor.Tag = this.tsmPaletteEditor;
            this.paletteEditor.VisibleChanged += new EventHandler(SubEditor_VisibleChanged);
            this.tsmPaletteEditor.Tag = this.paletteEditor;

            this.gfxEditor = new GFXParent(args);
            this.gfxEditor.SNESEditor = this;
            this.gfxEditor.Tag = this.tsmGFXEditor;
            this.gfxEditor.VisibleChanged += new EventHandler(SubEditor_VisibleChanged);
            this.tsmGFXEditor.Tag = this.gfxEditor;
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmSubEditor_Click(object sender, EventArgs e)
        {
            SNESSubEditor editor = (SNESSubEditor)((ToolStripMenuItem)sender).Tag;
            editor.Visible ^= true;
        }

        private void SubEditor_VisibleChanged(object sender, EventArgs e)
        {
            SNESSubEditor editor = (SNESSubEditor)sender;
            ((ToolStripMenuItem)editor.Tag).Checked = editor.Visible;
        }
    }
}