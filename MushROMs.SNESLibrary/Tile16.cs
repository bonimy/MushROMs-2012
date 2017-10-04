using System;

namespace MushROMs.SNESLibrary
{
    /// <summary>
    /// Represents a multidimensional array of <see cref="UInt16"/> values.
    /// </summary>
    /// <remarks>
    /// The <see cref="Tile16"/> class attempts to be a composition of a 2x2
    /// grid of <see cref="Tile8"/> tiles. No drawing methods are provided since
    /// there is too much information the class doesn't have access to and to
    /// not limit the programmer from more seemlessly implementing draw methods.
    /// The main advantage of this class is easy access to an unmanaged array.
    /// </remarks>
    public unsafe sealed class Tile16 : IDisposable
    {
        #region Exception strings
        /// <summary>
        /// An exception string that is called when an invalid number of tiles is provided.
        /// </summary>
        private const string ErrorNumTiles = "Number of tiles must be greater than zero.";
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="Tile16"/> is disposed by a call to the <see cref="Dispose()"/> method.
        /// </summary>
        public event EventHandler Disposed;
        #endregion

        #region Constants
        /// <summary>
        /// Represents the number of <see cref="Tile8"/> tiles that span horizontally across the <see cref="Tile16"/> tile.
        /// This field is constant.
        /// </summary>
        public const int NumTile8X = 2;
        /// <summary>
        /// Represents the number of <see cref="Tile8"/> tiles that span vertically across the <see cref="Tile16"/> tile.
        /// This field is constant.
        /// </summary>
        public const int NumTile8Y = NumTile8X;
        /// <summary>
        /// Represents the number of <see cref="Tile8"/> tiles that span across the <see cref="Tile16"/> tile.
        /// This field is constant.
        /// </summary>
        public const int NumTile8 = NumTile8Y * NumTile8X;
        #endregion

        #region Private variables
        /// <summary>
        /// An index value that specifies which unmanaged palette data from SNES.dll the current <see cref="Tile8"/> data is using.
        /// This field is read-only.
        /// </summary>
        private readonly int handle;
        /// <summary>
        /// The number of tiles available to the current <see cref="Tile16"/> object.
        /// This field is read-only.
        /// </summary>
        private readonly int numTiles;
        /// <summary>
        /// An array of dimensions <see cref="numTiles"/>×<see cref="NumTile8Y"/>×<see cref="NumTile8X"/> containing all the <see cref="Tile16"/> tile data.
        /// </summary>
        private readonly ushort*** tiles2D;
        /// <summary>
        /// An array of dimensions <see cref="numTiles"/>×<see cref="NumTile8"/> containing all the <see cref="Tile16"/> tile data.
        /// </summary>
        private readonly ushort** tiles1D;
        /// <summary>
        /// An array of size <see cref="numTiles"/>*<see cref="NumTile8"/> containing all the <see cref="Tile16"/> tile data.
        /// </summary>
        private readonly ushort* s8Tiles;

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
        /// Gets the number of tiles available to the current <see cref="Tile16"/> object.
        /// </summary>
        public int NumTiles
        {
            get { return this.numTiles; }
        }
        /// <summary>
        /// Gets an array of dimensions <see cref="NumTiles"/>×<see cref="NumTile8Y"/>×<see cref="NumTile8X"/> containing all the <see cref="Tile16"/> tile data.
        /// </summary>
        public ushort*** Tiles2D
        {
            get { return this.tiles2D; }
        }
        /// <summary>
        /// Gets an array of dimensions <see cref="NumTiles"/>×<see cref="NumTile8"/> containing all the <see cref="Tile16"/> tile data.
        /// </summary>
        public ushort** Tiles1D
        {
            get { return this.tiles1D; }
        }
        /// <summary>
        /// An array of size <see cref="numTiles"/>*<see cref="NumTile8"/> containing all the <see cref="Tile16"/> tile data.
        /// </summary>
        public ushort* S8Tiles
        {
            get { return this.s8Tiles; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Tile16"/> has been disposed of.
        /// </summary>
        public bool IsDisposed
        {
            get { return this.disposed; }
        }
        #endregion

        #region Initializers
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile16"/> class with the specified number of tiles.
        /// </summary>
        /// <param name="numTiles">
        /// The number of tiles the <see cref="Tile16"/> should have.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Number of tiles must be greater than zero.
        /// </exception>
        public Tile16(int numTiles)
        {
            // Array size must be positive, non-zero number.
            if (numTiles <= 0)
                throw new ArgumentOutOfRangeException(ErrorNumTiles);

            // Get unmanaged Tile8 data from import methods.
            this.numTiles = numTiles;
            this.handle = SNES.CreateNewTile16(numTiles);
            this.tiles2D = SNES.GetTile16(this.handle);
            this.tiles1D = *this.tiles2D;
            this.s8Tiles = *this.tiles1D;

            // Set all tiles to default zero.
            for (int i = numTiles * NumTile8; --i >= 0; )
                this.s8Tiles[i] = 0;
        }
        #endregion

        #region Disposing methods
        /// <summary>
        /// Disposes managed and unmanaged resources used by the current <see cref="Tile16"/> object.
        /// </summary>
        ~Tile16()
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

                // Releases allocated memory in unmanaged SNES.dll
                SNES.DestroyTile8(this.handle);

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