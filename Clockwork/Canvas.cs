using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
	public static class Canvas
	{
		
		internal static SpriteBatch SpriteBatch { get; set; }

		public static void DrawSprite(Sprite sprite, Vector2 position)
		{
			int width = sprite.Width;
			int height = sprite.Height;
            int row = (int)(sprite.Frame / (float)sprite.Cols);
            int column = sprite.Frame % sprite.Cols;
 
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X - (int)sprite.Origin.X, (int)position.Y - (int)sprite.Origin.Y, width, height);
            
            SpriteBatch.Draw(sprite.Texture, destinationRectangle, sourceRectangle, sprite.Blend);
		}
	}
}