using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork.Libraries
{
	public static class Libs
	{
		public static ContentLib<Texture2D> Textures { get; private set; }
		public static ContentLib<SpriteFont> SpriteFonts { get; private set; }

		internal static void Load(ContentManager content)
		{
			content.Unload();
			Textures = new ContentLib<Texture2D>(content);
			SpriteFonts = new ContentLib<SpriteFont>(content);
		}
	}
}