﻿//A rather large list holding static items for the SMB1 Editor.

namespace MushROMs.SMB1
{
    public unsafe partial class LevelElements
    {
        public static byte FlagPoleBall = 0x28;
        public static byte FlagPoleBar = 0x29;
        public static byte FlagPoleBase = 0x64;
        public static byte SpringBoardTop = 0x6F;
        public static byte SpringBoardBottom = 0x70;
        public static byte BowserBridgeTile = 0x8D;
        public static byte CanonHead = 0x6C;
        public static byte CanonNeck = 0x6D;
        public static byte CanonBody = 0x6E;
        public static byte BridgeTile = 0x6B;
        public static byte GroundTop = 0x56;
        public static byte GroundBottom = 0x57;
        public static byte GreenIslandNeckLeft = 0x45;
        public static byte GreenIslandNeckTile = 0x46;
        public static byte GreenIslandNeckRight = 0x47;
        public static byte GreenIslandNeckSingle = 0x48;
        public static byte GreenIslandBodyLeft = 0x49;
        public static byte GreenIslandBodyTile = 0x4A;
        public static byte GreenIslandBodyRight = 0x4B;
        public static byte GreenIslandBodySingle = 0x4C;
        public static byte RedIslandStemNeck = 0x4F;
        public static byte RedIslandStemBody = 0x50;
        public static byte TreeStemTile = 0x4E;
        public static byte SmallTreeHead = 0x12;
        public static byte BigTreeHeadTop = 0x11;
        public static byte BigTreeHeadBottom = 0x13;
        public static byte MaximumSmallCastleSize = 0x05;
        public static byte MaximumBigCastleSize = 0x0B;
        public static byte CastleStairsHeight = 0x03;
        public static byte CastleStairsWidth = 0x06;
        public static byte StandardCoin = 0xE9;
        public static byte WaterCoin = 0xEA;
        public static byte Invisible1UP = 0x63;
        public static byte BigCastleHeight = 0x0B;
        public static byte BigCastleWidth = 0x0A;
        public static byte SmallCastleHeight = 0x05;
        public static byte SmallCastleWidth = 0x06;
        public static byte StaircaseTile = 0x64;
        public static byte StaircaseHeight = 0x08;
        public static byte QuestionBlockRowTile = 0xE7;
        public static byte VerticalLiftRopeTile = 0x40;
        public static byte VerticalLiftRopeAir = 0x44;
        public static byte CeilingCapTile = 0x67;
        public static byte[] SingleTileObject = { 0xE8, 0xE7, 0x62, 0xFC, 0x10, 0xFD, 0x63, 0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F, 0x60, 0x61 };
        public static byte[] LPipe = { 0x00, 0x00, 0x20, 0x23, 0x00, 0x00, 0x21, 0x24, 0x14, 0x18, 0x22, 0x25, 0x15, 0x19, 0x19, 0x19 };
        public static byte[] Pipes = { 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x18, 0x19 };
        public static byte[] HorizontalObjects = { 0x0D, 0x05, 0x41, 0x0E, 0x04, 0x42, 0x0F, 0x06, 0x43 };
        public static byte[] Rectangular = { 0x51, 0x64, 0x8C, 0x4D, 0x71, 0x26, 0x6A, 0x8A, 0x8B, 0x57, 0x73, 0x69, 0x87, 0x88, 0x56, 0x72, 0x68 };
        public static byte[] IslandCap = { 0x1B, 0x1E, 0x1A, 0x1D, 0x1C, 0x1F };
        public static byte[] GroundObjects = { 0x56, 0x68, 0x57, 0x69, 0x07, 0xF9, 0x08, 0xEE, 0x0A, 0xFA, 0x0B, 0xF8, 0x09, 0xF8, 0x0C, 0xFF, 0x65, 0x66 };
        public static byte[] LongLPipe = { 0x00, 0x00, 0x18, 0x19, 0x20, 0x21, 0x22, 0x19, 0x23, 0x24, 0x25, 0x19 };
        public static byte[] CastleStairs = { 0xF5, 0xF6, 0xF6, 0x00, 0x00, 0xF3, 0xF4, 0xF6 };
        public static byte[] Castle = { 0x00, 0x00, 0x00, 0x00, 0xE4, 0xC0, 0xD0, 0xD5, 0xD5, 0xD5, 0xD5, 0x00, 0x00,
                                        0x00, 0x00, 0xE5, 0xC1, 0xD1, 0xD6, 0xD6, 0xD6, 0xD6, 0x00, 0x00, 0xE0, 0xD0,
                                        0xD5, 0xC2, 0xD2, 0xD7, 0xD7, 0xCE, 0xDC, 0xDE, 0xD0, 0xE1, 0xD1, 0xD6, 0xC3,
                                        0xCC, 0xDC, 0xD7, 0xCF, 0xDD, 0xC5, 0xDA, 0xC4, 0xCC, 0xDC, 0xC4, 0xCD, 0xDD,
                                        0xD7, 0xCE, 0xDC, 0xC6, 0xDB, 0xC7, 0x7D, 0x7B, 0xC7, 0xCC, 0xDC, 0xD7, 0x7F,
                                        0x7B, 0xDF, 0xD4, 0xE2, 0x78, 0x79, 0xC8, 0xCD, 0xDD, 0xD7, 0x7E, 0x7A, 0x00,
                                        0x00, 0xE3, 0xD4, 0xD9, 0xC9, 0xD2, 0xD7, 0xD7, 0xCF, 0xDD, 0x00, 0x00, 0x00,
                                        0x00, 0x00, 0xCA, 0xD3, 0xD8, 0xD8, 0xD8, 0xD8, 0x00, 0x00, 0x00, 0x00, 0xE6,
                                        0xCB, 0xD4, 0xD9, 0xD9, 0xD9, 0xD9 };
    }

    public unsafe partial class ObjectSelector
    {
        private static string[][] ListItems = {
            new string[]{   //Standard Objects
                "Question block with Mushroom",
                "Question block with Coin",
                "Invisible block with Coin",
                "Used block",
                "Rope for axe",
                "Axe",
                "Brick with Mushroom",
                "Brick with Vine",
                "Brick with Star",
                "Brick with 10 coins",
                "Brick with 1UP",
                "Invisible block with 1UP",
                "Reverse L-Pipe",
                "Flag Pole",
                "Spring Board"
            },
            new string[]{
                "Screen 0x00",
                "Screen 0x01",
                "Screen 0x02",
                "Screen 0x03",
                "Screen 0x04",
                "Screen 0x05",
                "Screen 0x06",
                "Screen 0x07",
                "Screen 0x08",
                "Screen 0x09",
                "Screen 0x0A",
                "Screen 0x0B",
                "Screen 0x0C",
                "Screen 0x0D",
                "Screen 0x0E",
                "Screen 0x0F",
            },
            new string[]{   //Expandable objects (Linear)
                "Canon",
                "Enterable Pipe",
                "Unenterable Pipe",
                "Bridge",
                "Grass (Scenery)",
                "Balance Rope"
            },
            new string[]{   //Expandable objects (Rectangular)
                "Breakable Brick",
                "Solid Block",
                "Cloud Ground",
                "Fence (Scenery)",
                "Solid Sea Block",
                "Solid Sea Vine",
                "Solid Castle Block",
                "Coin",
                "Water",
                "Lava",
                "Ground",
                "Sea Ground",
                "Castle Floor",
                "Green Tree Island",
                "Mushroom Island",
                "Long Length Ground"
            },
            new string[]{   //Ground objects
                "Ground with left ledge",
                "Ground with right ledge",
                "Ground with left and right ledges",
                "Castle floor with left ledge",
                "Castle floor with right ledge",
                "Castle floor with left and right ledges",
                "Castle ceiling"
            },
            new string[]{   //Misc. objects
                "Row of question blocks",
                "Bowser Bridge",
                "Ceiling Cap",
                "Lift\'s Vertical Rope",
                "Long Reverse L-Pipe",
                "Staircase"
            },
            new string[]{   //Castle tileset objects
                "Left castle floor edge with bottom",
                "Right castle floor edge with bottom",
                "Left castle edge with just bottom",
                "Right castle edge with just bottom",
                "Single with edge with bottom on both sides",
                "Single with edge with bottom on left side",
                "Single with edge with bottom on right side",
                "Single with edge with just bottom on both sides",
                "Single with edge with just bottom on left side",
                "Single with edge with just bottom on right side"
            },
            new string[]{   //Scenery Objects
                "Small Tree (Scenery)",
                "Big Tree (Scenery)",
                "Small Castle",
                "Big Castle",
                "Descending Stairs"
            },
            new string[]{   //Sprite commands
                "Scroll Stop",
                "Scroll Stop (Warp Zone)",
                "Cheep Cheep Generator",
                "Bullet Bill Generator",
                "Bowser\'s Fire Generator"
            }};
    }
}