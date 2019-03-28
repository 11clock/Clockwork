namespace Clockwork
{
	public class Animation
	{
		public Animator Parent { get; }
		public string Name { get; }

		public int CurrentSubimage
		{
			get => _currentSubimage;
			set
			{
				_currentSubimage = value;
				if (Parent != null)
				{
					Parent.Image.Subimage = value;
				}
			}
		}

		public float Fps
		{
			get => _fps;
			set
			{
				Delay = 0;
				_fps = value;
				if (value > 0)
					Delay = 1f / value;
			}
		}

		public int CurrentFrame
		{
			get => _currentFrame;
			set
			{
				int lastFrame = FrameCount - 1;
				int tempFrame = Reversed ? lastFrame - value : value;

				if (tempFrame >= 0)
				{
					if (!Looped && value > lastFrame)
					{
						Finished = true;
						_currentFrame = Reversed ? 0 : lastFrame;
					}
					else
					{
						_currentFrame = value;
					}
				}
				else
				{
					_currentFrame = 0;
				}

				CurrentSubimage = Subimages[CurrentFrame];

				if (Finished && Parent != null)
				{
					// TODO - Fire finish callback
				}
			}
		}

		public int FrameCount => Subimages.Length;
		public float Delay { get; private set; }
		public bool Finished { get; private set; } = true;
		public bool Paused { get; set; } = true;
		public bool Looped { get; }
		public bool Reversed { get; private set; }
		public int[] Subimages { get; }

		private float _frameTimer;
		private float _fps;
		private int _currentFrame;
		private int _currentSubimage;

		public Animation(Animator parent, string name, int[] subimages, float fps, bool looped = true,
			bool flipX = false, bool flipY = false)
		{
			Parent = parent;
			Name = name;
			
			Fps = fps;
			Subimages = subimages;
			Looped = looped;
		}

		public void Play(bool forceRestart = false, bool reversed = false, int startingFrame = 0)
		{
			if (!forceRestart && !Finished && Reversed == reversed)
			{
				Paused = false;
			}

			Reversed = reversed;
			Paused = false;
			_frameTimer = 0f;
			Finished = Delay == 0f;

			int lastFrame = FrameCount - 1;
			if (startingFrame < 0)
				startingFrame = 0;
			if (startingFrame > lastFrame)
				startingFrame = lastFrame;
			if (reversed)
				startingFrame = lastFrame - startingFrame;
			CurrentFrame = startingFrame;

			if (Finished)
			{
				// TODO - Fire finish callback
			}
		}

		public void Restart()
		{
			Play(true, Reversed);
		}

		public void Stop()
		{
			Finished = true;
			Paused = true;
		}

		public void Reset()
		{
			Stop();
			CurrentFrame = Reversed ? FrameCount - 1 : 0;
		}

		public void Finish()
		{
			Stop();
			CurrentFrame = Reversed ? 0 : FrameCount - 1;
		}

		public void Pause()
		{
			Paused = true;
		}

		public void Resume()
		{
			Paused = false;
		}

		public void Reverse()
		{
			Reversed = !Reversed;
			if (Finished)
				Play(false, Reversed);
		}

		public void Update(float delta)
		{
			if (Delay == 0f || Finished || Paused)
				return;

			_frameTimer += delta;
			while (_frameTimer > Delay && !Finished)
			{
				_frameTimer -= Delay;
				if (Reversed)
				{
					if (Looped && CurrentFrame == 0)
						CurrentFrame = FrameCount - 1;
					else
						CurrentFrame--;
				}
				else
				{
					if (Looped && CurrentFrame == FrameCount - 1)
						CurrentFrame = 0;
					else
						CurrentFrame++;
				}
			}
		}
	}
}