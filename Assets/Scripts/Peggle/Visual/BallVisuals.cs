using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Peggle
{
	public class BallVisuals : MonoBehaviour
	{
		[SerializeField] private TrailRenderer trailRenderer;
		[SerializeField] private Light2D light2D;

		private void Start()
		{
			OnSettingsChanged(PeggleManager.Settings);
		}

		private void OnEnable()
		{
			PeggleManager.OnSettingsChanged += OnSettingsChanged;
		}

		private void OnDisable()
		{
			PeggleManager.OnSettingsChanged -= OnSettingsChanged;
		}

		private void OnSettingsChanged(PeggleSettings settings)
		{
			trailRenderer.enabled = settings.ballTrail;
			trailRenderer.time = settings.ballTrailTime;
			light2D.enabled = settings.lightForBall;
		}
	}
}