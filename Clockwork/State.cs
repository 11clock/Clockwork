using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Clockwork
{
	public class State: Processor
	{
		internal readonly List<GameObject> Objects;

		internal readonly List<GameObject> RemoveQueue;

		private List<GameObject> _orderedUpdateObjects;
		private List<GameObject> _orderedDrawObjects;
		
		internal State()
		{
			Objects = new List<GameObject>();
			RemoveQueue = new List<GameObject>();
		}

		internal void PreUpdate()
		{
			_orderedUpdateObjects = new List<GameObject>(Objects);
			_orderedUpdateObjects.Sort(new ObjectUpdateOrderer());
			
			_orderedDrawObjects = new List<GameObject>(Objects);
			_orderedDrawObjects.Sort(new ObjectDrawOrderer());
		}

		public override void BeginUpdate()
		{
			base.BeginUpdate();
			foreach (GameObject go in _orderedUpdateObjects)
			{
				if (go.Active)
				{
					go.BeginUpdate();
				}
			}
		}

		public override void Update()
		{
			base.Update();
			foreach (GameObject go in _orderedUpdateObjects)
			{
				if (go.Active)
				{
					go.Update();
				}
			}
			foreach (GameObject go in _orderedUpdateObjects)
			{
				if (go.Active)
				{
					go.EndUpdate();
				}
			}
		}

		public override void EndUpdate()
		{
			base.EndUpdate();
			foreach (GameObject go in _orderedUpdateObjects)
			{
				if (go.Active)
				{
					go.EndUpdate();
				}
			}
		}

		public override void Draw()
		{
			base.Draw();
			foreach (GameObject go in _orderedDrawObjects)
            {
                if (go.Visible)
                {
                    go.Draw();
                }
            }
		}
		
		public T Add<T>(T go) where T : GameObject
		{
			Objects.Add(go);
			if (!go.Initialized)
			{
				go.Initialize();
			}
			go.OnAdd();
			return go;
		}
		
		public T Add<T>(T go, Vector2 position) where T : SpatialObject
		{
			Objects.Add(go);
			go.Position = position;
			go.StartInitialize();
			return go;
		}

		public void QueueRemove(GameObject go)
		{
			if (!RemoveQueue.Contains(go))
			{
				RemoveQueue.Add(go);
			}
		}

		public bool IsDestroyed(GameObject go)
		{
			return !Objects.Contains(go);
		}

		private class ObjectUpdateOrderer : IComparer<GameObject>
		{
			public int Compare(GameObject x, GameObject y)
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

		private class ObjectDrawOrderer : IComparer<GameObject>
		{
			public int Compare(GameObject x, GameObject y)
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