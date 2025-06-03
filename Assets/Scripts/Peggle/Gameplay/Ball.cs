using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Peggle
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Ball : MonoBehaviour
	{
		private Rigidbody2D _rb;

		void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}
		public void Launch(float launchSpeed)
		{
			//the slightest noise to prevent the dreaded "hit the top of a sphere perfectly and bounce perfectly"
			Vector3 noise = Random.insideUnitCircle * 0.001f;
			_rb.AddForce(-transform.up*launchSpeed+noise, ForceMode2D.Impulse);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			var ballhit = other.gameObject.GetComponent<IBallHit>();
			if (ballhit != null)
			{
				ballhit.Hit(this, other);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var death = other.gameObject.GetComponent<BallDeathZone>();
			if (death != null)
			{
				death.ConsumeBall(this);
				return;
			}

			var bucket = other.gameObject.GetComponent<Bucket>();
			if (bucket != null)
			{
				bucket.ConsumeBall(this);
			}
		}
	}
}