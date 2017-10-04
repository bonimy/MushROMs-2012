using System;
using System.IO;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNESLibrary;
using MushROMs.SMB1.Level;
using MushROMs.Properties;

namespace MushROMs.SMB1
{
    public unsafe partial class SMB1Editor : EditorForm
    {
        #region Constant Variables
        public const int MaxLevels = 0x100;
        public const int PaletteBackColor = 0x78B0C8;
        #endregion

        #region Project Variables
        public bool Open
        {
            get
            {
                return this.animator.Enabled;
            }
            set
            {
                this.animator.Enabled =
                this.tsmSaveAs.Enabled =
                this.tsmEdit.Enabled =
                this.tsmEditors.Enabled =
                this.tsbCut.Enabled =
                this.tsbCopy.Enabled =
                this.tsbPaste.Enabled =
                this.hsbLevel.Visible = value;

                if (value)
                {
                    this.EditorDirectory = this.parent.ProjectDirectory + @"\SMB1";

                    this.GFXEditor.GetDefaultIndexes();
                    this.GFXEditor.GetAllIndexes();
                    this.Map16Editor.LoadMap16();
                    this.Level = 0x25;
                }
            }
        }
        #endregion

        #region Editors
        private SMASEditor parent;
        private PaletteEditor PaletteEditor;
        private GFXEditor GFXEditor;
        private Map16Editor Map16Editor;
        private ObjectSelector ObjectSelector;

        public new SMASEditor Parent
        {
            get { return this.parent; }
        }

        public bool ShowPaletteEditor
        {
            get
            {
                return this.PaletteEditor.Visible;
            }
            set
            {
                this.PaletteEditor.Visible =
                this.tsmPaletteEditor.Checked = value;
            }
        }

        public bool ShowGFXEditor
        {
            get
            {
                return this.GFXEditor.Visible;
            }
            set
            {
                this.GFXEditor.Visible =
                this.tsmGFXEditor.Checked = value;
            }
        }

        public bool ShowMap16Editor
        {
            get
            {
                return this.Map16Editor.Visible;
            }
            set
            {
                this.Map16Editor.Visible =
                this.tsmMap16Editor.Checked = value;
            }
        }

        public bool ShowObjectSelector
        {
            get
            {
                return this.ObjectSelector.Visible;
            }
            set
            {
                this.ObjectSelector.Visible =
                this.tsmAddObjects.Checked = value;
            }
        }
        #endregion

        #region Level Variables
        private int level;
        private LevelObjectData levelData;

        public int Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;

                this.levelData = new LevelObjectData();

                this.drwLevel.Invalidate();
                this.PaletteEditor.LoadPalette();
                this.PaletteEditor.Redraw();
                this.GFXEditor.LoadGFX();
                this.GFXEditor.Redraw();
                this.Map16Editor.Redraw();
                this.ObjectSelector.Redraw();
            }
        }

        public Palette Palette
        {
            get { return this.PaletteEditor.Palette; }
        }

        public GFX GFX
        {
            get { return this.GFXEditor.GFX; }
        }

        public Map16 Map16
        {
            get { return this.Map16Editor.Map16; }
        }

        public HeaderInfo Header
        {
            get { return this.levelData.HeadInfo; }
        }

        public LevelType LevelType
        {
            get { return this.Header.LevelType; }
        }
        #endregion

        #region Animation Variables
        private System.Timers.Timer animator;
        private int fps;
        private int frameskip;

        public int FPS
        {
            get
            {
                return this.fps;
            }
            set
            {
                this.fps = value;
                this.animator.Interval = 1000 / value;
                this.frameskip = 60 / value;
            }
        }
        #endregion

        public SMB1Editor(SMASEditor parent)
        {
            InitializeComponent();

            this.parent = parent;
            this.PaletteEditor = new PaletteEditor(this);
            this.GFXEditor = new GFXEditor(this);
            this.Map16Editor = new Map16Editor(this);
            this.ObjectSelector = new ObjectSelector(this);

            this.animator = new System.Timers.Timer();
            this.animator.Enabled = false;
            this.animator.Elapsed += new System.Timers.ElapsedEventHandler(animator_Elapsed);
            this.FPS = 60;
        }

        private void SetScrollBarSize()
        {
            if (this.Open)
            {
                int viewable = this.drwLevel.ClientWidth / Map16.TileWidth - 0x0E;     //I have no clue what the 0x0E signifies, but it fixes sizing errors.
                this.hsbLevel.Maximum = this.levelData.Map.Width - viewable;
            }
        }

        #region Events
        private void NewProject_Click(object sender, EventArgs e)
        {
            NewProjectDialog dlg = new NewProjectDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                this.parent.CreateNewProject(dlg.ProjectDirectory, dlg.ProjectName);
        }

        private void OpenProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "mush";
            dlg.Filter = "MushROMs project files|*.mush|All files|*.*";
            dlg.Title = "Select MushROMs project path.";
            if (dlg.ShowDialog() == DialogResult.OK)
                this.parent.OpenProject(dlg.FileName);
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void drwLevel_Paint(object sender, PaintEventArgs e)
        {
            if (!this.Open)
                return;
        }

        private void SMB1Editor_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    ++this.Level;
                    break;
                case Keys.Down:
                    --this.Level;
                    break;
            }
        }

        private void tsmPaletteEditor_Click(object sender, EventArgs e)
        {
            this.ShowPaletteEditor = this.tsmPaletteEditor.Checked;
        }

        private void tsmGFXEditor_Click(object sender, EventArgs e)
        {
            this.ShowGFXEditor = this.tsmGFXEditor.Checked;
        }

        private void tsmMap16Editor_Click(object sender, EventArgs e)
        {
            this.ShowMap16Editor = this.tsmMap16Editor.Checked;
        }

        private void tsmAddObjects_Click(object sender, EventArgs e)
        {
            this.ShowObjectSelector = this.tsmAddObjects.Checked;
        }

        private void SMB1Editor_SizeChanged(object sender, EventArgs e)
        {
            SetScrollBarSize();
        }

        private void hsbLevel_ValueChanged(object sender, EventArgs e)
        {
            this.drwLevel.Invalidate();
        }

        private void animator_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
        }

        private void SMB1Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.parent.EditorClosing();
        }
        #endregion
    }
}