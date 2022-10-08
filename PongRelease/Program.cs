using KeyboardMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongRelease
{
    class Program
    {
        static void Main(string[] args)
        {
            UI.DisableCursor();
            Game myGame = new Game();
            myGame.Start();
        }
    }
}
