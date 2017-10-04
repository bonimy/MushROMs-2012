using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// A text box for writing a file to open. Included an open file dialog to prompt the user for a file. This class cannot be inherited.
    /// </summary>
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Designer(typeof(HorizontalScrollOnlyControlDesigner))]
    public sealed partial class OpenFileControl : UserControl
    {
        /// <summary>
        /// Occurs when the <see cref="OpenFileControl.Text"/> property value changes.
        /// </summary>
        [Description("Event raised when the value of the Text property is changed on Control.")]
        public new event EventHandler TextChanged;

        /// <summary>
        /// Occurs when the user clicks on the Open or Save button on a file dialog box.
        /// </summary>
        [Category("File Dialog")]
        [Description("Occurs when the user clicks on the Open or Save button on a file dialog box.")]
        public event CancelEventHandler DialogFileOk;

        /// <summary>
        /// Occurs when the user clicks the Help button on a common dialog box.
        /// </summary>
        [Category("File Dialog")]
        [Description("Occurs when the user clicks the Help button on a common dialog box.")]
        public event EventHandler DialogHelpRequest;

        /// <summary>
        /// Gets or sets the current text in the <see cref="SaveFileControl"/>.
        /// </summary>
        [Bindable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text
        {
            get { return this.tbxFilePath.Text; }
            set { this.tbxFilePath.Text = value; }
        }

        /// <summary>
        /// Gets or sets the file dialog box title.
        /// </summary>
        [DefaultValue("")]
        [Localizable(true)]
        [Category("File Browsing")]
        [Description("The string to display in the title bar of the dialog box.")]
        public string DialogTitle
        {
            get { return this.openFileDialog.Title; }
            set { this.openFileDialog.Title = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box automatically adds
        /// an extension to a file name if the user omits the extension.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Controls whether extensions are automatically added to file names.")]
        public bool AddExtension
        {
            get { return this.openFileDialog.AddExtension; }
            set { this.openFileDialog.AddExtension = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box displays a warning
        /// if the user specifies a file name that does not exist.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Indicates whether a warning appears when the user specifies a file that does not exist.")]
        public bool CheckFileExists
        {
            get { return this.openFileDialog.CheckFileExists; }
            set { this.openFileDialog.CheckFileExists = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box displays a warning
        /// if the user specifies a path that does not exist.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Checks that the specified path exists before returning from the dialog.")]
        public bool CheckPathExists
        {
            get { return this.openFileDialog.CheckPathExists; }
            set { this.openFileDialog.CheckPathExists = value; }
        }

        /// <summary>
        /// Gets the custom places collection for this <see cref="FileDialog"/> instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public FileDialogCustomPlacesCollection CustomPlaces
        {
            get { return this.openFileDialog.CustomPlaces; }
        }

        /// <summary>
        /// Gets or sets the default file name extension.
        /// </summary>
        [DefaultValue("")]
        [Category("File Browsing")]
        [Description("The default file name extension. If the user types a file name, this extension is added at the end of the file name if one is not specified.")]
        public string DefaultExt
        {
            get { return this.openFileDialog.DefaultExt; }
            set { this.openFileDialog.DefaultExt = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box returns the location
        /// of the file referenced by the shortcut or whether it returns the location
        /// of the shortcut (.lnk).
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Controls whether shortcuts are dereferenced before returning from the dialog.")]
        public bool DereferenceLinks
        {
            get { return this.openFileDialog.DereferenceLinks; }
            set { this.openFileDialog.DereferenceLinks = value; }
        }

        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices
        /// that appear in the "Save as file type" or "Files of type" box in the dialog
        /// box.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Filter format is invalid.
        /// </exception>
        [DefaultValue("")]
        [Localizable(true)]
        [Category("File Browsing")]
        [Description("The file filters to display in the dialog box, for example, \"C# files|*.cs|All files|*.*\".")]
        public string Filter
        {
            get { return this.openFileDialog.Filter; }
            set { this.openFileDialog.Filter = value; }
        }

        /// <summary>
        /// Gets or sets the index of the filter currently selected in the file dialog
        /// box.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(1)]
        [Description("The index of the file filter selected in the dialog box. The first item has an index of 1.")]
        public int FilterIndex
        {
            get { return this.openFileDialog.FilterIndex; }
            set { this.openFileDialog.FilterIndex = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box allows multiple files
        /// to be selected.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Controls whether multiple files can be selected in the dialog.")]
        public bool Multiselect
        {
            get { return this.openFileDialog.Multiselect; }
            set { this.openFileDialog.Multiselect = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the read-only check box is selected.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("The state of the read-only check box in the dialog.")]
        public bool ReadOnlyChecked
        {
            get { return this.openFileDialog.ReadOnlyChecked; }
            set { this.openFileDialog.ReadOnlyChecked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box restores the current
        /// directory before closing.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Controls whether the dialog box restores the current directory before closing.")]
        public bool RestoreDirectory
        {
            get { return this.openFileDialog.RestoreDirectory; }
            set { this.openFileDialog.RestoreDirectory = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Help button is displayed in the
        /// file dialog box.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Enables the Help button in the dialog box.")]
        public bool ShowDialogHelp
        {
            get { return this.openFileDialog.ShowHelp; }
            set { this.openFileDialog.ShowHelp = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box contains a read-only
        /// check box.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Controls whether to show the read-only checkbox in the dialog.")]
        public bool ShowReadOnly
        {
            get { return this.openFileDialog.ShowReadOnly; }
            set { this.openFileDialog.ShowReadOnly = value; }
        }

        /// <summary>
        /// Gets or sets whether the dialog box supports displaying and saving files
        /// that have multiple file name extensions.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Controls whether multidotted extensions are supported.")]
        public bool SupportMultiDottedExtension
        {
            get { return this.openFileDialog.SupportMultiDottedExtensions; }
            set { this.openFileDialog.SupportMultiDottedExtensions = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box accepts only valid
        /// Win32 file names.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Controls whether the dialog box ensures that file names do not contain invalid characters or sequences.")]
        public bool ValidateNames
        {
            get { return this.openFileDialog.ValidateNames; }
            set { this.openFileDialog.ValidateNames = value; }
        }

        /// <summary>
        /// Gets or sets a string containing the file name selected in the file dialog
        /// box.
        /// </summary>
        [DefaultValue("")]
        [Category("File Browsing")]
        [Description("The file first shown in the dialog box, or the last one selected by the user.")]
        public string FileName
        {
            get { return this.openFileDialog.FileName; }
            set { this.openFileDialog.FileName = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        [DefaultValue("")]
        [Category("File Browsing")]
        [Description("The intial directory for the dialog box.")]
        public string InitialDirectory
        {
            get { return this.openFileDialog.InitialDirectory; }
            set { this.openFileDialog.InitialDirectory = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this System.Windows.Forms.FileDialog
        /// instance should automatically upgrade appearance and behavior when running
        /// on Windows Vista.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(true)]
        public bool DialogAutoUpgradeEnabled
        {
            get { return this.openFileDialog.AutoUpgradeEnabled; }
            set { this.openFileDialog.AutoUpgradeEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can manually enter a file path
        /// in the text box.
        /// </summary>
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Determines whether the user can manually enter a file path in the text box.")]
        public bool AllowManualEdit
        {
            get { return this.tbxFilePath.Enabled; }
            set { this.tbxFilePath.Enabled = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileControl"/> class.
        /// </summary>
        public OpenFileControl()
        {
            InitializeComponent();

            this.openFileDialog.FileOk += new CancelEventHandler(openFileDialog_FileOk);
            this.openFileDialog.HelpRequest += new EventHandler(openFileDialog_HelpRequest);
        }

        /// <summary>
        /// Opens the file selected by the user, with read-only permission. The file
        /// is specified by the <see cref="FileDialog.FileName"/> property.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that specifies the read-only file selected by the user.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The file name is null.
        /// </exception>
        public Stream OpenFile()
        {
            return this.openFileDialog.OpenFile();
        }

        /// <summary>
        /// Resets all dialog box options to their default values.
        /// </summary>
        public void Reset()
        {
            this.openFileDialog.Reset();
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, 23, specified);
        }

        private new void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (DialogFileOk != null)
                DialogFileOk(this, e);
        }

        private void openFileDialog_HelpRequest(object sender, EventArgs e)
        {
            if (DialogHelpRequest != null)
                DialogHelpRequest(this, e);
        }

        private void tbxFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tbxFilePath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                    this.tbxFilePath.Text = files[0];
            }
        }

        private void tbxFilePath_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                this.Text = this.openFileDialog.FileName;
        }
    }
}