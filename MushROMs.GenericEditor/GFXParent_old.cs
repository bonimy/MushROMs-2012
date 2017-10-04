using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MushROMs.LunarCompress;
using MushROMs.Controls;
using MushROMs.SNESLibrary;

namespace MushROMs.GenericEditor
{
    public partial class GFXParent_old : SNESSubEditor
    {
        private const string DefaultExt = "bin";
        private const string DefaultFilter = "Binary Files|*.bin|LZ Compressed Files|*.lz1, *.lz2, *.lz3, *.lz4, *.lz5, *.lz6, *.lz7, *.lz8, *.lz9, *.lz10, *.lz11, *.lz12, *.lz13, *.lz14, *.lz15, *.lz16|All Files|*.*";
        private const int DefaultFilterIndex = 0;

        private GFXStatusForm gfxStatus;

        public GFXParent_old()
        {
            InitializeComponent();

            this.gfxStatus = new GFXStatusForm();
            this.gfxStatus.MdiParent = this;
            this.gfxStatus.Location = new Point(this.ClientWidth - this.gfxStatus.Width - 10, 10);
            this.gfxStatus.Show();
        }

        private void tsmNew_Click(object sender, EventArgs e)
        {
            GFXEditor_old editor = new GFXEditor_old(GFXEditor_old.MaxTiles);
            editor.MdiParent = this;
            editor.Show();
        }

        private void tsmOpen_Click(object sender, EventArgs e)
        {
            OpenGFX();
        }

        private void OpenGFX()
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = DefaultExt;
            dlg.Filter = DefaultFilter;
            dlg.FilterIndex = DefaultFilterIndex;
            dlg.Title = "Open a GFX file";
            if (dlg.ShowDialog() == DialogResult.OK)
                OpenGFX(dlg.FileName);
        }

        private void OpenGFX(string path)
        {
            GFXEditor_old editor = new GFXEditor_old(0x100);
            editor.MdiParent = this;
            editor.Show();
        }
    }
}