using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Peggle.Shapes
{
	public class ShapeCircle : MonoBehaviour, IShape
	{
		public float radius;
		public int pegCount;

		private IEnumerable<Vector3> CirclePoints()
		{

			for (int i = 0; i < pegCount; i++)
			{
				//yield for each point on a circle at radius.
				var p = new Vector3(Mathf.Cos((i/(float)pegCount)*360*Mathf.Deg2Rad)*radius, Mathf.Sin((i/(float)pegCount)*360 * Mathf.Deg2Rad)*radius, transform.position.z);
				yield return transform.TransformPoint(p);
			}
		}
		public IEnumerable<Vector3> Points => CirclePoints();
	}
}