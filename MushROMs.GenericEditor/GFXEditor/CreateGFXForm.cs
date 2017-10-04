using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MushROMs.LunarCompress;

namespace MushROMs.GenericEditor.GFXEditor
{
    public partial class CreateGFXForm : Form
    {
        public int NumTiles
        {
            get { return (int)this.nudNumTiles.Value; }
            set { this.nudNumTiles.Value = value; }
        }

        public GraphicsTypes GraphicType
        {
            get
            {
                int value = this.cbxGraphicsType.SelectedIndex + 1;
                if (value < 8)
                    return (GraphicsTypes)value;
                else if (value == 9)
                    return GraphicsTypes.Mode7_8BPP;
                else if (value == 10)
                    return GraphicsTypes.GBA_4BPP;
                else
                    return GraphicsTypes.None;
            }
            set
            {
                if (value == GraphicsTypes.GBA_4BPP)
                    this.cbxGraphicsType.SelectedIndex = 9;
                else if (value == GraphicsTypes.Mode7_8BPP)
                    this.cbxGraphicsType.SelectedIndex = 8;
                else
                {
                    int index = (int)value;
                    if (index >= 1 && index <= 8)
                        this.cbxGraphicsType.SelectedIndex = index - 1;
                    else
                        this.GraphicType = GraphicsTypes.SNES_4BPP;
                }
            }
        }

        public CreateGFXForm()
        {
            InitializeComponent();
        }
    }
}
