using System.Drawing;
using Tetris.Controllers;

namespace Tetris.Models
{
    public interface IModel
    {
        IController Controller { get; set; }
        Scene Scene { get; set; }
        int PlayerScore { get; set; }
        int CurTIme { get; set; }
        void Update();
        int Lines { get; set; }
        int Level { get; set; }
        void MoveEntity(Size direction);
    }
}
