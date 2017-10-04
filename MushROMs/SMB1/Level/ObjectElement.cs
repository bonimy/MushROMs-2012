using System;
using System.Drawing;
using MushROMs.SNESLibrary;
using MushROMs.LunarCompress;

namespace MushROMs.SMB1.Level
{
    public unsafe struct ObjectElement
    {
        private ObjectType data;
        private int value;
        private int screen;
        private int x;
        private int y;
        private int z;
        private int width;
        private int height;
        private Render8x8Flags flags;

        public ObjectType Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public int Screen
        {
            get { return this.screen; }
            set { this.screen = value; }
        }
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public int Z
        {
            get { return this.z; }
            set { this.z = value; }
        }
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        public Render8x8Flags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        public int GetDataSize()
        {
            switch (this.data)
            {
                case ObjectType.SingleTile:
                case ObjectType.StaticObject:
                case ObjectType.Vertical:
                case ObjectType.Horizontal:
                case ObjectType.StaticObjectExtra:
                case ObjectType.Command:
                    return 2;
                case ObjectType.Rectangular:
                case ObjectType.LongHorizontal:
                case ObjectType.GroundObject:
                case ObjectType.CastleTileset:
                case ObjectType.HorizontalExtra:
                case ObjectType.VerticalExtra:
                case ObjectType.Staircase:
                    return 3;
                case ObjectType.Map16Direct:
                    return 5;
                default:
                    throw new ArgumentException("Invalid Object Type");
            }
        }

        public Size GetObjectSize()
        {
            switch (this.data)
            {
                case ObjectType.SingleTile:
                    return new Size(1, 1);
                case ObjectType.StaticObject:
                    switch (this.value)
                    {
                        case 0:
                            return new Size(4, 4);
                        case 1:
                            return new Size(1, this.height);
                        case 2:
                            return new Size(1, 2);
                    }
                    break;
                case ObjectType.Map16Direct:
                    return new Size(this.width, this.height);
                case ObjectType.Vertical:
                    return new Size(1, this.height);
                case ObjectType.Horizontal:
                    return new Size(this.width, 1);
                case ObjectType.Rectangular:
                    return new Size(this.width, this.height);
                case ObjectType.LongHorizontal:
                    return new Size(this.width, 4);
                case ObjectType.HorizontalExtra:
                    return new Size(this.width, 1);
                case ObjectType.VerticalExtra:
                    return new Size(1, this.height);
                case ObjectType.Staircase:
                    return new Size(this.width, this.height);
                case ObjectType.Command:
                    return Size.Empty;
            }
            throw new ArgumentException("Data was an invalid format.");
        }

        public Rectangle GetObjectRectangle()
        {
            return new Rectangle(new Point(this.x, this.y), GetObjectSize());
        }

        private static Map map = null;
        private static HeaderInfo header;

        public static void SetMap(ref Map map, ref HeaderInfo header)
        {
            ObjectElement.map = map;
            ObjectElement.header = header;
        }

        public static void ReleaseMap()
        {
            ObjectElement.map = null;
            //Cannot nullify header because it is a struct. However, nullifying
            //map is enough to through an error if the user forget to do SetMap().
        }

        public void WriteObject(ref Map map, ref HeaderInfo header)
        {
            SetMap(ref map, ref header);
            WriteObject();
            ReleaseMap();
        }

        public void WriteObject()
        {
            ushort** tiles = ObjectElement.map.MapTiles;
            int value = this.value;
            int y = this.y;
            int x = (this.screen * Map.XTilesPerScreen) + this.x;
            int b = y + this.height;
            b = b < map.Height ? b : map.Height;
            int r = x + this.width;
            r = r < map.Width ? r : map.Width;
            int h = b - y;
            int w = r - x;
            switch (this.data)
            {
                case ObjectType.SingleTile:
                    if (value >= 6 && header.LevelType == LevelType.Ground)   //Brick items have different tiles on ground levels.
                        tiles[y][x] = LevelElements.SingleTileObject[value + 5];
                    else
                        tiles[y][x] = LevelElements.SingleTileObject[value];
                    return;
                case ObjectType.StaticObject:
                    break;
                case ObjectType.Map16Direct:
                    do
                    {
                        w = r;
                        do tiles[b][w] = (ushort)value;
                        while (--w >= x);
                    } while (--b >= y);
                    return;
                case ObjectType.Vertical:
                    break;
                case ObjectType.Horizontal:
                    break;
                case ObjectType.Rectangular:
                    break;
                case ObjectType.LongHorizontal:
                    break;
                case ObjectType.GroundObject:
                    break;
                case ObjectType.CastleTileset:
                    break;
                case ObjectType.VerticalExtra:
                    break;
                case ObjectType.Staircase:
                    break;
                case ObjectType.StaticObjectExtra:
                    break;
                case ObjectType.Command:
                    break;
                default:
                    throw new ArgumentException("Object Type is unrecognized value.");
            }
        }
    }

    public enum ObjectType
    {
        SingleTile,
        StaticObject,
        Map16Direct,
        Vertical,
        Horizontal,
        Rectangular,
        LongHorizontal,
        GroundObject,
        CastleTileset,
        HorizontalExtra,
        VerticalExtra,
        Staircase,
        StaticObjectExtra,
        Command,
    }
}