using System.Collections.Generic;
using UnityEngine;

public interface IShape 
{
	public IEnumerable<Vector3> Points { get; }
}
