using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aiv.Fast2D;
using Aiv.Fast2D.Utils.Input;

namespace Aiv.Fast2D.Utils.Input.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Window window = new Window(800, 600, "TEST");
            while (window.opened)
            {
                Input.Update(window);

                if ( Input.IsKeyDown( KeyCode.Space ) )
                {
                    Console.WriteLine( "KEY DOWN" );
                }

                window.Update();
            }
        }
    }
}
