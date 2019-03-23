using System;
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
		
		public int Frame { get; set; }
		public int TotalFrames { get; private set; }
		
		public int Rows { get; private set; }
		
		public int Cols { get; private set; }
		
		public int Width { get; private set; }
		
		public int Height { get; private set; }
		
		public Vector2 Origin { get; set; }

		public Color Blend { get; set; } = Color.White;
		
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
			Width = width;
            Height = height;
            Rows = Mathf.FloorToInt((float)Texture.Height / Height);
            Cols = Mathf.FloorToInt((float)Texture.Width / Width);
            TotalFrames = Rows * Cols;
            Frame = 0;
		}
		#endregion
		
		public void SetOrigin(Alignment origin)
		{
			Origin = GetFramePosition(origin);
		}

		public void SetRandomFrame()
		{
			Frame = Rng.RandRange(TotalFrames);
		}

		public Vector2 GetFocalPosition(Vector2 drawPosition, Alignment focal)
		{
			return drawPosition - Origin + GetFramePosition(focal);
		}

		private Vector2 GetFramePosition(Alignment focal)
		{
			Vector2 framePosition = Vector2.Zero;
			switch (focal)
			{
				case Alignment.TopLeft:
					break;
				case Alignment.Top:
					framePosition = new Vector2(Width / 2f, 0f);
					break;
				case Alignment.TopRight:
					framePosition = new Vector2(Width - 1f, 0f);
					break;
				case Alignment.Left:
					framePosition = new Vector2(0f, Height / 2f);
					break;
				case Alignment.Center:
					framePosition = new Vector2(Width / 2f, Height / 2f);
					break;
				case Alignment.Right:
					framePosition = new Vector2(Width - 1f, Height / 2f);
					break;
				case Alignment.BottomLeft:
					framePosition = new Vector2(0f, Height - 1f);
					break;
				case Alignment.Bottom:
					framePosition = new Vector2(Width / 2f, Height - 1f);
					break;
				case Alignment.BottomRight:
					framePosition = new Vector2(Width - 1f, Height - 1f);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(focal), focal, null);
			}

			return framePosition;
		}
	}
}