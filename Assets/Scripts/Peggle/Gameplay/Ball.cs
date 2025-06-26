using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Peggle
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Ball : MonoBehaviour
	{
		public Rigidbody2D RB => _rb;
		private Rigidbody2D _rb;
		public static Action<Collision2D> OnAnyHit;
		void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}
		public void Launch(float launchSpeed, Vector3 noise)
		{
			//the slightest noise to prevent the dreaded "hit the top of a sphere perfectly and bounce perfectly"
			_rb.AddForce(noise-transform.up*launchSpeed, ForceMode2D.Impulse);
		}

		private void Start()
		{
			//BallPrediction.ConfigurePredictionBall(this);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			var ballhit = other.gameObject.GetComponent<IBallHit>();
			if (ballhit != null)
			{
				ballhit.Hit(this, other);
				OnAnyHit?.Invoke(other);
			}
		}

		private void OnCollisionStay2D(Collision2D other)
		{
			var ballhit = other.gameObject.GetComponent<IBallHit>();
			if (ballhit != null)
			{
				ballhit.Stay(this, other);
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			var ballhit = other.gameObject.GetComponent<IBallHit>();
			if (ballhit != null)
			{
				ballhit.Exit(this, other);
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