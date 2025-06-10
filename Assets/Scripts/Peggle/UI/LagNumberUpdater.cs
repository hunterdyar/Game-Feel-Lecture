using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Peggle.Peggle.UI
{
	public class LagNumberUpdater
	{
		private int _desired;
		private int _current;
		private float timer;
		public Action<int> OnCurrentChanged;
		
		public void Tick(float delta)
		{
			if (_current == _desired)
			{
				return;
			}
			timer -= delta;
			if (timer <= 0)
			{
				float diff = _desired - _current;
				if (Mathf.Abs(diff) < 10)
				{
					_current = _desired;
					OnCurrentChanged?.Invoke(_current);
				}
				else
				{
					//move some amount closer.
					float step = diff * .2f;
					_current += Mathf.CeilToInt(step);
					OnCurrentChanged?.Invoke(_current);
				}
				timer = Random.Range(0.00f, 0.02f); //randomize the next update time
			}
		}

		public void SetDesired(int desired)
		{
			_desired = desired;
		}

		/// <summary>
		/// Setting this will skip animation.
		/// </summary>
		public void SetCurrent(int current)
		{
			_current = current;
			_desired = current;
			OnCurrentChanged?.Invoke(_current);
		}
	}
}