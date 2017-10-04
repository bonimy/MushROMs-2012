using System;
using System.Drawing;
using MushROMs.LunarCompress;
using MushROMs.SNESLibrary;
using MushROMs.SMB1.Level;

namespace MushROMs.SMB1
{
    public unsafe partial class LevelElements
    {
        public const int MaxObjects = 0x1000;
        public const int MaxScreens = 0x20;
        public const ushort NotSet = Map.NotSet;

        private Object[] objects;
        private int count;
        private Map map;
        private ushort[,] z;
        private LevelType type;
        private bool preview;
        private ushort time;
        private byte startX;
        private byte startY;
        private int height;
        private int width;
        private byte background;

        public Object[] Objects
        {
            get { return this.objects; }
        }
        public int ObjectCount
        {
            get { return this.count; }
        }
        public Map Map
        {
            get { return this.map; }
        }
        public ushort[,] Z
        {
            get { return this.z; }
        }
        public LevelType Type
        {
            get { return this.type; }
        }
        public bool PreviewNextLevel
        {
            get { return this.preview; }
        }
        public ushort Time
        {
            get { return this.time; }
        }
        public byte StartX
        {
            get { return this.startX; }
        }
        public byte StartY
        {
            get { return this.startY; }
        }
        public int Height
        {
            get { return this.height; }
        }
        public int Width
        {
            get { return this.width; }
        }
        public byte Background
        {
            get { return this.background; }
        }

        public LevelElements(int width, int height)
        {
            this.height = height;
            this.width = width;
            this.map = new Map(height, width);
            this.z = new ushort[height, width];
            this.objects = new Object[LevelElements.MaxObjects];
            this.count = 0;
            this.type = LevelType.Ground;
            this.preview = false;
            this.time = 0x400;
            this.startX = 0;
            this.startY = 0;
            this.background = 8;
        }

        public LevelElements(byte[] data, int width, int height)
        {
            InitializeData(ref data, width, height, 0, data.Length);
        }

        public LevelElements(byte[] data, int width, int height, int index, int length)
        {
            InitializeData(ref data, width, height, index, length);
        }

        private void InitializeData(ref byte[] data, int width, int height, int index, int length)
        {
            if (length - index < 6)
                throw new ArgumentException("Data too small to be valid level with header.");

            this.height = height;
            this.width = width;
            this.map = new Map(width, height);
            this.z = new ushort[height, width];
            this.objects = new Object[LevelElements.MaxObjects];

            fixed (byte* ptr1 = &data[index])
            fixed (Object* ptr2 = this.objects)
            {
                byte* src = ptr1;
                Object* dest = ptr2;

                GetHeader(ptr1);
                src += 5;
                length -= 5;

                GetNumObjects(src, length);

                for (int z = 0, screen = 0; z < ObjectCount; src += dest->DataSize, ++dest->Width, ++dest->Height, dest++->Z = z++)
                {
                    byte coordinates = *src;
                    byte command = src[1];

                    if (command >= 0x80)      //Gets the screen skip command and checks to make sure we did not exceed.
                        screen += Map.XTilesPerScreen;

                    command &= 0x7F;
                    if (coordinates != 0x0F)        //Standard object definitions
                    {
                        dest->X = screen + (coordinates >> 4);      //X-coordinate of the object

                        coordinates &= 0x0F;
                        if (coordinates < 0x0D)     //Objects defined in the accessible level region.
                        {
                            dest->Y = coordinates;      //Y-coordinate of the object
                            if (command < 0x0C)     //Objects of just 1 tile.
                            {
                                dest->DataSize = 2;
                                dest->Data = ObjectType.SingleTile;
                                dest->Value = command;
                            }
                            else if (command < 0x0F)     //Objects with non-changing dimensions and multiple tiles.
                            {
                                dest->DataSize = 2;
                                dest->Data = ObjectType.StaticObject;
                                dest->Value = command - 0x0C;
                            }
                            else if (command == 0x0F)   //Direct map16 tile insertion.
                            {
                                dest->DataSize = 5;
                                dest->Data = ObjectType.Map16Direct;
                                dest->Value = *((ushort*)(src + 2));
                                dest->Height = src[4] >> 4;
                                dest->Width = src[4] & 0x0F;
                            }
                            else if (command < 0x40)    //Objects with vertical extension
                            {
                                dest->DataSize = 2;
                                dest->Data = ObjectType.Vertical;
                                dest->Value = (command - 0x10) >> 4;
                                int h = command & 0x0F;
                                if (dest->Value != 0 && h >= 8)     //Pipes with pirhana plants
                                {
                                    h &= 7;
                                    dest->Value += 2;
                                }
                                dest->Height = h;
                            }
                            else if (command < 0x70)    //Objects with horizontal extension
                            {
                                dest->DataSize = 2;
                                dest->Data = ObjectType.Horizontal;
                                dest->Value = (command - 0x40) >> 4;
                                dest->Width = src[1] & 0x0F;
                            }
                            else if (command < 0x7F)    //Objects with rectangular extension
                            {
                                dest->DataSize = 3;
                                dest->Data = ObjectType.Rectangular;
                                dest->Value = command - 0x70;
                                dest->Height = src[2] >> 4;
                                dest->Width = src[2] & 0x0F;
                            }
                            else    //Objects with long horizontal extension
                            {
                                dest->DataSize = 3;
                                dest->Data = ObjectType.LongHorizontal;
                                dest->Value = 0;
                                dest->Width = src[2];
                                dest->Height = 4;
                            }
                        }
                        else if (coordinates == 0x0D)   //Objects mostly related to ground tiles
                        {
                            dest->DataSize = 3;     //All objects are three bytes, so increment here
                            dest->Y = (command << 4) >> 4;     //Get Y-coordinate

                            if (command < 0x70)     //Ground objects
                            {
                                dest->Data = ObjectType.GroundObject;
                                dest->Value = command >> 4;
                                dest->Height = src[2] & 0x0F;
                                dest->Width = src[2] >> 4;
                            }
                            else    //The remaing options have even more options! Such insanity.
                            {
                                command = (byte)(src[2] >> 4);      //Get the new command byte.
                                if (command < 9)    //Objects for castles.
                                {
                                    dest->Data = ObjectType.CastleTileset;
                                    dest->Value = command;
                                    dest->Height = src[2] & 0x0F;
                                }
                                else if (command < 0x0C)    //More horizontally extendable objects.
                                {
                                    dest->Data = ObjectType.HorizontalExtra;
                                    dest->Value = command - 9;
                                    dest->Width = src[2] & 0x0F;
                                }
                                else if (command < 0x0F)    //More vertically extendable objects.
                                {
                                    dest->Data = ObjectType.VerticalExtra;
                                    dest->Value = command - 0x0C;
                                    dest->Height = src[2] & 0x0F;
                                }
                                else    //The end-of-level staircase
                                {
                                    dest->Data = ObjectType.Staircase;
                                    dest->Value = 0;
                                    dest->Width = src[2] & 0x0F;
                                }
                            }
                        }
                        else if (coordinates == 0x0E)   //More extra objects
                        {
                            dest->DataSize = 2;     //All two-byte objects
                            if (command < 0x50)     //More objects with non-changing sizes of multiple tiles
                            {
                                dest->Data = ObjectType.StaticObjectExtra;
                                dest->Value = command >> 4;
                                dest->Y = command & 0x0F;
                            }
                            else    //Screen skip. Sets the current screen to the value (not really on object).
                                screen = (command - 0x50) << 8;
                        }
                    }
                    else if (coordinates == 0x0F)   //Objects which are actually commands (like generators)
                    {
                        dest->DataSize = 2;
                        dest->Data = ObjectType.Command;
                        dest->Value = command >> 4;
                        dest->X = screen + (command & 0x0F);
                    }
                }
            }

            WriteMap();
        }

        private void GetHeader(byte* data)
        {
            this.type = (LevelType)(*data >> 6);
            this.preview = (*data & 0x20) > 0;
            this.time = (ushort)((*data & 0x0F) << 8);
            this.time |= data[1];
            this.startX = data[2];
            this.startY = data[3];
            this.background = data[4];
        }

        private void GetNumObjects(byte* src, int length)
        {
            this.count = 0;
            int index = 0;
            int screen = 0;

            for (; src[index] != 0xFF && index != length; ++this.count)
            {
                byte y = (byte)(src[index] & 0x0F);
                byte o = (byte)(src[index + 1] & 0x7F);

                if (o > 0x7F)
                    ++screen;
                if (screen >= MaxScreens)
                    throw new ArgumentException("Maximum screen limit reached.");

                if (y < 0x0D)
                {
                    if (o < 0x0F)
                        index += 2;
                    else if (o == 0x0F)
                        index += 5;
                    else if (o < 0x70)
                        index += 2;
                    else
                        index += 3;
                }
                else if (y == 0x0D)
                    index += 3;
                else
                    index += 2;
            }
            if (index == length && src[index] != 0xFF)
                throw new ArgumentOutOfRangeException("Level data did not have a proper end byte.");
        }

        public void WriteMap()
        {
            fixed (Object* src = this.objects)
            //fixed (ushort* dest = this.map.Tiles)
            fixed (ushort* zMap = this.z)
            {
                ushort* dest = this.map.Tiles;
                for (int i = this.height * this.width; --i >= 0; )
                {
                    dest[i] = 0;
                    zMap[i] = LevelElements.NotSet;
                }

                for (int z = 0; z < this.count; ++z)
                    src[GetObjectIndexFromZ(z)].WriteObject(dest, this.width, this.height, this.type, zMap);
            }
        }

        public int GetObjectIndexFromZ(int z)
        {
            fixed (Object* src = this.objects)
                for (int i = 0; i < this.count; ++i)
                    if (src[i].Z == z)
                        return i;
            return -1;
        }

        public void ChangeZOrder(int zOld, int zNew)
        {
            fixed (Object* objects = this.objects)
            {
                int max = zOld > zNew ? zOld : zNew;
                int min = zOld < zNew ? zOld : zNew;
                int i = GetObjectIndexFromZ(min);

                for (int z = min; ++z <= max; )
                    --objects[GetObjectIndexFromZ(z)].Z;

                objects[i].Z = max;
            }
        }

        public void Delete(int z)
        {

        }

        public unsafe struct Object
        {
            private ObjectType data;
            private int value;
            private int width;
            private int height;
            private int x;
            private int y;
            private int z;
            private int size;
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
            public int Width
            {
                get { return this.width - this.x; }
                set { this.width = value + this.x; }
            }
            public int Height
            {
                get { return this.height - this.y; }
                set { this.height = value + this.y; }
            }
            public int X
            {
                get
                {
                    return this.x;
                }
                set
                {
                    this.width += value - this.x;
                    this.x = value;
                }
            }
            public int Y
            {
                get
                {
                    return this.y;
                }
                set
                {
                    this.height += value - this.y;
                    this.y = value;
                }
            }
            public int Z
            {
                get { return this.z; }
                set { this.z = value; }
            }
            public int DataSize
            {
                get { return this.size; }
                set { this.size = value; }
            }
            public Render8x8Flags Flags
            {
                get { return this.flags; }
                set { this.flags = value; }
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
                }
                return new Size();
            }

            public Rectangle GetObjectRectangle()
            {
                return new Rectangle(new Point(this.x, this.y), GetObjectSize());
            }

            private void WriteTile(ushort* dest, int width, int height, int x, int y, ushort map16, ushort* zMap)
            {
                if (x < width && y < height && x >= 0 && y >= 0)
                {
                    int offset = (y * width) + x;
                    dest[offset] = map16;
                    if (zMap != null)
                        zMap[offset] = (ushort)this.z;
                }
            }

            public void WriteObject(ushort* dest, int width, int height, LevelType LevelType, ushort* zMap)
            {
                switch (this.data)
                {
                    case ObjectType.SingleTile: //Unchangable tiles of one tile
                        {
                            ushort map16;
                            if (this.value >= 6 && LevelType == LevelType.Ground)
                                map16 = LevelElements.SingleTileObject[this.value + 5];   //Brick items have different tiles on ground levels.
                            else
                                map16 = LevelElements.SingleTileObject[this.value];
                            WriteTile(dest, width, height, this.x, this.y, map16, zMap);
                            break;
                        }
                    case ObjectType.StaticObject:   //Unchangable tiles of several tiles
                        switch (this.value)
                        {
                            case 0:     //Reverse L-Pipe
                                fixed (byte* data = LevelElements.LPipe)
                                {
                                    byte* src = data + 0x10;
                                    for (int x = this.x + 4; --x >= this.x; )
                                        for (int y = this.y + 4; --y >= this.y; )
                                            WriteTile(dest, width, height, x, y, *--src, zMap);
                                }
                                break;
                            case 1:     //Flag pole
                                if (this.y >= height - 5)
                                    this.y = height - 5;

                                WriteTile(dest, width, height, this.x, this.y, LevelElements.FlagPoleBall, zMap);

                                for (int y = height - 3; --y > this.y; )
                                    WriteTile(dest, width, height, this.x, y, LevelElements.FlagPoleBar, zMap);

                                WriteTile(dest, width, height, this.x, height - 3, LevelElements.FlagPoleBase, zMap);
                                break;
                            case 2:     //Spring board
                                WriteTile(dest, width, height, this.x, this.y, LevelElements.SpringBoardTop, zMap);
                                WriteTile(dest, width, height, this.x, this.y + 1, LevelElements.SpringBoardBottom, zMap);
                                break;
                        }
                        break;
                    case ObjectType.Map16Direct:    //Insert direct map16 tile
                        for (int x = this.width; --x >= this.x; )
                            for (int y = this.height; --y >= this.y; )
                                WriteTile(dest, width, height, x, y, (ushort)this.value, zMap);
                        break;
                    case ObjectType.Vertical:   //Objects which can be extended vertically
                        switch (this.value)
                        {
                            case 0:     //Canon
                                WriteTile(dest, width, height, this.x, this.y, LevelElements.CanonHead, zMap);
                                if (this.height - this.y >= 2)
                                {
                                    WriteTile(dest, width, height, this.x, this.y + 1, LevelElements.CanonNeck, zMap);
                                    for (int y = this.y + 2; y < this.height; ++y)
                                        WriteTile(dest, width, height, this.x, y, LevelElements.CanonBody, zMap);
                                }
                                break;
                            case 1:     //Standard pipes
                            case 2:
                            case 3:
                            case 4:
                                {
                                    int add = ((this.value - 1) & 1) << 1;
                                    WriteTile(dest, width, height, this.x, this.y, LevelElements.Pipes[add], zMap);
                                    WriteTile(dest, width, height, this.x + 1, this.y, LevelElements.Pipes[add + 1], zMap);

                                    ushort left = LevelElements.Pipes[add + 4];
                                    ushort right = LevelElements.Pipes[add + 5];
                                    for (int y = this.height; --y > this.y; )
                                    {
                                        WriteTile(dest, width, height, this.x, y, left, zMap);
                                        WriteTile(dest, width, height, this.x + 1, y, right, zMap);
                                    }
                                }
                                break;
                        }
                        break;
                    case ObjectType.Horizontal:     //All horizontal objects
                        if (this.value != 0 || this.width - this.x > 1)
                        {
                            //Write left tile first.
                            WriteTile(dest, width, height, this.x, this.y, LevelElements.HorizontalObjects[this.value + 3], zMap);

                            if (this.width - this.x >= 2)
                            {
                                //Write right tile.
                                WriteTile(dest, width, height, this.width - 1, this.y, LevelElements.HorizontalObjects[this.value + 6], zMap);

                                //Write all tiles in middle.
                                ushort map16 = LevelElements.HorizontalObjects[this.value];
                                for (int x = this.width - 1; --x > this.x; )
                                    WriteTile(dest, width, height, x, this.y, map16, zMap);
                            }
                        }
                        if (this.value == 0)    //Bridge is a special case.
                            for (int x = this.width; --x >= this.x; )
                                WriteTile(dest, width, height, x, this.y + 1, LevelElements.BridgeTile, zMap);
                        break;
                    case ObjectType.Rectangular:    //Rectangular extendable object.
                        switch (this.value)
                        {
                            case 0:     //Rectangular objects of 1 tile
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                {
                                    ushort map16 = LevelElements.Rectangular[this.value];
                                    for (int x = this.width; --x >= this.x; )
                                        for (int y = this.height; --y >= this.y; )
                                            WriteTile(dest, width, height, x, y, map16, zMap);
                                }
                                break;
                            case 7:     //Coin
                                {
                                    //Tile changes when under water.
                                    ushort map16 = LevelType != LevelType.Underwater ? LevelElements.StandardCoin : LevelElements.WaterCoin;
                                    for (int x = this.width; --x >= this.x; )
                                        for (int y = this.height; --y >= this.y; )
                                            WriteTile(dest, width, height, x, y, map16, zMap);
                                }
                                break;
                            case 8:     //Rectangular objects of two tiles
                            case 9:
                            case 0x0A:
                            case 0x0B:
                            case 0x0C:
                                {
                                    ushort top = LevelElements.Rectangular[this.value + 4];
                                    ushort bottom = LevelElements.Rectangular[this.value - 1];
                                    for (int x = this.width; --x >= this.x; )
                                    {
                                        WriteTile(dest, width, height, x, this.y, top, zMap);
                                        for (int y = this.height; --y > this.y; )
                                            WriteTile(dest, width, height, x, y, bottom, zMap);
                                    }
                                }
                                break;
                            case 0x0D:      //Green Tree Island (I hate this)
                                WriteTile(dest, width, height, this.x, this.y, LevelElements.IslandCap[2], zMap); //Left cap of ledge
                                if (this.width - this.x >= 2)
                                {
                                    ushort middle = LevelElements.IslandCap[0];    //Middle ground piece
                                    for (int x = this.width - 1; --x > this.x; )
                                        WriteTile(dest, width, height, x, this.y, middle, zMap);

                                    //Right cap of ledge
                                    WriteTile(dest, width, height, this.width - 1, this.y, LevelElements.IslandCap[4], zMap);

                                    if (this.height - this.y >= 2)
                                    {
                                        int x = this.x + 1;
                                        int y = this.x + 1;
                                        if (this.width - this.x > 3)    //stem is more than 1 tile wide 
                                        {
                                            WriteTile(dest, width, height, x, y, LevelElements.GreenIslandNeckLeft, zMap);
                                            while (++x < this.width - 2)
                                                WriteTile(dest, width, height, x, y, LevelElements.GreenIslandNeckTile, zMap);
                                            WriteTile(dest, width, height, x, y, LevelElements.GreenIslandNeckRight, zMap);

                                            for (y = this.y + 2, x = this.x + 1; y < this.height; ++y, x = this.x + 1)
                                            {
                                                WriteTile(dest, width, height, x, y, LevelElements.GreenIslandBodyLeft, zMap);
                                                while (++x < this.width - 2)
                                                    WriteTile(dest, width, height, x, y, LevelElements.GreenIslandBodyTile, zMap);
                                                WriteTile(dest, width, height, x, y, LevelElements.GreenIslandBodyRight, zMap);
                                            }
                                        }
                                        else if (this.width - this.x > 2)    //stem is exactly 1 tile wide
                                        {
                                            WriteTile(dest, width, height, x, y, LevelElements.GreenIslandNeckSingle, zMap);

                                            for (y = this.y + 2; y < this.height; ++y)
                                                WriteTile(dest, width, height, x, y, LevelElements.GreenIslandBodySingle, zMap);
                                        }
                                    }
                                }
                                break;
                            case 0x0E:      //Red Mushroom Island
                                WriteTile(dest, width, height, this.x, this.y, LevelElements.IslandCap[3], zMap);

                                if (this.width - this.x >= 2)
                                {
                                    WriteTile(dest, width, height, this.width - 1, this.y, LevelElements.IslandCap[5], zMap);
                                    for (int x = this.width - 1; --x > this.x; )
                                        WriteTile(dest, width, height, x, this.y, LevelElements.IslandCap[1], zMap);
                                    if ((this.width - this.x >= 3) && (this.height - this.y >= 2))
                                    {
                                        int x = (this.x + this.width - 1) >> 1;
                                        WriteTile(dest, width, height, x, this.y + 1, LevelElements.RedIslandStemNeck, zMap);
                                        for (int y = this.y + 2; y < this.height; ++y)
                                            WriteTile(dest, width, height, x, y, LevelElements.RedIslandStemBody, zMap);
                                    }
                                }
                                break;
                        }
                        break;
                    case ObjectType.LongHorizontal:     //Maybe one day it can support more stuff, but for now, just the long, vertical ground object
                        for (int x = this.width; --x >= this.x; )
                        {
                            WriteTile(dest, width, height, x, this.y, LevelElements.GroundTop, zMap);
                            for (int y = this.height; --y > this.y; )
                                WriteTile(dest, width, height, x, y, LevelElements.GroundBottom, zMap);
                        }
                        break;
                    case ObjectType.GroundObject:
                        fixed (byte* ground = LevelElements.GroundObjects)
                        {
                            if (this.value >= 0 && this.value < 6)      //Ground objects
                            {
                                int type = this.value;
                                if (type >= 3)     //Castle ground objects
                                    type -= 3;
                                int i = this.value >= 3 ? 1 : 0;  //Change index for castle ground objects

                                if (type < 2 || this.width - this.x > 1)
                                {
                                    for (int x = this.width; --x >= this.x; )   //Top of ground (middle)
                                        WriteTile(dest, width, height, x, this.y, ground[i], zMap);
                                    if (type != 1)     //Left ledge
                                        WriteTile(dest, width, height, this.x, this.y, ground[4 + i], zMap);
                                    if (type != 0)     //Right ledge
                                        WriteTile(dest, width, height, this.width - 1, this.y, ground[6 + i], zMap);

                                    for (int y = this.height; --y > this.y; )
                                    {
                                        for (int x = this.width; --x >= this.x; )     //Center ground
                                            WriteTile(dest, width, height, x, y, ground[2 + i], zMap);
                                        if (type != 1)     //Left edge
                                            WriteTile(dest, width, height, this.x, y, ground[8 + i], zMap);
                                        if (type != 0)     //Right edge
                                            WriteTile(dest, width, height, this.width - 1, y, ground[0x0A + i], zMap);
                                    }
                                }
                                else
                                {
                                    WriteTile(dest, width, height, this.x, this.y, ground[0x0C + i], zMap);
                                    for (int y = this.height; --y > this.y; )
                                        WriteTile(dest, width, height, this.x, y, ground[0x0E + i], zMap);
                                }
                            }
                            else    //Castle ceiling
                                for (int y = this.height; --y >= this.y; )
                                    for (int x = this.width; --x >= this.x; )
                                        WriteTile(dest, width, height, x, y, ground[0x10 + ((x ^ y) & 1)], zMap);
                        }
                        break;
                    case ObjectType.CastleTileset:
                        switch (this.value)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                break;
                        }
                        break;
                    case ObjectType.HorizontalExtra:    //Row of question blocks (with coin) or Bowser Bridge
                        {
                            ushort map16 = this.value == 0 ? LevelElements.QuestionBlockRowTile : LevelElements.BowserBridgeTile;
                            for (int x = this.width; --x >= this.x; )
                                WriteTile(dest, width, height, x, this.y, map16, zMap);
                            break;
                        }
                    case ObjectType.VerticalExtra:
                        switch (this.value)
                        {
                            case 0:     //Ceiling cap
                                for (int y = this.y + 1; y < this.height; y += 2)
                                    WriteTile(dest, width, height, this.x, y, LevelElements.CeilingCapTile, zMap);
                                break;
                            case 1:     //Lift Rope
                                for (int y = this.height; --y >= this.y; )
                                    WriteTile(dest, width, height, this.x, y, LevelElements.VerticalLiftRopeTile, zMap);
                                for (int y = this.height; y < height; ++y)
                                    WriteTile(dest, width, height, this.x, y, LevelElements.VerticalLiftRopeAir, zMap);
                                break;
                            case 2:     //Long Reverse L-Pipe
                                fixed (byte* tiles = LevelElements.LongLPipe)
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        int x = this.x + i;
                                        int y;
                                        for (y = this.y; y < this.Height - 2; y++)
                                            WriteTile(dest, width, height, x, y, tiles[i], zMap);

                                        WriteTile(dest, width, height, x, y, tiles[4 + i], zMap);
                                        WriteTile(dest, width, height, x, y + 1, tiles[8 + i], zMap);
                                    }
                                }
                                break;
                        }
                        break;
                    case ObjectType.Staircase:
                        {
                            int w = this.width - this.y;
                            int max = w < LevelElements.StaircaseHeight ? w : LevelElements.StaircaseHeight;
                            for (int x = max; --x >= 0; )
                                for (int y = max - (x + 1); y < max; ++y)
                                    WriteTile(dest, width, height, this.x + x, this.y + y, LevelElements.StaircaseTile, zMap);
                            if (w > LevelElements.StaircaseHeight)
                                for (int y = 0; y < LevelElements.StaircaseHeight; ++y)
                                    WriteTile(dest, width, height, this.x + LevelElements.StaircaseHeight, this.y + y, LevelElements.StaircaseTile, zMap);
                        }
                        break;
                    case ObjectType.StaticObjectExtra:
                        switch (this.value)
                        {
                            case 0:     //Small tree
                                WriteTile(dest, width, height, this.x, this.y, LevelElements.SmallTreeHead, zMap);
                                WriteTile(dest, width, height, this.x, this.y + 1, LevelElements.TreeStemTile, zMap);
                                break;
                            case 1:     //Big tree
                                WriteTile(dest, width, height, this.x, this.y, LevelElements.BigTreeHeadTop, zMap);
                                WriteTile(dest, width, height, this.x, this.y + 1, LevelElements.BigTreeHeadBottom, zMap);
                                WriteTile(dest, width, height, this.x, this.y + 2, LevelElements.TreeStemTile, zMap);
                                break;
                            case 2:     //Small castle
                                fixed (byte* tiles = &LevelElements.Castle[LevelElements.BigCastleHeight * 2])
                                {
                                    for (int i = LevelElements.SmallCastleWidth; --i >= 0; )
                                    {
                                        int index = i * BigCastleHeight;
                                        for (int j = LevelElements.SmallCastleHeight; --j >= 0; )
                                            WriteTile(dest, width, height, this.x + i, this.y + j, tiles[index + j], zMap);
                                    }
                                }
                                break;
                            case 3:     //Big castle
                                fixed (byte* tiles = LevelElements.Castle)
                                {
                                    for (int i = LevelElements.BigCastleWidth; --i >= 0; )
                                    {
                                        int index = i * BigCastleHeight;
                                        for (int j = LevelElements.BigCastleHeight; --j >= 0; )
                                            WriteTile(dest, width, height, this.x + i, this.y + j, tiles[index + j], zMap);
                                    }
                                }
                                break;
                            case 4:     //Descending stairs of castle
                                fixed (byte* tiles = LevelElements.CastleStairs)
                                {
                                    byte[] offset = { 0, 0, 0, 5, 4, 3 };
                                    for (int i = offset.Length; --i >= 0; )
                                    {
                                        for (int j = LevelElements.CastleStairsHeight; --j >= 0; )
                                        {
                                            ushort map16 = tiles[offset[i] + j];
                                            if (map16 != 0)
                                                WriteTile(dest, width, height, this.x + i, this.y + j, map16, zMap);
                                        }
                                    }
                                    break;
                                }
                        }
                        break;
                    case ObjectType.Command:
                        break;
                }
            }
        }
    }
}