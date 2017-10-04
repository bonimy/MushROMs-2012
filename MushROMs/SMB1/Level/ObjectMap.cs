using System;
using MushROMs.SNESLibrary;
using MushROMs.LunarCompress;

namespace MushROMs.SMB1.Level
{
    public unsafe sealed class ObjectMap
    {
        public const int MaxObjects = 0x1000;
        public const int MaxScreens = 0x20;
        public const int Maxheight = 0x0D;
        public const byte EndLevel = 0xFF;

        private ObjectElement[] elements;
        private int[] zRelativeIndex;
        private int[] max;
        private int count;

        public ObjectElement[] ObjectElements
        {
            get { return this.elements; }
        }
        public int[] ZRelativeIndex
        {
            get { return this.zRelativeIndex; }
        }
        public int ObjectCount
        {
            get { return this.count; }
        }

        public ObjectMap()
        {
            this.elements = new ObjectElement[MaxObjects];
            this.zRelativeIndex = new int[MaxObjects];
            this.max = new int[MaxScreens];
        }

        public ObjectMap(byte* src, int index, int size)
        {
            if (index > size)
                throw new ArgumentException("Index exceeded array size.");

            this.elements = new ObjectElement[MaxObjects];
            this.zRelativeIndex = new int[MaxObjects];
            this.max = new int[MaxScreens];

            fixed (ObjectElement* elements = this.elements)
            fixed (int* zIndex = this.zRelativeIndex)
            fixed (int* max = this.max)
            {
                ObjectElement* dest = elements;

                this.count = GetNumObjects(src, index, size);
                if (this.count > MaxObjects)
                    throw new ArgumentOutOfRangeException("Source array contains too many objects.");
                src += index;

                for (int z = 0, r = 0, screen = 0; *src != EndLevel; )
                {
                    byte coordinates = *src;
                    byte command = *(src + 1);

                    if (command >= 0x80)    //Gets the screen skip command and checks to make sure we did not exceed.
                    {
                        ++screen;
                        z = 0;
                    }
                    command &= 0x7F;
                    dest->Screen = screen;

                    if (coordinates != 0x0F)    //Standard objects definitions
                    {
                        dest->X = coordinates >> 4;     //X-coordinate of the object.
                        coordinates &= 0x0F;
                        if (coordinates < 0x0D)     //Objects defined in the accessible level region.
                        {
                            dest->Y = coordinates;
                            if (command < 0x0C)     //Static objects of just one tile (like a mushroom block).
                            {
                                dest->Data = ObjectType.SingleTile;
                                dest->Value = command;
                            }
                            else if (command < 0x0F)    //Static objects made of several tiles (like a reverse L-pipe).
                            {
                                dest->Data = ObjectType.StaticObject;
                                dest->Value = command - 0x0C;
                            }
                            else if (command == 0x0F)   //Direct map16 tile insertion.
                            {
                                dest->Data = ObjectType.Map16Direct;
                                dest->Value = *((ushort*)(src + 2));
                                dest->Height = src[4] >> 4;
                                dest->Width = src[4] & 0x0F;
                            }
                            else if (command < 0x40)    //Objects with vertical extension (like pipes and bullet shooters).
                            {
                                dest->Data = ObjectType.Vertical;
                                dest->Value = (command >> 4) - 1;
                                dest->Height = command & 0x0F;
                                if (dest->Value != 0 && dest->Height >= 8)     //Pipes have pirhana plants if height >= 8
                                {
                                    dest->Height &= 7;  //Reset height
                                    dest->Value += 2;   //Enable pipes with pirhana plants.
                                }
                            }
                            else if (command < 0x70)    //Objects with horizontal extension (like coin blocks or bridges).
                            {
                                dest->Data = ObjectType.Horizontal;
                                dest->Value = (command >> 4) - 4;
                                dest->Width = command & 0x0F;
                            }
                            else if (command < 0x7F)    //Objecs with rectangular extension (like coins or mushroom platforms).
                            {
                                dest->Data = ObjectType.Rectangular;
                                dest->Value = command & 0x0F;
                                dest->Height = src[2] >> 4;
                                dest->Width = src[2] & 0x0F;
                            }
                            else    //Objects with long horizontal extension (basic ground).
                            {
                                dest->Data = ObjectType.LongHorizontal;
                                dest->Value = 0;
                                dest->Width = src[2];
                                //dest->Height = 4;
                            }
                        }
                        else if (coordinates == 0x0D)   //Objects mostly related to ground tiles.
                        {
                            dest->Y = command & 0x0F;     //Get Y-coordinate
                            if (command < 0x70)     //Ground objects
                            {
                                dest->Data = ObjectType.GroundObject;
                                dest->Value = command >> 4;
                                dest->Height = src[2] & 0x0F;
                                dest->Width = src[2] >> 4;
                            }
                            else    //The remaing options have even more options...
                            {
                                command = src[2];      //Get the new command byte.
                                if (command < 0x90)    //Objects for castles.
                                {
                                    dest->Data = ObjectType.CastleTileset;
                                    dest->Value = command >> 4;
                                    dest->Height = command & 0x0F;
                                }
                                else if (command < 0xC0)    //More horizontally extendable objects.
                                {
                                    dest->Data = ObjectType.HorizontalExtra;
                                    dest->Value = (command >> 4) - 9;
                                    dest->Width = command & 0x0F;
                                }
                                else if (command < 0xF0)    //More vertically extendable objects.
                                {
                                    dest->Data = ObjectType.VerticalExtra;
                                    dest->Value = (command >> 4) - 0x0C;
                                    dest->Height = command & 0x0F;
                                }
                                else    //The end-of-level staircase
                                {
                                    dest->Data = ObjectType.Staircase;
                                    dest->Value = 0;
                                    dest->Width = command & 0x0F;
                                }
                            }
                        }
                        else if (coordinates == 0x0E)   //More extra objects
                        {
                            if (command < 0x50)     //More objects with non-changing sizes of multiple tiles
                            {
                                dest->Data = ObjectType.StaticObjectExtra;
                                dest->Value = command >> 4;
                                dest->Y = command & 0x0F;
                            }
                            else    //Screen skip. Sets the current screen to the value (not really an object).
                            {
                                screen = command - 0x50;
                                if (screen != dest->Screen)
                                    z = 0;
                                src += 2;
                                continue;   //This isn't an object, so we immediately return to the beginning of the loop.
                            }
                        }
                    }
                    else    //Objects which are actually commands (like sprite generators).
                    {
                        dest->Data = ObjectType.Command;
                        dest->Value = command >> 4;
                        dest->X = command & 0x0F;
                    }

                    zIndex[r] = r++;            //Set the z-relative object index (global values ignoring screen boundaries)
                    dest->Z = ++z;              //set the z-value of the current object (z value is relative to screen).
                    dest->Width++;
                    dest->Height++;
                    ++max[dest->Screen];        //Increment the number of objects on the current screen.
                    src += dest->GetDataSize(); //Increment the source pointer and move on.
                    ++dest;                     //Load next object element.
                }
            }
        }

        private int GetNumObjects(byte* src, int index, int size)
        {
            int count = 0;
            for (int screen = 0; src[index] != EndLevel && index < size; ++count)
            {
                int type = src[index] & 0x0F;
                int obj = src[index + 1];

                if (obj >= 0x80)
                    ++screen;
                if (screen >= MaxScreens)
                    throw new ArgumentException("Maximum screen limit reached.");
                obj &= 0x7F;

                if (type < 0x0D)
                {
                    if (obj < 0x0F)             //Standard objects
                        index += 2;
                    else if (obj == 0x0F)        //Direct Map16 objects
                        index += 5;
                    else if (obj < 0x70)        //Linear expandable objects
                        index += 2;
                    else                        //Rectangular expandable objects
                        index += 3;
                }
                else if (type == 0x0D)          //Misc expandable objects
                    index += 3;
                else if (type == 0x0E)          //Misc. objects
                {
                    index += 2;
                    if (obj >= 0x50)    //Screen skips do not count as objects
                        --count;        //count is decremented to "zero out"
                }
                else if (src[index] == 0x0F)    //Commands and generators
                    index += 2;
                else
                    throw new ArgumentException("Unknown object type parsed.");
            }
            if (index > size)
                throw new ArgumentOutOfRangeException("Level data did not have a proper end byte.");
            return count;
        }

        public void AddObject(ObjectType type, int value, int screen, int x, int y, int width, int height, Render8x8Flags flags)
        {
            if (this.count == MaxObjects)
                throw new ArgumentException("Cannot add any more objects.");

            int i = 0;
            while (this.elements[i].Z != 0)
                ++i;

            this.elements[i].Data = type;
            this.elements[i].Value = value;
            this.elements[i].Screen = screen;
            this.elements[i].X = x;
            this.elements[i].Y = y;
            this.elements[i].Width = width;
            this.elements[i].Height = height;
            this.elements[i].Flags = flags;
            
            this.elements[i].Z = ++this.max[screen];
            int r = GetAbsoluteZ(screen, this.elements[i].Z);
            for (int n = this.count++; n > r; )
                this.zRelativeIndex[--n + 1] = this.zRelativeIndex[n];
            this.zRelativeIndex[r] = i;
        }

        public void DeleteObject(int screen, int z)
        {
            int r = GetAbsoluteZ(screen, z);
            int i = this.zRelativeIndex[r];
            this.elements[i].Z = 0;
            for (int n = this.max[screen]--; --n > z; )
                this.elements[this.zRelativeIndex[n]].Z--;

            --this.count;
            for (int m = i; m < this.count; )
                this.zRelativeIndex[m] = this.zRelativeIndex[++m];
            //this.zRelativeIndex[this.count] = 0;  //This line, pragmatically, isn't needed.
        }

        public void DecreaseZ(int screen, int z, int amount)
        {
            if (z == 0)         //No need to decrease z if already at bottom
                return;
            if (amount > z)     //Do not decrease beyond zero
                amount = z;
            
            int q = GetAbsoluteZ(screen, 0);        //The z value of first object on given screen
            z += q;                                 //variable now represents the absolute z value
            amount = z - amount;                    //variable now represents destination z value for object
            
            int dummy = this.zRelativeIndex[z];     //Save the index value of the selected object
            
            for (int n = z; --n >= amount; )        //This loop shifts all objects in the z range up one z value
            {
                this.elements[this.zRelativeIndex[n]].Z++;
                this.zRelativeIndex[n + 1] = this.zRelativeIndex[n];
            }
            
            this.zRelativeIndex[amount] = dummy;        //Set the z relative index value of the last index to the selected object index
        }

        public void IncreaseZ(int screen, int z, int amount)
        {
            int m = this.max[screen] - z; //the highest z value of the given screen

            if (m == 0)           //No need to increment of object is at the top
                return;
            if (amount > m)       //Do not increment beyond max limit
                amount = m;

            int q = GetAbsoluteZ(screen, 0);        //The z value of first object on given screen
            z += q;                                 //variable now represents the absolute z value
            amount += z;                    //variable now represents destination z value for object

            int dummy = this.zRelativeIndex[z];     //Save the index value of the selected object
            
            for (int n = z; ++n <= amount; )    //This loops shifts back the z values of object in range by one
            {
                this.elements[this.zRelativeIndex[n]].Z--;
                this.zRelativeIndex[n - 1] = this.zRelativeIndex[n];
            }

            this.zRelativeIndex[amount] = dummy;    //Set the z relative index value of the last index to the selected object index
        }

        public void BringToTop(int screen, int z)
        {
            IncreaseZ(screen, z, this.max[screen] - z);
        }

        public void MoveObject(int screen, int z, int newX, int newY, int newScreen)
        {
            BringToTop(screen, z);
            int q = GetAbsoluteZ(screen, this.max[screen]);
            int i = this.zRelativeIndex[q];
            this.elements[i].X = newX;
            this.elements[i].Y = newY;
            
            if (newScreen > screen)
            {
                int last = GetAbsoluteZ(newScreen, this.max[newScreen]);
                for (int n = q; n < last; )
                    this.zRelativeIndex[n] = this.zRelativeIndex[++n];
                this.zRelativeIndex[last] = i;
                --this.max[screen];
                this.elements[i].Z = ++this.max[newScreen];
            }
            else if (newScreen < screen)
            {
                int first = GetAbsoluteZ(newScreen, this.max[newScreen]);
                for (int n = q; n > first; )
                    this.zRelativeIndex[n] = this.zRelativeIndex[--n];
                this.zRelativeIndex[first] = i;
                --this.max[screen];
                this.elements[i].Z = ++this.max[newScreen];
            }
        }

        private int GetAbsoluteZ(int screen, int z)
        {
            while (screen > 0)
                z += this.max[--screen];
            return --z;
        }

        private int GetIndex(int screen, int z)
        {
            return this.zRelativeIndex[GetAbsoluteZ(screen, z)];
        }
    }
}