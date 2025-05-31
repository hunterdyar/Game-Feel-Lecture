using System;
using UnityEngine;

namespace Peggle
{
	[RequireComponent(typeof(TrailRenderer))]
	public class BallTrailSettings : SettingsChangeListener
	{
		private TrailRenderer _trailRenderer;

		private void Awake()
		{
			_trailRenderer = GetComponent<TrailRenderer>();
		}

		private void Start()
		{
			OnSettingsChanged(PeggleManager.Settings);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		protected override void OnSettingsChanged(PeggleSettings settings)
		{
			_trailRenderer.enabled = settings.ballTrail;
			_trailRenderer.time = settings.ballTrailTime;
		}
	}
}