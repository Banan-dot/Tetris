using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Enums;
using Tetris.Models.Blocks;

namespace Tetris
{
    public static class Config
    {
        public static readonly Dictionary<int, int> LinesToScore = new Dictionary<int, int>()
        {
            [1] = 100,
            [2] = 300,
            [3] = 700,
            [4] = 1500
        };

        public static readonly Dictionary<BlocksType, Brush> BlockToBrushColor = new Dictionary<BlocksType, Brush>()
        {
            [BlocksType.Blue_Ricky] = Brushes.Blue,
            [BlocksType.Cleveland_Z] = Brushes.Green,
            [BlocksType.Hero] = Brushes.LightBlue,
            [BlocksType.Orange_Ricky] = Brushes.Orange,
            [BlocksType.Rhode_Island_Z] = Brushes.Red,
            [BlocksType.Smashboy] = Brushes.Yellow,
            [BlocksType.Teewee] = Brushes.Purple,
        };  

        public static readonly Dictionary<Keys, Tuple<Size, bool>> KeysStates = new Dictionary<Keys, Tuple<Size, bool>>()
        {
            [Keys.A] = Tuple.Create(new Size(-1, 0), false),
            [Keys.D] = Tuple.Create(new Size(1, 0), false),
            [Keys.S] = Tuple.Create(new Size(0, 1), false)
        };

        public static readonly Dictionary<Slides, Keys> SlideToKeys = new Dictionary<Slides, Keys>()
        {
            [Slides.LEFT] = Keys.A,
            [Slides.RIGHT] = Keys.D,
            [Slides.DOWN] = Keys.S,
        };

        public static readonly Dictionary<Slides, Size> SlideToSize = new Dictionary<Slides, Size>()
        {
            [Slides.BOTTOM] = new Size(0, 0),
            [Slides.RIGHT]= new Size(1, 0),
            [Slides.DOWN] = new Size(0, 1),
            [Slides.LEFT] = new Size(-1, 0),
            [Slides.DOWNLEFT] = new Size(-1, 1),
            [Slides.DOWNRIGHT] = new Size(1, 1),
        };

        public static readonly Dictionary<Size, Slides> PossibleDirection = new Dictionary<Size, Slides>
        {
            [new Size(0, 0)] = Slides.BOTTOM,
            [new Size(1, 0)] = Slides.RIGHT,
            [new Size(0, 1)] = Slides.DOWN,
            [new Size(-1, 0)] = Slides.LEFT,
            [new Size(-1, 1)] = Slides.DOWNLEFT,
            [new Size(1, 1)] = Slides.DOWNRIGHT,
        };

        public static Random random = new Random();

        public static readonly Tuple<int, int> MAP_SIZE = Tuple.Create(10, 18);

        public static readonly Tuple<int, int> SPAWN_COORD = Tuple.Create(0, 4);

        public static readonly Dictionary<int, int> Levels = new Dictionary<int, int>()
        {
            [1] = 10,
            [2] = 9,
            [3] = 8,
            [4] = 7,
            [5] = 6,
            [6] = 5,
            [7] = 4,
            [8] = 3,
            [9] = 2,
            [10] = 1,
        };

        public static readonly Dictionary<int, int[,]> OrangeRickyToMatrix = new Dictionary<int, int[,]>()
        {
            [3] = new int[,]{
                { 2, 0 },
                { 2, 0 },
                { 2, 2 },
            },
            [0] = new int[,]{
                { 0, 0, 2 },
                { 2, 2, 2 },
            },
            [1] = new int[,]{
                { 2, 2 },
                { 0, 2 },
                { 0, 2 },
            },
            [2] = new int[,]{
                { 2, 2, 2 },
                { 2, 0, 0 },
            },
        };

        public static readonly Dictionary<int, int[,]> BlueRickyToMatrix = new Dictionary<int, int[,]>()
        {
            [0] = new int[,]{
                { 1, 0, 0 },
                { 1, 1, 1 },
            }, 
            [1] = new int[,]{
                { 0, 1 },
                { 0, 1 },
                { 1, 1 },
            },
            [2] = new int[,]{
                { 1, 1, 1 },
                { 0, 0, 1 },
            },
            [3] = new int[,]{
                { 1, 1 },
                { 1, 0 },
                { 1, 0 },
            },
        };

        public static readonly Dictionary<int, int[,]> CleverToMatrix = new Dictionary<int, int[,]>()
        {
            [0] = new int[,]{
                { 0, 3, 3 },
                { 3, 3, 0 },
            },
            [1] = new int[,]{
                { 3, 0 },
                { 3, 3 },
                { 0, 3 },
            }
        };

        public static readonly Dictionary<int, int[,]> RhobeIslandToMatrix = new Dictionary<int, int[,]>()
        {
            [0] = new int[,]{
                { 4, 4, 0 },
                { 0, 4, 4 }
            },
            [1] = new int[,]{
                { 0, 4 },
                { 4, 4 },
                { 4, 0 },
            },
        };

        public static readonly Dictionary<int, int[,]> SmashboyToMatrix = new Dictionary<int, int[,]>()
        {
            [0] = new int[,]{
                { 5, 5 },
                { 5, 5 },
            }
        };

        public static readonly Dictionary<int, int[,]> HeroToMatrix = new Dictionary<int, int[,]>()
        {
            [0] = new int[,]{
                { 6, 6, 6, 6 },
            },
            [1] = new int[,]{
                { 6 },
                { 6 },
                { 6 },
                { 6 },
            },
        };

        public static readonly Dictionary<int, int[,]> TeeweeToMatrix = new Dictionary<int, int[,]>()
        {
            [2] = new int[,]{
                { 7, 7, 7 },
                { 0, 7, 0 },
                { 0, 0, 0 },
            },
            [3] = new int[,]{
                { 7, 0, 0 },
                { 7, 7, 0 },
                { 7, 0, 0 },
            },
            [0] = new int[,]{
                { 0, 7, 0 },
                { 7, 7, 7 },
                { 0, 0, 0 },
            },
            [1] = new int[,]{
                { 0, 7, 0 },
                { 7, 7, 0 },
                { 0, 7, 0 },
            },
        };

        public static readonly Dictionary<BlocksType, Dictionary<int, int[,]>> BlocksToDictMatrix =
    new Dictionary<BlocksType, Dictionary<int, int[,]>>()
    {
        [BlocksType.Blue_Ricky] = BlueRickyToMatrix,
        [BlocksType.Cleveland_Z] = CleverToMatrix,
        [BlocksType.Hero] = HeroToMatrix,
        [BlocksType.Orange_Ricky] = OrangeRickyToMatrix,
        [BlocksType.Rhode_Island_Z] = RhobeIslandToMatrix,
        [BlocksType.Smashboy] = SmashboyToMatrix,
        [BlocksType.Teewee] = TeeweeToMatrix,
    };
    }
}
