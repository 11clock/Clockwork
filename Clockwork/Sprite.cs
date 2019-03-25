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

		public Vector2 Origin { get; set; }

		#region Constructors

		public Sprite(Texture2D texture, Vector2? origin = null, int width = 0, int height = 0)
		{
			SetProperties(texture, origin, width, height);
		}

		public Sprite(Texture2D texture, Alignment origin, int width = 0, int height = 0)
		{
			SetProperties(texture, origin, width, height);
		}

		public Sprite(string textureName, Vector2? origin = null, int width = 0, int height = 0)
		{
			SetProperties(textureName, origin, width, height);
		}

		public Sprite(string textureName, Alignment origin, int width = 0, int height = 0)
		{
			SetProperties(textureName, origin, width, height);
		}

		#endregion

		#region SetProperties

		public void SetProperties(Texture2D texture, Vector2? origin = null, int width = 0, int height = 0)
		{
			Texture = texture;
			SetCalculatedProperties(width, height);
			Origin = origin ?? Vector2.Zero;
			Name = Libs.Textures.FetchName(texture);
		}

		public void SetProperties(Texture2D texture, Alignment origin, int width = 0, int height = 0)
		{
			Texture = texture;
			SetCalculatedProperties(width, height);
			SetOrigin(origin);
			Name = Libs.Textures.FetchName(texture);
		}

		public void SetProperties(string textureName, Vector2? origin = null, int width = 0, int height = 0)
		{
			Texture = Libs.Textures.Fetch(textureName);
			SetCalculatedProperties(width, height);
			Origin = origin ?? Vector2.Zero;
			Name = textureName;
		}

		public void SetProperties(string textureName, Alignment origin, int width = 0, int height = 0)
		{
			Texture = Libs.Textures.Fetch(textureName);
			SetCalculatedProperties(width, height);
			SetOrigin(origin);
			Name = textureName;
		}

		private void SetCalculatedProperties(int width, int height)
		{
			Width = Mathf.Max(width, 1);
			Height = Mathf.Max(height, 1);
			Rows = Mathf.FloorToInt((float) Texture.Height / Height);
			Cols = Mathf.FloorToInt((float) Texture.Width / Width);
			SubimageCount = Rows * Cols;
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