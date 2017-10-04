/* Do not implement until necessary
using System;
using MushROMs.LunarCompress;

namespace MushROMs.SNESLibrary
{
    public unsafe sealed class Map
    {
        public const int XTilesPerScreen = 0x10;
        public const ushort NotSet = 0xFFFF;

        private int handle;
        private int fhandle;
        private int height;
        private int width;
        private int size;
        private ushort** mapTiles;
        private ushort* tiles;
        private Render8x8Flags** flagsMap;
        private Render8x8Flags* flags;

        public int Height
        {
            get { return this.height; }
        }
        public int Width
        {
            get { return this.width; }
        }
        public int Size
        {
            get { return this.size; }
        }
        public ushort** MapTiles
        {
            get { return this.mapTiles; }
        }
        public ushort* Tiles
        {
            get { return this.tiles; }
        }
        public Render8x8Flags** FlagsMap
        {
            get { return this.flagsMap; }
        }
        public Render8x8Flags* Flags
        {
            get { return this.flags; }
        }

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.size = height * width;

            this.handle = SNES.CreateNewMap(width, height);
            this.mapTiles = SNES.GetMap(this.handle);
            this.tiles = *this.mapTiles;
            for (int i = this.size; --i >= 0; )
                this.tiles[i] = NotSet;

            this.fhandle = SNES.CreateNewFlagsMap(width, height);
            this.flagsMap = (Render8x8Flags**)SNES.GetFlagsMap(this.fhandle);
            this.flags = *this.flagsMap;
        }

        ~Map()
        {
            SNES.DestroyMap(this.handle);
            SNES.DestroyFlagsMap(this.fhandle);
        }
    }
}
*/