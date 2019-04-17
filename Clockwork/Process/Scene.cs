using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Clockwork.Process
{
	public class Scene : Processor
	{
		private readonly List<BaseObject> _objects;

		private readonly List<BaseObject> _removeQueue;

		private List<BaseObject> _orderedUpdateObjects;
		private List<BaseObject> _orderedDrawObjects;

		public Scene()
		{
			_objects = new List<BaseObject>();
			_removeQueue = new List<BaseObject>();
		}

		internal override void PreUpdate()
		{
			base.PreUpdate();
			_orderedUpdateObjects = GetOrderedUpdateObjects();
			_orderedUpdateObjects.ForEach(go => go.PreUpdate());
		}

		public override void BeginUpdate()
		{
			base.BeginUpdate();
			_orderedUpdateObjects.ForEach(go => go.BeginUpdate());
		}

		internal override void UpdateAlarms()
		{
			base.UpdateAlarms();
			_orderedUpdateObjects.ForEach(go => go.UpdateAlarms());
		}

		public override void Update()
		{
			base.Update();
			_orderedUpdateObjects.ForEach(go => go.Update());
		}

		internal void UpdateCollisions()
		{
			Dictionary<GameObject, GameObject> collisions = new Dictionary<GameObject, GameObject>();
			
			List<GameObject> collisionObjects =
				_orderedUpdateObjects.OfType<GameObject>().Where(go => go.BBox != null).ToList();

			foreach (GameObject go1 in collisionObjects)
			{
				foreach (GameObject go2 in collisionObjects)
				{
					if (go1 == go2)
						continue;

					if (go1.CollidesWith(go2))
					{
						collisions[go1] = go2;
					}
				}
			}

			foreach (KeyValuePair<GameObject,GameObject> collision in collisions)
			{
				collision.Key.OnCollision(collision.Value);
			}
		}

		public override void EndUpdate()
		{
			base.EndUpdate();
			_orderedUpdateObjects.ForEach(go => go.EndUpdate());
		}

		internal override void PostUpdate()
		{
			base.PostUpdate();
			_orderedUpdateObjects.ForEach(go => go.PostUpdate());
			CleanRemoveQueue();
		}

		internal override void OnSceneEnd()
		{
			base.OnSceneEnd();
			GetOrderedUpdateObjects().ForEach(go => go.OnSceneEnd());
		}

		internal override void PreDraw()
		{
			base.PreDraw();
			_orderedDrawObjects = GetOrderedDrawObjects();
			_orderedDrawObjects.ForEach(go => go.PreDraw());
		}

		public override void Draw()
		{
			base.Draw();
			_orderedDrawObjects.ForEach(go => go.Draw());
		}

		internal override void PostDraw()
		{
			base.PostDraw();
			_orderedDrawObjects.ForEach(go => go.PostDraw());
		}

		public T Add<T>(T go) where T : BaseObject
		{
			_objects.Add(go);
			go.Scene = this;
			if (!go.Initialized)
			{
				go.StartInitialize();
			}

			go.OnAdd();
			return go;
		}

		public T Add<T>(T go, Vector2 position) where T : GameObject
		{
			_objects.Add(go);
			go.Scene = this;
			go.Position = position;
			if (!go.Initialized)
			{
				go.StartInitialize();
			}

			go.OnAdd();
			return go;
		}

		public T Add<T>(T go, float x, float y) where T : GameObject
		{
			return Add(go, new Vector2(x, y));
		}

		public void QueueRemove(BaseObject go)
		{
			if (!_removeQueue.Contains(go))
			{
				_removeQueue.Add(go);
			}
		}

		public bool Contains(BaseObject go)
		{
			return !_objects.Contains(go);
		}

		public void Goto(Scene nextScene)
		{
			CGame.Game.QueuedScene = nextScene;
		}

		public List<BaseObject> GetObjects()
		{
			return new List<BaseObject>(_objects);
		}

		public List<T> GetObjects<T>() where T : BaseObject
		{
			return _objects.Where(o => o is T).Cast<T>().ToList();
		}

		public T GetCollided<T>(GameObject go) where T : GameObject
		{
			return GetCollided<T>(go, go.Position);
		}

		public List<T> GetCollidedList<T>(GameObject go) where T : GameObject
		{
			return GetCollidedList<T>(go, go.Position);
		}

		public T GetCollided<T>(GameObject go, Vector2 position) where T : GameObject
		{
			foreach (T other in GetObjects<T>())
			{
				if (go.CollidesWith(position, other))
					return other;
			}

			return null;
		}

		public List<T> GetCollidedList<T>(GameObject go, Vector2 position) where T : GameObject
		{
			List<T> collided = new List<T>();
			foreach (T other in GetObjects<T>())
			{
				if (go.CollidesWith(position, other))
					collided.Add(other);
			}

			return collided;
		}

		public T GetCollided<T>(Vector2 position) where T : GameObject
		{
			foreach (T other in GetObjects<T>())
			{
				if (other.CollidesWith(position))
					return other;
			}

			return null;
		}

		public List<T> GetCollidedList<T>(Vector2 position) where T : GameObject
		{
			List<T> collided = new List<T>();
			foreach (T other in GetObjects<T>())
			{
				if (other.CollidesWith(position))
					collided.Add(other);
			}

			return collided;
		}

		private List<BaseObject> GetOrderedUpdateObjects()
		{
			List<BaseObject> orderedUpdateObjects = _objects.Where(o => o.Active).ToList();
			orderedUpdateObjects.Sort(new ObjectUpdateOrderer());
			return orderedUpdateObjects;
		}

		private List<BaseObject> GetOrderedDrawObjects()
		{
			List<BaseObject> orderedDrawObjects = _objects.Where(o => o.Visible).ToList();
			orderedDrawObjects.Sort(new ObjectDrawOrderer());
			return orderedDrawObjects;
		}

		private void CleanRemoveQueue()
		{
			foreach (BaseObject go in _removeQueue)
			{
				go.OnRemove();
				_objects.Remove(go);
				go.Scene = null;
			}

			_removeQueue.Clear();
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