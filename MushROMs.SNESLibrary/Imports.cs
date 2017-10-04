using System.Runtime.InteropServices;

namespace MushROMs.SNESLibrary
{
    /// <summary>
    /// Provides constants and static methods for importing unmanaged functions from SNES.dll.
    /// </summary>
    internal static unsafe class SNES
    {
        /// <summary>
        /// Specifies the path of the SNES DLL file. This field is constant.
        /// </summary>
        internal const string DLLPath = @"Libraries\SNES.dll";

        [DllImport(DLLPath)]
        internal static extern int CreateNewPointer(int size);
        [DllImport(DLLPath)]
        internal static extern void DestroyPointer(int index);
        [DllImport(DLLPath)]
        internal static extern void* GetPointer(int index);

        // Imports for Palette functions.
        [DllImport(DLLPath)]
        internal static extern int CreateNewPalette(int numColors);
        [DllImport(DLLPath)]
        internal static extern void DestroyPalette(int index);
        [DllImport(DLLPath)]
        internal static extern uint* GetPalette(int index);

        // Imports for GFX functions.
        [DllImport(DLLPath)]
        internal static extern int CreateNewGFX(int numTiles);
        [DllImport(DLLPath)]
        internal static extern void DestroyGFX(int index);
        [DllImport(DLLPath)]
        internal static extern byte*** GetGFX(int index);

        // Imports for Tile8 functions.
        [DllImport(DLLPath)]
        internal static extern int CreateNewTile8(int numTiles);
        [DllImport(DLLPath)]
        internal static extern void DestroyTile8(int index);
        [DllImport(DLLPath)]
        internal static extern ushort* GetTile8(int index);

        // Imports for Tile16 functions.
        [DllImport(DLLPath)]
        internal static extern int CreateNewTile16(int numTiles);
        [DllImport(DLLPath)]
        internal static extern void DestroyTile16(int index);
        [DllImport(DLLPath)]
        internal static extern ushort*** GetTile16(int index);

        // Imports for Map functions.
        [DllImport(DLLPath)]
        internal static extern int CreateNewMap(int width, int height);
        [DllImport(DLLPath)]
        internal static extern void DestroyMap(int index);
        [DllImport(DLLPath)]
        internal static extern ushort** GetMap(int index);

        // Imports for FlagsMap functions.
        [DllImport(DLLPath)]
        internal static extern int CreateNewFlagsMap(int width, int height);
        [DllImport(DLLPath)]
        internal static extern void DestroyFlagsMap(int index);
        [DllImport(DLLPath)]
        internal static extern int** GetFlagsMap(int index);

        // Imports for ROM functions.
        [DllImport(DLLPath)]
        internal static extern int CreateNewROM(int numBanks, int romType);
        [DllImport(DLLPath)]
        internal static extern void DestroyROM(int index);
        [DllImport(DLLPath)]
        internal static extern byte** GetROM(int index);
    }
}