using System;
using System.Collections.Generic;
using OpenTK;

namespace Aiv.Fast2D.Utils.Input
{
	public static class Input
	{
		private const int joysticks = 4;
		private static Window window;
		private static readonly Dictionary<KeyCode, ButtonState> keys;
		private static readonly Dictionary<MouseButton, ButtonState> mouseButtons;
		private static readonly Dictionary<JoystickButton, List<ButtonState>> joystickButtons;

		static Input()
		{
			//Fill dict with Key States
			Input.keys = new Dictionary<KeyCode, ButtonState>();
			foreach ( var item in Enum.GetValues( typeof( KeyCode ) ) )
			{
				KeyCode key = (KeyCode)item;
				Input.keys.Add( key, new ButtonState() );
			}

			//Fill dict with Mouse States
			Input.mouseButtons = new Dictionary<MouseButton, ButtonState>();
			foreach ( var item in Enum.GetValues( typeof( MouseButton ) ) )
			{
				MouseButton button = (MouseButton)item;
				Input.mouseButtons.Add( button, new ButtonState() );
			}

			//Fill dict with Joystick States
			Input.joystickButtons = new Dictionary<JoystickButton, List<ButtonState>>();
			foreach ( var item in Enum.GetValues( typeof( JoystickButton ) ) )
			{
				JoystickButton button = (JoystickButton)item;
				Input.joystickButtons.Add( button, new List<ButtonState>() );
				for ( int i = 0; i < Input.joysticks; i++ )
				{
					Input.joystickButtons[button].Add( new ButtonState() );
				}
			}
		}

		public static void Update( Window window )
		{
			Input.window = window;

			//Keyboard Update
			foreach ( var item in Input.keys )
			{
				item.Value.Update( window.GetKey( item.Key ) );
			}

			//Mouse Update
			Input.mouseButtons[MouseButton.Left].Update( window.mouseLeft );
			Input.mouseButtons[MouseButton.Middle].Update( window.mouseMiddle );
			Input.mouseButtons[MouseButton.Right].Update( window.mouseRight );

			Input.mouseButtons[MouseButton.Button1].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button1 ) );
			Input.mouseButtons[MouseButton.Button2].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button2 ) );
			Input.mouseButtons[MouseButton.Button3].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button3 ) );
			Input.mouseButtons[MouseButton.Button4].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button4 ) );
			Input.mouseButtons[MouseButton.Button5].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button5 ) );
			Input.mouseButtons[MouseButton.Button6].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button6 ) );
			Input.mouseButtons[MouseButton.Button7].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button7 ) );
			Input.mouseButtons[MouseButton.Button8].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button8 ) );
			Input.mouseButtons[MouseButton.Button9].Update( window.context.Mouse.GetState().IsButtonDown( OpenTK.Input.MouseButton.Button9 ) );

			Input.MouseX = window.mouseX;
			Input.MouseY = window.mouseY;
			Input.MousePosition = window.mousePosition;

			//Joystick Update
			for ( int i = 0; i < Input.joysticks; i++ )
			{
				//Buttons
				Input.joystickButtons[JoystickButton.A][i].Update( window.JoystickA( i ) );
				Input.joystickButtons[JoystickButton.B][i].Update( window.JoystickB( i ) );
				Input.joystickButtons[JoystickButton.X][i].Update( window.JoystickX( i ) );
				Input.joystickButtons[JoystickButton.Y][i].Update( window.JoystickY( i ) );

				Input.joystickButtons[JoystickButton.Up][i].Update( window.JoystickUp( i ) );
				Input.joystickButtons[JoystickButton.Down][i].Update( window.JoystickDown( i ) );
				Input.joystickButtons[JoystickButton.Left][i].Update( window.JoystickLeft( i ) );
				Input.joystickButtons[JoystickButton.Right][i].Update( window.JoystickRight( i ) );

				Input.joystickButtons[JoystickButton.Start][i].Update( window.JoystickStart( i ) );
				Input.joystickButtons[JoystickButton.Back][i].Update( window.JoystickBack( i ) );
				Input.joystickButtons[JoystickButton.BigButton][i].Update( window.JoystickBigButton( i ) );

				Input.joystickButtons[JoystickButton.ShoulderLeft][i].Update( window.JoystickShoulderLeft( i ) );
				Input.joystickButtons[JoystickButton.ShoulderRight][i].Update( window.JoystickShoulderRight( i ) );

				Input.joystickButtons[JoystickButton.LeftStick][i].Update( window.JoystickLeftStick( i ) );
				Input.joystickButtons[JoystickButton.RightStick][i].Update( window.JoystickRightStick( i ) );

				Input.Joysticks = window.Joysticks;
			}
		}

		//Keyboard Events
		public static bool IsKeyPressed( KeyCode key )
		{
			return Input.keys[key].Pressed;
		}

		public static bool IsKeyUp( KeyCode key )
		{
			return Input.keys[key].Up;
		}

		public static bool IsKeyDown( KeyCode key )
		{
			return Input.keys[key].Down;
		}

		//Mouse Events
		public static float MouseX { get; private set; }
		public static float MouseY { get; private set; }
		public static Vector2 MousePosition { get; private set; }

		public static bool IsMouseButtonDown( MouseButton button )
		{
			return Input.mouseButtons[button].Down;
		}

		public static bool IsMouseButtonUp( MouseButton button )
		{
			return Input.mouseButtons[button].Up;
		}

		public static bool IsMouseButtonPressed( MouseButton button )
		{
			return Input.mouseButtons[button].Pressed;
		}

		//Joystick Events
		public static string[] Joysticks { get; private set; }

		public static bool IsJoystickButtonDown( JoystickButton button, JoystickIndex index )
		{
			return Input.joystickButtons[button][(int)index].Down;
		}

		public static bool IsJoystickButtonUp( JoystickButton button, JoystickIndex index )
		{
			return Input.joystickButtons[button][(int)index].Up;
		}

		public static bool IsJoystickButtonPressed( JoystickButton button, JoystickIndex index )
		{
			return Input.joystickButtons[button][(int)index].Pressed;
		}

		public static void JoystickVibrate( JoystickIndex index, float left, float right )
		{
			Input.window.JoystickVibrate( (int)index, left, right );
		}

		public static string JoystickDebug( JoystickIndex index )
		{
			return Input.window.JoystickDebug( (int)index );
		}

		public static Vector2 JoystickAxisLeft( JoystickIndex index, float threshold = 0.1f )
		{
			return Input.window.JoystickAxisLeft( (int)index, threshold );
		}

		public static Vector2 JoystickAxisLeftRaw( JoystickIndex index )
		{
			return Input.window.JoystickAxisLeftRaw( (int)index );
		}

		public static Vector2 JoystickAxisRight( JoystickIndex index, float threshold = 0.1f )
		{
			return Input.window.JoystickAxisRight( (int)index, threshold );
		}

		public static Vector2 JoystickAxisRightRaw( JoystickIndex index )
		{
			return Input.window.JoystickAxisRightRaw( (int)index );
		}

		public static float JoystickTriggerLeft( JoystickIndex index, float threshold = 0.1f )
		{
			return Input.window.JoystickTriggerLeft( (int)index, threshold );
		}

		public static float JoystickTriggerLeftRaw( JoystickIndex index )
		{
			return Input.window.JoystickTriggerLeftRaw( (int)index );
		}

		public static float JoystickTriggerRight( JoystickIndex index, float threshold = 0.1f )
		{
			return Input.window.JoystickTriggerRight( (int)index, threshold );
		}

		public static float JoystickTriggerRightRaw( JoystickIndex index )
		{
			return Input.window.JoystickTriggerRightRaw( (int)index );
		}
	}
}