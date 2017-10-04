using System;
using MushROMs.SNESLibrary;

namespace MushROMs.SMB1.Level
{
    public unsafe sealed partial class LevelObjectData
    {
        private HeaderInfo header;
        private ObjectMap objectMap;
        private Map map;

        public HeaderInfo HeadInfo
        {
            get { return this.header; }
        }
        public ObjectMap ObjectMap
        {
            get { return this.objectMap; }
        }
        public Map Map
        {
            get { return this.map; }
        }

        public LevelObjectData()
        {
            this.header = new HeaderInfo();
            this.objectMap = new ObjectMap();
            this.map = new Map(ObjectMap.MaxScreens * Map.XTilesPerScreen, ObjectMap.Maxheight);
        }

        public LevelObjectData(byte[] data)
        {
            fixed (byte* ptr = data)
                InitializeData(ptr, data.Length);
        }

        public LevelObjectData(byte[] data, int index)
        {
            fixed (byte* ptr = &data[index])
                InitializeData(ptr, data.Length - index);
        }

        private void InitializeData(byte* data, int size)
        {
            if (size <= 5)
                throw new ArgumentException("Data is too small.");

            this.header = new HeaderInfo(data);
            this.objectMap = new ObjectMap(data, HeaderInfo.HeaderSize, size);
            this.map = new Map(ObjectMap.MaxScreens * Map.XTilesPerScreen, ObjectMap.Maxheight);

            WriteMap();
        }

        public void WriteMap()
        {
            ObjectElement.SetMap(ref this.map, ref this.header);

            int count = this.objectMap.ObjectCount;
            fixed (ObjectElement* elements = this.objectMap.ObjectElements)
            fixed (int* z = this.objectMap.ZRelativeIndex)
                for (int i = 0; i < count; i++)
                    elements[z[i]].WriteObject();

            ObjectElement.ReleaseMap();
        }
    }
}