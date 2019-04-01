using System.Collections.Generic;
using System.Linq;
using Clockwork.Utils;

namespace Clockwork
{
	// Based on HaxeFlixel's animation system 
	public class Animator
	{
		public const string DefaultAllName = "all";
		
		public string Name
		{
			get => _currentAnimation?.Name;
			set => Play(Name);
		}

		public bool Paused
		{
			get => _currentAnimation?.Paused ?? false;
			set
			{
				if (_currentAnimation != null)
				{
					if (value)
					{
						_currentAnimation.Pause();
					}
					else
					{
						_currentAnimation.Resume();
					}
				}
			}
		}

		public bool Finished
		{
			get => _currentAnimation?.Finished ?? true;
			set {
				if (value)
				{
					_currentAnimation?.Finish();
				}
			}
		}

		public float SpeedScale { get; set; } = 1f;

		private Dictionary<string, Animation> _animations;

		public Image Image { get; }
		private Animation _currentAnimation;

		public Animator(Image image)
		{
			Image = image;
			_animations = new Dictionary<string, Animation>();
		}

		internal void Update()
		{
			_currentAnimation?.Update(Time.Delta * SpeedScale);
		}

		public void ClearAnimations()
		{
			_animations.Clear();
		}

		public void Add(string name, int[] subimages, float fps, bool looped = true, Sprite sprite = null)
		{
			if (subimages.Length > 0)
			{
				Animation animation = new Animation(this, name, subimages.ToArray(), fps, looped, sprite);
				_animations[name] = animation;
			}
		}
		
		public void Add(string name, int subimage, bool looped = true, Sprite sprite = null)
		{
			Add(name, new []{subimage}, 0f, looped, sprite);
		}
		
		public void AddRange(string name, int firstSubimage, int lastSubimage, float fps, bool looped = true, Sprite sprite = null)
		{
			Add(name, CommonUtils.Sequence(firstSubimage, lastSubimage).ToArray(), fps, looped, sprite);
		}

		public void AddAll(string name, float fps, bool looped = true, Sprite sprite = null)
		{
			if (sprite == null)
			{
				AddRange(name, 0, Image.Sprite.SubimageCount - 1, fps, looped);
			}
			else
			{
				AddRange(name, 0, sprite.SubimageCount - 1, fps, looped, sprite);
			}
		}

		public void Remove(string name)
		{
			if (_animations.ContainsKey(name))
			{
				_animations.Remove(name);
			}
		}

		public void Play(string name, bool forceRestart = false, bool reversed = false, int startingFrame = 0)
		{
			if (name == null)
			{
				if (_currentAnimation != null)
				{
					_currentAnimation.Stop();
				}
				_currentAnimation = null;
			}

			if (name == null || !_animations.ContainsKey(name))
			{
				return;
			}

			if (_currentAnimation != null && name != _currentAnimation.Name)
			{
				_currentAnimation.Stop();
			}

			_currentAnimation = _animations[name];
			_currentAnimation.Play(forceRestart, reversed, startingFrame);
		}

		public void PlayAll(float fps, bool looped = true, Sprite sprite = null, bool forceRestart = false, bool reversed = false, int startingFrame = 0)
		{
			Remove(DefaultAllName);
			AddAll(DefaultAllName, fps, looped, sprite);
			Play(DefaultAllName, forceRestart, reversed, startingFrame);
		}

		public void Reset()
		{
			_currentAnimation?.Reset();
		}

		public void Finish()
		{
			_currentAnimation?.Finish();
		}

		public void Stop()
		{
			_currentAnimation?.Stop();
		}

		public void Pause()
		{
			_currentAnimation?.Pause();
		}

		public void Resume()
		{
			_currentAnimation?.Resume();
		}

		public void Reverse()
		{
			_currentAnimation?.Reverse();
		}

		public Animation GetByName(string name)
		{
			return _animations.ContainsKey(name) ? _animations[name] : null;
		}
		
		
	}
}