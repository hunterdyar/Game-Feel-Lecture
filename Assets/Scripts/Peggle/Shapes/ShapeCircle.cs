using System.Collections.Generic;
using UnityEngine;

namespace Peggle.Shapes
{
	public class ShapeCircle : MonoBehaviour, IShape
	{
		public float radius;
		public int pegCount;
		[Range(0, 1)] public float circlePercent;
		private IEnumerable<Vector3> CirclePoints()
		{
			for (int i = 0; i < pegCount; i++)
			{
				//yield for each point on a circle at radius.
				float y = i/(float)pegCount;
				if (y <= circlePercent)
				{
					float x = y * 360 * Mathf.Deg2Rad;
					//(i / (float)pegCount) * 360 * Mathf.Deg2Rad
					var p = new Vector3(Mathf.Cos(x) * radius, Mathf.Sin(x) * radius, transform.position.z);
					yield return transform.TransformPoint(p);
				}
			}
		}
		public IEnumerable<Vector3> Points => CirclePoints();
	}
}