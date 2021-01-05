using System;
using System.Windows.Forms;
using Tetris.Controllers;
using Tetris.Models;
    
namespace Tetris
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var view = new GameForm() { WindowState = FormWindowState.Maximized };
            var game = new GameModel();
            var controller = new GameController(view, game);
            Application.Run(view);
        }
    }
}
