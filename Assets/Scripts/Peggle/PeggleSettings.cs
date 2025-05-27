using System;
using UnityEngine;

namespace Peggle
{
	[System.Serializable]
	public struct PeggleSettings
	{
		[Header("Ball Settings")]
		public Ball ballPrefab;
		public bool ballTrail;
		public float ballTrailLength;
		
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

		[Header("Bucket Settings")]
		public bool randomizeBucketStartPosition;
		public float bucketMoveSpeed;

		[Header("Game Loop Settings")]
		public int BallsPerGame;
		[Tooltip("Required == Orange")]
		public int RequiredPegCount;
	}
}