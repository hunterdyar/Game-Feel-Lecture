using System;
using UnityEngine;

namespace Peggle
{
	public class PeggleCameraShake : MonoBehaviour
	{
		private SpringJoint2D _springJoint;
		private Rigidbody2D _rigidbody;
		public float intensity;
		private void Awake()
		{
			_springJoint = GetComponent<SpringJoint2D>();
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		private void OnEnable()
		{
			Ball.OnAnyHit += OnHit;
		}

		private void OnHit(Collision2D collision)
		{
			if (PeggleManager.Settings.cameraShake)
			{
				var f = collision.relativeVelocity;
				_rigidbody.AddForce(f*intensity, ForceMode2D.Impulse);
			}
		}

		private void OnDisable()
		{
			Ball.OnAnyHit -= OnHit;
		}

		private void Update()
		{
			_springJoint.enabled = PeggleManager.Settings.cameraShake;
		}
	}
}
