using System;
using System.Collections.Generic;
using UnityEngine;

namespace Peggle.Shapes
{
	public class ShapeLine : MonoBehaviour, IShape
	{
		public IEnumerable<Vector3> Points => LinePoints();
		[Min(0)]
		public int Count;

		private IEnumerable<Vector3> LinePoints()
		{
			for (int i = 0; i < Count; i++)
			{
				float t = i / (float)Count;
				yield return Vector3.Lerp(transform.GetChild(0).position, transform.GetChild(1).position, t);
			}
		}
		private void OnValidate()
		{
			while (transform.childCount < 2)
			{
				var child = new GameObject();
				child.transform.SetParent(transform);
				child.gameObject.name = "Line End";
			}
		}
	}
}