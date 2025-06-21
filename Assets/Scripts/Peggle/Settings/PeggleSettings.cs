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
		[Min(0)]
		public int LauncherTrailTickCount;
		public bool LauncherPredictionEnabled;
		
		[Header("Peg Settings")]
		public PegLayout pegLayout;
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
		public bool showTextUI;

		[Header("Bucket Settings")]
		public bool randomizeBucketStartPosition;
		public float bucketMoveSpeed;

		[Header("Game Loop Settings")]
		public int BallsPerGame;
		[Tooltip("Required == Orange")]
		public int RequiredPegCount;

		[Header("Score Settings")] public int BaseScoreNormalPeg;
		public int BaseScoreRequiredPeg;
		public int BaseScoreSpecialPeg;
		public int BaseBucketScore;
		public bool AnimateScoreChanges;
		public bool AnimateScoreDings;

		public int GetScoreMultiplier(int remainingRequiredPegs)
		{
			//https://peggle.fandom.com/wiki/Scoring_System
			//I'll need to modify these for the smaller peg counts.
			if (remainingRequiredPegs == 0)
			{
				//FEVER
				return 100;
			}
			if (remainingRequiredPegs <= 3)
			{
				return 10;
			}

			if (remainingRequiredPegs <= 6)
			{
				return 5;
			}
			if(remainingRequiredPegs <= 10)
			{
				return 3;
			}

			if (remainingRequiredPegs <= 15)
			{
				return 2;
			}

			return 1;
		}

		public int GetBaseScore(PegType pegType)
		{
			switch (pegType)
			{
				case PegType.Basic:
					return BaseScoreNormalPeg;
				case PegType.Required:
					return BaseScoreRequiredPeg;
				case PegType.SuperDuper:
					return BaseScoreSpecialPeg;
			}

			return 0;
		}
	}
}