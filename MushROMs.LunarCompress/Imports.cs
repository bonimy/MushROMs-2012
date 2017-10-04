using System;
using System.Runtime.InteropServices;

namespace MushROMs.LunarCompress
{
    public unsafe partial class LC
    {
        /// <summary>
        /// Returns a pointer to the foreground window (the window with which the user is currently working).
        /// </summary>
        /// <returns>
        /// A pointer to the foreground window (the topmost window).
        /// </returns>
        /// <remarks>
        /// The foreground window applies only to top-level windows (frame windows or dialog boxes).
        /// </remarks>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Specifies the path of the Lunar DLL file. This field is constant.
        /// </summary>
        public const string DLLPath = @"Libraries\Lunar Compress.dll";

        [DllImport(DLLPath)]
        internal static extern uint LunarVersion();
        [DllImport(DLLPath)]
        internal static extern int LunarOpenFile(string fileName, uint fileMode);
        [DllImport(DLLPath)]
        internal static extern byte* LunarOpenRAMFile(void* data, uint fileMode, uint size);
        [DllImport(DLLPath)]
        internal static extern int LunarSaveRAMFile(string fileName);
        [DllImport(DLLPath)]
        internal static extern bool LunarCloseFile();
        [DllImport(DLLPath)]
        internal static extern uint LunarGetFileSize();
        [DllImport(DLLPath)]
        internal static extern uint LunarReadFile(void* destination, uint size, uint address, uint seek);
        [DllImport(DLLPath)]
        internal static extern uint LunarWriteFile(void* source, uint size, uint address, uint seek);
        [DllImport(DLLPath)]
        internal static extern uint LunarSetFreeBytes(uint value);
        [DllImport(DLLPath)]
        internal static extern uint LunarSNEStoPC(uint pointer, uint romType, uint header);
        [DllImport(DLLPath)]
        internal static extern uint LunarPCtoSNES(uint pointer, uint romType, uint header);
        [DllImport(DLLPath)]
        internal static extern uint LunarDecompress(void* destination, uint addressToStart, uint maxDataSize, uint format1, uint format2, uint* lastROMPosition);
        [DllImport(DLLPath)]
        internal static extern uint LunarRecompress(void* source, void* destination, uint dataSize, uint maxDataSize, uint format, uint format2);
        [DllImport(DLLPath)]
        internal static extern bool LunarEraseArea(uint address, uint size);
        [DllImport(DLLPath)]
        internal static extern uint LunarExpandROM(uint mBits);
        [DllImport(DLLPath)]
        internal static extern uint LunarVerifyFreeSpace(uint addressStart, uint addressEnd, uint size, uint bankType);
        [DllImport(DLLPath)]
        internal static extern uint LunarIPSCreate(void* hwnd, string ipsFileName, string romFileName, string rom2FileName, uint flags);
        [DllImport(DLLPath)]
        internal static extern uint LunarIPSApply(void* hwnd, string ipsFileName, string romFileName, uint flags);
        [DllImport(DLLPath)]
        internal static extern bool LunarCreatePixelMap(void* source, void* destination, uint numTiles, uint gfxType);
        [DllImport(DLLPath)]
        internal static extern int LunarCreateBppMap(byte* source, byte* destination, uint numTiles, uint gfxType);
        [DllImport(DLLPath)]
        internal static extern uint LunarSNEStoPCRGB(uint snesColor);
        [DllImport(DLLPath)]
        internal static extern uint LunarPCtoSNESRGB(uint pcColor);
        [DllImport(DLLPath)]
        internal static extern bool LunarRender8x8(uint* theMapBits, int theWidth, int theHeight, int displayAtX, int displayAtY, void* pixelMap, uint* pcPalette, uint map8Tile, uint extra);
        [DllImport(DLLPath)]
        internal static extern uint LunarWriteRatArea(void* theData, uint size, uint preferredAddress, uint minRange, uint maxRange, uint flags);
        [DllImport(DLLPath)]
        internal static extern uint LunarEraseRatArea(uint address, uint size, uint flags);
        [DllImport(DLLPath)]
        internal static extern uint LunarGetRatAreaSize(uint address, uint flags);
    }
}