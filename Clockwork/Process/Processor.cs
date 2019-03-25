namespace Clockwork.Process
{
	public abstract class Processor
	{
		public virtual void Initialize()
		{
		}

		internal virtual void PreUpdate()
		{
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