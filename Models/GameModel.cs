using System.Drawing;
using System.Windows.Forms;
using Tetris.Controllers;
using Tetris.Enums;

namespace Tetris.Models
{
    class GameModel : IModel
    {
        public IController Controller { get; set; }
        public Scene Scene { get; set; }
        public int PlayerScore { get; set; }
        public int CurTIme { get; set; }
        public int Level { get; set; }
        public int Lines { get; set; }
        public GameModel()
        {
            Level = 1;
            CurTIme = 0;
            Scene = new Scene();
            PlayerScore = 0;
        }

        public void MoveEntity(Size direction)
        {
            Scene.Block.Slide = Config.PossibleDirection[direction];
            if (Scene.IsBlockStoreFull())
                Scene.Block.HaltGame();
        }

        public void Update()
        {
            if (!Scene.CheckCollisionByDirection(1, 0)
                //&& !Scene.CheckCollide()
                && CurTIme % Config.Levels[Level] == 0)
                Scene.Block.MoveEntityToBottom(Config.KeysStates[Keys.S].Item2);
            else if (!Scene.Block.is_halt_mode && CurTIme % Config.Levels[Level] == 0)
            {
                Scene.AddCurrentBlockToStore();
                CalculateScore();
                Scene.Block.GetNextShape();
                Scene.Block.ResetBlock();
            }
            
            if (!Scene.IsBlockTouchBlock() 
                && !Scene.IsBlockStoreFull()
                && CurTIme % 2 == 0)
                Scene.Block.MoveEntity();
            ++CurTIme;
        }

        private void CalculateScore()
        {
            var rowsCount = 0;
            var isComplete = false;
            for (var i = 3; i >= 0; i--)
                if (Scene.Block.Y - i > 0 && Scene.IsCompleteRow(Scene.Block.Y - i))
                {
                    isComplete = true;
                    rowsCount++;
                }
            if ((Lines + rowsCount) / 3 > Lines / 3)
                Level++;
            Lines += rowsCount;
            if (isComplete)
                PlayerScore += Config.LinesToScore[rowsCount];
        }
    }
}
