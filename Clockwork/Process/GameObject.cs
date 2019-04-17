using System.Collections.Generic;
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

		public Image Image { get; }
		public Animator Animator { get; }

		public Sprite Sprite
		{
			get => Image.Sprite;
			set => Image.Sprite = value;
		}

		public BBox BBox { get; set; }
		public Vector2 TopLeft => BBox != null ? Position + BBox.TopLeft : Position;
		public Vector2 BottomRight => BBox != null ? Position + BBox.BottomRight : Position;

		public GameObject()
		{
			Image = new Image();
			Animator = new Animator(Image);
		}

		internal override void PreUpdate()
		{
			base.PreUpdate();
			Animator.Update();
		}

		public virtual void OnCollision(GameObject other)
		{
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

		#region Mouse Input

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

		#endregion

		#region Collision With GameObject

		public bool CollidesWith(GameObject other)
		{
			return CollidesWith(Position, other);
		}

		public bool CollidesWith(Vector2 position, GameObject other)
		{
			return BBox.CollidesWith(position, other.BBox, other.Position);
		}

		public bool CollidesWith<T>() where T : GameObject
		{
			return CollidesWith<T>(Position);
		}

		public bool CollidesWith<T>(Vector2 position) where T : GameObject
		{
			List<T> others = Scene.GetObjects<T>();
			foreach (T other in others)
			{
				if (CollidesWith(position, other))
					return true;
			}

			return false;
		}

		#endregion

		#region Collision With Position

		public bool CollidesWith(Vector2 otherPosition)
		{
			return BBox.CollidesWith(Position, otherPosition);
		}

		public bool CollidesWith(Vector2 position, Vector2 otherPosition)
		{
			return BBox.CollidesWith(position, otherPosition);
		}

		#endregion

		#region Get Collided Shortcuts

		public T GetCollided<T>() where T : GameObject
		{
			return Scene.GetCollided<T>(this);
		}

		public List<T> GetCollidedList<T>() where T : GameObject
		{
			return Scene.GetCollidedList<T>(this);
		}

		public T GetCollided<T>(Vector2 position) where T : GameObject
		{
			return Scene.GetCollided<T>(this, position);
		}

		public List<T> GetCollidedList<T>(Vector2 position) where T : GameObject
		{
			return Scene.GetCollidedList<T>(this, position);
		}

		#endregion
	}
}