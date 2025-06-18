using System;
using UnityEngine;

namespace Peggle
{
	public class MatchTransform : MonoBehaviour
	{
		private Transform _target;

		void Update()
		{
			Follow();
		}

		private void LateUpdate()
		{
			Follow();
		}

		public void SetTarget(Transform target)
		{
			this._target = target;
			Follow();
		}

		private void Follow()
		{
			if (_target != null)
			{
				transform.position = _target.position;
				transform.rotation = _target.rotation;
				transform.localScale = _target.localScale;
			}
		}
		
	}
}