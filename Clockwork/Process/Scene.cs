using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Clockwork.Process
{
	public class Scene : Processor
	{
		internal readonly List<BaseObject> Objects;

		internal readonly List<BaseObject> RemoveQueue;

		private List<BaseObject> _orderedUpdateObjects;
		private List<BaseObject> _orderedDrawObjects;

		public Scene()
		{
			Objects = new List<BaseObject>();
			RemoveQueue = new List<BaseObject>();
		}

		internal override void PreUpdate()
		{
			base.PreUpdate();
			_orderedUpdateObjects = new List<BaseObject>(Objects);
			_orderedUpdateObjects.Sort(new ObjectUpdateOrderer());

			_orderedDrawObjects = new List<BaseObject>(Objects);
			_orderedDrawObjects.Sort(new ObjectDrawOrderer());
			
			foreach (BaseObject go in _orderedUpdateObjects)
            {
            	if (go.Active)
            	{
            		go.PreUpdate();
            	}
            }
		}

		public override void BeginUpdate()
		{
			base.BeginUpdate();
			foreach (BaseObject go in _orderedUpdateObjects)
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
			foreach (BaseObject go in _orderedUpdateObjects)
			{
				if (go.Active)
				{
					go.Update();
				}
			}
		}

		public override void EndUpdate()
		{
			base.EndUpdate();
			foreach (BaseObject go in _orderedUpdateObjects)
			{
				if (go.Active)
				{
					go.EndUpdate();
				}
			}
		}

		internal override void PostUpdate()
		{
			base.PostUpdate();
			foreach (BaseObject go in _orderedUpdateObjects)
			{
				if (go.Active)
				{
					go.PostUpdate();
				}
			}
		}

		public override void Draw()
		{
			base.Draw();
			foreach (BaseObject go in _orderedDrawObjects)
			{
				if (go.Visible)
				{
					go.Draw();
				}
			}
		}

		public T Add<T>(T go) where T : BaseObject
		{
			Objects.Add(go);
			if (!go.Initialized)
			{
				go.Initialize();
			}

			go.OnAdd();
			return go;
		}

		public T Add<T>(T go, Vector2 position) where T : GameObject
		{
			Objects.Add(go);
			go.Position = position;
			go.StartInitialize();
			return go;
		}

		public void Remove(BaseObject go)
		{
			if (!RemoveQueue.Contains(go))
			{
				RemoveQueue.Add(go);
			}
		}

		public bool IsDestroyed(BaseObject go)
		{
			return !Objects.Contains(go);
		}

		public void Goto(Scene nextScene)
		{
			CGame.Game.QueuedScene = nextScene;
		}

		private class ObjectUpdateOrderer : IComparer<BaseObject>
		{
			public int Compare(BaseObject x, BaseObject y)
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

		private class ObjectDrawOrderer : IComparer<BaseObject>
		{
			public int Compare(BaseObject x, BaseObject y)
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