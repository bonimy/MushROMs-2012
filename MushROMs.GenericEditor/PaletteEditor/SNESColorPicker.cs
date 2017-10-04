using System.ComponentModel;
using MushROMs.Controls;

namespace MushROMs.GenericEditor.PaletteEditor
{
    public class SNESColorPicker : ColorPicker
    {
        [DefaultValue(0)]
        public new ushort SelectedColor
        {
            get { return PaletteForm.SystemToSNESColor(base.SelectedColor); }
            set { base.SelectedColor = PaletteForm.SNESToSystemColor(value); }
        }
    }
}