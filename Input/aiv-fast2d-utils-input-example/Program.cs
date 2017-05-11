using System;

namespace Aiv.Fast2D.Utils.Input.Example
{
	class Program
	{
		static void Main( string[] args )
		{
			Window window = new Window( 800, 600, "TEST" );
			while ( window.opened )
			{
				Input.Update( window );

				if ( Input.IsKeyDown( KeyCode.Space ) )
				{
					Console.WriteLine( "KEY DOWN" );
				}

				window.Update();
			}
		}
	}
}