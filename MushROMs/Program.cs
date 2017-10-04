using System;
using System.IO;
using System.Windows.Forms;
using System.Security.AccessControl;
using MushROMs.LunarCompress;
using MushROMs.Properties;

namespace MushROMs
{
    static class Program
    {
        #region Constant Strings
        private const string WindowTitle = "MushROMs";
        private const string NoROM = "Note that MushROMs cannot run without a base ROM to work with.";
        private const string NoLunarCompress = "Could not load editor. Lunar Compress.dll is needed for program operations.";
        #endregion

        static SMASEditor Editor;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!LC.CheckDLLExists())
            {
                MessageBox.Show(NoLunarCompress, WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(Settings.Default.BaseROMPath))
            {
                WelcomeDialog dlg = new WelcomeDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.BaseROMPath = dlg.ROMPath;
                    Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show(NoROM, WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Editor = new SMASEditor();
        }
    }
}