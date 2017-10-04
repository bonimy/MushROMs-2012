using System.ComponentModel;
using System.Windows.Forms;
using MushROMs.Controls;

namespace MushROMs.GenericEditor
{
    public class SNESSubEditor : EditorForm
    {
        private SNESEditor snesEditor;

        [Browsable(false)]
        public SNESEditor SNESEditor
        {
            get { return this.snesEditor; }
            set { this.snesEditor = value; }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && this.snesEditor != null)
            {
                this.Visible = false;
                e.Cancel = true;
            }

            base.OnFormClosing(e);
        }
    }
}