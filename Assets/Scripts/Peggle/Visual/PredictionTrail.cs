using System;
using UnityEngine;

namespace Peggle
{
	[RequireComponent(typeof(LineRenderer))]
	public class PredictionTrail : MonoBehaviour
	{
		private LineRenderer _lineRenderer;
		private bool _showLauncher = true;
		private void Awake()
		{
			_lineRenderer = GetComponent<LineRenderer>();
		}

		void OnEnable()
		{
			PeggleManager.OnGameStateChange += OnGameStateChange;
		}

		private void OnDisable()
		{
			PeggleManager.OnGameStateChange -= OnGameStateChange;
		}

		private void OnGameStateChange(PeggleGameState state)
		{
			_showLauncher = state == PeggleGameState.PlayerCanShoot;
		}

		private void Update()
		{
			_lineRenderer.enabled = PeggleManager.Settings.LauncherPredictionEnabled && _showLauncher;
			if (PeggleManager.Settings.LauncherPredictionEnabled)
			{
				if (BallPrediction.PredictionPositions == null)
				{
					//race condition. Just wait.
					return;
				}else if (BallPrediction.PredictionPositions.Length != PeggleManager.Settings.LauncherTrailTickCount)
				{
					//also, if we happen to change values in the inspector.
					BallPrediction.MarkDirty();
				}
				
				if (_lineRenderer.positionCount != BallPrediction.PredictionPositions.Length)
				{
					_lineRenderer.positionCount = BallPrediction.PredictionPositions.Length;
				}

				_lineRenderer.SetPositions(BallPrediction.PredictionPositions);
			}
		}
	}
}