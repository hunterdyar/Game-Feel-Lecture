using UnityEngine;
using UnityEngine.Splines;

namespace Diagrams
{
	public class DotOnSpline : MonoBehaviour
	{
		private Spline _spline;
		private float _offset;

		public void SetSpline(Spline spline)
		{
			_spline = spline;
		}

		public void SetLerp(float t)
		{
			transform.localPosition = _spline.EvaluatePosition((t+_offset)%1f);
		}

		public void SetOffset(float f)
		{
			_offset = f;
		}
	}
}