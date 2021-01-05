using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Models;
using Tetris.Views;

namespace Tetris.Controllers
{
    class GameController : IController
    {
        private IView View { get; set; }
        public IModel Model { get; set; }
        private Size resultMove;
        public GameController(IView view, IModel model)
        {
            resultMove = Size.Empty;
            View = view;
            Model = model;
            View.Controller = this;
            Model.Controller = this;
            var timer = new Timer { Interval = 50 };
            timer.Tick += Update;
            timer.Start();
        }
        
        public void AddEvent(Keys key, bool down)
        {
            if (down)
            {
                if (key == Keys.P && !Model.Scene.Block.is_halt_mode)
                {
                    Model.Scene.Block.HaltGame();
                    return;
                }
                else if (key == Keys.P && Model.Scene.Block.is_halt_mode)
                {
                    Model.Scene.Block.ContinueGame();
                    return;
                }
                
                if (key == Keys.R)
                {
                    Model.Scene.CreateMap();
                    Model.Scene.Block.ContinueGame();
                    return;
                }

                if (down && (key == Keys.Q || key == Keys.E))
                {
                    Model.Scene.Block.RotateFigure(key);
                    if (Model.Scene.CheckCollisionByDirection(0, 0))
                        if (key == Keys.Q)
                            Model.Scene.Block.RotateFigure(Keys.E);
                        else
                            Model.Scene.Block.RotateFigure(Keys.Q);
                    return;
                }
            }

            if (!Config.KeysStates.ContainsKey(key))
                return;

            if (down && !Model.Scene.IsBlockStoreFull())
            {

                if (!Config.KeysStates[key].Item2)
                {
                    resultMove += Config.KeysStates[key].Item1;
                    Config.KeysStates[key] = Tuple.Create(Config.KeysStates[key].Item1, true);
                }
            }
            else if (!Model.Scene.IsBlockStoreFull())
            {
                if (Config.KeysStates[key].Item2)
                {
                    resultMove -= Config.KeysStates[key].Item1;
                    Config.KeysStates[key] = Tuple.Create(Config.KeysStates[key].Item1, false);
                }
            }
            Model.MoveEntity(resultMove);
        }

        public void Update(object sender, EventArgs e)
        {
            Model.Update();
            View.Update();
        }
    }
}
