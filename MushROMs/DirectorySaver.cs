using System.Collections.Generic;
using System.IO;

namespace MushROMs
{
    public unsafe static class DirectorySaver
    {
        private static List<byte> AllData;

        public static byte[] SaveDirectory(string path)
        {
            AllData = new List<byte>();
            LoadFiles(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            return AllData.ToArray();
        }

        private static void LoadFiles(string path)
        {
            string[] text = Directory.GetFiles(path);
            AddLength(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                AddString(text[i].Substring(path.Length + 1));
                byte[] data = File.ReadAllBytes(text[i]);
                AddLength(data.Length);
                AllData.AddRange(data);
            }

            text = Directory.GetDirectories(path);
            AddLength(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                string dir = text[i].Substring(path.Length + 1);
                AddString(dir);
                LoadFiles(text[i]);
            }
        }

        private static void AddLength(int length)
        {
            AllData.Add((byte)length);
            AllData.Add((byte)(length >> 8));
            AllData.Add((byte)(length >> 0x10));
            AllData.Add((byte)(length >> 0x18));
        }

        private static void AddString(string text)
        {
            byte[] data = new byte[(text.Length + 1) * 2];
            fixed (char* c = text)
            fixed (byte* ptr = data)
            {
                char* dest = (char*)ptr;
                for (int i = text.Length + 1; --i >= 0; )
                    dest[i] = c[i];
            }
            AllData.AddRange(data);
        }

        public static void LoadDirectory(string path, byte[] data)
        {
            fixed (byte* src = data)
                Write(path, src);
        }

        private static int Write(string path, byte* data)
        {
            Directory.CreateDirectory(path);

            byte* src = data;
            int num = *((int*)src);
            src += 4;
            for (int i = 0; i < num; i++)
            {
                string name = new string((char*)src);
                src += (name.Length + 1) * 2;
                int size = *((int*)src);
                src += 4;
                byte[] file = new byte[size];
                fixed (byte* dest = file)
                    for (int j = size; --j >= 0; )
                        dest[j] = src[j];
                File.WriteAllBytes(Path.Combine(path, name), file);
                src += size;
            }

            num = *((int*)src);
            src += 4;
            for (int i = 0; i < num; i++)
            {
                string dir = new string((char*)src);
                src += (dir.Length + 1) * 2;
                src += Write(Path.Combine(path, dir), src);
            }
            return (int)(src - data);
        }
    }
}