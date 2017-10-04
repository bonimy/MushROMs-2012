using System;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    public partial class ErrorForm : Form
    {
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public string Message
        {
            get { return this.tbxMessage.Text; }
            set { this.tbxMessage.Text = value; }
        }

        public ErrorForm()
        {
            InitializeComponent();
        }

        private void ErrorForm_Shown(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Hand.Play();
        }
    }
}
