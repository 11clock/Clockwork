using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Clockwork.Libraries;
using Microsoft.Xna.Framework;

namespace Clockwork.Process
{
	public class Scene : Processor
	{
		internal readonly List<BaseObject> Objects;

		internal readonly List<BaseObject> RemoveQueue;

		public Scene()
		{
			Objects = new List<BaseObject>();
			RemoveQueue = new List<BaseObject>();
		}

		internal override void PreUpdate()
		{
			base.PreUpdate();
			
			foreach (BaseObject go in GetOrderedUpdateObjects())
			{
				go.PreUpdate();	
			}
		}

		public override void BeginUpdate()
		{
			base.BeginUpdate();
			foreach (BaseObject go in GetOrderedUpdateObjects())
			{
				go.BeginUpdate();
			}
		}

		internal override void UpdateAlarms()
		{
			base.UpdateAlarms();
			foreach (BaseObject go in GetOrderedUpdateObjects())
			{
				go.UpdateAlarms();
			}
		}

		public override void Update()
		{
			base.Update();
			foreach (BaseObject go in GetOrderedUpdateObjects())
			{
				go.Update();
			}
		}

		internal void UpdateCollisions()
		{
			List<GameObject> collisionObjects = GetOrderedCollisionObjects();
			
			foreach (GameObject go1 in collisionObjects)
			{
				foreach (GameObject go2 in collisionObjects)
				{
					if (go1 == go2)
						continue;

					if (go1.CollidesWith(go2))
					{
						go1.OnCollision(go2);
					}
				}
			}
		}

		public override void EndUpdate()
		{
			base.EndUpdate();
			foreach (BaseObject go in GetOrderedUpdateObjects())
			{
				go.EndUpdate();
			}
		}

		internal override void PreDraw()
		{
			base.PreDraw();
			
			foreach (BaseObject go in GetOrderedDrawObjects())
            {
            	go.PreDraw();
            }
		}

		public override void Draw()
		{
			base.Draw();
			foreach (BaseObject go in GetOrderedDrawObjects())
			{
				go.Draw();
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
		
		public T Add<T>(T go, float x, float y) where T : GameObject
		{
			Objects.Add(go);
			go.Position = new Vector2(x, y);
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

		private List<BaseObject> GetOrderedUpdateObjects()
		{
			List<BaseObject> orderedUpdateObjects = new List<BaseObject>(Objects.Where(o => o.Active).Except(RemoveQueue).ToList());
            orderedUpdateObjects.Sort(new ObjectUpdateOrderer());
			return orderedUpdateObjects;
		}
		
		private List<BaseObject> GetOrderedDrawObjects()
        {
        	List<BaseObject> orderedDrawObjects = new List<BaseObject>(Objects.Where(o => o.Visible).ToList());
            orderedDrawObjects.Sort(new ObjectDrawOrderer());
        	return orderedDrawObjects;
        }

		private List<GameObject> GetOrderedCollisionObjects()
		{
			List<GameObject> collisionObjects = new List<GameObject>(GetOrderedUpdateObjects().OfType<GameObject>().Where(go => go.BBox != null));
			return collisionObjects;
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