using System;
using UnityEngine;

namespace Peggle
{
	public class RotateShape : MonoBehaviour
	{
		[SerializeField] PeggleManager _manager;

		private void Update()
		{
			if (_manager.Settings.shapeRotationSpeed != 0)
			{
				transform.Rotate(Vector3.forward, _manager.Settings.shapeRotationSpeed);
			}
		}
		
	}
}