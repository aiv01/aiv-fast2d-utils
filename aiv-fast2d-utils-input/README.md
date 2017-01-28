# aiv-fast2d-utils-input
The *Input.cs* static class supplies a better input management than *Window.cs* does;
In order to work with this, you have to add Fast2D and OpenTK libraries as dependencies to the project
(you can download them from NuGet or you can find them at these repositories: [Fast2D](https://github.com/aiv01/aiv-fast2d) and [OpenTK](https://github.com/aiv01/opentk)).

#First Setup:
Call Input.Update(Window window) at the start of every frame, providing it the game window:
Window window = new Window("TEST");
while (window.opened)
{
	Input.Update(window);

	//Game

	window.Update();
}

#Keyboards events:
You have three states for each value in *KeyCode* enum:
+ Input.IsKeyDown(KeyCode key);		//return true only the first frame it is pressed
+ Input.IsKeyUp(KeyCode key);		//return true only the first frame it is released
+ Input.IsKeyPressed(KeyCode key);    	//return true if hold, otherwise false

#Mouse events:
You have three states for each value in *MouseButton* enum:
+ Input.IsMouseButtonDown(MouseButton button);		//return true only the first frame it is pressed
+ Input.IsMouseButtonUp(MouseButton button);		//return true only the first frame it is released
+ Input.IsMouseButtonPressed(MouseButton button);    	//return true if hold, otherwise false
You can get MousePosition too;

#Joystick events:
You have three states for each value in *JoystickButton* enum (you have to provide the JoystickIndex):
+ Input.IsJoystickButtonDown(JoystickButton button, JoystickIndex index);	//return true only the first frame it is pressed
+ Input.IsJoystickButtonUp(JoystickButton button, JoystickIndex index);		//return true only the first frame it is released
+ Input.IsJoystickButtonPressed(JoystickButton button, JoystickIndex index);    //return true if hold, otherwise false
You can get the other events of Joystick too;
