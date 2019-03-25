using System;
using Clockwork.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Clockwork.Input
{
	public static class VMouse
	{
		private static MouseState _oldState;
		private static MouseState _newState;

		public static Vector2 Position { get; private set; }

		public static bool IsButtonHeld(MouseButtons button)
		{
			switch (button)
			{
				case MouseButtons.Left:
					return _newState.LeftButton == ButtonState.Pressed;
				case MouseButtons.Right:
					return _newState.RightButton == ButtonState.Pressed;
				default:
					throw new ArgumentOutOfRangeException(nameof(button), button, null);
			}
		}

		public static bool IsButtonPressed(MouseButtons button)
		{
			switch (button)
			{
				case MouseButtons.Left:
					return _newState.LeftButton == ButtonState.Pressed && _oldState.LeftButton == ButtonState.Released;
				case MouseButtons.Right:
					return _newState.RightButton == ButtonState.Pressed &&
							_oldState.RightButton == ButtonState.Released;
				default:
					throw new ArgumentOutOfRangeException(nameof(button), button, null);
			}
		}

		public static bool IsButtonReleased(MouseButtons button)
		{
			switch (button)
			{
				case MouseButtons.Left:
					return _newState.LeftButton == ButtonState.Released && _oldState.LeftButton == ButtonState.Pressed;
				case MouseButtons.Right:
					return _newState.RightButton == ButtonState.Released &&
							_oldState.RightButton == ButtonState.Pressed;
				default:
					throw new ArgumentOutOfRangeException(nameof(button), button, null);
			}
		}

		internal static void UpdateState()
		{
			_oldState = _newState;
			_newState = Mouse.GetState();
			Vector2 mousePosition = new Vector2(_newState.X, _newState.Y);
			Vector2 virtualMousePosition = Vector2.Transform(
				mousePosition - new Vector2(Resolution.Viewport.X, Resolution.Viewport.Y),
				Matrix.Invert(Resolution.GetTransformationMatrix()));
			virtualMousePosition =
				new Vector2(Mathf.Floor(virtualMousePosition.X), Mathf.Floor(virtualMousePosition.Y));
			Position = virtualMousePosition;
		}
	}
}