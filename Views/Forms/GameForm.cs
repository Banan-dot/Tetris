using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Controllers;
using Tetris.Enums;
using Tetris.Views;

namespace Tetris
{
    public partial class GameForm : Form, IView
    {
        public IController Controller { get; set; }
        private readonly int cellSize;
        private Keys key;

        public GameForm()
        {
            cellSize = 25;
            DoubleBuffered = true;
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            key = e.KeyCode;
            base.OnKeyDown(e);
            HandleKey(e.KeyCode, true);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            HandleKey(e.KeyCode, false);
        }

        private void DrawMap(Graphics g) => Controller.Model.Scene.DrawMap(g, cellSize);

        private void DrawGrid(Graphics g) => Controller.Model.Scene.DrawGrid(g, cellSize);

        //private void DrawBottomShape(Graphics g) => Controller.Model.Scene.DrawBottomShape(this, g, cellSize);

        private void HandleKey(Keys key, bool down) => Controller.AddEvent(key, down);

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawTexts(e.Graphics);
            DrawMap(e.Graphics);
            DrawFrame(e.Graphics);
            DrawGrid(e.Graphics);
            //DrawBottomShape(e.Graphics);
            Text = key.ToString();
        }

        private void DrawFrame(Graphics e)
        {
            using (Font font = new Font("Times New Roman", 30, FontStyle.Bold, GraphicsUnit.Pixel))
                e.DrawString("Next:", font, Brushes.Black, new PointF(19*cellSize, 3 * cellSize));
            var shiftY = 7 * cellSize;
            var shiftX = 19 * cellSize;
            var nextShape = Controller.Model.Scene.Block.Next_shape;
            Pen blackPen = new Pen(Color.Black, 5);
            e.DrawRectangle(blackPen, 17 * cellSize, 5 * cellSize, 7 * cellSize, 6 * cellSize );
            for (var i = 0; i < Config.BlocksToDictMatrix[nextShape][0].GetLength(0); i++)
            {
                for (var j = 0; j < Config.BlocksToDictMatrix[nextShape][0].GetLength(1); j++)
                {
                    if (Config.BlocksToDictMatrix[nextShape][0][i, j] != 0 && nextShape == BlocksType.Hero)
                        e.FillRectangle(Config.BlockToBrushColor[nextShape], new Rectangle(shiftX - cellSize / 2 + 1, shiftY + cellSize / 2 + 1, cellSize - 1, cellSize - 1));
                    else if (Config.BlocksToDictMatrix[nextShape][0][i, j] != 0 && nextShape == BlocksType.Smashboy)
                        e.FillRectangle(Config.BlockToBrushColor[nextShape], new Rectangle(shiftX + cellSize / 2 + 1, shiftY + 1, cellSize - 1, cellSize - 1));
                    else if (Config.BlocksToDictMatrix[nextShape][0][i, j] != 0)
                        e.FillRectangle(Config.BlockToBrushColor[nextShape], new Rectangle(shiftX + 1, shiftY + 1, cellSize - 1, cellSize - 1));
                    shiftX += cellSize;
                }
                shiftY += cellSize;
                shiftX = 19 * cellSize;
            }
        }

        private void DrawTexts(Graphics e)
        {
            var isStoreFull = Controller.Model.Scene.IsBlockStoreFull();

            using (Font font = new Font("Times New Roman", 100, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                if (isStoreFull)
                {
                    Controller.Model.Scene.Block.is_halt_mode = true;
                    e.DrawString("Game over", font, Brushes.Black, new PointF(25, 600));
                }
                if (Controller.Model.Scene.Block.is_halt_mode && !isStoreFull)
                    e.DrawString("Pause", font, Brushes.Black, new PointF(100, 400));
            }

            using (Font font = new Font("Times New Roman", 35, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                e.DrawString("Level:" + Controller.Model.Level.ToString(), font, Brushes.Black, new PointF(50, 400));
                e.DrawString("Lines:" + Controller.Model.Lines.ToString(), font, Brushes.Black, new PointF(50, 200));
                e.DrawString("Score:" + Controller.Model.PlayerScore.ToString(), font, Brushes.Black, new PointF(50, 300));
                e.DrawString("Current block:" + Controller.Model.Scene.Block.BlockType.ToString(), font, Brushes.Black, new PointF(50, 500));
            }

            using (Font font = new Font("Times New Roman", 25, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                var shiftY = 50;
                var shiftX = 1000;
                for (var i = 0; i < Controller.Model.Scene.Single_block.GetLength(0); i++)
                {
                    for (var j = 0; j < Controller.Model.Scene.Single_block.GetLength(1); j++)
                    {
                        e.DrawString(string.Format("{0,3}", Controller.Model.Scene.Block_store[i, j]), font,
                            Brushes.Black, new PointF(shiftX, shiftY));
                        shiftX += 50;
                    }
                    shiftY += 25;
                    shiftX = 1000;
                }
            }
        }

        void IView.Update()
        {
            Invalidate();
            Update();
        }
    }
}
