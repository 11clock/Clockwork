using System.Collections.Generic;
using System.Diagnostics;

namespace Clockwork
{
	public class State: BasicObject
	{
		private readonly List<BasicObject> _objects;
	
		private readonly List<BasicObject> _removeQueue;

		private List<BasicObject> _orderedObjects;

		public State()
		{
			_objects = new List<BasicObject>();
			_removeQueue = new List<BasicObject>();
		}

		internal void PreUpdate()
		{
			_orderedObjects = new List<BasicObject>(_objects);
			_orderedObjects.Sort(new ObjectUpdateOrderer());
		}

		public override void Update()
		{
			List<BasicObject> orderedObjects = new List<BasicObject>(_objects);
			orderedObjects.Sort(new ObjectUpdateOrderer());

			foreach (BasicObject go in orderedObjects)
			{
				if (go.Active)
				{
					go.BeginUpdate();
				}
			}
			foreach (BasicObject go in orderedObjects)
			{
				if (go.Active)
				{
					go.Update();
				}
			}
			foreach (BasicObject go in orderedObjects)
			{
				if (go.Active)
				{
					go.EndUpdate();
				}
			}
		}
		
		private class ObjectUpdateOrderer : IComparer<BasicObject>
		{
			public int Compare(BasicObject x, BasicObject y)
			{
				Debug.Assert(x != null, nameof(x) + " != null");
				Debug.Assert(y != null, nameof(y) + " != null");
				if (x.UpdateOrder > y.UpdateOrder)
					return 1;
				if (x.UpdateOrder < y.UpdateOrder)
					return -1;
				return x.CompareTo(y);
			}
		}

		private class ObjectDrawOrderer : IComparer<BasicObject>
		{
			public int Compare(BasicObject x, BasicObject y)
			{
				Debug.Assert(x != null, nameof(x) + " != null");
				Debug.Assert(y != null, nameof(y) + " != null");
				if (x.DrawOrder > y.DrawOrder)
					return 1;
				if (x.DrawOrder < y.DrawOrder)
					return -1;
				return x.CompareTo(y);
			}
		}
	}
}