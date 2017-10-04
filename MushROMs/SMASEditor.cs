using System;
using System.IO;
using System.Windows.Forms;
using MushROMs.SMB1;
using MushROMs.Properties;

namespace MushROMs
{
    public class SMASEditor
    {
        private string projectPath;
        private string projectDirectory;
        private string projectName;
        private string romName;
        private string asmName;

        public string ProjectDirectory
        {
            get { return this.projectDirectory; }
        }

        public bool Open
        {
            get { return this.smb1Editor.Open; }
            set { this.smb1Editor.Open = value; }
        }

        private SMB1Editor smb1Editor;

        public SMASEditor()
        {
            this.smb1Editor = new SMB1Editor(this);

            if (File.Exists(Settings.Default.LastProjectPath))
                OpenProject(Settings.Default.LastProjectPath);

            switch (Settings.Default.StartGame)
            {
                case Games.SMB1:
                    Application.Run(this.smb1Editor);
                    break;
            }
        }

        public void CreateNewProject(string directory, string name)
        {
            this.projectDirectory = directory;
            this.projectName = Path.GetFileNameWithoutExtension(name);
            string[] lines = new string[2];
            lines[0] = this.romName = Path.ChangeExtension(this.projectName, Path.GetExtension(Settings.Default.BaseROMPath));
            lines[1] = this.asmName = "main.asm";
            this.projectPath = Path.Combine(directory, this.projectName);
            this.projectPath = Path.ChangeExtension(this.projectPath, "mush");

            try
            {
                File.WriteAllLines(this.projectPath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not write file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Open = true;
        }

        public void OpenProject(string projectPath)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(projectPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not read file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lines.Length != 2)
            {
                MessageBox.Show("Could not load project.\n" + projectPath, "Invalid project file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.projectPath = projectPath;
            this.projectDirectory = Path.GetDirectoryName(projectPath);
            this.projectName = Path.GetFileNameWithoutExtension(projectPath);
            this.romName = lines[0];
            this.asmName = lines[1];
            this.Open = true;
        }

        public void EditorClosing()
        {
            if (this.Open && File.Exists(this.projectPath))
            {
                Settings.Default.LastProjectPath = this.projectPath;
                Settings.Default.Save();
            }
        }
    }

    public enum Games
    {
        SMAS = 0,
        SMB1 = 1,
        SMB2J = 2,
        SMB2U = 3,
        SMB3 = 4,
        SMW = 5
    }

    public enum FrameAdvance
    {
        Advance1 = 0,
        Advance2 = 1,
        Advance4 = 2,
        Advance8 = 3
    }
}