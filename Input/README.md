# aiv-fast2d-utils-input
The *Input.cs* static class supplies a better input management than *Window.cs* does;

In order to work with this, you have to add Fast2D and OpenTK libraries as dependencies in your project
(you can download them from NuGet or you can find them at these repositories: [Fast2D](https://github.com/aiv01/aiv-fast2d) and [OpenTK](https://github.com/aiv01/opentk)).

#First Setup:
Call Input.Update(Window window) at the start of every frame, providing it the game window:
```cs
Window window = new Window("TEST");
while (window.opened)
{
	Input.Update(window);

	if ( Input.IsKeyDown( KeyCode.Space ) )
	{
		Console.WriteLine( "KEY DOWN" );
	}

	window.Update();
}
```
#Keyboards events:
You have three states for each value in **_KeyCode_** enum:
+ Input.IsKeyDown(KeyCode key);		_//return true only the first frame it is pressed_
+ Input.IsKeyUp(KeyCode key);		_//return true only the first frame it is released_
+ Input.IsKeyPressed(KeyCode key);    	_//return true if hold, otherwise false_

#Mouse events:
You have three states for each value in **_MouseButton_** enum:
+ Input.IsMouseButtonDown(MouseButton button);          _//return true only the first frame it is pressed_
+ Input.IsMouseButtonUp(MouseButton button);		_//return true only the first frame it is released_
+ Input.IsMouseButtonPressed(MouseButton button);    	_//return true if hold, otherwise false_

You can get MousePosition too;

#Joystick events:
You have three states for each value in **_JoystickButton_** enum (you have to provide the **_JoystickIndex_**):
+ Input.IsJoystickButtonDown(JoystickButton button, JoystickIndex index);	_//return true only the first frame it is pressed_
+ Input.IsJoystickButtonUp(JoystickButton button, JoystickIndex index);		_//return true only the first frame it is released_
+ Input.IsJoystickButtonPressed(JoystickButton button, JoystickIndex index);    _//return true if hold, otherwise false_

You can get the other events of Joystick too;
