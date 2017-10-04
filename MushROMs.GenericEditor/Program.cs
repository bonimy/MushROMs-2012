using System;
using System.IO;
using System.IO.Compression;
using System.Timers;
using System.Windows.Forms;
using MushROMs.Controls;
using MushROMs.SNESLibrary;
using MushROMs.GenericEditor.PaletteEditor;
using MushROMs.GenericEditor.GFXEditor;

namespace MushROMs.GenericEditor
{
    public static class Program
    {
        public const char FilterPredicate = '*';
        public const char FilterSeperator = '|';
        public const char FilterExtSeperator = ';';
        public const string NoExtensionFilter = "*.*";

        public const int GZipMagicNumber = 0x1F8B08;

        private const bool DefaultAnimate = true;
        private const FPSModes DefaultFPSMode = FPSModes.NTSC;
        private const FrameReductions DefaultFrameReduction = FrameReductions.None;
        public const double DefaultDashWait = 200;

        private static FPSModes fpsMode;
        private static FrameReductions frameReduction;

        private static double fps;
        private static double interval;

        private static EventTimer animator;

        public static EventTimer Animator
        {
            get { return Program.animator; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ResetStandardSettings();

            Program.animator = new EventTimer(Program.interval);
            Program.animator.Start();

            Application.Run(new GFXParent(args));
        }

        public static void ResetStandardSettings()
        {
            Program.fpsMode = DefaultFPSMode;
            Program.frameReduction = DefaultFrameReduction;
            Program.fps = (double)Program.fpsMode / (double)Program.frameReduction;
            Program.interval = 1000.0 / Program.fps;
        }

        public static bool IsGZipCompressed(ref byte[] data)
        {
            if ((data[0] << 0x10) != (GZipMagicNumber & 0xFF0000))
                return false;
            if ((data[1] << 8) != (GZipMagicNumber & 0xFF00))
                return false;
            return data[2] == (GZipMagicNumber & 0xFF);
        }

        public static byte[] GZipDecompress(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            GZipDecompress(ref data);
            return data;
        }

        public static void GZipDecompress(ref byte[] data)
        {
            if (!IsGZipCompressed(ref data))
                return;

            const int size = 0x10000;
            byte[] buffer = new byte[size];
            int count = 0;

            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream stream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                    while ((count = stream.Read(buffer, 0, size)) > 0)
                        memory.Write(buffer, 0, count);
                data = memory.ToArray();
            }
        }

        public static void GZipCompress(ref byte[] data)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream stream = new GZipStream(memory, CompressionMode.Compress, true))
                    stream.Write(data, 0, data.Length);
                data = memory.ToArray();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Taken from http://tech.pro/tutorial/668/csharp-tutorial-dealing-with-unhandled-exceptions
            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                ErrorForm dlg = new ErrorForm();
                dlg.Title = "Fatal Error";
                dlg.Message = ex.Message + ex.StackTrace;
                dlg.ShowDialog();
                Application.Exit();
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}