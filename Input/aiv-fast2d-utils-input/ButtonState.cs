using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Fast2D.Utils.Input
{
    internal class ButtonState
    {
        public bool Down { get; private set; }
        public bool Up { get; private set; }
        public bool Pressed { get; private set; }
        public void Update(bool pressed)
        {
            if (pressed)
            {
                this.Down = !this.Pressed;
                this.Pressed = true;
            }
            else
            {
                this.Up = this.Pressed;
                this.Down = false;
                this.Pressed = false;
            }
        }
    }
}
