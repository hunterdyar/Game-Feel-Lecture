using System;
using UnityEngine;

namespace Peggle
{
	[System.Serializable]
	public class PeggleSettings
	{
		[Header("Ball Settings")]
		public Ball ballPrefab;
		public bool ballTrail;
		public float ballTrailTime;
		
		[Header("Bounciness Settings")]
		public float ballBounciness;
		public float pegBounciness;
		public float wallBounciness;
		public float bucketBounciness;
		
		[Header("Launcher Settings")]
		[Min(0)]
		public float launcherRotationSpeed;
		public float launchSpeed;
		public bool limitLauncherAngle;
		public float degreesAwayFromUpToPrevent;

		[Header("Peg Settings")]
		public Peg PegPrefab;
		public float delayBetweenPegClears;
		public float timeOfContinuousContactBeforeRemovingPeg;
		public int shapeRotationSpeed;
		public Color basicPegColor;
		public Color requiredPegColor;
		public Color specialPegColor;
		
		[Header("Visual Settings")]
		public bool bonkPegAnimationOnHit;
		public bool pegsShowHitState;
		public bool lightForBall;

		[Header("Bucket Settings")]
		public bool randomizeBucketStartPosition;
		public float bucketMoveSpeed;

		[Header("Game Loop Settings")]
		public int BallsPerGame;
		[Tooltip("Required == Orange")]
		public int RequiredPegCount;
	}
}