using System;

namespace MushROMs.SNESLibrary
{
    /// <summary>
    /// Represents an array of <see cref="UInt16"/> values.
    /// </summary>
    /// <remarks>
    /// The <see cref="Tile8"/> class is intended to be an array of values whose
    /// format is identical to 8x8 tiles in SNES format. No drawing methods are provided
    /// so as to not limit what the user could possibly do. Too much information is needed
    /// that the <see cref="Tile8"/> class should not be keeping track of. For simplicity
    /// and flexibility, the class only contains the vital information. Namely, an unmanaged
    /// pointer of the tile data.
    /// </remarks>
    public unsafe sealed class Tile8 : IDisposable
    {
        #region Exception strings
        /// <summary>
        /// An exception string that is called when an invalid number of tiles is provided.
        /// </summary>
        private const string ErrorNumTiles = "Number of tiles must be greater than zero.";
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the <see cref="Tile8"/> is disposed by a call to the <see cref="Dispose()"/> method.
        /// </summary>
        public event EventHandler Disposed;
        #endregion

        #region Private variables
        /// <summary>
        /// An index value that specifies which unmanaged palette data from SNES.dll the current <see cref="Tile8"/> data is using.
        /// This field is read-only.
        /// </summary>
        private readonly int handle;
        /// <summary>
        /// The number of tiles available to the current <see cref="Tile8"/> object.
        /// This field is read-only.
        /// </summary>
        private readonly int numTiles;
        /// <summary>
        /// An array of size <see cref="numTiles"/> containing all the tile data.
        /// This field is read-only.
        /// </summary>
        private readonly ushort* tiles;

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
        /// Gets the number of tiles available to the current <see cref="Tile8"/> object.
        /// </summary>
        public int NumTiles
        {
            get { return this.numTiles; }
        }
        /// <summary>
        /// Gets an array of size <see cref="NumTiles"/> containing all the tile data.
        /// </summary>
        public ushort* Tiles
        {
            get { return this.tiles; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Tile8"/> has been disposed of.
        /// </summary>
        public bool IsDisposed
        {
            get { return this.disposed; }
        }
        #endregion

        #region Initializers
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile8"/> class with the specified number of tiles.
        /// </summary>
        /// <param name="numTiles">
        /// The number of tiles the <see cref="Tile8"/> should have.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Number of tiles must be greater than zero.
        /// </exception>
        public Tile8(int numTiles)
        {
            // Array size must be positive, non-zero number.
            if (numTiles <= 0)
                throw new ArgumentOutOfRangeException(ErrorNumTiles);

            // Get unmanaged Tile8 data from import methods.
            this.numTiles = numTiles;
            this.handle = SNES.CreateNewPointer(sizeof(ushort) * numTiles);
            this.tiles = (ushort*)SNES.GetTile8(this.handle);
            this.disposed = false;

            // Set all tiles to default zero.
            for (int i = numTiles; --i >= 0; )
                this.tiles[i] = 0;
        }
        #endregion

        #region Disposing methods
        /// <summary>
        /// Disposes managed and unmanaged resources used by the current <see cref="Tile8"/> object.
        /// </summary>
        ~Tile8()
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
                SNES.DestroyPointer(this.handle);

                // Set value to prevent resources being disposed more than once.
                this.disposed = true;

                // Invoke Disposed event.
                if (Disposed != null)
                    Disposed(this, EventArgs.Empty);
            }
        }
        #endregion

        /* Do not implement until necessary
        public struct Data
        {
            private ushort value;

            public ushort Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            public int Tile
            {
                get { return this.value & 0x3FF; }
                set { unchecked { this.value &= (ushort)~0x3FF; } this.value |= (ushort)(value & 0x3FF); }
            }

            public int PaletteRow
            {
                get { return (this.value & (7 << 10)) >> 10; }
                set { unchecked { this.value &= (ushort)~(7 << 10); } this.value |= (ushort)((value & 7) << 10); }
            }

            public FlipModes FlipMode
            {
                get { return (FlipModes)(this.Tile & (ushort)FlipModes.FlipXY); }
                set { unchecked { this.value &= (ushort)~FlipModes.FlipXY; } this.value |= (ushort)value; }
            }

            public Data(ushort value)
            {
                this.value = value;
            }

            public static implicit operator Data(ushort value)
            {
                return new Data(value);
            }

            public static explicit operator ushort(Data data)
            {
                return data.value;
            }
        }

        public enum FlipModes : ushort
        {
            NoFlip = 0,
            FlipY = 0x4000,
            FlipX = 0x8000,
            FlipXY = FlipY | FlipX
        }
         * */
    }
}