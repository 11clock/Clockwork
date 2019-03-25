using Clockwork.Utils;
using Microsoft.Xna.Framework;

namespace Clockwork
{
	public class Image
	{
		private Sprite _sprite;

		public Sprite Sprite
		{
			get => _sprite;
			set
			{
				_sprite = value;
				Subimage = 0f;
			}
		}

		private float _rotation;
		public float Rotation
		{
			get => _rotation;
			set => _rotation = Mathf.Wrap(value, 0f, 360f);
		}

		public Vector2 Scale { get; set; } = Vector2.One;
		public Color Color { get; set; } = Color.White;
		public float Fps { get; set; } = 15f;
		public bool FlipX { get; set; }
		public bool FlipY { get; set; }

		private float _subimage;

		public float Subimage
		{
			get => _subimage;
			set => _subimage = Mathf.Wrap(value, 0f, Sprite?.SubimageCount ?? 0f);
		}
		
		internal void UpdateAnimation()
		{
			if (_sprite == null)
				return;
			if (Fps == 0f)
				return;

			Subimage += Fps * Time.Delta;
		}
	}
}