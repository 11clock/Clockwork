using System;
using System.Runtime.InteropServices;
using Clockwork.DataTypes;
using Microsoft.Xna.Framework;

namespace Clockwork
{
	public class BBox
	{
		public Vector2 Size { get; set; }
		
		public float Width
		{
			get => Size.X;
			set => Size = new Vector2(value, Size.Y);
		}

		public float Height
		{
			get => Size.Y;
			set => Size = new Vector2(Size.X, value);
		}

		public Vector2 Origin { get; set; }

		public Vector2 TopLeft => -Origin;

		public Vector2 BottomRight => Size - Vector2.One - Origin;
		
		public BBox(Vector2 size, Vector2 origin)
		{
			Size = size;
			Origin = origin;
		}

		public BBox(Vector2 size, Alignment origin)
		{
			Size = size;
			SetOrigin(origin);
		}
		
		public BBox(float width, float height, Vector2 origin)
		{
			Size = new Vector2(width, height);
			Origin = origin;
		}

		public BBox(float width, float height, Alignment origin)
		{
			Size = new Vector2(width, height);
			SetOrigin(origin);
		}

		public void SetOrigin(Alignment origin)
		{
			switch (origin)
			{
				case Alignment.TopLeft:
					Origin = Vector2.Zero;
					break;
				case Alignment.Top:
					Origin = new Vector2(Size.X / 2f, 0f);
					break;
				case Alignment.TopRight:
					Origin = new Vector2(Size.X - 1f, 0f);
					break;
				case Alignment.Left:
					Origin = new Vector2(0f, Size.Y / 2f);
					break;
				case Alignment.Center:
					Origin = new Vector2(Size.X / 2f, Size.Y / 2f);
					break;
				case Alignment.Right:
					Origin = new Vector2(Size.X - 1f, Size.Y / 2f);
					break;
				case Alignment.BottomLeft:
					Origin = new Vector2(0f, Size.Y - 1f);
					break;
				case Alignment.Bottom:
					Origin = new Vector2(Size.X / 2f, Size.Y - 1f);
					break;
				case Alignment.BottomRight:
					Origin = new Vector2(Size.X - 1f, Size.Y - 1f);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(origin), origin, null);
			}
		}

		public bool CollidesWith(Vector2 position, BBox other, Vector2 otherPosition)
		{
			Vector2 l1 = position + TopLeft;
			Vector2 r1 = position + BottomRight;
			Vector2 l2 = otherPosition + other.TopLeft;
			Vector2 r2 = otherPosition + other.BottomRight;

			return DoOverlap(l1, r1, l2, r2);
		}
		
		private bool DoOverlap(Vector2 l1, Vector2 r1, Vector2 l2, Vector2 r2) 
		{ 
			// If one rectangle is on left side of other 
			if (l1.X > r2.X || l2.X > r1.X) 
				return false; 
  
			// If one rectangle is above other 
			if (l1.Y > r2.Y || l2.Y > r1.Y) 
				return false; 
  
			return true; 
		} 
	}
}