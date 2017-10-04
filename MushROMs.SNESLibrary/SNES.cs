using System.IO;
using System.Windows.Forms;

namespace MushROMs.SNESLibrary
{
    /// <summary>
    /// Specifies constants defining particular SNES frame rates.
    /// </summary>
    public enum FPSModes
    {
        /// <summary>
        /// National Television Systems Committee. For American ROMs. Runs at 60 FPS.
        /// </summary>
        NTSC = 60,
        /// <summary>
        /// Phase Alternating Line. For ROMs not in America. Runs at 50 FPS.
        /// </summary>
        PAL = 50
    }

    /// <summary>
    /// Specifies constant defining a fractional amount of frame reduction the editor's animator should have.
    /// Useful for running on computers with slower processing power.
    /// </summary>
    public enum FrameReductions
    {
        /// <summary>
        /// Do not reduce frame rate.
        /// </summary>
        None = 1,
        /// <summary>
        /// Reduce frame rate by half of its usual value.
        /// </summary>
        Half = 2,
        /// <summary>
        /// Reduce frame rate by a fourth of its usual value.
        /// </summary>
        Fourth = 4,
        /// <summary>
        /// Reduce frame rate by an eighth of its usual value.
        /// </summary>
        Eighth = 8
    }

    /// <summary>
    /// Specifies constants defining which header-type a <see cref="ROM"/> has.
    /// </summary>
    public enum HeaderTypes
    {
        /// <summary>
        /// The <see cref="ROM"/> has no header.
        /// </summary>
        NoHeader = 0,
        /// <summary>
        /// The <see cref="ROM"/> has a 512 byte copier header.
        /// </summary>
        Header = 0x200
    }
}