using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides a <see cref="TextBox"/> and <see cref="SaveFileDialog"/> for selecting a
    /// location for saving a file. This class cannot be inherited.
    /// </summary>
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Designer(typeof(HorizontalScrollOnlyControlDesigner))]
    public partial class SaveFileControl : UserControl
    {
        /// <summary>
        /// Occurs when the <see cref="SaveFileControl.Text"/> property value changes.
        /// </summary>
        [Browsable(true)]
        [Description("Event raised when the value of the Text property is changed on Control.")]
        public new event EventHandler TextChanged;

        /// <summary>
        /// Occurs when the user clicks on the Open or Save button on a file dialog box.
        /// </summary>
        [Browsable(true)]
        [Category("File Dialog")]
        [Description("Occurs when the user clicks on the Open or Save button on a file dialog box.")]
        public event CancelEventHandler DialogFileOk;

        /// <summary>
        /// Occurs when the user clicks the Help button on a common dialog box.
        /// </summary>
        [Browsable(true)]
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
        [Browsable(true)]
        [Category("File Browsing")]
        [Description("The string to display in the title bar of the dialog box.")]
        public string DialogTitle
        {
            get { return this.saveFileDialog.Title; }
            set { this.saveFileDialog.Title = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box automatically adds
        /// an extension to a file name if the user omits the extension.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Controls whether extensions are automatically added to file names.")]
        public bool AddExtension
        {
            get { return this.saveFileDialog.AddExtension; }
            set { this.saveFileDialog.AddExtension = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box displays a warning
        /// if the user specifies a file name that does not exist.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Indicates whether a warning appears when the user specifies a file that does not exist.")]
        public bool CheckFileExists
        {
            get { return this.saveFileDialog.CheckFileExists; }
            set { this.saveFileDialog.CheckFileExists = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box displays a warning
        /// if the user specifies a path that does not exist.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Checks that the specified path exists before returning from the dialog.")]
        public bool CheckPathExists
        {
            get { return this.saveFileDialog.CheckPathExists; }
            set { this.saveFileDialog.CheckPathExists = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box prompts the user for
        /// permission to create a file if the user specifies a file that does not exist.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Controls whether to prompt the user when a new file is about to be cereated. It is only applicable if 'ValidateNames' is set to true.")]
        public bool CreatePrompt
        {
            get { return this.saveFileDialog.CreatePrompt; }
            set { this.saveFileDialog.CreatePrompt = value; }
        }

        /// <summary>
        /// Gets or sets the default file name extension.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [Description("The default file name extension. If the user types a file name, this extension is added at the end of the file name if one is not specified.")]
        public string DefaultExt
        {
            get { return this.saveFileDialog.DefaultExt; }
            set { this.saveFileDialog.DefaultExt = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box returns the location
        /// of the file referenced by the shortcut or whether it returns the location
        /// of the shortcut (.lnk).
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Controls whether shortcuts are dereferenced before returning from the dialog.")]
        public bool DereferenceLinks
        {
            get { return this.saveFileDialog.DereferenceLinks; }
            set { this.saveFileDialog.DereferenceLinks = value; }
        }

        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices
        /// that appear in the "Save as file type" or "Files of type" box in the dialog
        /// box.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Filter format is invalid.
        /// </exception>
        [Browsable(true)]
        [Category("File Browsing")]
        [Description("The file filters to display in the dialog box, for example, \"C# files|*.cs|All files|*.*\".")]
        public string Filter
        {
            get { return this.saveFileDialog.Filter; }
            set { this.saveFileDialog.Filter = value; }
        }

        /// <summary>
        /// Gets or sets the index of the filter currently selected in the file dialog
        /// box.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(1)]
        [Description("The index of the file filter selected in the dialog box. The first item has an index of 1.")]
        public int FilterIndex
        {
            get { return this.saveFileDialog.FilterIndex; }
            set { this.saveFileDialog.FilterIndex = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Save As dialog box displays a
        /// warning if the user specifies a file name that already exists.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Controls whether to prompt the user when an existing file is about to be overwritten. It is only applicable if 'ValidateNames' is set to true.")]
        public bool OverwritePrompt
        {
            get { return this.saveFileDialog.OverwritePrompt; }
            set { this.saveFileDialog.OverwritePrompt = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box restores the current
        /// directory before closing.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Controls whether the dialog box restores the current directory before closing.")]
        public bool RestoreDirectory
        {
            get { return this.saveFileDialog.RestoreDirectory; }
            set { this.saveFileDialog.RestoreDirectory = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Help button is displayed in the
        /// file dialog box.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Enables the Help button in the dialog box.")]
        public bool ShowDialogHelp
        {
            get { return this.saveFileDialog.ShowHelp; }
            set { this.saveFileDialog.ShowHelp = value; }
        }

        /// <summary>
        /// Gets or sets whether the dialog box supports displaying and saving files
        /// that have multiple file name extensions.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(false)]
        [Description("Controls whether multidotted extensions are supported.")]
        public bool SupportMultiDottedExtension
        {
            get { return this.saveFileDialog.SupportMultiDottedExtensions; }
            set { this.saveFileDialog.SupportMultiDottedExtensions = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog box accepts only valid
        /// Win32 file names.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Controls whether the dialog box ensures that file names do not contain invalid characters or sequences.")]
        public bool ValidateNames
        {
            get { return this.saveFileDialog.ValidateNames; }
            set { this.saveFileDialog.ValidateNames = value; }
        }

        /// <summary>
        /// Gets or sets a string containing the file name selected in the file dialog
        /// box.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [Description("The file first shown in the dialog box, or the last one selected by the user.")]
        public string FileName
        {
            get { return this.saveFileDialog.FileName; }
            set { this.saveFileDialog.FileName = value; }
        }

        /// <summary>
        /// Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [Description("The intial directory for the dialog box.")]
        public string InitialDirectory
        {
            get { return this.saveFileDialog.InitialDirectory; }
            set { this.saveFileDialog.InitialDirectory = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this System.Windows.Forms.FileDialog
        /// instance should automatically upgrade appearance and behavior when running
        /// on Windows Vista.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(true)]
        public bool DialogAutoUpgradeEnabled
        {
            get { return this.saveFileDialog.AutoUpgradeEnabled; }
            set { this.saveFileDialog.AutoUpgradeEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can manually enter a file path
        /// in the text box.
        /// </summary>
        [Browsable(true)]
        [Category("File Browsing")]
        [DefaultValue(true)]
        [Description("Determines whether the user can manually enter a file path in the text box.")]
        public bool AllowManualEdit
        {
            get { return this.tbxFilePath.Enabled; }
            set { this.tbxFilePath.Enabled = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFileControl"/> class.
        /// </summary>
        public SaveFileControl()
        {
            InitializeComponent();

            this.saveFileDialog.FileOk += new CancelEventHandler(saveFileDialog_FileOk);
            this.saveFileDialog.HelpRequest += new EventHandler(saveFileDialog_HelpRequest);
        }

        /// <summary>
        /// Opens the file with read/write permission selected by the user.
        /// </summary>
        /// <returns>
        /// The read/write file selected by the user.
        /// </returns>
        public Stream OpenFile()
        {
            return this.saveFileDialog.OpenFile();
        }

        /// <summary>
        /// Resets all dialog box options to their default values.
        /// </summary>
        public void Reset()
        {
            this.saveFileDialog.Reset();
        }

        private new void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, 71, specified);
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (DialogFileOk != null)
                DialogFileOk(this, e);
        }

        private void saveFileDialog_HelpRequest(object sender, EventArgs e)
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
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
                this.Text = this.saveFileDialog.FileName;
        }
    }
}