using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Lets the user know that they are exiting the application with some files still unsaved.
    /// A list of the unsaved files is shown and the user is given the option to save them.
    /// </summary>
    [DefaultEvent("FileOk")]
    [Description("Displays a dialog box that warns the user of exiting the application while unsaved files still exist.")]
    public class UnsavedDialog : MarshalByRefObject, IComponent, IDisposable
    {
        /// <summary>
        /// Occurs when the user clicks on the OK button.
        /// </summary>
        [Browsable(true)]
        [Category("Dialog")]
        [Description("Occurs when the user clicks on the Open or Save button on a file dialog box.")]
        public event CancelEventHandler FileOk;

        /// <summary>
        /// Occurs when the component is disposed by a call to the
        /// <see cref="System.ComponentModel.Component.Dispose()"/> method.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public event EventHandler Disposed;

        private UnsavedForm form;

        /// <summary>
        /// Gets the <see cref="System.ComponentModel.IContainer"/> that contains the <see cref="System.ComponentModel.Component"/>.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IContainer Container
        {
            get { return this.form.Container; }
        }

        /// <summary>
        /// Gets the <see cref="System.ComponentModel.ISite"/> that contains the <see cref="System.ComponentModel.Component"/>.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ISite Site
        {
            get { return this.form.Site; }
            set { this.form.Site = value; }
        }

        [Category("Editor")]
        [Description("A list of files to show.")]
        public List<string> Files
        {
            get { return this.form.Files; }
            set { this.form.Files = value; }
        }

        [Category("Appearance")]
        [Description("The title of the dialog window.")]
        public string Title
        {
            get { return this.form.Text; }
            set { this.form.Text = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsavedDialog"/> class.
        /// </summary>
        public UnsavedDialog()
        {
            this.form = new UnsavedForm();
            this.form.Disposed += new EventHandler(form_Disposed);
            this.form.FormClosing += new FormClosingEventHandler(form_FormClosing);
        }

        ~UnsavedDialog()
        {
            this.form.Dispose();
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the currently active window set
        /// as its owner.
        /// </summary>
        /// <returns>
        /// One of the <see cref="System.Windows.Forms.DialogResult"/> values.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The form specified in the owner parameter is the same as the form being shown.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The form being shown is already visible.  -or- The form being shown is disabled.
        ///  -or- The form being shown is not a top-level window.  -or- The form being
        /// shown as a dialog box is already a modal form.  -or- The current process
        /// is not running in user interactive mode (for more information, see <see cref="System.Windows.Forms.SystemInformation.UserInteractive"/>).
        /// </exception>
        public DialogResult ShowDialog()
        {
            return this.form.ShowDialog();
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">
        /// Any object that implements <see cref="System.Windows.Forms.IWin32Window"/> that represents
        /// the top-level window that will own the modal dialog box.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The form specified in the owner parameter is the same as the form being shown.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The form being shown is already visible.  -or- The form being shown is disabled.
        ///  -or- The form being shown is not a top-level window.  -or- The form being
        /// shown as a dialog box is already a modal form.  -or- The current process
        /// is not running in user interactive mode (for more information, see <see cref="System.Windows.Forms.SystemInformation.UserInteractive"/>).
        /// </exception>
        public DialogResult ShowDialog(IWin32Window owner)
        {
            return this.form.ShowDialog(owner);
        }

        /// <summary>
        /// Creates an object that contains all the relevant information required to
        /// generate a proxy used to communicate with a remote object.
        /// </summary>
        /// <param name="requestedType">
        /// The <see cref="System.Type"/> of the object that the new <see cref="System.Runtime.Remoting.ObjRef"/>
        /// will reference.
        /// </param>
        /// <returns>
        /// Information required to generate a proxy.
        /// </returns>
        /// <exception cref="System.Runtime.Remoting.RemotingException">
        /// This instance is not a valid remoting object.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// The immediate caller does not have infrastructure permission.
        /// </exception>
        public override ObjRef CreateObjRef(Type requestedType)
        {
            return this.form.CreateObjRef(requestedType);
        }

        /// <summary>
        /// Obtains a lifetime service object to control the lifetime policy for this
        /// instance.
        /// </summary>
        /// <returns>
        /// An object of type <see cref="System.Runtime.Remoting.Lifetime.ILease"/> used to control
        /// the lifetime policy for this instance. This is the current lifetime service
        /// object for this instance if one exists; otherwise, a new lifetime service
        /// object initialized to the value of the <see cref="System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime"/>
        /// property.
        /// </returns>
        public override object InitializeLifetimeService()
        {
            return this.form.InitializeLifetimeService();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="System.ComponentModel.Component"/>.
        /// </summary>
        public void Dispose()
        {
            this.form.Dispose();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.form.DialogResult == DialogResult.OK)
                if (FileOk != null)
                    FileOk(this, e);
        }

        private void form_Disposed(object sender, EventArgs e)
        {
            if (Disposed != null)
                Disposed(this, e);
        }
    }
}
