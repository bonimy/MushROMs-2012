using System;
using MushROMs.Unmanaged;

namespace MushROMs.SNESLibrary
{
    /// <summary>
    /// Represents a multidimensional array of <see cref="Byte"/> values.
    /// </summary>
    /// <remarks>
    /// Each pixel element in the <see cref="GFX"/> class doesn't represent a color, but instead
    /// represents an index of which color from a <see cref="Palette"/> to read from. Like the
    /// <see cref="Palette"/> class. No methods for drawing are provided, as there are too many
    /// variations. Also, the <see cref="GFX"/> class only provides data on the number of tiles,
    /// and makes no attempt to organize them by rows and columns, or to determine which BPP format
    /// they are. The attractive feature of the <see cref="GFX"/> class is easy access to an
    /// unmanaged multidimensional array which accurately represents the tile structure.
    /// </remarks>
    public unsafe sealed class GFX : IDisposable
    {
        #region Exception strings
        /// <summary>
        /// An exception string that is called when an invalid number of tiles is provided.
        /// </summary>
        private const string ErrorNumTiles = "Number of tiles must be greater than zero.";
        #endregion

        #region Constants
        /// <summary>
        /// The width, in pixels, of a single <see cref="GFX"/> tile.
        /// </summary>
        public const int TileWidth = 8;
        /// <summary>
        /// The height, in pixels, of a signle <see cref="GFX"/> tile.
        /// </summary>
        public const int TileHeight = TileWidth;
        /// <summary>
        /// The size, in pixels, of a single <see cref="GFX"/> tile.
        /// </summary>
        public const int TileSize = TileHeight * TileWidth;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="GFX"/> is disposed by a call to the <see cref="Dispose()"/> method.
        /// </summary>
        public event EventHandler Disposed;
        #endregion

        #region Private variables
        /// <summary>
        /// The number of tiles available to the current <see cref="GFX"/> object.
        /// This field is read-only.
        /// </summary>
        private readonly int numTiles;
        /// <summary>
        /// An array of dimensions <see cref="numTiles"/>×<see cref="TileHeight"/>×<see cref="TileWidth"/> containing all the <see cref="GFX"/> pixel data.
        /// This field is read-only.
        /// </summary>
        private readonly byte*** tiles;
        /// <summary>
        /// An array of dimensions <see cref="numTiles"/>×<see cref="TileWidth"/> containing all the <see cref="GFX"/> pixel data.
        /// This feild is read-only.
        /// </summary>
        private readonly byte** rows;
        /// <summary>
        /// An array of size <see cref="numTiles"/>*<see cref="TileSize"/> containing all the pixel data.
        /// This field is read-only.
        /// </summary>
        private readonly byte* pixels;

        /// <summary>
        /// Prevents resources from being disposed more than once.
        /// </summary>
        /// <remarks>
        /// Once true, resources will not be disposed again.
        /// </remarks>
        private bool disposed;
        #endregion

        #region Public variables
        /// <summary>
        /// Gets the number of tiles available to the current <see cref="GFX"/> object.
        /// </summary>
        public int NumTiles
        {
            get { return this.numTiles; }
        }
        /// <summary>
        /// Gets an array of dimensions <see cref="NumTiles"/>×<see cref="TileHeight"/>×<see cref="TileWidth"/> containing all the <see cref="GFX"/> pixel data.
        /// </summary>
        public byte*** Tiles
        {
            get { return this.tiles; }
        }
        /// <summary>
        /// Gets an array of size <see cref="numTiles"/>*<see cref="TileSize"/> containing all the <see cref="GFX"/> pixel data.
        /// </summary>
        public byte* Pixels
        {
            get { return this.pixels; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="GFX"/> has been disposed of.
        /// </summary>
        public bool IsDisposed
        {
            get { return this.disposed; }
        }
        #endregion

        #region Initializers
        /// <summary>
        /// Initializes a new instance of the <see cref="GFX"/> class with the specified number of tiles.
        /// </summary>
        /// <param name="numTiles">
        /// The number of tiles the <see cref="GFX"/> should have.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Number of tiles must be greater than zero.
        /// </exception>
        public GFX(int numTiles)
        {
            // Array size must be positive, non-zero number.
            if (numTiles <= 0)
                throw new ArgumentOutOfRangeException(ErrorNumTiles);

            // Create unmanaged GFX data.
            this.numTiles = numTiles;
            this.pixels = (byte*)Pointer.CreatePointer(sizeof(byte) * numTiles * TileSize);
            this.rows = (byte**)Pointer.CreatePointer(sizeof(byte*) * numTiles * TileHeight);
            this.tiles = (byte***)Pointer.CreatePointer(sizeof(byte**) * numTiles);

            // Properly set up the multidimensional arrays.
            for (int i = numTiles * TileHeight; --i >= 0; )
                this.rows[i] = this.pixels + (i * TileWidth);

            for (int i = numTiles; --i >= 0; )
            {
                this.tiles[i] = this.rows + (i * TileHeight);
                for (int j = TileHeight; --j >= 0; )
                    this.tiles[i][j] = this.pixels + (i * TileSize) + (j * TileWidth);
            }

            // This object is not disposed.
            this.disposed = false;
        }
        #endregion

        #region Disposing methods
        /// <summary>
        /// Disposes managed and unmanaged resources used by the current <see cref="GFX"/> object.
        /// </summary>
        ~GFX()
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
                    // Clean up any future managed resources here.
                }

                // Releases allocated memory.
                Pointer.FreePointer((IntPtr)this.tiles);
                Pointer.FreePointer((IntPtr)this.rows);
                Pointer.FreePointer((IntPtr)this.pixels);

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