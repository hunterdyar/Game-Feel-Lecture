using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Peggle
{
	public class Launcher : MonoBehaviour
	{
		public PeggleManager peggleManager;
		public InputActionReference Rotate;
		public InputActionReference Launch;
		[SerializeField] Transform _launchPoint;
		private void OnEnable()
		{
			Rotate.action.Enable();
			Launch.action.Enable();
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
		}

		void TryLaunch()
		{
			if (peggleManager.CanLaunchBall())
			{
				var b = Instantiate(PeggleManager.Settings.ballPrefab, _launchPoint.position, _launchPoint.rotation);
				b.Launch(PeggleManager.Settings.launchSpeed);
				peggleManager.BallLaunched(b);
				//apply ball speed?
			}
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
		}
	}
}