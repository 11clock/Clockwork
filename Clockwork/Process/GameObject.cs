using Clockwork.DataTypes;
using Clockwork.Input;
using Microsoft.Xna.Framework;

namespace Clockwork.Process
{
	public abstract class GameObject : BaseObject
	{
		public Vector2 Position { get; set; } = Vector2.Zero;

		public float X
		{
			get => Position.X;
			set => Position = new Vector2(value, Position.Y);
		}

		public float Y
		{
			get => Position.Y;
			set => Position = new Vector2(Position.X, value);
		}

		public Image Image { get; } = new Image();

		public Sprite Sprite
		{
			get => Image.Sprite;
			set => Image.Sprite = value;
		}

		public BBox BBox { get; set; }
		public Vector2 TopLeft => BBox != null ? Position - BBox.Origin : Position;
		public Vector2 BottomRight => BBox != null ? Position + BBox.Size - Vector2.One - BBox.Origin : Position;

		internal override void PreUpdate()
		{
			base.PreUpdate();
			Image.UpdateAnimation();
		}

		public override void Draw()
		{
			base.Draw();
			DrawSelf();
		}

		public void DrawSelf()
		{
			Canvas.DrawImage(Image, Position);
		}

		public bool IsPressed(MouseButtons mouseButtons)
		{
			return IsHovered() && VMouse.IsButtonPressed(mouseButtons);
		}

		public bool IsHeld(MouseButtons mouseButtons)
		{
			return IsHovered() && VMouse.IsButtonHeld(mouseButtons);
		}

		public bool IsReleased(MouseButtons mouseButtons)
		{
			return IsHovered() && VMouse.IsButtonReleased(mouseButtons);
		}

		public bool IsHovered()
		{
			if (BBox == null)
				return false;
			Vector2 mousePosition = VMouse.Position;
			return mousePosition.X >= TopLeft.X && mousePosition.X <= BottomRight.X &&
					mousePosition.Y >= TopLeft.Y && mousePosition.Y <= BottomRight.Y;
		}
	}
}