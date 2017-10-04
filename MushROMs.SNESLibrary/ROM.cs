using System;
using MushROMs.LunarCompress;

namespace MushROMs.SNESLibrary
{
    /// <summary>
    /// Represents binary data as an array of banks organized by a specified format.
    /// </summary>
    public unsafe class ROM
    {
        /// <summary>
        /// Specifies the size, in bytes, of a header. This field is constant.
        /// </summary>
        public const int HeaderSize = 0x200;

        /// <summary>
        /// Specifies the size, in bytes, of a bank for a <see cref="ROMTypes.LoROM"/> formatted <see cref="ROM"/>.
        /// This field is constant.
        /// </summary>
        public const int LoBankSize = 0x8000;
        /// <summary>
        /// Specifies the size, in bytes, of a bank for a <see cref="ROMTypes.HiROM"/> formatted <see cref="ROM"/>.
        /// This field is constant.
        /// </summary>
        public const int HiBankSize = 0x10000;

        /* Do not implement until necessary
        private const string ErrorNegBanks = "Number of banks must be greater than zero.";
        private const string ErrorMaxBanks = "Number of banks cannot exceed 0x80.";
        private const string ErrorFormatUnknown = "The given ROM type format is either unsupported or invalid.";



        private readonly int handle;
        private int numBanks;
        private int bankSize;
        private ROMTypes romType;

        private byte* pcData;
        private byte** data;

        /// <summary>
        /// Gets the number of banks of the current <see cref="ROM"/>.
        /// </summary>
        public int NumBanks
        {
            get { return this.numBanks; }
        }

        /// <summary>
        /// Gets the size, in bytes, of the current <see cref="ROM"/>.
        /// </summary>
        public int BankSize
        {
            get { return this.bankSize; }
        }

        public ROMTypes ROMType
        {
            get { return this.romType; }
        }

        public byte* PCData
        {
            get { return this.pcData; }
        }

        public byte** Data
        {
            get { return this.data; }
        }

        public ROM(int numBanks, ROMTypes romType)
        {
            if (numBanks <= 0)
                throw new ArgumentOutOfRangeException(ErrorNegBanks);
            if (numBanks > 0x80)
                throw new ArgumentOutOfRangeException(ErrorMaxBanks);

            switch (romType)
            {
                case ROMTypes.LoROM:
                    this.bankSize = LoBankSize;
                    break;
                case ROMTypes.HiROM:
                    this.bankSize = HiBankSize;
                    break;
                default:
                    throw new ArgumentException(ErrorFormatUnknown);
            }

            this.numBanks = numBanks;
            this.romType = romType;

            this.handle = SNES.CreateNewROM(numBanks, (int)romType);
            this.data = SNES.GetROM(this.handle);
            this.pcData = *this.data;

            for (int i = this.numBanks * this.bankSize; --i >= 0; )
                this.pcData[i] = 0;
        }
         * */
    }
}