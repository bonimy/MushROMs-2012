using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MushROMs.Controls
{
    /// <summary>
    /// Provides a <see cref="TextBox"/> and <see cref="FolderBrowserDialog"/> for selecting
    /// a folder. This class cannot be inherited.
    /// </summary>
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Designer(typeof(HorizontalScrollOnlyControlDesigner))]
    public sealed partial class FolderBrowserControl : UserControl
    {
        /// <summary>
        /// Occurs when the <see cref="FolderBrowserControl.Text"/> property value changes.
        /// </summary>
        [Browsable(true)]
        [Description("Event raised when the value of the Text property is changed on Control.")]
        public new event EventHandler TextChanged;

        /// <summary>
        /// Gets or sets the current text in the <see cref="FolderBrowserControl"/>.
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
        /// Gets or sets the descriptive text displayed above the tree view control in
        /// the dialog box.
        /// </summary>
        [Browsable(true)]
        [Category("Folder Browsing")]
        [Description("The string that is displayed above the tree view control in the dialog box. This string can be used to specify instructions to the user.")]
        public string Description
        {
            get { return this.folderBrowserDialog.Description; }
            set { this.folderBrowserDialog.Description = value; }
        }

        /// <summary>
        /// Gets or sets the path selected by the user.
        /// </summary>
        [Browsable(true)]
        [Category("Folder Browsing")]
        [Description("The path of the folder first selected in the dialog or the last one selected by the user.")]
        public string SelectedPath
        {
            get { return this.folderBrowserDialog.SelectedPath; }
            set { this.folderBrowserDialog.SelectedPath = value; }
        }

        /// <summary>
        /// Gets or sets the root folder where the browsing starts from.
        /// </summary>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
        /// The value assigned is not one of the <see cref="System.Environment.SpecialFolder"/> values.
        /// </exception>
        [Browsable(true)]
        [Category("Folder Browsing")]
        [DefaultValue(Environment.SpecialFolder.Desktop)]
        [Description("The location of the root folder from which to start browsing. Only the specified folder and any subfolders that are beneath it will appear in the dialog box.")]
        public Environment.SpecialFolder RootFolder
        {
            get { return this.folderBrowserDialog.RootFolder; }
            set { this.folderBrowserDialog.RootFolder = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the New Folder button appears in
        /// the folder browser dialog box.
        /// </summary>
        [Browsable(true)]
        [Category("Folder Browsing")]
        [DefaultValue(true)]
        [Description("Include the New Folder button in the dialog box.")]
        public bool ShowNewFolderButton
        {
            get { return this.folderBrowserDialog.ShowNewFolderButton; }
            set { this.folderBrowserDialog.ShowNewFolderButton = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether the user can manually enter a
        /// file path in the text box.
        /// </summary>
        [Browsable(true)]
        [Category("Folder Browsing")]
        [DefaultValue(true)]
        [Description("Determines whether the user can manually enter a file path in the text box.")]
        public bool AllowManualEdit
        {
            get { return this.tbxFilePath.Enabled; }
            set { this.tbxFilePath.Enabled = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderBrowserControl"/> class.
        /// </summary>
        public FolderBrowserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Resets properties to their default values.
        /// </summary>
        public void Reset()
        {
            this.folderBrowserDialog.Reset();
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
            if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
                this.Text = this.folderBrowserDialog.SelectedPath;
        }
    }
}