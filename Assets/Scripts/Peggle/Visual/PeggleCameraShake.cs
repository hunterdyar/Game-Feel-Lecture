using System;
using UnityEngine;

namespace Peggle
{
	public class PeggleCameraShake : MonoBehaviour
	{
		private SpringJoint2D _springJoint;

		private void Awake()
		{
			_springJoint = GetComponent<SpringJoint2D>();
		}

		private void OnEnable()
		{
			Peg.OnPegHit += OnPegHit;
		}

		private void OnPegHit(Peg obj)
		{
			
		}

		private void OnDisable()
		{
			Peg.OnPegHit -= OnPegHit;

		}

		

		private void Update()
		{
			_springJoint.enabled = PeggleManager.Settings.cameraShake;
		}
	}
}
