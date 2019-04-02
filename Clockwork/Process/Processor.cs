namespace Clockwork.Process
{
	public abstract class Processor
	{
		public AlarmClock AlarmClock { get; }

		public Processor()
		{
			AlarmClock = new AlarmClock(this);
		}
		
		public virtual void Initialize()
		{
		}

		internal virtual void PreUpdate()
		{
		}
		
		public virtual void BeginUpdate()
		{
		}

		internal virtual void UpdateAlarms()
		{
			AlarmClock.Update();
		}

		public virtual void Update()
		{
		}

		public virtual void EndUpdate()
		{
		}
		
		internal virtual void PostUpdate()
		{
		}
		
		internal virtual void OnSceneEnd()
		{
		}
		
		internal virtual void PreDraw()
		{
		}

		public virtual void Draw()
		{
		}
		
		internal virtual void PostDraw()
		{
		}
	}
}