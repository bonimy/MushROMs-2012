using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNESLibrary;

namespace MushROMs.GenericEditor.PaletteEditor
{
    public unsafe partial class PaletteParent : SNESSubEditor
    {
        public const string DialogCaption = "Generic Palette Editor";

        private const int DefaultColumns = PaletteForm.SNESPaletteWidth;
        private const int DefaultRows = PaletteForm.SNESPaletteHeight;
        private const int DefaultNumColors = PaletteForm.SNESPaletteSize;
        private const uint FallbackColor = 0xF800F8;         //SNES-Magenta
        private const PaletteDataFormats DefaultFormat = PaletteDataFormats.TPL;
        private const PaletteZoomScales DefaultZoom = PaletteZoomScales.Zoom32x;

        private const PaletteBGSizes DefaultBGSize = PaletteBGSizes.Size8x;
        private const uint DefaultBGColor1 = 0xF8F8F8;
        private const uint DefaultBGColor2 = 0xC0C0C0;

        private const int DefaultDashLength1 = 4;
        private const int DefaultDashLength2 = DefaultDashLength1;
        private const uint DefaultDashColor1 = 0x000000;
        private const uint DefaultDashColor2 = 0xFFFFFF;

        private static readonly Cursor EditCursor = Cursors.Cross;

        private const string FilterOptionsAll = "All valid Palette files";
        private const string FilterOptionsTPL = "Tile Layer Pro files";
        private const string FilterOptionsPAL = "PAL Palette files";
        private const string FilterOptionsMW3 = "Lunar Magic Palette files";
        private const string FilterOptionsS9X = "SNES9x v1.53/1.51 save state files";
        private const string FilterOptionsZST = "ZSNES v1.51 save state files";
        private const string FilterOptionsSMC = "Super NES ROM files";
        private const string FilterOptionsDMP = "SNES9x Debugger palette dump";
        private const string FilterOptionsBIN = "Binary files";

        private const string DefaultExt = PaletteForm.ExtensionTPL;
        private const string DefaultOpenTitle = "Open a palette file";
        private const string DefaultSaveTitle = "Save current palette to file.";

        private const string ErrorNumColors = "Number of colors must be greater than zero.";
        private const string ErrorPathDoesNotExist = "Path does not exist or could not be accessed.";
        private const string OptionCreatePalette = "Would you like to create a new Palette?";
        private const string OptionLocateFile = "Would you like to locate the file?";
        private const string OptionNewFileLocation = "Would you like to create a new save path?";
        private const string OptionPromptSave = "There are unsaved palettes. Would you like to save them?";

        private OpenFileDialog ofdPalette;
        private SaveFileDialog sfdPalette;
        private FindReplaceForm findForm;
        private PaletteEditorSettings editorSettings;
        private AboutForm aboutForm;

        private List<PaletteForm> editors;
        private PaletteForm currentEditor;
        private int lastEditorIndex;

        private PaletteForm.CopyData copyData;

        [Browsable(false)]
        public PaletteForm.CopyData CopyDataSelection
        {
            get { return this.copyData; }
        }

        [Browsable(false)]
        public List<PaletteForm> PaletteEditors
        {
            get { return this.editors; }
        }

        public int LastGotoAddress
        {
            get { return this.LastGotoAddress; }
            set { this.LastGotoAddress = value; }
        }

        [Browsable(false)]
        public string Status
        {
            set { this.tssMain.Text = value; }
        }

        public PaletteParent(string[] args)
        {
            InitializeComponent();

            this.editors = new List<PaletteForm>();

            if (PaletteSettings.Default.FirstTime)
            {
                PaletteSettings.Default.LastFiles = new StringCollection();
                ResetSettings();
            }

            this.editorSettings = new PaletteEditorSettings();
            this.editorSettings.DefaultColumns = PaletteSettings.Default.DefaultViewWidth;
            this.editorSettings.DefaultRows = PaletteSettings.Default.DefaultViewHeight;
            this.editorSettings.DefaultZoomSize = PaletteSettings.Default.DefaultZoom;
            this.editorSettings.DefaultBackColor1 = PaletteSettings.Default.DefaultBGColor1;
            this.editorSettings.DefaultBackColor2 = PaletteSettings.Default.DefaultBGColor2;
            this.editorSettings.DefaultBGSize = PaletteSettings.Default.DefaultBGSize;
            this.editorSettings.DashLength1 = PaletteSettings.Default.DefaultDashLength1;
            this.editorSettings.DashColor1 = PaletteSettings.Default.DefaultDashColor1;
            this.editorSettings.DashLength2 = PaletteSettings.Default.DefaultDashLength2;
            this.editorSettings.DashColor2 = PaletteSettings.Default.DefaultDashColor2;
            this.editorSettings.EditCursor = PaletteSettings.Default.DefaultCursor;

            string filter = PaletteParent.CreateFilter(PaletteDataFormats.None);
            this.ofdPalette = new OpenFileDialog();
            this.ofdPalette.DefaultExt = DefaultExt;
            this.ofdPalette.Filter = filter;
            this.ofdPalette.Multiselect = true;
            this.ofdPalette.Title = DefaultOpenTitle;
            this.ofdPalette.FileOk += new CancelEventHandler(ofdPalette_FileOk);

            this.sfdPalette = new SaveFileDialog();
            this.sfdPalette.DefaultExt = DefaultExt;
            this.sfdPalette.Title = DefaultSaveTitle;
            this.sfdPalette.FileOk += new CancelEventHandler(sfdPalette_FileOk);

            this.findForm = new FindReplaceForm();
            this.findForm.FindNext += new EventHandler(findForm_FindNext);
            this.AddOwnedForm(this.findForm);

            this.aboutForm = new AboutForm();
            this.AddOwnedForm(this.aboutForm);

            DisplayRecentFiles();
            ToggleEditorControls(false);

            for (int i = 0; i < args.Length; i++)
                if (File.Exists(args[i]))
                    OpenPalette(args[i]);
        }

        private void DisplayRecentFiles()
        {
            StringCollection files = PaletteSettings.Default.LastFiles;
            for (int i = files.Count; --i >= 0; )
                AddRecentFile(files[i]);
        }

        private void AddRecentFile(string path)
        {
            RemoveRecentFile(path);
            ToolStripMenuItem tsm = new ToolStripMenuItem();
            tsm.Text = path;
            tsm.Tag = path;
            tsm.Click += new EventHandler(tsmOpenRecentFile_Click);
            this.tsmRecentFiles.DropDownItems.Insert(0, tsm);
            this.tsmRecentFiles.Enabled = true;
        }

        private void RemoveRecentFile(string path)
        {
            ToolStripItemCollection items = this.tsmRecentFiles.DropDownItems;
            for (int i = items.Count; --i >= 0; )
                if ((string)items[i].Tag == path)
                    items[i].Dispose();

            int max = PaletteSettings.Default.MaxRecentFiles;
            while (items.Count > max)
                items[max].Dispose();
        }

        private void StoreRecentFiles()
        {
            ToolStripItemCollection items = this.tsmRecentFiles.DropDownItems;
            StringCollection files = PaletteSettings.Default.LastFiles;
            files.Clear();
            for (int i = items.Count; --i >= 0; )
                files.Insert(0, (string)items[i].Tag);
        }

        private void ResetSettings()
        {
            PaletteSettings.Default.DefaultViewWidth = DefaultColumns;
            PaletteSettings.Default.DefaultViewHeight = DefaultRows;
            PaletteSettings.Default.FallbackColor = PaletteForm.PCToSystemColor(FallbackColor);
            PaletteSettings.Default.DefaultZoom = DefaultZoom;
            PaletteSettings.Default.DefaultBGSize = DefaultBGSize;
            PaletteSettings.Default.DefaultBGColor1 = PaletteForm.PCToSystemColor(DefaultBGColor1);
            PaletteSettings.Default.DefaultBGColor2 = PaletteForm.PCToSystemColor(DefaultBGColor2);
            PaletteSettings.Default.DefaultDashLength1 = DefaultDashLength1;
            PaletteSettings.Default.DefaultDashLength2 = DefaultDashLength2;
            PaletteSettings.Default.DefaultDashColor1 = PaletteForm.PCToSystemColor(DefaultDashColor1);
            PaletteSettings.Default.DefaultDashColor2 = PaletteForm.PCToSystemColor(DefaultDashColor2);
            PaletteSettings.Default.DefaultCursor = PaletteParent.EditCursor;

            for (int i = this.editors.Count; --i >= 0; )
            {
                this.editors[i].SetEditorCursor(PaletteSettings.Default.DefaultCursor);
                this.editors[i].Redraw();
            }

            if (!PaletteSettings.Default.FirstTime)
                this.Status = "Default settings applied successfully.";
            PaletteSettings.Default.FirstTime = false;
        }

        private void ApplyCustomSettings()
        {
            PaletteSettings.Default.DefaultViewWidth = this.editorSettings.DefaultColumns;
            PaletteSettings.Default.DefaultViewHeight = this.editorSettings.DefaultRows;
            PaletteSettings.Default.DefaultZoom = this.editorSettings.DefaultZoomSize;
            PaletteSettings.Default.DefaultBGSize = this.editorSettings.DefaultBGSize;
            PaletteSettings.Default.DefaultBGColor1 = this.editorSettings.DefaultBackColor1;
            PaletteSettings.Default.DefaultBGColor2 = this.editorSettings.DefaultBackColor2;
            PaletteSettings.Default.DefaultDashLength1 = this.editorSettings.DashLength1;
            PaletteSettings.Default.DefaultDashLength2 = this.editorSettings.DashLength2;
            PaletteSettings.Default.DefaultDashColor1 = this.editorSettings.DashColor1;
            PaletteSettings.Default.DefaultDashColor2 = this.editorSettings.DashColor2;
            PaletteSettings.Default.DefaultCursor = this.editorSettings.EditCursor;

            for (int i = this.editors.Count; --i >= 0; )
            {
                this.editors[i].SetEditorCursor(PaletteSettings.Default.DefaultCursor);
                this.editors[i].Redraw();
            }

            this.Status = "Custom settings applied successfully.";
        }

        private void ToggleEditorControls(bool value)
        {
            this.tsmSave.Enabled =
            this.tsmSaveAs.Enabled =
            this.tsmSaveAll.Enabled =
            this.tsmCut.Enabled =
            this.tsmCopy.Enabled =
            this.tsmDelete.Enabled =
            this.tsmSelectAll.Enabled =
            this.tsmFindAndReplace.Enabled = 
            this.tsmInvert.Enabled =
            this.tsmVerticalGradient.Enabled =
            this.tsmHorizontalGradient.Enabled =
            this.tsmColorize.Enabled =
            this.tsmLumaGrayscale.Enabled =
            this.tsmWeightedGrayscale.Enabled =
            this.tsmEditBackColor.Enabled =
            this.tsbSave.Enabled =
            this.tsbCut.Enabled =
            this.tsbCopy.Enabled = value;
        }

        private void LoadPaletteEditor(PaletteForm paletteEditor)
        {
            paletteEditor.Cursor = PaletteSettings.Default.DefaultCursor;
            this.editors.Add(paletteEditor);
            ToggleEditorControls(true);

            paletteEditor.MdiParent = this;
            paletteEditor.FormClosing += new FormClosingEventHandler(PaletteEditor_FormClosing);
            paletteEditor.DataModified += new EventHandler(PaletteEditor_DataModified);
            paletteEditor.LastUndo += new EventHandler(paletteEditor_LastUndoAction);
            paletteEditor.LastRedo += new EventHandler(paletteEditor_LastRedoAction);
            paletteEditor.Show();
        }

        private int GetCurrentEditorIndex()
        {
            for (int i = this.editors.Count; --i >= 0; )
                if (this.ActiveMdiChild == this.editors[i])
                    return i;
            return -1;
        }

        private void NewPalette()
        {
            CreatePaletteForm dlg = new CreatePaletteForm();
            dlg.EnableCopyOption = this.copyData != null;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.CopyFrom)
                    NewPalette(this.copyData);
                else
                    NewPalette(dlg.NumColors);
            }
        }

        private void NewPalette(int numColors)
        {
            PaletteForm paletteEditor = new PaletteForm();
            paletteEditor.CreateNew(numColors);
            LoadPaletteEditor(paletteEditor);
            this.Status = "New palette successfully created.";
        }

        private void NewPalette(PaletteForm.CopyData data)
        {
            PaletteForm paletteEditor = new PaletteForm();
            paletteEditor.CreateCopy(data);
            LoadPaletteEditor(paletteEditor);
            this.Status = "Sucessfuly created new palette from copy data.";
        }

        private void OpenPalette()
        {
            this.ofdPalette.ShowDialog();
        }

        private void ofdPalette_FileOk(object sender, CancelEventArgs e)
        {
            for (int i = this.ofdPalette.FileNames.Length; --i >= 0; )
                OpenPalette(this.ofdPalette.FileNames[i]);
        }

        private void OpenPalette(string path)
        {
            if (!File.Exists(path))
            {
                if (MessageBox.Show(ErrorPathDoesNotExist + "\n" + path + "\n\n" + OptionLocateFile, DialogCaption,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    OpenPalette();
                this.Status = "Could not find palette file.";
                return;
            }

            string fullpath = Path.GetFullPath(path);
            for (int i = this.editors.Count; --i >= 0; )
            {
                string fn = this.editors[i].FilePath;
                if (fn != string.Empty && fullpath == Path.GetFullPath(fn))
                {
                    this.editors[i].Activate();
                    AddRecentFile(path);
                    this.Status = "File already open.";
                    return;
                }
            }

            PaletteForm paletteEditor = new PaletteForm();
            try
            {
                paletteEditor.Open(path);
            }
            catch (IOException ex)
            {
                if (MessageBox.Show(ex.Message, DialogCaption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    OpenPalette(path);
                this.Status = "Could not read palette data from file.";
                return;
            }
            LoadPaletteEditor(paletteEditor);
            AddRecentFile(path);
            this.Status = "Palette successfully loaded from file.";
        }

        private void SavePalette()
        {
            SavePalette(this.lastEditorIndex);
        }

        private void SavePalette(int editorIndex)
        {
            this.sfdPalette.Filter = CreateFilter(this.editors[editorIndex].PaletteDataFormat);
            this.sfdPalette.DefaultExt = PaletteForm.GetExtension(this.editors[editorIndex].PaletteDataFormat);
            this.sfdPalette.Tag = editorIndex;
            this.sfdPalette.ShowDialog();
        }

        private void sfdPalette_FileOk(object sender, CancelEventArgs e)
        {
            SavePalette(this.sfdPalette.FileName, (int)this.sfdPalette.Tag);
        }

        private void SavePalette(string path)
        {
            SavePalette(path, this.lastEditorIndex);
        }

        private void SavePalette(string path, int editorIndex)
        {
            if (path == string.Empty)
            {
                SavePalette(editorIndex);
                return;
            }

            try
            {
                this.editors[editorIndex].Save(path);
            }
            catch (IOException ex)
            {
                if (MessageBox.Show(ex.Message + '\n' + OptionNewFileLocation, DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    SavePalette();
                this.Status = "Could not write palette to specified path.";
                return;
            }

            for (int i = this.editors.Count; --i >= 0; )
                if (this.editors[i].FilePath == path && i != editorIndex)
                    this.editors[i].Close();

            AddRecentFile(path);
            this.Status = "Palette successfully saved to file.";
        }

        private void SaveAllPalettes()
        {
            for (int i = this.editors.Count; --i >= 0; )
            {
                if (this.editors[i].Unsaved)
                {
                    SavePalette(this.editors[i].FilePath, i);
                    i = this.editors.Count;
                }
            }
            this.Status = "All palettes saved.";
        }

        private void CutSelection()
        {
            this.copyData = this.currentEditor.Cut();
            GetSearchSelection();
            this.tsmPaste.Enabled =
            this.tsbPaste.Enabled = true;
            this.Status = "Palette selection cut.";
        }

        private void CopySelection()
        {
            this.copyData = this.currentEditor.Copy();
            GetSearchSelection();
            this.tsmPaste.Enabled =
            this.tsbPaste.Enabled = true;
            this.Status = "Palette selection copied.";
        }

        private void GetSearchSelection()
        {
            if (this.copyData.Width > 0x10)
                return;

                for (int i = this.copyData.Width; --i >= 0; )
                    this.findForm.Colors[i] = this.copyData.Colors[0, i];
                this.findForm.SearchSize = this.copyData.Width;
        }

        private void PasteSelection()
        {
            this.currentEditor.Paste(this.copyData);
            this.Status = "Copy data pasted to selection.";
        }

        private void DeleteSelection()
        {
            this.currentEditor.DeleteSelection();
            this.Status = "Palette selection deleted.";
        }

        private void SelectAll()
        {
            this.currentEditor.SelectAll();
            this.Status = "All colors selected.";
        }

        private void FindNext()
        {
            //this.currentEditor.FindNext(this.findForm.Colors, this.findForm.SearchSize, this.findForm.FindDirection);
        }

        private void tsmNew_Click(object sender, EventArgs e)
        {
            NewPalette();
        }

        private void tsmOpen_Click(object sender, EventArgs e)
        {
            OpenPalette();
        }

        private void tsmOpenRecentFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            OpenPalette((string)tsm.Tag);
        }

        private void tsmSave_Click(object sender, EventArgs e)
        {
            SavePalette(this.currentEditor.FilePath);
        }

        private void tsmSaveAs_Click(object sender, EventArgs e)
        {
            SavePalette();
        }

        private void tsmSaveAll_Click(object sender, EventArgs e)
        {
            SaveAllPalettes();
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmUndo_Click(object sender, EventArgs e)
        {
            this.currentEditor.Undo();
            this.tsmRedo.Enabled = true;
            this.Status = "Undo action completed.";
        }

        private void tsmRedo_Click(object sender, EventArgs e)
        {
            this.currentEditor.Redo();
            this.tsmUndo.Enabled = true;
            this.Status = "Redo action completed.";
        }

        private void tsmCut_Click(object sender, EventArgs e)
        {
            CutSelection();
        }

        private void tsmCopy_Click(object sender, EventArgs e)
        {
            CopySelection();
        }

        private void tsmPaste_Click(object sender, EventArgs e)
        {
            PasteSelection();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            DeleteSelection();
        }

        private void tsmSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void tsmGoto_Click(object sender, EventArgs e)
        {
            this.currentEditor.GoTo();
        }

        private void tsmInvert_Click(object sender, EventArgs e)
        {
            this.currentEditor.Invert();
        }

        private void tsmHorizontalGradient_Click(object sender, EventArgs e)
        {
            this.currentEditor.Gradient(false);
        }

        private void tsmVerticalGradient_Click(object sender, EventArgs e)
        {
            this.currentEditor.Gradient(true);
        }

        private void tsmColorize_Click(object sender, EventArgs e)
        {
            this.currentEditor.Colorize();
        }

        private void tsmLumaGrayscale_Click(object sender, EventArgs e)
        {
            this.currentEditor.LumaGrayscale();
        }

        private void tsmWeightedGrayscale_Click(object sender, EventArgs e)
        {
            this.currentEditor.WeightedGrayScale();
        }

        private void tsmEditBackColor_Click(object sender, EventArgs e)
        {
            //Needs reimplimentation
        }

        private void findForm_FindNext(object sender, EventArgs e)
        {
            FindNext();
        }

        private void PaletteParent_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
                this.Status = "Drop to load palette.";
            }
            else
            {
                e.Effect = DragDropEffects.None;
                this.Status = "Invalid item.";
            }
        }

        private void PaletteParent_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.All)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                for (int i = 0; i < files.Length; i++)
                    OpenPalette(files[i]);
            }
        }

        private void PaletteEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && this.currentEditor.Unsaved)
            {
                DialogResult result = MessageBox.Show("Save the current Palette?", "Palette Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    SavePalette(this.lastEditorIndex);
                if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
            if (e.Cancel || e.CloseReason != CloseReason.UserClosing)
                return;

            this.editors.Remove((PaletteForm)sender);

            this.lastEditorIndex = GetCurrentEditorIndex();
            if (this.lastEditorIndex == -1)
                this.lastEditorIndex = this.editors.Count - 1;

            if (this.lastEditorIndex == -1)
                ToggleEditorControls(false);
            else
                this.currentEditor = this.editors[this.lastEditorIndex];
        }

        private void PaletteEditor_DataModified(object sender, EventArgs e)
        {
            this.tsmUndo.Enabled = true;
            this.tsmRedo.Enabled = false;
        }

        private void paletteEditor_LastUndoAction(object sender, EventArgs e)
        {
            this.tsmUndo.Enabled = false;
            this.Status = "Last undo applied.";
        }

        private void paletteEditor_LastRedoAction(object sender, EventArgs e)
        {
            this.tsmRedo.Enabled = false;
            this.Status = "Last redo applied.";
        }

        private void PaletteParent_MdiChildActivate(object sender, EventArgs e)
        {
            int index = GetCurrentEditorIndex();
            if (index == -1)
                return;

            this.currentEditor = this.editors[this.lastEditorIndex = index];
            this.tsmGoto.Enabled = this.currentEditor.PaletteDataFormat == PaletteDataFormats.SNES;
            this.Status = "Viewing " + this.currentEditor.FileName;
        }

        private void PaletteParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> names = new List<string>();
            for (int i = this.editors.Count; --i >= 0; )
                if (this.editors[i].Unsaved)
                    names.Add(this.editors[i].FileName);

            if (names.Count > 0)
            {
                UnsavedDialog dlg = new UnsavedDialog();
                dlg.Title = DialogCaption;
                dlg.Files = names;
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.Yes)
                    SaveAllPalettes();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }

            StoreRecentFiles();
            PaletteSettings.Default.Save();
        }

        private void tsmCustomize_Click(object sender, EventArgs e)
        {
            if (editorSettings.ShowDialog() == DialogResult.OK)
                ApplyCustomSettings();
        }

        private void ResetSettings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will reset all custom settings. Do you want to continue?", DialogCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                ResetSettings();
        }

        public static string CreateFilter(PaletteDataFormats format)
        {
            StringBuilder sb = new StringBuilder(FilterOptionsAll);

            sb.Append(Program.FilterSeperator);

            sb.Append(Program.FilterPredicate);
            sb.Append(PaletteForm.ExtensionTPL);

            sb.Append(Program.FilterExtSeperator);
            sb.Append(Program.FilterPredicate);
            sb.Append(PaletteForm.ExtensionPAL);

            sb.Append(Program.FilterExtSeperator);
            sb.Append(Program.FilterPredicate);
            sb.Append(PaletteForm.ExtensionMW3);

            sb.Append(Program.FilterExtSeperator);
            sb.Append(Program.FilterPredicate);
            sb.Append(PaletteForm.ExtensionBIN);

            if (format == PaletteDataFormats.SNES || format == PaletteDataFormats.None)
            {
                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionSMC);

                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionSFC);

                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionSWC);

                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionFIG);
            }

            if (format == PaletteDataFormats.S9X || format == PaletteDataFormats.None)
            {
                for (int i = 0; i <= 9; i++)
                {
                    sb.Append(Program.FilterExtSeperator);
                    sb.Append(Program.FilterPredicate);
                    sb.Append(PaletteForm.ExtensionS9X_0to9);
                    sb.Append(i);
                }
            }

            if (format == PaletteDataFormats.ZST || format == PaletteDataFormats.None)
            {
                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionZST);

                for (int i = 10; i <= 99; i++)
                {
                    sb.Append(Program.FilterExtSeperator);
                    sb.Append(Program.FilterPredicate);
                    sb.Append(PaletteForm.ExtensionZST_10to99);
                    sb.Append(i);
                }

                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.AltExtensionZST);
            }

            if (format == PaletteDataFormats.SNES || format == PaletteDataFormats.None)
            {
                sb.Append(Program.FilterSeperator);
                sb.Append(FilterOptionsSMC);
                sb.Append(Program.FilterSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionSMC);

                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionSFC);

                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionSWC);

                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionFIG);
            }

            sb.Append(Program.FilterSeperator);
            sb.Append(FilterOptionsTPL);
            sb.Append(Program.FilterSeperator);
            sb.Append(Program.FilterPredicate);
            sb.Append(PaletteForm.ExtensionTPL);

            sb.Append(Program.FilterSeperator);
            sb.Append(FilterOptionsPAL);
            sb.Append(Program.FilterSeperator);
            sb.Append(Program.FilterPredicate);
            sb.Append(PaletteForm.ExtensionPAL);

            sb.Append(Program.FilterSeperator);
            sb.Append(FilterOptionsMW3);
            sb.Append(Program.FilterSeperator);
            sb.Append(Program.FilterPredicate);
            sb.Append(PaletteForm.ExtensionMW3);

            if (format == PaletteDataFormats.S9X || format == PaletteDataFormats.None)
            {
                sb.Append(Program.FilterSeperator);
                sb.Append(FilterOptionsS9X);
                sb.Append(Program.FilterSeperator);
                for (int i = 0; i <= 9; i++)
                {
                    sb.Append(Program.FilterPredicate);
                    sb.Append(PaletteForm.ExtensionZST_0to9);
                    sb.Append(i);
                    sb.Append(Program.FilterExtSeperator);
                }
            }

            if (format == PaletteDataFormats.ZST || format == PaletteDataFormats.None)
            {
                sb.Append(Program.FilterSeperator);
                sb.Append(FilterOptionsZST);
                sb.Append(Program.FilterSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.ExtensionZST);
                for (int i = 10; i <= 99; i++)
                {
                    sb.Append(Program.FilterExtSeperator);
                    sb.Append(Program.FilterPredicate);
                    sb.Append(PaletteForm.ExtensionZST_10to99);
                    sb.Append(i);
                }
                sb.Append(Program.FilterExtSeperator);
                sb.Append(Program.FilterPredicate);
                sb.Append(PaletteForm.AltExtensionZST);
            }

            sb.Append(Program.FilterSeperator);
            sb.Append(FilterOptionsBIN);
            sb.Append(Program.FilterSeperator);
            sb.Append(Program.FilterPredicate);
            sb.Append(PaletteForm.ExtensionBIN);

            return sb.ToString();
        }

        private void tsmFindAndReplace_Click(object sender, EventArgs e)
        {
            this.findForm.Show();
        }

        private void tsmAbout_Click(object sender, EventArgs e)
        {
            this.aboutForm.Show();
        }
    }
}