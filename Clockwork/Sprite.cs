using System;
using Clockwork.DataTypes;
using Clockwork.Libraries;
using Clockwork.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
	public class Sprite
	{
		public string Name { get; private set; }

		public Texture2D Texture { get; private set; }

		public int SubimageCount { get; private set; }

		public int Rows { get; private set; }

		public int Cols { get; private set; }

		public int Width { get; private set; }

		public int Height { get; private set; }
		
		public int HOffset { get; private set; }
		
		public int VOffset { get; private set; }

		public Vector2 Origin { get; set; }

		#region Constructors

		public Sprite(Texture2D texture, Vector2 origin, int width, int height)
		{
			SetProperties(texture, origin, width, height);
		}

		public Sprite(Texture2D texture, Alignment origin, int width, int height)
		{
			SetProperties(texture, origin, width, height);
		}

		public Sprite(string textureName, Vector2 origin, int width, int height)
		{
			SetProperties(textureName, origin, width, height);
		}

		public Sprite(string textureName, Alignment origin, int width, int height)
		{
			SetProperties(textureName, origin, width, height);
		}

		#endregion

		#region Advanced Constructors

		public Sprite(Texture2D texture, Vector2 origin, int width, int height,
			int subimageCount, int subimagesPerRow, int hOffset, int vOffset)
		{
			SetProperties(texture, origin, width, height, subimageCount, subimagesPerRow, hOffset, vOffset);
		}
		
		public Sprite(Texture2D texture, Alignment origin, int width, int height,
			int subimageCount, int subimagesPerRow, int hOffset, int vOffset)
		{
			SetProperties(texture, origin, width, height, subimageCount, subimagesPerRow, hOffset, vOffset);
		}
		
		public Sprite(string textureName, Vector2 origin, int width, int height,
			int subimageCount, int subimagesPerRow, int hOffset, int vOffset)
		{
			SetProperties(textureName, origin, width, height, subimageCount, subimagesPerRow, hOffset, vOffset);
		}
		
		public Sprite(string textureName, Alignment origin, int width, int height,
			int subimageCount, int subimagesPerRow, int hOffset, int vOffset)
		{
			SetProperties(textureName, origin, width, height, subimageCount, subimagesPerRow, hOffset, vOffset);
		}

		#endregion
		
		#region SetProperties

		private void SetProperties(Texture2D texture, Vector2 origin, int width, int height,
			int? subimageCount = null, int? subimagesPerRow = null, int? hOffset = null, int? vOffset = null)
		{
			Texture = texture;
			SetCalculatedProperties(width, height, subimageCount, subimagesPerRow, hOffset, vOffset);
			Origin = origin;
			Name = Libs.Textures.FetchName(texture);
		}

		private void SetProperties(Texture2D texture, Alignment origin, int width, int height,
			int? subimageCount = null, int? subimagesPerRow = null, int? hOffset = null, int? vOffset = null)
		{
			Texture = texture;
			SetCalculatedProperties(width, height, subimageCount, subimagesPerRow, hOffset, vOffset);
			SetOrigin(origin);
			Name = Libs.Textures.FetchName(texture);
		}

		private void SetProperties(string textureName, Vector2 origin, int width, int height,
			int? subimageCount = null, int? subimagesPerRow = null, int? hOffset = null, int? vOffset = null)
		{
			Texture = Libs.Textures.Fetch(textureName);
			SetCalculatedProperties(width, height, subimageCount, subimagesPerRow, hOffset, vOffset);

			Origin = origin;
			Name = textureName;
		}

		private void SetProperties(string textureName, Alignment origin, int width, int height,
			int? subimageCount = null, int? subimagesPerRow = null, int? hOffset = null, int? vOffset = null)
		{
			Texture = Libs.Textures.Fetch(textureName);
			SetCalculatedProperties(width, height, subimageCount, subimagesPerRow, hOffset, vOffset);

			SetOrigin(origin);
			Name = textureName;
		}

		private void SetCalculatedProperties(int width, int height,
			int? subimageCount = null, int? subimagesPerRow = null, int? hOffset = null, int? vOffset = null)
		{
			if (subimageCount != null && subimagesPerRow != null && hOffset != null && vOffset != null)
			{
				SubimageCount = (int) subimageCount;
                Width = Mathf.Max(width, 1);
                Height = Mathf.Max(height, 1);
                Rows = Mathf.CeilToInt(SubimageCount / (float) subimagesPerRow);
                Cols = (int) subimagesPerRow;
                HOffset = (int) hOffset;
                VOffset = (int) vOffset;
			}
			else
			{
				Width = Mathf.Max(width, 1);
                Height = Mathf.Max(height, 1);
                Rows = Mathf.FloorToInt((float) Texture.Height / Height);
                Cols = Mathf.FloorToInt((float) Texture.Width / Width);
                SubimageCount = Rows * Cols;
			}
		}

		#endregion

		public void SetOrigin(Alignment origin)
		{
			Origin = GetSubimagePosition(origin);
		}

		public Vector2 GetFocalPosition(Vector2 drawPosition, Alignment focal)
		{
			return drawPosition - Origin + GetSubimagePosition(focal);
		}

		private Vector2 GetSubimagePosition(Alignment focal)
		{
			Vector2 subimagePosition = Vector2.Zero;
			switch (focal)
			{
				case Alignment.TopLeft:
					break;
				case Alignment.Top:
					subimagePosition = new Vector2(Width / 2f, 0f);
					break;
				case Alignment.TopRight:
					subimagePosition = new Vector2(Width - 1f, 0f);
					break;
				case Alignment.Left:
					subimagePosition = new Vector2(0f, Height / 2f);
					break;
				case Alignment.Center:
					subimagePosition = new Vector2(Width / 2f, Height / 2f);
					break;
				case Alignment.Right:
					subimagePosition = new Vector2(Width - 1f, Height / 2f);
					break;
				case Alignment.BottomLeft:
					subimagePosition = new Vector2(0f, Height - 1f);
					break;
				case Alignment.Bottom:
					subimagePosition = new Vector2(Width / 2f, Height - 1f);
					break;
				case Alignment.BottomRight:
					subimagePosition = new Vector2(Width - 1f, Height - 1f);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(focal), focal, null);
			}

			return subimagePosition;
		}
	}
}