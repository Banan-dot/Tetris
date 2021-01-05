using System;
using System.Windows.Forms;
using Tetris.Enums;

namespace Tetris.Models.Blocks
{
    public class Block
    {
        public int Y { get; set; }
        public int X { get; set; }
        public int Rotation { get; set; }
        public BlocksType Next_shape { get; set; }
        public bool is_halt_mode;
        public BlocksType BlockType { get; set; }
        public Slides Slide { get; set; }

        public Block()
        {
            BlockType = GetNewRandShape(); 
            Next_shape = GetNewRandShape();
            Slide = Slides.BOTTOM;
            ResetBlock();
        }

        public void ResetBlock()
        {
            Y = Config.SPAWN_COORD.Item1;
            X = Config.SPAWN_COORD.Item2;
            is_halt_mode = false;
            Rotation = 0;
            BlockType = Next_shape;
            SetNextShape();
        }

        public BlocksType GetNextShape() => Next_shape;

        public Block Clone() => new Block();

        public void MoveEntityToBottom(bool isDownArrow)
        {
            if (!is_halt_mode && !IsCollide() && (!isDownArrow || Slide == Slides.DOWNLEFT || Slide == Slides.DOWNRIGHT))
                Y++;
        }

        public void MoveEntity()
        {
            if (!is_halt_mode)
            {
                X += Config.SlideToSize[Slide].Width;
                Y += Config.SlideToSize[Slide].Height;
            }
        }

        public void RotateFigure(Keys key)
        {
            if (!is_halt_mode)
                if (key == Keys.Q)
                {
                    Rotation = (4 + (Rotation - 1) % 4) % 4; // (b + (a % b)) % b - минимальный положительный остаток
                }
                else if (key == Keys.E)
                {
                    Rotation = (4 + (Rotation + 1) % 4) % 4;
                }
        }

        public void HaltGame() => is_halt_mode = true;
        public void ContinueGame() => is_halt_mode = false;

        public bool IsCollide() => Y >= Config.MAP_SIZE.Item2 - 1;
        public BlocksType GetNewRandShape() => (BlocksType)(Config.random.Next(1, 7));

        private void SetNextShape() => Next_shape = GetNewRandShape();
    }
}
