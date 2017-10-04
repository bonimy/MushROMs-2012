using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.LunarCompress;

namespace MushROMs.GenericEditor.GFXEditor
{
    public partial class GFXParent : SNESSubEditor
    {
        public const string DialogCaption = "Generic GFX Editor";

        private const int DefaultColumns = 0x10;
        private const int DefaultRows = DefaultColumns;
        private const int DefaultNumTiles = DefaultRows * DefaultColumns;
        private const byte FallbackPixel = 0;

        private const uint DefaultBGColor1 = 0xF8F8F8;
        private const uint DefaultBGColor2 = 0xC0C0C0;

        private const int DefaultDashLength1 = 4;
        private const int DefaultDashLength2 = DefaultDashLength1;
        private const uint DefaultDashColor1 = 0x000000;
        private const uint DefaultDashColor2 = 0xFFFFFF;

        private static readonly Cursor EditCursor = Cursors.Cross;

        private const string FilterOptionAll = "All valid GFX files";
        private const string FilterOptionsS9X = "SNES9x v1.53 save state files";
        private const string FilterOptionsZST = "ZSNES v1.51 save state files";
        private const string FilterOptionsSMC = "Super NES ROM files";
        private const string FilterOptionsBIN = "Binary files";

        private const string DefaultOpenTitle = "Open a GFX file.";
        private const string DefaultSaveTitle = "Save current GFX to file.";

        private OpenFileDialog ofdGFX;
        private SaveFileDialog sfdGFX;

        private List<GFXForm> editors;
        private GFXForm currentEditor;
        private int lastEditorIndex;

        public GFXParent(string[] args)
        {
            InitializeComponent();
            this.editors = new List<GFXForm>();
            NewGFX(0x100, GraphicsTypes.SNES_4BPP);
        }

        private void NewGFX()
        {
            CreateGFXForm dlg = new CreateGFXForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                NewGFX(dlg.NumTiles, dlg.GraphicType);
            }
        }

        private void NewGFX(int numTiles, GraphicsTypes gfxType)
        {
            GFXForm editor = new GFXForm();
            editor.CreateNew(gfxType, numTiles);
            LoadGFXEditor(editor);
        }

        private void LoadGFXEditor(GFXForm editor)
        {
            this.editors.Add(editor);

            editor.MdiParent = this;
            editor.Show();
        }

        private void tsmNew_Click(object sender, EventArgs e)
        {
            NewGFX();
        }
    }
}
