using System;
using System.Windows.Forms;
using Tetris.Models;

namespace Tetris.Controllers
{
    public interface IController
    {
        IModel Model { get; set; }
        void AddEvent(Keys key, bool down);
        void Update(object sender, EventArgs e);
    }
}
