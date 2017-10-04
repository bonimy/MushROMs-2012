using System;
using System.ComponentModel;
using MushROMs.Unmanaged;

namespace MushROMs.SNESLibrary
{
    /// <summary>
    /// Represents an array of <see cref="UInt32"/> color values.
    /// </summary>
    /// <remarks>
    /// The <see cref="Palette"/> class is not formatted as a 16x16 array or a 16xN array.
    /// Not all palettes (primarily those in <see cref="ROM"/> data) have variable sizes. Thus,
    /// the only manageable option was to make a linear array whose size is the number of colors.
    /// Further, no alpha components or drawing methods are provided. There are too many possibilities,
    /// so it is left to the programmer to decided how to handle such circumstances. The main attractive
    /// feature of this class is easy access to an unmanaged array for increased performance.
    /// </remarks>
    [DesignTimeVisible(false)]
    public unsafe sealed class Palette : IComponent
    {
        #region Exception strings
        /// <summary>
        /// An exception string that is called when an invalid number of colors is provided.
        /// </summary>
        private const string ErrorNumColors = "Number of colors must be greater than zero.";
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="Palette"/> is disposed by a call to the <see cref="Dispose()"/> method.
        /// </summary>
        public event EventHandler Disposed;
        #endregion

        #region Variables
        /// <summary>
        /// The number of colors available to the current <see cref="Palette"/> object.
        /// This field is read-only.
        /// </summary>
        private readonly int numColors;
        /// <summary>
        /// An array of size <see cref="numColors"/> represent all colors of the current <see cref="Palette"/> object.
        /// This field is read-only.
        /// </summary>
        private readonly uint* colors;

        /// <summary>
        /// Prevents resources from being disposed more than once.
        /// </summary>
        /// <remarks>
        /// Once true, resource will not be disposed again.
        /// </remarks>
        private bool disposed;
        private ISite site;
        #endregion

        #region Accessors
        /// <summary>
        /// Gets the number of colors available to the current <see cref="Palette"/> object.
        /// </summary>
        public int NumColors
        {
            get { return this.numColors; }
        }
        /// <summary>
        /// Gets an array of size <see cref="NumColors"/> representing all colors of the current <see cref="Palette"/> object.
        /// </summary>
        public uint* Colors
        {
            get { return this.colors; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Palette"/> has been disposed of.
        /// </summary>
        public bool IsDisposed
        {
            get { return this.disposed; }
        }
        /// <summary>
        /// Gets or sets the <see cref="ISite"/> associated with the <see cref="Palette"/>.
        /// </summary>
        public ISite Site
        {
            get { return this.site; }
            set { this.site = value; }
        }
        #endregion

        #region Initializers
        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class with a specified number of colors.
        /// </summary>
        /// <param name="numColors">
        /// The number of colors the <see cref="Palette"/> should have.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Number of colors must be greater than zero.
        /// </exception>
        public Palette(int numColors)
        {
            // Array size must be positive, non-zero number.
            if (numColors <= 0)
                throw new ArgumentOutOfRangeException(ErrorNumColors);

            // Create paletted data
            this.numColors = numColors;
            this.colors = (uint*)Pointer.CreateEmptyPointer(sizeof(uint) * numColors);

            // This object is not disposed.
            this.disposed = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class with a specified number of colors.
        /// </summary>
        /// <param name="numColors">
        /// The number of colors the <see cref="Palette"/> should have.
        /// </param>
        /// <param name="container">
        /// An <see cref="IContainer"/> that represents the container for the palette.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Number of colors must be greater than zero.
        /// </exception>
        public Palette(int numColors, IContainer container)
        {
            // Add this palette to the container.
            if (container == null)
                throw new ArgumentNullException("container");
            container.Add(this);

            // Array size must be positive, non-zero number.
            if (numColors <= 0)
                throw new ArgumentOutOfRangeException(ErrorNumColors);

            // Get unmanaged alette data from import functions.
            this.numColors = numColors;
            this.colors = (uint*)Pointer.CreateEmptyPointer(sizeof(uint) * numColors);

            // This object is not disposed
            this.disposed = false;
        }
        #endregion

        #region Disposing methods
        /// <summary>
        /// Disposes managed and unmanaged resources used by the current <see cref="Palette"/> object.
        /// </summary>
        ~Palette()
        {
            Dispose(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">'
        /// true if managed resources should be disposed; otherwise, false.
        /// </param>
        private void Dispose(bool disposing)
        {
            // Make sure we haven't already disposed the object.
            if (!this.disposed)
            {
                if (disposing)
                {
                    //Clean up any managed resources here.
                }

                // Releases allocated memory.
                Pointer.FreePointer((IntPtr)this.colors);

                // Set value to prevent resources being disposed more than once.
                this.disposed = true;

                // Invoke Disposed event.
                if (Disposed != null)
                    Disposed(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}