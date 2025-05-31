using System;
using UnityEngine;

namespace Peggle
{
	public class RotateShape : MonoBehaviour
	{
		private void Update()
		{
			if (PeggleManager.Settings.shapeRotationSpeed != 0)
			{
				transform.Rotate(Vector3.forward, PeggleManager.Settings.shapeRotationSpeed);
			}
		}
	}
}