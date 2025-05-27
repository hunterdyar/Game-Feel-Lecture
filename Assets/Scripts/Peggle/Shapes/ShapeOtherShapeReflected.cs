using System;
using System.Collections.Generic;
using UnityEngine;

namespace Peggle.Shapes
{
	public class ShapeOtherShapeReflected : MonoBehaviour, IShape
	{
		public enum MirrorDirection { Vertical, Horizontal }
		public IEnumerable<Vector3> Points => ReflectShape();
		[SerializeField] private MirrorDirection _mirrorDirection;
		public Vector2 NormalOfReflection => _mirrorDirection == MirrorDirection.Vertical ? Vector2.right : Vector2.up;
		
		[SerializeField] public GameObject OtherShape;
		private IShape otherShape;
		public IEnumerable<Vector3> ReflectShape()
		{
			foreach (var point in otherShape.Points)
			{
				var p= Vector3.Reflect(point-transform.position,NormalOfReflection) + transform.position;
				
				//on the line of symmetry, don't want duplicate points.
				if (p != point)
				{
					yield return p;
				}
			}
		}

		private void OnValidate()
		{
			if (OtherShape is null)
			{
				return;
			}

			var other = OtherShape.GetComponent<IShape>();
			if (other != null)
			{
				otherShape = other;
			}
			else
			{
				Debug.LogError("Only IShape is supported.", this);
			}
		}
	}
}