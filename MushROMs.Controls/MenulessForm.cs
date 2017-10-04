using System.Windows.Forms;

namespace MushROMs.Controls
{
    public unsafe class MenulessForm : EditorForm
    {
        private const int WS_SYSMENU = 0x80000;

        public MenulessForm()
        {
            this.MaximizeBox =
            this.MinimizeBox = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_SYSMENU;
                return cp;
            }
        }
    }
}