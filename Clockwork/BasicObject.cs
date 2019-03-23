using System;
using Microsoft.Xna.Framework.Graphics;

namespace Clockwork
{
	public abstract class BasicObject : IComparable<BasicObject>
	{
		
		private static int InstanceIdCounter = 0;
		
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

			InstanceId = InstanceIdCounter;
			InstanceIdCounter++;
			Initialize();
			Initialized = true;
		}
		
		public virtual void Initialize()
		{
			
		}

		public virtual void OnAdd()
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

		public virtual void Draw()
		{

		}

		public virtual void OnRemove()
		{

		}

		public int CompareTo(BasicObject obj)
		{
			if (InstanceId > obj.InstanceId)
				return 1;
			if (InstanceId < obj.InstanceId)
				return -1;
			return 0;
		}
	}
}
