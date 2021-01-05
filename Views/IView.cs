using Tetris.Controllers;

namespace Tetris.Views
{
    interface IView
    {
        IController Controller { get; set; }
        void Update();
    }
}
