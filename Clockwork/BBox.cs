using System;
using Microsoft.Xna.Framework;

namespace Clockwork
{
	public class BBox
	{
		public Vector2 Size { get; set; }

		public Vector2 Origin { get; set; }

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
	}
}