using Clockwork.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
	public static class Canvas
	{
		internal static SpriteBatch SpriteBatch { get; set; }

		public static void DrawSprite(
			Sprite sprite,
			Vector2 position,
			int subimage = 0,
			Vector2? scale = null,
			float rotation = 0f,
			Color? color = null,
			bool flipX = false,
			bool flipY = false)
		{
			if (sprite == null)
				return;

			int width = sprite.Width;
			int height = sprite.Height;
			int row = (int) ((subimage % sprite.SubimageCount) / (float) sprite.Cols);
			int column = (subimage % sprite.SubimageCount) % sprite.Cols;

			Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

			Vector2 fscale = scale ?? Vector2.One;
			Color fcolor = color ?? Color.White;

			SpriteEffects effects = SpriteEffects.None;
			if (flipX)
			{
				effects = effects | SpriteEffects.FlipHorizontally;
			}

			if (flipY)
			{
				effects = effects | SpriteEffects.FlipVertically;
			}

			SpriteBatch.Draw(
				sprite.Texture,
				position,
				sourceRectangle,
				fcolor,
				-Mathf.Deg2Rad(rotation),
				sprite.Origin,
				fscale,
				effects,
				0f);
		}

		public static void DrawImage(Image image, Vector2 position)
		{
			DrawSprite(image.Sprite, position, Mathf.FloorToInt(image.Subimage), image.Scale, image.Rotation, image.Color, image.FlipX,
				image.FlipY);
		}
	}
}