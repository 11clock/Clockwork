using Microsoft.Xna.Framework;

namespace Clockwork
{
	public abstract class SpatialObject: GameObject
	{
		public Vector2 Position { get; set; } = Vector2.Zero;
		public Sprite Sprite { get; set; }
		public BBox BBox { get; set; }

		public Vector2 TopLeft => BBox != null ? Position - BBox.Origin : Position;

		public Vector2 BottomRight => BBox != null ? Position + BBox.Size - Vector2.One - BBox.Origin : Position;
		
		public override void Draw()
		{
			base.Draw();
			DrawSelf();
		}

		public void DrawSelf()
		{
			if (Sprite != null)
			{
				Canvas.DrawSprite(Sprite, Position);
			}
		}
		
		public bool IsPressed(MouseButton mouseButton)
		{
			return IsHovered() && VMouse.IsButtonPressed(mouseButton);
		}
		
		public bool IsHeld(MouseButton mouseButton)
		{
			return IsHovered() && VMouse.IsButtonHeld(mouseButton);
		}
		
		public bool IsReleased(MouseButton mouseButton)
		{
			return IsHovered() && VMouse.IsButtonReleased(mouseButton);
		}
		
		public bool IsHovered()
		{
			Vector2 mousePosition = VMouse.Position;
			return mousePosition.X >= TopLeft.X && mousePosition.X <= BottomRight.X &&
			       mousePosition.Y >= TopLeft.Y && mousePosition.Y <= BottomRight.Y;
		}
	}
}