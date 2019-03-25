using Microsoft.Xna.Framework.Input;

namespace Clockwork.Input
{
	public static class VKeyboard
	{
		private static KeyboardState _oldState;
		private static KeyboardState _newState;

		public static bool IsHeld(Keys key)
		{
			return _newState.IsKeyDown(key);
		}

		public static bool IsPressed(Keys key)
		{
			return _newState.IsKeyDown(key) && _oldState.IsKeyUp(key);
		}

		public static bool IsReleased(Keys key)
		{
			return _newState.IsKeyUp(key) && _oldState.IsKeyDown(key);
		}

		internal static void UpdateState()
		{
			_oldState = _newState;
			_newState = Keyboard.GetState();
		}
	}
}