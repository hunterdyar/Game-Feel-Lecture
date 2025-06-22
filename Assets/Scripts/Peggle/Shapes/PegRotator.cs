using UnityEngine;

namespace Peggle
{
	public class PegRotator : MonoBehaviour
	{
		[SerializeField] private float _modifier = 1;
		public void Update()
		{
			if (PeggleManager.Settings.PegCircleRotationSpeed > 0)
			{
				transform.Rotate(Vector3.forward, PeggleManager.Settings.PegCircleRotationSpeed*Time.deltaTime*
				                                  _modifier);
			}
		}
	}
}