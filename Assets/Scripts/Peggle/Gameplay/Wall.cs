using System;
using UnityEngine;

namespace Peggle
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Wall : MonoBehaviour, IBallHit
	{
		public static Action OnHit;
		private BoxCollider2D _boxCollider2D;
		private void Awake()
		{
			_boxCollider2D = GetComponent<BoxCollider2D>();
		}

		private void Start()
		{
			BallPrediction.RegisterBoxColliderForPrediction(gameObject, _boxCollider2D);
		}
		void OnDestroy()
		{
			BallPrediction.UnregisterColliderForPrediction(gameObject);
		}

		public void Hit(Ball ball, Collision2D collision)
		{
			OnHit?.Invoke();
		}

		public void Exit(Ball ball, Collision2D collision)
		{
		}

		public void Stay(Ball ball, Collision2D collision)
		{
		}
	}
}