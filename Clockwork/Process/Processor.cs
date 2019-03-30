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
			AlarmClock.Update();
		}
		
		public virtual void BeginUpdate()
		{
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

		public virtual void Draw()
		{
		}
	}
}