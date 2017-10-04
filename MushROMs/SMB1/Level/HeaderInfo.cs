using System;

namespace MushROMs.SMB1.Level
{
    public unsafe struct HeaderInfo
    {
        public const int HeaderSize = 5;

        private LevelType type;
        private bool preview;
        private ushort time;
        private byte startX;
        private byte startY;
        private byte background;

        public LevelType LevelType
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public bool PreviewNextLevel
        {
            get { return this.preview; }
            set { this.preview = value; }
        }
        public ushort Time
        {
            get { return this.time; }
            set { this.time = value; }
        }
        public byte StartX
        {
            get { return this.startX; }
            set { this.startX = value; }
        }
        public byte StartY
        {
            get { return this.startY; }
            set { this.startY = value; }
        }
        public byte Background
        {
            get { return this.background; }
            set { this.background = value; }
        }

        public HeaderInfo(LevelType type, bool preview, ushort time, byte startX, byte startY, byte background)
        {
            this.type = type;
            this.preview = preview;
            this.time = time;
            this.startX = startX;
            this.startY = startY;
            this.background = background;
        }

        public HeaderInfo(byte* data)
        {
            this.type = (LevelType)(*data >> 6);
            this.preview = (*data & 0x20) != 0;
            this.time = (ushort)((*data & 0x0F) << 8);
            this.time |= data[1];
            this.startX = data[2];
            this.startY = data[3];
            this.background = data[4];
        }

        public HeaderInfo(byte* data, int index)
        {
            data += index;
            this.type = (LevelType)(*data >> 6);
            this.preview = (*data & 0x20) != 0;
            this.time = (ushort)((*data & 0x0F) << 8);
            this.time |= data[1];
            this.startX = data[2];
            this.startY = data[3];
            this.background = data[4];
        }

        public void WriteHeader(byte* data)
        {
            *data = (byte)((int)this.type << 6);
            if (this.preview)
                *data |= 0x20;
            *data |= (byte)((this.time >> 8) & 0x0F);
            data[1] = (byte)this.time;
            data[2] = this.startX;
            data[3] = this.startY;
            data[4] = this.background;
        }

        public void WriteHeader(byte* data, int index)
        {
            data += index;
            *data = (byte)((int)this.type << 6);
            if (this.preview)
                *data |= 0x20;
            *data |= (byte)((this.time >> 8) & 0x0F);
            data[1] = (byte)this.time;
            data[2] = this.startX;
            data[3] = this.startY;
            data[4] = this.background;
        }
    }

    public enum LevelType
    {
        Underwater = 0,
        Ground = 1,
        Underground = 2,
        Castle = 3
    }
}