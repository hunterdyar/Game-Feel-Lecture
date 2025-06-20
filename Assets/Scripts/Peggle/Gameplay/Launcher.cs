using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Peggle
{
	public class Launcher : MonoBehaviour
	{
		public static Action<Ball> OnBallLaunch;
		public PeggleManager peggleManager;
		public InputActionReference Rotate;
		public InputActionReference Launch;
		private Vector3 Noise = Vector3.zero;
		[SerializeField] Transform _launchPoint;
		private void OnEnable()
		{
			Rotate.action.Enable();
			Launch.action.Enable();
		//	Noise = Random.insideUnitCircle * 0.001f;
		Noise = Vector3.zero;
		}

		private void Update()
		{
			float r = Rotate.action.ReadValue<float>();
			if (r != 0)
			{
				DoRotate(r);
			}

			if (Launch.action.WasPerformedThisFrame())
			{
				TryLaunch();
			}
			
			BallPrediction.PredictLaunch(PeggleManager.Settings.ballPrefab, _launchPoint.position, _launchPoint.rotation, PeggleManager.Settings.launchSpeed, Noise);
		}

		void TryLaunch()
		{
			if (peggleManager.CanLaunchBall())
			{
				var b = Instantiate(PeggleManager.Settings.ballPrefab, _launchPoint.position, _launchPoint.rotation);
				b.Launch(PeggleManager.Settings.launchSpeed, Noise);
				peggleManager.BallLaunched(b);
				OnBallLaunch?.Invoke(b);
				ResetNoise();
				//apply ball speed?
			}
		}

		void ResetNoise()
		{
			// Noise = Random.insideUnitCircle * 0.001f;
			Noise = Vector3.zero;
		}

		private void DoRotate(float delta)
		{
			if (PeggleManager.Settings.limitLauncherAngle)
			{
				float _currentAngle = Vector2.SignedAngle(Vector2.up, -transform.up);
				if (Mathf.Abs(_currentAngle) <= PeggleManager.Settings.degreesAwayFromUpToPrevent)
				{
					if (delta > 0 && _currentAngle < 0)
					{
						return;
					}
					else if (delta < 0 && _currentAngle > 0)
					{
						return;
					}
				}
			}

			float r = delta * Time.deltaTime * PeggleManager.Settings.launcherRotationSpeed;
			transform.Rotate(Vector3.forward, r);
			BallPrediction.MarkDirty();
		}
	}
}