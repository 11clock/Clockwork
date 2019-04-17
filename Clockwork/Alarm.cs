using System;

namespace Clockwork
{
	public class Alarm
	{
		public Action Action { get; }

		public bool Paused { get; set; }

		public float TimeLeft { get; set; }
		public float TotalTime { get; set; }

		public bool Finished { get; private set; } = true;

		public bool Temporary { get; internal set; }

		public Alarm(Action action, float totalTime = 0f)
		{
			Action = action;
			TotalTime = totalTime;
		}

		internal void Update()
		{
			if (Finished || Paused)
				return;

			TimeLeft -= Time.Delta;
			if (TimeLeft <= 0f)
			{
				Stop();
				Action();
			}
		}

		public void Start(float? time = null)
		{
			if (time != null)
			{
				TotalTime = (float) time;
			}

			if (TotalTime > 0f)
			{
				TimeLeft = TotalTime;
				Finished = false;
				Paused = false;
			}
		}

		public void Stop()
		{
			Finished = true;
			TimeLeft = 0f;
		}

		public void Pause()
		{
			Paused = true;
		}

		public void Resume()
		{
			Paused = false;
		}
	}
}