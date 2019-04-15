using System;
using System.Collections.Generic;
using Clockwork.Process;

namespace Clockwork
{
	public class AlarmClock
	{
		public Processor Parent { get; }

		private Dictionary<string, Alarm> _alarms;

		public AlarmClock(Processor parent)
		{
			_alarms = new Dictionary<string, Alarm>();
			Parent = parent;
		}

		internal void Update()
		{
			List<Alarm> alarmsToUpdate = new List<Alarm>(_alarms.Values);
			foreach (Alarm alarm in alarmsToUpdate)
			{
				alarm.Update();
			}

			List<KeyValuePair<string, Alarm>> expiredPairs = new List<KeyValuePair<string, Alarm>>();
			foreach (KeyValuePair<string, Alarm> pair in _alarms)
			{
				Alarm alarm = pair.Value;
				if (alarm.Temporary)
				{
					if (alarm.Finished)
					{
						expiredPairs.Add(pair);
					}
				}
			}

			foreach (KeyValuePair<string, Alarm> pair in expiredPairs)
			{
				_alarms.Remove(pair.Key);
			}
		}

		public void Add(Action action, float totalTime = 0f)
		{
			if (BelongsToParent(action))
			{
				Alarm alarm = new Alarm(action, totalTime);
				Add(alarm);
			}
		}

		public void Remove(Action action)
		{
			if (BelongsToParent(action))
			{
				string name = action.Method.Name;
				if (_alarms.ContainsKey(name))
				{
					_alarms.Remove(name);
				}
			}
		}

		public void Start(Action action, float? time = null)
		{
			Alarm alarm = Get(action);
			if (alarm != null)
			{
				alarm.Start(time);
			}
			else
			{
				if (BelongsToParent(action))
				{
					alarm = new Alarm(action, time ?? 0f) {Temporary = true};
					Add(alarm);
					alarm.Start();
				}
			}
		}

		public void Stop(Action action)
		{
			Get(action)?.Stop();
		}

		public void Pause(Action action)
		{
			Get(action)?.Pause();
		}

		public void Resume(Action action)
		{
			Get(action)?.Resume();
		}

		public bool IsFinished(Action action)
		{
			return Get(action)?.Finished ?? true;
		}

		public bool IsPaused(Action action)
		{
			return Get(action)?.Paused ?? false;
		}

		public Alarm Get(Action action)
		{
			return Get(action.Method.Name);
		}

		public Alarm Get(string actionName)
		{
			return _alarms.ContainsKey(actionName) ? _alarms[actionName] : null;
		}

		private void Add(Alarm alarm)
		{
			_alarms[alarm.Action.Method.Name] = alarm;
		}

		private bool BelongsToParent(Action action)
		{
			return action.Target == Parent;
		}
	}
}