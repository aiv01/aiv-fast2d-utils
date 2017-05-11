using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Aiv.Fast2D.Utils.Input
{
    public enum MouseButton
    {
        Left,
        Middle,
        Right
    }
    public enum JoystickButton
    {
        A,
        B,
        X,
        Y,
        Up,
        Down,
        Left,
        Right,
        Start,
        Back,
        BigButton,
        ShoulderLeft,
        ShoulderRight,
        LeftStick,
        RightStick
    }
    public enum JoystickIndex
    {
        One,
        Two,
        Three,
        Four
    }
    public static class Input
    {
        private static Dictionary<KeyCode, ButtonState> keys;
        private static Dictionary<MouseButton, ButtonState> mouseButtons;
        private static Dictionary<JoystickButton, List<ButtonState>> joystickButtons;
        private static Window Window { get; set; }
        static Input()
        {
            //Fill dict with Key States
            keys = new Dictionary<KeyCode, ButtonState>();
            foreach (object item in Enum.GetValues(typeof(KeyCode)))
            {
                KeyCode key = (KeyCode)item;
                keys.Add(key, new ButtonState());
            }

            //Fill dict with Mouse States
            mouseButtons = new Dictionary<MouseButton, ButtonState>();
            foreach (object item in Enum.GetValues(typeof(MouseButton)))
            {
                MouseButton button = (MouseButton)item;
                mouseButtons.Add(button, new ButtonState());
            }

            //Fill dict with Joystick States
            joystickButtons = new Dictionary<JoystickButton, List<ButtonState>>();
            foreach (object item in Enum.GetValues(typeof(JoystickButton)))
            {
                JoystickButton button = (JoystickButton)item;
                joystickButtons.Add(button, new List<ButtonState>());
                for (int i = 0; i < 4; i++)
                    joystickButtons[button].Add(new ButtonState());
            }
        }
        public static void Update(Window window)
        {
            Window = window;

            //Keyboard Update
            foreach (var item in keys)
                item.Value.Update(window.GetKey(item.Key));

            //Mouse Update
            mouseButtons[MouseButton.Left].Update(window.mouseLeft);
            mouseButtons[MouseButton.Middle].Update(window.mouseMiddle);
            mouseButtons[MouseButton.Right].Update(window.mouseRight);

            MouseX = window.mouseX;
            MouseY = window.mouseY;
            MousePosition = window.mousePosition;

            //Joystick Update
            for (int i = 0; i < 4; i++)
            {
                //Buttons
                joystickButtons[JoystickButton.A][i].Update(window.JoystickA(i));
                joystickButtons[JoystickButton.B][i].Update(window.JoystickB(i));
                joystickButtons[JoystickButton.X][i].Update(window.JoystickX(i));
                joystickButtons[JoystickButton.Y][i].Update(window.JoystickY(i));

                joystickButtons[JoystickButton.Up][i].Update(window.JoystickUp(i));
                joystickButtons[JoystickButton.Down][i].Update(window.JoystickDown(i));
                joystickButtons[JoystickButton.Left][i].Update(window.JoystickLeft(i));
                joystickButtons[JoystickButton.Right][i].Update(window.JoystickRight(i));

                joystickButtons[JoystickButton.Start][i].Update(window.JoystickStart(i));
                joystickButtons[JoystickButton.Back][i].Update(window.JoystickBack(i));
                joystickButtons[JoystickButton.BigButton][i].Update(window.JoystickBigButton(i));

                joystickButtons[JoystickButton.ShoulderLeft][i].Update(window.JoystickShoulderLeft(i));
                joystickButtons[JoystickButton.ShoulderRight][i].Update(window.JoystickShoulderRight(i));

                joystickButtons[JoystickButton.LeftStick][i].Update(window.JoystickLeftStick(i));
                joystickButtons[JoystickButton.RightStick][i].Update(window.JoystickRightStick(i));

                Joysticks = window.Joysticks;
            }
        }

        //Keyboard Events
        public static bool IsKeyPressed(KeyCode key)
        {
            return keys[key].Pressed;
        }
        public static bool IsKeyUp(KeyCode key)
        {
            return keys[key].Up;
        }
        public static bool IsKeyDown(KeyCode key)
        {
            return keys[key].Down;
        }

        //Mouse Events
        public static float MouseX { get; private set; }
        public static float MouseY { get; private set; }
        public static Vector2 MousePosition { get; private set; }
        public static bool IsMouseButtonDown(MouseButton button)
        {
            return mouseButtons[button].Down;
        }
        public static bool IsMouseButtonUp(MouseButton button)
        {
            return mouseButtons[button].Up;
        }
        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return mouseButtons[button].Pressed;
        }

        //Joystick Events
        public static string[] Joysticks { get; private set; }
        public static bool IsJoystickButtonDown(JoystickButton button, JoystickIndex index)
        {
            return joystickButtons[button][(int)index].Down;
        }
        public static bool IsJoystickButtonUp(JoystickButton button, JoystickIndex index)
        {
            return joystickButtons[button][(int)index].Up;
        }
        public static bool IsJoystickButtonPressed(JoystickButton button, JoystickIndex index)
        {
            return joystickButtons[button][(int)index].Pressed;
        }
        public static void JoystickVibrate(JoystickIndex index, float left, float right)
        {
            Window.JoystickVibrate((int)index, left, right);
        }
        public static string JoystickDebug(JoystickIndex index)
        {
            return Window.JoystickDebug((int)index);
        }
        public static Vector2 JoystickAxisLeft(JoystickIndex index, float threshold = 0.1f)
        {
            return Window.JoystickAxisLeft((int)index, threshold);
        }
        public static Vector2 JoystickAxisLeftRaw(JoystickIndex index)
        {
            return Window.JoystickAxisLeftRaw((int)index);
        }
        public static Vector2 JoystickAxisRight(JoystickIndex index, float threshold = 0.1f)
        {
            return Window.JoystickAxisRight((int)index, threshold);
        }
        public static Vector2 JoystickAxisRightRaw(JoystickIndex index)
        {
            return Window.JoystickAxisRightRaw((int)index);
        }
        public static float JoystickTriggerLeft(JoystickIndex index, float threshold = 0.1f)
        {
            return Window.JoystickTriggerLeft((int)index, threshold);
        }
        public static float JoystickTriggerLeftRaw(JoystickIndex index)
        {
            return Window.JoystickTriggerLeftRaw((int)index);
        }
        public static float JoystickTriggerRight(JoystickIndex index, float threshold = 0.1f)
        {
            return Window.JoystickTriggerRight((int)index, threshold);
        }
        public static float JoystickTriggerRightRaw(JoystickIndex index)
        {
            return Window.JoystickTriggerRightRaw((int)index);
        }
        private class ButtonState
        {
            public bool Down { get; protected set; }
            public bool Up { get; protected set; }
            public bool Pressed { get; protected set; }
            public void Update(bool pressed)
            {
                if (pressed)
                {
                    if (!Pressed)
                    {
                        Down = true;
                    }
                    else
                    {
                        Down = false;
                    }
                    Pressed = true;
                }
                else
                {
                    if (Pressed)
                    {
                        Up = true;
                    }
                    else
                    {
                        Up = false;
                    }
                    Down = false;
                    Pressed = false;
                }
            }
        }
    }
}
