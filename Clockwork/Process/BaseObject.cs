using System;

namespace Clockwork.Process
{
	public abstract class BaseObject : Processor, IComparable<BaseObject>
	{
		private static int _instanceIdCounter;

		public long InstanceId { get; private set; }

		public int UpdateOrder { get; set; }
		public int DrawOrder { get; set; }

		public bool Active { get; set; } = true;
		public bool Visible { get; set; } = true;

		public bool Initialized { get; private set; }

		internal void StartInitialize()
		{
			if (Initialized)
			{
				throw new Exception($"{this} is already initialized.");
			}

			InstanceId = _instanceIdCounter;
			_instanceIdCounter++;
			Initialize();
			Initialized = true;
		}

		public virtual void OnAdd()
		{
		}

		public virtual void OnRemove()
		{
		}

		public virtual void OnSceneEnd()
		{
		}

		public int CompareTo(BaseObject obj)
		{
			if (InstanceId > obj.InstanceId)
				return 1;
			if (InstanceId < obj.InstanceId)
				return -1;
			return 0;
		}
	}
}