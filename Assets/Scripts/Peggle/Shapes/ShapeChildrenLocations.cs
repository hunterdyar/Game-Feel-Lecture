using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Peggle.Shapes
{
	public class ShapeChildrenLocations : MonoBehaviour, IShape
	{
		public IEnumerable<Vector3> Points => (transform as IEnumerable<Transform>).Select(x=>x.position);
	}
}