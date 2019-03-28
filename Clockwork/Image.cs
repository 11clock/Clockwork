using System.Collections.Generic;
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
				Subimage = 0;
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
		public bool FlipX { get; set; }
		public bool FlipY { get; set; }

		private int _subimage;
		public int Subimage
		{
			get => _subimage;
			set => _subimage = Mathf.Wrap(value, 0, Sprite.SubimageCount);
		}

		public int GetRandomSubimage()
		{
			return Sprite != null ? Rng.RandRange(Sprite.SubimageCount) : 0;
		}
	}
}