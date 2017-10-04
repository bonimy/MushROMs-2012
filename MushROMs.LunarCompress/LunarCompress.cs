using System;
using System.IO;
using System.Windows.Forms;

namespace MushROMs.LunarCompress
{
    /// <summary>
    /// Provides a strong class-library (originally built in C) for SNES-related functionally in programming.
    /// </summary>
    public static unsafe partial class LC
    {
        private const uint NOSEEK = 0;
        private const uint SEEK = 1;
        private const uint CREATEARRAY = 0x10;
        private const uint SAVEONCLOSE = 0x20;
        private const uint QUIET = 0x40000000;
        private const uint LOG = 0x80000000;
        private const uint COMPRESSED = 0x800;
        private const ushort SNESRGBbit = 0x8000;
        private const uint PCRGBbyte = 0xFF000000;

        /// <summary>
        /// The width, in pixels, of a single GFX tile.
        /// </summary>
        public const int TileWidth = 8;
        /// <summary>
        /// The height, in pixels, of a single GFX tile.
        /// </summary>
        public const int TileHeight = TileWidth;
        /// <summary>
        /// The total size, in pixels, of a single GFX tile.
        /// </summary>
        public const int TileSize = TileWidth * TileHeight;

        private static bool open = false;
        private static string path = string.Empty;
        private static IntPtr data = IntPtr.Zero;

        /// <summary>
        /// Gets a pointer to the byte array that Lunar Compress references for its functions.
        /// It will return <see cref="IntPtr.Zero"/> when no array is open.
        /// </summary>
        public static IntPtr Data
        {
            get { return LC.data; }
        }

        /// <summary>
        /// Gets the size of the file in bytes that is currently open in the DLL.
        /// It will return 0 on failure.
        /// </summary>
        public static int DataSize
        {
            get { return (int)LunarGetFileSize(); }
        }

        /// <summary>
        /// The current version of the DLL as an integer.
        /// For example, version 1.30 of the DLL would return "130" (decimal).
        /// </summary>
        public static int Version
        {
            get { return (int)LunarVersion(); }
        }

        /// <summary>
        /// Determines if the unmanaged DLL file exists.
        /// </summary>
        /// <returns>
        /// True if the file exists or is found, otherwise false.
        /// </returns>
        public static bool CheckDLLExists()
        {
            return CheckDLLExists(true);
        }

        /// <summary>
        /// Determines if the unmanaged DLL file exists.
        /// </summary>
        /// <param name="prompt">
        /// If true, the function will ask the user to locate the file if the DLL is not found.
        /// </param>
        /// <returns>
        /// True if the file exists or is found, otherwise false.
        /// </returns>
        public static bool CheckDLLExists(bool prompt)
        {
            if (File.Exists(DLLPath))
                return true;

            if (prompt && MessageBox.Show("Could not load " + DLLPath + ". Would you like to locate the file?", "Lunar Compress", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "dll";
                dlg.Filter = "Lunar Compress DLL File|*.dll|All Files|*.*";
                dlg.Title = "Find Lunar Compress.dll file.";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(DLLPath));
                    File.Copy(dlg.FileName, DLLPath);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Open file for access by the DLL.  Files of any size can be opened, since the 
        /// DLL does not load the entire file into RAM for manipulations.
        /// If another file is already open, <see cref="CloseFile()"/> will be used
        /// to close it first.
        /// </summary>
        /// <param name="path">
        /// Name of File.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// The DLL does not prevent other applications from reading/writing to the 
        /// file at the same time, though of course that isn't recommended.
        /// </remarks>
        public static bool OpenFile(string path)
        {
            return OpenFile(path, FileModes.ReadOnly);
        }

        /// <summary>
        /// Open file for access by the DLL.  Files of any size can be opened, since the 
        /// DLL does not load the entire file into RAM for manipulations.
        /// If another file is already open, <see cref="CloseFile()"/> will be used
        /// to close it first.
        /// </summary>
        /// <param name="path">
        /// Name of File.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// The DLL does not prevent other applications from reading/writing to the 
        /// file at the same time, though of course that isn't recommended.
        /// </remarks>
        public static bool OpenFile(string path, FileModes fileMode)
        {
            CloseFile();
            if (LC.open = LunarOpenFile(path, (uint)fileMode) != 0)
                LC.path = path;
            return LC.open;
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// The file will not be automatically saved when <see cref="CloseFile"/> is called.
        /// </remarks>
        public static IntPtr OpenRAMFile(string path)
        {
            return OpenRAMFile(path, FileModes.ReadOnly, LockArraySizeOptions.None, false, 0);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// The file will not be automatically saved when <see cref="CloseFile"/> is called.
        /// </remarks>
        public static IntPtr OpenRAMFile(string path, FileModes fileMode)
        {
            return OpenRAMFile(path, fileMode, LockArraySizeOptions.None, false, 0);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        /// <remarks>
        /// The file will not be automatically saved when <see cref="CloseFile"/> is called.
        /// </remarks>
        public static IntPtr OpenRAMFile(string path, FileModes fileMode, LockArraySizeOptions flags)
        {
            return OpenRAMFile(path, fileMode, flags, false, 0);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <param name="saveOnClose">
        /// If true, will automatically save the contents of the byte array back to the file
        /// when <see cref="CloseFile()"/> is called.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(string path, FileModes fileMode, LockArraySizeOptions flags, bool saveOnClose)
        {
            return OpenRAMFile(path, fileMode, flags, saveOnClose, 0);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="path">
        /// The name of the file to open.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <param name="saveOnClose">
        /// If true, will automatically save the contents of the byte array back to the file
        /// when <see cref="CloseFile()"/> is called.
        /// </param>
        /// <param name="size">
        /// The minimum size of the array to be allocated.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(string path, FileModes fileMode, LockArraySizeOptions flags, bool saveOnClose, int size)
        {
            CloseFile();
            fixed (char* ptr = path)
                return LC.data = (IntPtr)LunarOpenRAMFile(ptr, (uint)fileMode | (uint)flags | CREATEARRAY |(uint)(saveOnClose ? SAVEONCLOSE : 0), (uint)size);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Array of bytes to use as file.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(byte[] data)
        {
            fixed (byte* ptr = data)
                return OpenRAMFile(ptr, data.Length, FileModes.ReadOnly, LockArraySizeOptions.None);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Array of bytes to use as file.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(byte[] data, FileModes fileMode)
        {
            fixed (byte* ptr = data)
                return OpenRAMFile(ptr, data.Length, fileMode, LockArraySizeOptions.None);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Array of bytes to use as file.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(byte[] data, FileModes fileMode, LockArraySizeOptions flags)
        {
            fixed (byte* ptr = data)
                return OpenRAMFile(ptr, data.Length, fileMode, flags);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Pointer to array of bytes to use as file.
        /// </param>
        /// <param name="size">
        /// Size of the user-supplied array.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(void* data, int size)
        {
            return OpenRAMFile(data, size, FileModes.ReadOnly, LockArraySizeOptions.None);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Pointer to array of bytes to use as file.
        /// </param>
        /// <param name="size">
        /// Size of the user-supplied array.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(void* data, int size, FileModes fileMode)
        {
            return OpenRAMFile(data, size, fileMode, LockArraySizeOptions.None);
        }

        /// <summary>
        /// This function causes all file related functions of the DLL to operate on
        /// an array of data in RAM instead of an actual file. This may be useful for 
        /// decompressing structures from memory, or if your program loads an entire 
        /// ROM into memory you can have the DLL operate on it instead of the file.
        /// Working with a file loaded into RAM will speed up file operations, however
        /// there's still the overhead of loading/saving the entire file.
        /// </summary>
        /// <param name="data">
        /// Pointer to array of bytes to use as file.
        /// </param>
        /// <param name="size">
        /// Size of the user-supplied array.
        /// </param>
        /// <param name="fileMode">
        /// The mode to open the file in.
        /// </param>
        /// <param name="flags">
        /// Options for preventing the DLL from accessing beyond the array's maximum size.
        /// </param>
        /// <returns>
        /// Pointer to array on success, <see cref="IntPtr.Zero"/> on fail.
        /// </returns>
        public static IntPtr OpenRAMFile(void* data, int size, FileModes fileMode, LockArraySizeOptions flags)
        {
            CloseFile();
            return LC.data = (IntPtr)LunarOpenRAMFile(data, (uint)fileMode | (uint)flags, (uint)size);
        }

        /// <summary>
        /// Saves the currently open byte array in RAM to a file.
        /// </summary>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool SaveRAMFile()
        {
            return LunarSaveRAMFile(LC.path) != 0;
        }

        /// <summary>
        /// Saves the currently open byte array in RAM to a file.
        /// You can specify null to save back to the same file if
        /// <see cref="OpenRAMFile(string)"/> was used.
        /// </summary>
        /// <param name="path">
        /// The file to write to.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool SaveRAMFile(string path)
        {
            if (LunarSaveRAMFile(path) != 0)
            {
                LC.path = path;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Closes the file or RAM byte array currently open in the DLL.
        /// </summary>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool CloseFile()
        {
            bool open = LunarCloseFile();
            if (open)
            {
                LC.open = false;
                LC.path = string.Empty;
                LC.data = IntPtr.Zero;
            }
            return open;
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <returns>
        /// A byte array of the data from the currently open file on success, otherwise null.
        /// </returns>
        public static byte[] ReadFile()
        {
            return ReadFile((int)LunarGetFileSize(), 0, false);
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <returns>
        /// A byte array of the data from the currently open file on success, otherwise null.
        /// </returns>
        public static byte[] ReadFile(int size, int address)
        {
            return ReadFile(size, address, false);
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="seek">
        /// Not seeking to the address can speed up file I/O if you're reading
        /// consecutive chunks of data.
        /// </param>
        /// <returns>
        /// A byte array of the data from the currently open file on success, otherwise null.
        /// </returns>
        public static byte[] ReadFile(int size, int address, bool seek)
        {
            byte[] data = new byte[size];
            fixed (byte* ptr = data)
                return ReadFile(ptr, size, address, seek) ? data : null;
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="dest">
        /// Pointer to the destination byte array.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool ReadFile(void* dest)
        {
            return LunarReadFile(dest, LunarGetFileSize(), 0, 0) != 0;
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="dest">
        /// Pointer to the destination byte array.
        /// </param>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool ReadFile(void* dest, int size, int address)
        {
            return LunarReadFile(dest, (uint)size, (uint)address, 0) != 0;
        }

        /// <summary>
        /// Reads data from the currently open file into a byte array.
        /// </summary>
        /// <param name="dest">
        /// Pointer to the destination byte array.
        /// </param>
        /// <param name="size">
        /// Number of bytes to read.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="seek">
        /// Not seeking to the address can speed up file I/O if you're reading
        /// consecutive chunks of data.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool ReadFile(void* dest, int size, int address, bool seek)
        {
            return LunarReadFile(dest, (uint)size, (uint)address, (uint)(seek ? SEEK : NOSEEK)) != 0;
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Source byte array.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(byte[] data)
        {
            fixed (byte* ptr = data)
                return WriteFile(ptr, data.Length, 0, false);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Source byte array.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(byte[] data, int size, int address)
        {
            fixed (byte* ptr = data)
                return WriteFile(ptr, size, address, false);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Source byte array.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="seek">
        /// Not seeking to the address can speed up file I/O if you're reading
        /// consecutive chunks of data.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(byte[] data, int size, int address, bool seek)
        {
            fixed (byte* ptr = data)
                return WriteFile(ptr, size, address, seek);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Pointer to source byte array.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(void* data, int size)
        {
            return WriteFile(data, size, 0, false);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Pointer to source byte array.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(void* data, int size, int address)
        {
            return WriteFile(data, size, address, false);
        }

        /// <summary>
        /// Writes data from a byte array to the currently open file.
        /// </summary>
        /// <param name="data">
        /// Pointer to source byte array.
        /// </param>
        /// <param name="size">
        /// Number of bytes to write.
        /// </param>
        /// <param name="address">
        /// File offset to get data from.
        /// </param>
        /// <param name="seek">
        /// Not seeking to the address can speed up file I/O if you're reading
        /// consecutive chunks of data.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool WriteFile(void* data, int size, int address, bool seek)
        {
            return LunarWriteFile(data, (uint)size, (uint)address, (uint)(seek ? SEEK : NOSEEK)) != 0;
        }

        /// <summary>
        /// Changes the bytes used for scanning for free space and erasing data.
        /// </summary>
        /// <param name="val1">
        /// First free space byte.
        /// </param>
        /// <param name="val2">
        /// Second free space byte.
        /// </param>
        /// <param name="erase">
        /// Byte for erasing (good idea to make same as <paramref name="val1"/> or <paramref name="val2"/>).
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool SetFreeBytes(byte val1, byte val2, byte erase)
        {
            return LunarSetFreeBytes((uint)(val1 | (val2 << 8) | (erase << 0x10))) != 0;
        }

        /// <summary>
        /// Converts a SNES ROM Address to a PC file offset.
        /// </summary>
        /// <param name="pointer">
        /// SNES address to convert.
        /// </param>
        /// <param name="romType">
        /// The ROM type.
        /// </param>
        /// <param name="header">
        /// True if the ROM has a 0x200 byte copier header.
        /// </param>
        /// <returns>
        /// The PC file offset of the SNES ROM address.
        /// It will return an undefined value for non-ROM addresses.
        /// </returns>
        /// <remarks>
        /// Do NOT specify an ExROM type if your ROM is not larger than 32 Mbit!
        ///
        /// A warning on 64 Mbit ROMs:  Since banks 7E and 7F are used for RAM instead
        /// of ROM, the SNES cannot access the last 64 KB of a 64 Mbit ExLoROM file
        /// or 64 KB from the second and fourth last 32K chunks in a 64 Mbit ExHiROM file.
        /// Thus you should not store anything past $7F:01FF PC in an ExLoROM file with
        /// a copier header, or in the ranges $7E:0200-7E:81FF and $7F:0200-7F:81FF PC 
        /// in an ExHiROM file with a copier header.
        /// 
        /// Also note that for 64 Mbit ExHiROM files, the area from 0000-7FFF of banks
        /// $70 - $77 is usually used by SRAM, so the corresponding areas in the ROM 
        /// are not accessible.
        /// </remarks>
        public static int SNEStoPC(int pointer, ROMTypes romType, bool header)
        {
            return (int)LunarSNEStoPC((uint)pointer, (uint)romType, (uint)(header ? 1 : 0));
        }

        /// <summary>
        /// Converts PC file offset to SNES ROM address.
        /// </summary>
        /// <param name="pointer">
        /// PC file offset to convert
        /// </param>
        /// <param name="romType">
        /// The ROM type.
        /// </param>
        /// <param name="header">
        /// True if the ROM has a 0x200 byte copier header.
        /// </param>
        /// <returns>
        /// The SNES ROM address of the PC file offset.
        /// It will return an undefined value for non-ROM addresses.
        /// </returns>
        /// <remarks>
        /// Do NOT specify an ExROM type if your ROM is not larger than 32 Mbit!
        ///
        /// A warning on 64 Mbit ROMs:  Since banks 7E and 7F are used for RAM instead
        /// of ROM, the SNES cannot access the last 64 KB of a 64 Mbit ExLoROM file
        /// or 64 KB from the second and fourth last 32K chunks in a 64 Mbit ExHiROM file.
        /// Thus you should not store anything past $7F:01FF PC in an ExLoROM file with
        /// a copier header, or in the ranges $7E:0200-7E:81FF and $7F:0200-7F:81FF PC 
        /// in an ExHiROM file with a copier header.
        /// 
        /// Also note that for 64 Mbit ExHiROM files, the area from 0000-7FFF of banks
        /// $70 - $77 is usually used by SRAM, so the corresponding areas in the ROM 
        /// are not accessible.
        /// </remarks>
        public static int PCtoSNES(int pointer, ROMTypes romType, bool header)
        {
            return (int)LunarPCtoSNES((uint)pointer, (uint)romType, (uint)(header ? 1 : 0));
        }

        /// <summary>
        /// Gets the decompressed data size of the currently opened file.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <returns>
        /// The size of the decompressed data. A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int GetDecompressSize(CompressionFormats compressionFormat)
        {
            return GetDecompressSize(compressionFormat, 0, 0);
        }

        /// <summary>
        /// Gets the decompressed data size of the currently opened file.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <returns>
        /// The size of the decompressed data. A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int GetDecompressSize(CompressionFormats compressionFormat, int offset)
        {
            return GetDecompressSize(compressionFormat, offset, 0);
        }

        /// <summary>
        /// Gets the decompressed data size of the currently opened file.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the decompressed data. A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int GetDecompressSize(CompressionFormats compressionFormat, int offset, int otherOptions)
        {
            return (int)LunarDecompress(null, (uint)offset, 0, (uint)compressionFormat, (uint)otherOptions, null);
        }

        /// <summary>
        /// Gets the decompressed data size of the currently opened file.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <param name="lastROMPosition">
        /// Returns the offset of the next byte that comes after the compressed data. This could be used to calculate the size
        /// of the compressed data after calling the function, using the simple function <paramref name="lastROMPosition"/>-<paramref name="offset"/>.
        /// </param>
        /// <returns>
        /// The size of the decompressed data. A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        public static int GetDecompressSize(CompressionFormats compressionFormat, int offset, int otherOptions, out int lastROMPosition)
        {
            fixed (int* last = &lastROMPosition)
                return (int)LunarDecompress(null, (uint)offset, 0, (uint)compressionFormat, (uint)otherOptions, (uint*)last);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <returns>
        /// A byte array of the decompressed data.
        /// </returns>
        public static byte[] Decompress(CompressionFormats compressionFormat)
        {
            return Decompress(compressionFormat, GetDecompressSize(compressionFormat), 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress. Note that the returned array will be this size. For RLE3 and
        /// RLE4, you must specify the exact size of the decompressed data.
        /// </param>
        /// <returns>
        /// A byte array of <paramref name="maxSize"/> of the decompressed data.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize)
        {
            return Decompress(compressionFormat, maxSize, 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress. Note that the returned array will be this size. For RLE3 and
        /// RLE4, you must specify the exact size of the decompressed data.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <returns>
        /// A byte array of <paramref name="maxSize"/> of the decompressed data.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset)
        {
            return Decompress(compressionFormat, maxSize, offset, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress. Note that the returned array will be this size. For RLE3 and
        /// RLE4, you must specify the exact size of the decompressed data.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, it can be used to specify the
        /// exact size of the data if <paramref name="maxSize"/> doesn't. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// A byte array of <paramref name="maxSize"/> of the decompressed data.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions)
        {
            byte[] data = new byte[maxSize];
            fixed (byte* ptr = data)
                LunarDecompress(ptr, (uint)offset, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions, null);
            return data;
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="compressionFormat">
        /// Compression format to decompress data as.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to decompress. Note that the returned array will be this size. For RLE3 and
        /// RLE4, you must specify the exact size of the decompressed data.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, it can be used to specify the
        /// exact size of the data if <paramref name="maxSize"/> doesn't. For all other instances, the value should be 0.
        /// </param>
        /// <param name="lastROMPosition">
        /// Returns the offset of the next byte that comes after the compressed data. This could be used to calculate the size
        /// of the compressed data after calling the function, using the simple function <paramref name="lastROMPosition"/>-<paramref name="offset"/>.
        /// </param>
        /// <returns>
        /// A byte array of <paramref name="maxSize"/> of the decompressed data.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Decompress(CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions, out int lastROMPosition)
        {
            byte[] data = new byte[maxSize];
            fixed (byte* ptr = data)
            fixed (int* last = &lastROMPosition)
                LunarDecompress(ptr, (uint)offset, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions, (uint*)last);
            return data;
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If <paramref name="destination"/>=null, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(void* destination, CompressionFormats compressionFormat)
        {
            return Decompress(destination, compressionFormat, GetDecompressSize(compressionFormat), 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(void* destination, CompressionFormats compressionFormat, int maxSize)
        {
            return Decompress(destination, compressionFormat, maxSize, 0, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(void* destination, CompressionFormats compressionFormat, int maxSize, int offset)
        {
            return Decompress(destination, compressionFormat, maxSize, offset, 0);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, it can be used to specify the
        /// exact size of the data if <paramref name="maxSize"/> doesn't. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(void* destination, CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions)
        {
            return (int)LunarDecompress(destination, (uint)offset, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions, null);
        }

        /// <summary>
        /// Decompress data from the currently open file into an array.
        /// </summary>
        /// <param name="destination">
        /// Pointer to the destination byte array for the decompressed data.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, it can be used to specify the
        /// exact size of the data if <paramref name="maxSize"/> doesn't. For all other instances, the value should be 0.
        /// </param>
        /// <param name="lastROMPosition">
        /// Returns the offset of the next byte that comes after the compressed data. This could be used to calculate the size
        /// of the compressed data after calling the function, using the simple function <paramref name="lastROMPosition"/>-<paramref name="offset"/>.
        /// </param>
        /// <returns>
        /// The size of the decompressed data.  A value of zero indicates either
        /// failure or a decompressed structure of size 0.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Decompress(void* destination, CompressionFormats compressionFormat, int maxSize, int offset, int otherOptions, out int lastROMPosition)
        {
            fixed (int* last = &lastROMPosition)
                return (int)LunarDecompress(destination, (uint)offset, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions, (uint*)last);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// The byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(byte[] data, CompressionFormats compressionFormat)
        {
            return GetCompressSize(data, compressionFormat, 0, data.Length, 0);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// The byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(byte[] data, CompressionFormats compressionFormat, int address, int dataSize)
        {
            return GetCompressSize(data, compressionFormat, address, dataSize, 0);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// The byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(byte[] data, CompressionFormats compressionFormat, int address, int dataSize, int otherOptions)
        {
            fixed (byte* ptr = &data[address])
                return GetCompressSize(ptr, compressionFormat, dataSize, otherOptions);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// The byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(byte* data, CompressionFormats compressionFormat, int dataSize)
        {
            return GetCompressSize(data, compressionFormat, dataSize, 0);
        }

        /// <summary>
        /// Gets the compressed size of the provided data.
        /// </summary>
        /// <param name="data">
        /// A pointer to byte array of the data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format to compress data as.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        public static int GetCompressSize(byte* data, CompressionFormats compressionFormat, int dataSize, int otherOptions)
        {
            return (int)LunarRecompress(data, null, (uint)dataSize, 0, (uint)compressionFormat, (uint)otherOptions);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
        /// </returns>
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat)
        {
            return Recompress(data, compressionFormat, 0x10000, 0, data.Length, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
        /// </returns>
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat, int address, int dataSize)
        {
            return Recompress(data, compressionFormat, 0x10000, 0, data.Length, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into destination.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat, int maxSize)
        {
            return Recompress(data, compressionFormat, maxSize, 0, data.Length, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into destination.
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat, int maxSize, int address, int dataSize)
        {
            return Recompress(data, compressionFormat, maxSize, address, dataSize, 0);
        }

        /// <summary>
        /// Compresses data from a byte array.
        /// </summary>
        /// <param name="data">
        /// Source byte array of data to compress.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into destination
        /// </param>
        /// <param name="address">
        /// Starting offset to read data at.
        /// </param>
        /// <param name="dataSize">
        /// Size of data to compress.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// A byte array of the compressed data. Returns null on failure.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static byte[] Recompress(byte[] data, CompressionFormats compressionFormat, int maxSize, int address, int dataSize, int otherOptions)
        {
            byte[] compressed = new byte[maxSize];
            fixed (byte* src = &data[address])
            fixed (byte* dest = compressed)
                if (Recompress(src, dest, dataSize, maxSize, compressionFormat, otherOptions) == 0)
                    return null;
            return compressed;
        }

        /// <summary>
        /// Compress data from a byte array and place it into another array. 
        /// </summary>
        /// <param name="source">
        /// Pointer to the source byte array of data to compress.
        /// </param>
        /// <param name="destination">
        /// Pointer of the destination byte array for compressed data.
        /// </param>
        /// <param name="dataSize">
        /// Size of the data to compress in bytes.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Recompress(void* source, void* destination, int dataSize, int maxSize, CompressionFormats compressionFormat)
        {
            return Recompress(source, destination, dataSize, maxSize, compressionFormat, 0);
        }

        /// <summary>
        /// Compress data from a byte array and place it into another array. 
        /// </summary>
        /// <param name="source">
        /// Pointer to the source byte array of data to compress.
        /// </param>
        /// <param name="destination">
        /// Pointer of the destination byte array for compressed data.
        /// </param>
        /// <param name="dataSize">
        /// Size of the data to compress in bytes.
        /// </param>
        /// <param name="maxSize">
        /// Maxinum number of bytes to copy into <paramref name="destination"/>.
        /// </param>
        /// <param name="compressionFormat">
        /// Compression format.
        /// </param>
        /// <param name="otherOptions">
        /// Other compression options. For LZ1 and LZ2 decompression, to convert 3BPP graphics to 4BPP, let the value be 1.
        /// For LZ16, the value must be the number of 8x8 tile rows desired. For RLE3 and RLE4, use it to specify the
        /// exact size of the data. For all other instances, the value should be 0.
        /// </param>
        /// <returns>
        /// The size of the compressed data. A value of zero indicates failure.
        /// </returns>
        /// <remarks>
        /// If the size of the compressed data is greater than <paramref name="maxSize"/>, the data 
        /// will be truncated to fit into the array.  Note however that the size value 
        /// returned by the function will not be the truncated size.  
        /// 
        /// In general, a max limit of 0x10000 bytes is supported for the uncompressed
        /// data, which is the size of a HiROM SNES bank.  A few formats may have lower 
        /// limits depending on their design.
        /// 
        /// If <paramref name="destination"/>=null and/or <paramref name="maxSize"/>=0, no data will be copied to the
        /// array but the function will still compress the data so it can return the size of it.
        /// </remarks>
        public static int Recompress(void* source, void* destination, int dataSize, int maxSize, CompressionFormats compressionFormat, int otherOptions)
        {
            return (int)LunarRecompress(source, destination, (uint)dataSize, (uint)maxSize, (uint)compressionFormat, (uint)otherOptions);
        }

        /// <summary>
        /// Erases an area in a file/ROM by overwriting it with 0's. The 0 byte used 
        /// to erase the area can be changed with the <see cref="SetFreeBytes(byte, byte, byte)"/> function.
        /// </summary>
        /// <param name="offset">
        /// File offset to start at.
        /// </param>
        /// <param name="size">
        /// Number of bytes to zero out.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// This function will not prevent you from erasing past the end of the file,
        /// which will expand the file size.
        /// </remarks>
        public static bool EraseArea(int offset, int size)
        {
            return LunarEraseArea((uint)offset, (uint)size);
        }

        /// <summary>
        /// Expands a SNES ROM by appending 0's to the end of the file, and fixes the
        /// size byte (if possible). The 0 byte used for expansion can be changed with
        /// the <see cref="SetFreeBytes(byte, byte, byte)"/> function.
        /// </summary>
        /// <param name="mBits">
        /// MegaBits to expand the ROM to (0-32).
        /// For sizes greater than 32Mbit, use <see cref="ExpandROM(ExROMExpansionModes)"/>
        /// </param>
        /// <returns>
        /// The expanded size of the ROM. If the ROM is bigger than the size passed, returns the ROMs original size.
        /// Returns 0 on fail.
        /// </returns>
        /// <remarks>
        /// Be warned that the EXLOROM_1 type expansions will physically move the ROM's
        /// original data banks to PC offset 40:0000, which may cause problems if your
        /// code is set up to use hard coded PC file offsets.  Also note that not all
        /// ROMs can be expanded to sizes above 32 Mbits -- check the documentation of 
        /// the Lunar Expand utility for more details (you can get it from FuSoYa's
        /// site).
        /// 
        /// If your emulator can play ToP, it supports 48 Mbit ExHiROM games.  For
        /// 64 Mbit HiROM and 32 &amp; 64 MBit LoROM games, you must use Snes9x 1.39a 
        /// (or 1.39mk3) or higher (zsnes does not yet support these).
        /// </remarks>
        public static int ExpandROM(int mBits)
        {
            return (int)LunarExpandROM((uint)mBits);
        }

        /// <summary>
        /// Expands a SNES ROM by appending 0's to the end of the file, and fixes the
        /// size byte (if possible). The 0 byte used for expansion can be changed with
        /// the <see cref="SetFreeBytes(byte, byte, byte)"/> function.
        /// </summary>
        /// <param name="mBits">
        /// MegaBits to expand the ROM to.
        /// For sizes less than 32Mbit, use <see cref="ExpandROM(int)"/>
        /// </param>
        /// <returns>
        /// The expanded size of the ROM. If the ROM is bigger than the size passed, returns the ROMs original size.
        /// Returns 0 on fail.
        /// </returns>
        /// <remarks>
        /// Be warned that the EXLOROM_1 type expansions will physically move the ROM's
        /// original data banks to PC offset 40:0000, which may cause problems if your
        /// code is set up to use hard coded PC file offsets.  Also note that not all
        /// ROMs can be expanded to sizes above 32 Mbits -- check the documentation of 
        /// the Lunar Expand utility for more details (you can get it from FuSoYa's
        /// site).
        /// 
        /// If your emulator can play ToP, it supports 48 Mbit ExHiROM games.  For
        /// 64 Mbit HiROM and 32 a&amp; 64 MBit LoROM games, you must use Snes9x 1.39a 
        /// (or 1.39mk3) or higher (zsnes does not yet support these).
        /// </remarks>
        public static int ExpandROM(ExROMExpansionModes mBits)
        {
            return (int)LunarExpandROM((uint)mBits);
        }

        /// <summary>
        /// Verifies free space in the ROM available for inserting data (free space is
        /// defined as an area filled with 0's). The 0 byte used to check 
        /// for free space can be changed with the <see cref="SetFreeBytes(byte, byte, byte)"/> function.
        /// </summary>
        /// <param name="addressStart">
        /// File offset to start searching for space.
        /// </param>
        /// <param name="addressEnd">
        /// File offset to start searching
        /// </param>
        /// <param name="size">
        /// Amount of free space to find, in bytes.
        /// </param>
        /// <param name="bankType">
        /// Ignore results the cross the specified bank boundaries.
        /// </param>
        /// <returns>
        /// Returns the file offset of the first valid location in the specified range
        /// that matches the size of the free space requested.  A value of zero indicates
        /// failure.
        /// </returns>
        public static int VerifyFreeSpace(int addressStart, int addressEnd, int size, BankTypes bankType)
        {
            return (int)LunarVerifyFreeSpace((uint)addressStart, (uint)addressEnd, (uint)size, (uint)bankType);
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate()
        {
            return LunarIPSCreate((void*)GetForegroundWindow(), null, null, null, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(IWin32Window owner)
        {
            return LunarIPSCreate((void*)owner.Handle, null, null, null, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="romPath">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(string romPath)
        {
            return LunarIPSCreate((void*)GetForegroundWindow(), null, romPath, null, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <param name="romPath">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(IWin32Window owner, string romPath)
        {
            return LunarIPSCreate((void*)owner.Handle, null, romPath, null, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(string romPath1, string romPath2)
        {
            return LunarIPSCreate((void*)GetForegroundWindow(), null, romPath1, romPath2, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(IWin32Window owner, string romPath1, string romPath2)
        {
            return LunarIPSCreate((void*)owner.Handle, null, romPath1, romPath2, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS patch to create. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(string romPath1, string romPath2, string ipsPath)
        {
            return LunarIPSCreate((void*)GetForegroundWindow(), ipsPath, romPath1, romPath2, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS patch to create. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(IWin32Window owner, string romPath1, string romPath2, string ipsPath)
        {
            return LunarIPSCreate(null, ipsPath, romPath1, romPath2, 0) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS patch to create. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="quiet">
        /// Suppress all message boxes other than file name prompts.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(string romPath1, string romPath2, string ipsPath, bool quiet)
        {
            return LunarIPSCreate((void*)GetForegroundWindow(), ipsPath, romPath1, romPath2, (uint)(quiet ? QUIET : 0)) != 0;
        }

        /// <summary>
        /// Creates an IPS patch file by measuring differences between two other files.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <param name="romPath1">
        /// Path of the unmodified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="romPath2">
        /// Path of the modified ROM to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS patch to create. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="quiet">
        /// Suppress all message boxes other than file name prompts.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSCreate(IWin32Window owner, string romPath1, string romPath2, string ipsPath, bool quiet)
        {
            return LunarIPSCreate((void*)owner.Handle, ipsPath, romPath1, romPath2, (uint)(quiet ? QUIET : 0)) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply()
        {
            return LunarIPSApply((void*)GetForegroundWindow(), null, null, 0) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(IWin32Window owner)
        {
            return LunarIPSApply((void*)owner.Handle, null, null, 0) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(string romPath)
        {
            return LunarIPSApply((void*)GetForegroundWindow(), null, romPath, 0) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(IWin32Window owner, string romPath)
        {
            return LunarIPSApply((void*)owner.Handle, null, romPath, 0) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(string romPath, string ipsPath)
        {
            return LunarIPSApply((void*)GetForegroundWindow(), ipsPath, romPath, 0) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(IWin32Window owner, string romPath, string ipsPath)
        {
            return LunarIPSApply((void*)owner.Handle, ipsPath, romPath, 0) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="quiet">
        /// Suppress all message boxes other than file name prompts.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(string romPath, string ipsPath, bool quiet)
        {
            return LunarIPSApply((void*)GetForegroundWindow(), ipsPath, romPath, (uint)(quiet ? QUIET : 0)) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="quiet">
        /// Suppress all message boxes other than file name prompts.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(IWin32Window owner, string romPath, string ipsPath, bool quiet)
        {
            return LunarIPSApply((void*)owner.Handle, ipsPath, romPath, (uint)(quiet ? QUIET : 0)) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="quiet">
        /// Suppress all message boxes other than file name prompts.
        /// </param>
        /// <param name="log">
        /// Create a log file of the patch.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(string romPath, string ipsPath, bool quiet, bool log)
        {
            return LunarIPSApply((void*)GetForegroundWindow(), ipsPath, romPath, (uint)(quiet ? QUIET : 0) | (uint)(log ? LOG : 0)) != 0;
        }

        /// <summary>
        /// Apply an IPS patch file to another file.
        /// This is based off the code in the LunarIPS (LIPS) utility on FuSoYa's site.
        /// The function ignores any use of the <see cref="LunarOpenRAMFile"/>() function.
        /// </summary>
        /// <param name="owner">
        /// The window that will own the modal dialog box. This is used only to prevent
        /// user input to the window. You can set this to null if you want.
        /// </param>
        /// <param name="romPath">
        /// Path of the ROM file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="ipsPath">
        /// Path of the IPS file to use. Set this to null and Lunar Compress
        /// will prompt the user for the file name.
        /// </param>
        /// <param name="quiet">
        /// Suppress all message boxes other than file name prompts.
        /// </param>
        /// <param name="log">
        /// Create a log file of the patch.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Lunar Compress will prompt the user for the files not specified.
        /// </remarks>
        public static bool IPSApply(IWin32Window owner, string romPath, string ipsPath, bool quiet, bool log)
        {
            return LunarIPSApply((void*)owner.Handle, ipsPath, romPath, (uint)(quiet ? QUIET : 0) | (uint)(log ? LOG : 0)) != 0;
        }

        /// <summary>
        /// Converts standard 8x8 SNES tiles into an indexed pixel map, useful for
        /// drawing to a bitmap when combined with a palette (see the 
        /// <see cref="Render8x8(uint*, int, int, int, int, void*, uint*, ushort, Render8x8Flags)"/> function).
        /// </summary>
        /// <param name="source">
        /// Byte array of SNES source graphics.
        /// </param>
        /// <param name="address">
        /// Starting offset to begin reading at.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 tiles to convert
        /// </param>
        /// <param name="gfxType">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// A byte array of the pixel map.
        /// </returns>
        /// <remarks>
        /// Each 8x8 tile is converted into an array of 64 bytes.  Each byte represents 
        /// the color number of a single pixel.  1bpp tiles use color numbers from 0-1, 
        /// 2bpp is 0-3, 3bpp is 0-7, 4bpp is 0-15, etc.  The bytes are in line order by 
        /// tile (the first 8 bytes are for the 8 pixels on the top line of the first 
        /// 8x8 tile, the next 8 bytes are for the 8 pixels on the second line of the 
        /// same 8x8 tile, and so on until you get to the next tile).
        /// 
        /// In other words, the format is basically like having a 256 color bitmap 
        /// with a width of 8 and a height of 8*NumTiles, except there is no palette
        /// included.
        /// 
        /// The source array size must be at least <paramref name="numTiles"/>*8*BPP bytes.
        /// </remarks>
        public static byte[] CreatePixelMap(byte[] source, int address, int numTiles, GraphicsTypes gfxType)
        {
            byte[] data = new byte[numTiles * TileSize];
            fixed (byte* src = &source[address])
            fixed (byte* dest = data)
                return CreatePixelMap(src, dest, numTiles, gfxType) ? data : null;
        }

        /// <summary>
        /// Converts standard 8x8 SNES tiles into an indexed pixel map, useful for
        /// drawing to a bitmap when combined with a palette (see the 
        /// <see cref="Render8x8(uint*, int, int, int, int, void*, uint*, ushort, Render8x8Flags)"/> function).
        /// </summary>
        /// <param name="source">
        /// Pointer to byte array of SNES source graphics.
        /// </param>
        /// <param name="destination">
        /// Pointer to byte array of destination graphics.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 tiles to convert.
        /// </param>
        /// <param name="gfxType">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        /// <remarks>
        /// Each 8x8 tile is converted into an array of 64 bytes.  Each byte represents 
        /// the color number of a single pixel.  1bpp tiles use color numbers from 0-1, 
        /// 2bpp is 0-3, 3bpp is 0-7, 4bpp is 0-15, etc.  The bytes are in line order by 
        /// tile (the first 8 bytes are for the 8 pixels on the top line of the first 
        /// 8x8 tile, the next 8 bytes are for the 8 pixels on the second line of the 
        /// same 8x8 tile, and so on until you get to the next tile).
        /// 
        /// In other words, the format is basically like having a 256 color bitmap 
        /// with a width of 8 and a height of 8*NumTiles, except there is no palette
        /// included.
        /// 
        /// The source array size must be at least <paramref name="numTiles"/>*8*BPP bytes.
        /// The destination array size must be at least <paramref name="numTiles"/>*64 bytes.
        /// The <paramref name="source"/> and <paramref name="destination"/> variables must NOT point to the same array.
        /// </remarks>
        public static bool CreatePixelMap(byte* source, byte* destination, int numTiles, GraphicsTypes gfxType)
        {
            switch (gfxType)
            {
                case GraphicsTypes.Mode7_8BPP:
                    for (int i = numTiles * TileSize; --i >= 0; )
                        destination[i] = source[i];
                    return true;
                default:
                    return LunarCreatePixelMap(source, destination, (uint)numTiles, (uint)gfxType);
            }
        }

        /// <summary>
        /// Converts an indexed pixel map (such as one created with
        /// <see cref="CreatePixelMap(byte[], int, int, GraphicsTypes)"/>) into standard 8x8 SNES BPP tiles.
        /// </summary>
        /// <param name="source">
        /// Byte array of source graphics.
        /// </param>
        /// <param name="address">
        /// Starting offset to begin reading at.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 tiles to convert.
        /// </param>
        /// <param name="gfxType">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// A byte array of the 8x8 SNES BPP tiles
        /// </returns>
        /// <remarks>
        /// The source array size must be at least <paramref name="numTiles"/>*64 bytes.
        /// </remarks>
        public static byte[] CreateBPPMap(byte[] source, int address, int numTiles, GraphicsTypes gfxType)
        {
            byte[] data = new byte[(numTiles * ((int)gfxType & 0x0F)) << 3];
            fixed (byte* src = &source[address])
            fixed (byte* dest = data)
                return CreateBPPMap(src, dest, numTiles, gfxType) ? data : null;
        }

        /// <summary>
        /// Converts an indexed pixel map (such as one created with
        /// <see cref="CreatePixelMap(byte*, byte*, int, GraphicsTypes)"/> into standard 8x8 SNES BPP tiles.
        /// </summary>
        /// <param name="source">
        /// Pointer to byte array of source graphics.
        /// </param>
        /// <param name="destination">
        /// Pointer to byte array of SNES destination graphics.
        /// </param>
        /// <param name="numTiles">
        /// Number of 8x8 tiles to convert
        /// </param>
        /// <param name="gfxType">
        /// Format of SNES graphics.
        /// </param>
        /// <returns>
        /// True on sucess, false on fail.
        /// </returns>
        /// <remarks>
        /// The source array size must be at least <paramref name="numTiles"/>*64 bytes.
        /// The destination array size must be at least <paramref name="numTiles"/>*8*BPP bytes.
        /// The <paramref name="source"/> and <paramref name="destination"/> variables must NOT point to the same array.
        /// </remarks>
        public static bool CreateBPPMap(byte* source, byte* destination, int numTiles, GraphicsTypes gfxType)
        {
            switch (gfxType)
            {
                case GraphicsTypes.Mode7_8BPP:
                    for (int i = numTiles * TileSize; --i >= 0; )
                        destination[i] = source[i];
                    return true;
                default:
                    return LunarCreateBppMap(source, destination, (uint)numTiles, (uint)gfxType) != 0;
            }
        }

        /// <summary>
        /// Converts a standard SNES 15-bit color into a PC 24-bit color.
        /// </summary>
        /// <param name="snesColor">
        /// SNES RGB value. (?bbbbbgg gggrrrrr)
        /// </param>
        /// <returns>
        /// PC color value. (00000000 rrrrr000 ggggg000 bbbbb000)
        /// </returns>
        public static uint SNEStoPCRGB(uint snesColor)
        {
            return SNEStoPCRGB(snesColor, false);
        }

        /// <summary>
        /// Converts a standard SNES 16-bit color into a PC 32-bit color.
        /// </summary>
        /// <param name="snesColor">
        /// SNES RGB value. (abbbbbgg gggrrrrr)
        /// </param>
        /// <param name="alpha">
        /// When true, the alpha bit will be included. Otherwise, it is treated as zero.
        /// </param>
        /// <returns>
        /// PC color value. (aaaaaaaa rrrrr000 ggggg000 bbbbb000)
        /// </returns>
        public static uint SNEStoPCRGB(uint snesColor, bool alpha)
        {
            return LunarSNEStoPCRGB(snesColor) | (alpha && (snesColor & SNESRGBbit) != 0 ? PCRGBbyte : 0);
        }

        /// <summary>
        /// Converts a standard PC 24-bit color into the nearest SNES 15-bit color, by
        /// rounding each color component to the nearest 5-bit value.
        /// </summary>
        /// <param name="pcColor">
        /// PC RGB value (???????? rrrrrrrr gggggggg bbbbbbbb).
        /// </param>
        /// <returns>
        /// SNES color value. (0bbbbbgg gggrrrrr)
        /// </returns>
        public static ushort PCtoSNESRGB(uint pcColor)
        {
            return PCtoSNESRGB(pcColor, false);
        }

        /// <summary>
        /// Converts a standard PC 32-bit color into the nearest SNES 16-bit color, by
        /// rounding each color component to the nearest 5-bit value.
        /// </summary>
        /// <param name="pcColor">
        /// PC RGB value (aaaaaaaa rrrrrrrr gggggggg bbbbbbbb).
        /// </param>
        /// <param name="alpha">
        /// When true, the alpha bit will be included. Otherwise, it is treated as zero.
        /// </param>
        /// <returns>
        /// SNES color value. (abbbbbgg gggrrrrr)
        /// </returns>
        public static ushort PCtoSNESRGB(uint pcColor, bool alpha)
        {
            return (ushort)(LunarPCtoSNESRGB(pcColor) | (ushort)(alpha && (pcColor & PCRGBbyte) != 0 ? SNESRGBbit : 0));
        }

        /// <summary>
        /// Draws an SNES tile to a PC bitmap (with optional effects). Both the sprite and
        /// BG tile data types are supported.
        /// </summary>
        /// <param name="scan0">
        /// Pointer to the BitmapData Scan0 variable.
        /// </param>
        /// <param name="width">
        /// Width of the bitmap you're drawing to.
        /// </param>
        /// <param name="height">
        /// Height of the bitmap you're drawing to.
        /// </param>
        /// <param name="x">
        /// X-position in the bitmap to draw the tile to.
        /// </param>
        /// <param name="y">
        /// Y-position in the bitmap to draw the tile to.
        /// </param>
        /// <param name="pixelMap">
        /// Pointer to a pixel map of SNES tiles. The array should have at least 0x400 tiles (0x10000 bytes) for
        /// BG tiles, or 0x200 tiles (0x8000 bytes) for sprite tiles.
        /// </param>
        /// <param name="palette">
        /// Pointer to an array of 32-bit uints containg the PC colors to use to render the tiles. The array should
        /// contain at least 16 palettes of 16 colors each (0x400 bytes).
        /// </param>
        /// <param name="tile">
        /// SNES data that defines the tile number and flags used to render the tile.
        /// </param>
        /// <param name="flags">
        /// Special flags for rendering. These flags can be combined.
        /// </param>
        /// <returns>
        /// True on success, false on fail.
        /// </returns>
        public static bool Render8x8(uint* scan0, int width, int height, int x, int y, void* pixelMap, uint* palette, ushort tile, Render8x8Flags flags)
        {
            return LunarRender8x8(scan0, width, height, x, y, pixelMap, palette, (uint)tile, (uint)flags);
        }

        /// <summary>
        /// Scans the ROM in the user-defined range for free space to store the user
        /// supplied data of the given size, and prepends a RAT to it.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data)
        {
            fixed (byte* ptr = data)
                return WriteRATArea(ptr, data.Length, 0, LC.DataSize, 0, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in the user-defined range for free space to store the user
        /// supplied data of the given size, and prepends a RAT to it.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data, int minRange, int maxRange)
        {
            fixed (byte* ptr = data)
                return WriteRATArea(ptr, data.Length, maxRange, maxRange, 0, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data, int minRange, int maxRange, int preferredAddress)
        {
            fixed (byte* ptr = data)
                return WriteRATArea(ptr, data.Length, minRange, maxRange, preferredAddress, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="offset">
        /// The offset of the data to be stored
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data, int minRange, int maxRange, int preferredAddress, int offset, int size)
        {
            fixed (byte* ptr = &data[offset])
                return WriteRATArea(ptr, size, minRange, maxRange, preferredAddress, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="offset">
        /// The offset of the data to be stored
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality. Multiple flags can be combined.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte[] data, int minRange, int maxRange, int preferredAddress, int offset, int size, RATFunctionFlags flags)
        {
            fixed (byte* ptr = &data[offset])
                return WriteRATArea(ptr, size, minRange, maxRange, preferredAddress, flags);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte* data, int size)
        {
            return WriteRATArea(data, size, 0, LC.DataSize, 0, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte* data, int size, int minRange, int maxRange)
        {
            return WriteRATArea(data, size, minRange, maxRange, 0, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte* data, int size, int minRange, int maxRange, int preferredAddress)
        {
            return WriteRATArea(data, size, minRange, maxRange, preferredAddress, RATFunctionFlags.None);
        }

        /// <summary>
        /// Scans the ROM in a user-defined range to store the given data at.
        /// </summary>
        /// <param name="data">
        /// A byte array containg the data to store
        /// </param>
        /// <param name="preferredAddress">
        /// Address you'd like to start scanning at first. If you don't care, set to 0 and the function will start at <paramref name="minRange"/>.
        /// </param>
        /// <param name="size">
        /// The size of the data.
        /// </param>
        /// <param name="minRange">
        /// Min address to scan and store data at.
        /// </param>
        /// <param name="maxRange">
        /// Max address to scan and store data at.
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality. Multiple flags can be combined.
        /// </param>
        /// <returns>
        /// The address where the data was written. Note, this is the address of the actual data, not the address of the RATS tag.
        /// </returns>
        public static int WriteRATArea(byte* data, int size, int minRange, int maxRange, int preferredAddress, RATFunctionFlags flags)
        {
            return (int)LunarWriteRatArea(data, (uint)size, (uint)preferredAddress, (uint)maxRange, (uint)maxRange, (uint)flags);
        }

        /// <summary>
        /// Erases the data at the specified location using the size specified by the data's RAT (if it exists).
        /// </summary>
        /// <returns>
        /// The size of the data erased, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int EraseRATArea(int address)
        {
            return EraseRATArea(address, RATEraseFlags.None, CompressionFormats.None, 0);
        }

        /// <summary>
        /// Erases the data at the specified location using the size specified by the data's RAT (if it exists).
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality.
        /// </param>
        /// <returns>
        /// The size of the data erased, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int EraseRATArea(int address, RATEraseFlags flags)
        {
            return EraseRATArea(address, flags, CompressionFormats.None, 0);
        }

        /// <summary>
        /// Erases the data at the specified location using the size specified by the data's RAT (if it exists).
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality.
        /// </param>
        /// <param name="compressionFormat">
        /// If the data has no RAT, the function can attempt to get the size by using <see cref="Decompress(CompressionFormats)"/>.
        /// Do not attempt to use a format that requires knowing the size of the decompressed data in advance, such as <see cref="CompressionFormats.RLE3"/>.
        /// </param>
        /// <returns>
        /// The size of the data erased, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int EraseRATArea(int address, RATEraseFlags flags, CompressionFormats compressionFormat)
        {
            return EraseRATArea(address, flags, compressionFormat, 0);
        }

        /// <summary>
        /// Erases the data at the specified location using the size specified by the data's RAT (if it exists).
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <param name="flags">
        /// Flags to specify additional functionality.
        /// </param>
        /// <param name="compressionFormat">
        /// If the data has no RAT, the function can attempt to get the size by using <see cref="Decompress(CompressionFormats)"/>.
        /// Do not attempt to use a format that requires knowing the size of the decompressed data in advance, such as <see cref="CompressionFormats.RLE3"/>.
        /// </param>
        /// <param name="size">
        /// Default size to use for the data to erase, in bytes. This is only used if there is no RAT for the data
        /// and the decompression fails. If you don't want the function to erase anything in this case, set the value to 0.
        /// </param>
        /// <returns>
        /// The size of the data erased, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int EraseRATArea(int address, RATEraseFlags flags, CompressionFormats compressionFormat, int size)
        {
            return (int)LunarEraseRatArea((uint)address, (uint)size, (uint)flags | (compressionFormat != CompressionFormats.None ? ((uint)compressionFormat | COMPRESSED) : 0));
        }

        /// <summary>
        /// Determines the size of a segment of data defined by a RAT.  This may be
        /// useful in some cases if you want to know the size without actually erasing
        /// anything, but otherwise it's not necessary.
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <returns>
        /// The size of the data, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int GetRATAreaSize(int address)
        {
            return GetRATAreaSize(address, CompressionFormats.None);
        }

        /// <summary>
        /// Determines the size of a segment of data defined by a RAT.  This may be
        /// useful in some cases if you want to know the size without actually erasing
        /// anything, but otherwise it's not necessary.
        /// </summary>
        /// <param name="address">
        /// Address of the data to erase. This is the address of the data, not the RAT (if it exists).
        /// </param>
        /// <param name="compressionFormat">
        /// If the data has no RAT, the function can attempt to get the size by using <see cref="Decompress(CompressionFormats)"/>.
        /// Do not attempt to use a format that requires knowing the size of the decompressed data in advance, such as <see cref="CompressionFormats.RLE3"/>.
        /// </param>
        /// <returns>
        /// The size of the data, not including the RAT (if it exists). It returns 0 on failure.
        /// </returns>
        public static int GetRATAreaSize(int address, CompressionFormats compressionFormat)
        {
            return (int)LunarGetRatAreaSize((uint)address, (compressionFormat != CompressionFormats.None ? ((uint)compressionFormat | COMPRESSED) : 0));
        }
    }
}