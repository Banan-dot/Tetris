using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tetris.Enums;
using Tetris.Models.Blocks;


namespace Tetris.Models
{
    public class Scene
    {
        public int[,] Single_block;
        public int[,] Block_store;
        private Size resolution = Screen.PrimaryScreen.Bounds.Size;
        private readonly int drawShiftX;
        private readonly int drawShiftY;
        public Scene()
        {
            drawShiftY = resolution.Height / 4 - 100;
            drawShiftX = resolution.Width / 2 - 100;
            CreateMap();
        }
        public Block Block { get; set; } 

        public void CreateMap()
        {
            Block = new Block();
            Single_block = new int[Config.MAP_SIZE.Item2, Config.MAP_SIZE.Item1];
            Block_store = new int[Config.MAP_SIZE.Item2, Config.MAP_SIZE.Item1];

            for (var x = 0; x < Config.MAP_SIZE.Item1; x++)
            {
                for (var y = 0; y < Config.MAP_SIZE.Item2; y++)
                {
                    Single_block[y, x] = 0;
                    Block_store[y, x] = 0;
                }
            }
        }

        public void SetSingleBlockElement(int col, int row, int shape_value)
        {
            if (row >= 0 && row < Config.MAP_SIZE.Item2 && col >= 0 && col < Config.MAP_SIZE.Item1)
                Single_block[row, col] = shape_value;
        }

        public int GetBlockStoreMatrixElement(int row, int col)
            => row >= 0 && row < Config.MAP_SIZE.Item2 && col >= 0 && col < Config.MAP_SIZE.Item1
                ? Block_store[row, col] 
                : -1;

        public bool IsBlockStoreFull()
            => Enumerable
                .Range(0, Config.MAP_SIZE.Item1)
                .Any(x => Block_store[0, x] > 0);

        public void ShiftBlocksDown(int row)
        {
            for (var k = row; k > 0; k--)
            {
                for (var j = 0; j < Config.MAP_SIZE.Item1; j++)
                    Block_store[k, j] = 0;
                for (var j = 0; j < Config.MAP_SIZE.Item1; j++)
                    Block_store[k, j] = Block_store[k - 1, j];
            }
        }

        public bool IsCompleteRow(int row)
        {
            var isComplete = !Enumerable
                 .Range(0, Config.MAP_SIZE.Item1)
                 .Any(x => Block_store[row, x] == 0);
            if (isComplete)
                ShiftBlocksDown(row);
            return isComplete;
        }

        public void AddCurrentBlockToStore()
        {
            for (var y = 0; y < Config.MAP_SIZE.Item2; y++)
                for (var x = 0; x < Config.MAP_SIZE.Item1; x++)
                    Block_store[y, x] += Single_block[y, x];
        }

        public void UpdateSingleBlockMatrix(Block block)
		{
			var row = block.Y;
			var col = block.X;
			var rotation = block.Rotation;
			var shape = block.BlockType;
			ClearSingleBLockMatrix();
			SetSingleBlockElements(shape, rotation, row, col);
		}

        public void DrawMap(Graphics g, int size)
        {
            UpdateSingleBlockMatrix(Block);
            for (var y = 0; y < Config.MAP_SIZE.Item2; y++)
                for (var x = 0; x < Config.MAP_SIZE.Item1; x++)
                    if (Single_block[y, x] != 0)
                        g.FillRectangle(Config.BlockToBrushColor[(BlocksType)Single_block[y, x]],
                            new Rectangle(drawShiftX + x * size + 1, drawShiftY + y * size + 1, size - 1, size - 1));
                    else if (Block_store[y, x] != 0)
                        try
                        {
                            g.FillRectangle(Config.BlockToBrushColor[(BlocksType)Block_store[y, x]],
                                new Rectangle(drawShiftX + x * size + 1, drawShiftY + y * size + 1, size - 1, size - 1));
                        }
                        catch
                        {
                            return;
                        };
        }

        public void DrawGrid(Graphics g, int cellSize)
        {
            for (var x = 0; x <= Config.MAP_SIZE.Item1; x++)
                g.DrawLine(Pens.Black, new Point(drawShiftX + x * cellSize, drawShiftY), 
                    new Point(drawShiftX + x * cellSize, drawShiftY + Config.MAP_SIZE.Item2 * cellSize));
            for (var y = 0; y <= Config.MAP_SIZE.Item2; y++)
                g.DrawLine(Pens.Black, new Point(drawShiftX, drawShiftY + y * cellSize), 
                    new Point(drawShiftX + Config.MAP_SIZE.Item1 * cellSize,  drawShiftY + y * cellSize));
        }

        //public void DrawBottomShape(GameForm gameForm, Graphics g, int size)
        //{
        //    var rotation = GetCorrectRotation(Block.BlockType, Block.Rotation);
        //    var blockMatrix = Config.BlocksToDictMatrix[Block.BlockType][rotation];
        //    var sizeColMatrix = blockMatrix.GetLength(1);
        //    var sizeRowMatrix = blockMatrix.GetLength(0);
        //    var flag = false;
        //    var mindy = 18;
        //    var minY = 18;
        //    for (var y = Block.Y + 1; y < Config.MAP_SIZE.Item2; y++)
        //    {
        //        for (var x = Block.X - 1; x < Block.X + sizeColMatrix - 1; x++)
        //        {
        //            if (Block_store[y, x] != 0)
        //            {
                        
        //            }
        //        }
        //        if (flag)
        //            break;
        //    }
        //    if (flag)
        //        DrawFigure(g, blockMatrix, Block.X - 1, 17, size);
        //    else
        //        DrawFigure(g, blockMatrix, Block.X - 1, minY - sizeRowMatrix, size);
        //}

        //private int GetCollideY(int y, int x, int size)
        //    => Enumerable
        //        .Range(0, y - Block.Y - size)
        //        .Where(i => y - i >= 0 && Single_block[y - i, x] != 0)
        //        .Select(i => Tuple.Create(i, y - i))
        //        .OrderBy(tuple => tuple.Item1)
        //        .Select(item => item.Item2)
        //        .FirstOrDefault();
            

        //private void DrawFigure(Graphics g, int[,] matrix, int x, int y, int size)
        //{
        //    for (var _x = x; _x < x + matrix.GetLength(1); _x++)
        //        for (var _y = y; _y < y + matrix.GetLength(0); _y++)
        //            if (matrix[_y - y, _x-x] != 0)
        //                g.FillRectangle(new SolidBrush(Color.FromArgb(64, Color.Black)), 
        //                                new Rectangle (drawShiftX + _x * size + 1, drawShiftY + _y * size + 1, size - 1, size - 1));
        //}

        public bool IsBlockTouchBlock() => CheckCollisionByDirection(Config.SlideToSize[Block.Slide].Height, Config.SlideToSize[Block.Slide].Width);

        public bool CheckCollisionByDirection(int dy, int dx)
        {
            var rotation = GetCorrectRotation(Block.BlockType, Block.Rotation);
            var sizeRowMatrix = Config.BlocksToDictMatrix[Block.BlockType][rotation].GetLength(0);
            var sizeColMatrix = Config.BlocksToDictMatrix[Block.BlockType][rotation].GetLength(1);
            for (var y = Block.Y - sizeRowMatrix + 1; y <= Block.Y; y++)
            {
                for (var x = Block.X - 1; x < Block.X + sizeColMatrix - 1; x++)
                {
                    if (x + dx < 0 || x + dx > Config.MAP_SIZE.Item1 - 1 || y + dy > Config.MAP_SIZE.Item2 - 1)
                        return true;
                    if (y < 0)
                        continue;
                    if (Single_block[y, x] != 0)
                        if (Block_store[y + dy, x + dx] != 0)
                            return true;
                }
            }
            return false;
        }

        private void SetSingleBlockElements(BlocksType blockType, int rotation, int row, int col)
        {
            rotation = GetCorrectRotation(blockType, rotation);
            var intMatrix = Config.BlocksToDictMatrix[blockType][rotation];
            var lenY = intMatrix.GetLength(0);
            var lenX = intMatrix.GetLength(1);
            for (var y = lenY; y > 0; y--)
            {
                var dy = y - lenY;
                for (var x = -1; x < lenX - 1; x++)
                    if (row + dy >= 0 && row + dy < Config.MAP_SIZE.Item2 && col + x >= 0 && col + x < Config.MAP_SIZE.Item1)
                        Single_block[row + dy, col + x] = intMatrix[y - 1, x + 1];
            }
        }

        private int GetCorrectRotation(BlocksType blockType, int rotation)
        {
            switch (blockType)
            {
                case BlocksType.Cleveland_Z:
                case BlocksType.Rhode_Island_Z:
                case BlocksType.Hero:
                    return rotation % 2;
                case BlocksType.Smashboy:
                    return 0;
            }
            return rotation;
        }

        private void ClearSingleBLockMatrix()
        {
            for (var x = 0; x < Config.MAP_SIZE.Item1; x++)
                for (var y = 0; y < Config.MAP_SIZE.Item2; y++)
                    Single_block[y, x] = 0;
        }
    }
}
