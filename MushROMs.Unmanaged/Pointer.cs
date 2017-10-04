using System;
using System.Runtime.InteropServices;

namespace MushROMs.Unmanaged
{
    /// <summary>
    /// Provides constants and static methods for importing unmanaged pointers.
    /// </summary>
    /// <remarks>
    /// This class is designed for people who know what they are doing. Only use for
    /// large arrays when speed is vital and C#'s stackalloc is not viable.
    /// </remarks>
    public static unsafe class Pointer
    {
        /// <summary>
        /// Specified the path of the DLL file.
        /// This field is constant.
        /// </summary>
        internal const string DLLPath = @"Libraries\Unmanaged.dll";

        [DllImport(DLLPath)]
        public static extern IntPtr CreatePointer(int size);
        [DllImport(DLLPath)]
        public static extern IntPtr CreateEmptyPointer(int size);
        [DllImport(DLLPath)]
        public static extern IntPtr ResizePointer(IntPtr ptr, int size);
        [DllImport(DLLPath)]
        public static extern void FreePointer(IntPtr ptr);
        [DllImport(DLLPath)]
        public static extern IntPtr SetMemory(IntPtr ptr, int value, int size);
        [DllImport(DLLPath)]
        public static extern IntPtr MoveMemory(IntPtr dest, IntPtr src, int size);
        [DllImport(DLLPath)]
        public static extern IntPtr CopyMemory(IntPtr dest, IntPtr src, int size);
        [DllImport(DLLPath)]
        public static extern int CompareMemory(IntPtr ptr1, IntPtr ptr2, int size);
    }
}