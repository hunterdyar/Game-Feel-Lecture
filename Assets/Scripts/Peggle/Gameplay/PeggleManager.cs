using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Peggle
{
	[CreateAssetMenu(fileName = "Peggle Manager", menuName = "Peggle/Peggle Manager")]
	public class PeggleManager : ScriptableObject
	{
		public static Action<PeggleGameState> OnGameStateChange;
		public static Action OnRoundStart;
		public static Action<PeggleSettings> OnSettingsChanged;
		public static Action<int> OnRemainingBallCountChanged;
		public static Action PrepareGame;
		public static Action StartGame;
		
		[SerializeField] private PeggleSettings _settings;
		public static PeggleSettings Settings;
		private PeggleGameState _state;
		
		public int RemainingBalls => _remainingBalls;
		private int _remainingBalls;

		public bool CanLaunchBall() => _remainingBalls > 0 && _state == PeggleGameState.PlayerCanShoot;
		private RoundManager _roundManager;

		private List<Ball> ActiveBalls = new List<Ball>();
		private List<Peg> _hitPegs = new List<Peg>();
		private List<Peg> _allPegs = new List<Peg>();
		private List<Peg> _requiredPegs => _allPegs.Where(x => x.PegType == PegType.Required).ToList();
		
		private void SetSettings(PeggleSettings newSettings)
		{
			_settings = newSettings;
			Settings = _settings;
			OnSettingsChanged?.Invoke(_settings);
		}

		public void Init()
		{
			Settings = _settings;
		}
		public void StartNewGame(RoundManager roundManager)
		{
			//call again just to force update.
			OnSettingsChanged?.Invoke(Settings);
			//
			_roundManager = roundManager;
			_remainingBalls = _settings.BallsPerGame;
			OnRemainingBallCountChanged?.Invoke(_remainingBalls);
			ActiveBalls.Clear();
			_hitPegs.Clear();
			_allPegs.Clear();
			ChangeState(PeggleGameState.PlayerCanShoot);
			PrepareGame?.Invoke();
			_allPegs.Shuffle();
			for (int i = 0; i < _allPegs.Count; i++)
			{
				if (i < Settings.RequiredPegCount)
				{
					_allPegs[i].SetPegType(PegType.Required);
				}
				else
				{
					_allPegs[i].SetPegType(PegType.Basic);
				}
			}
			StartGame?.Invoke();
		}

		public void BallLaunched(Ball ball)
		{
			_remainingBalls--;
			OnRemainingBallCountChanged?.Invoke(_remainingBalls);
			ActiveBalls.Add(ball);
			ChangeState(PeggleGameState.Shooting);
		}

		public void BallLeftPlay(Ball ball)
		{
			if (ball && ActiveBalls.Contains(ball))
			{
				ActiveBalls.Remove(ball);
			}
			else
			{
				Debug.LogError("Ball doesn't exist in list.", ball);
			}

			if (ActiveBalls.Count == 0)
			{
				if (_remainingBalls > 0)
				{
					ChangeState(PeggleGameState.PlayerCanShoot);
				}
				else
				{
					ChangeState(PeggleGameState.OutOfBallsFailure);
				}

				RoundEndedClearPegs();
			}
		}

		private void ChangeState(PeggleGameState state)
		{
			_state = state;
			OnGameStateChange?.Invoke(state);
		}

		private void RoundEndedClearPegs()
		{
			_roundManager.StartCoroutine(ClearHitPegs());
		}

		private IEnumerator ClearHitPegs()
		{
			for (var i = 0; i < _hitPegs.Count; i++)
			{
				Peg peg = _hitPegs[i];
				if (peg != null)//with multiball shenanigans or hacks, pegs could get cleared while game is running.
				{
					peg.Clear();
					if (Settings.delayBetweenPegClears > 0)
					{
						yield return new WaitForSeconds(Settings.delayBetweenPegClears);
					}
				}
			}
		}

		
		public void PegLitUp(Peg peg)
		{
			if (!_hitPegs.Contains(peg))
			{
				_hitPegs.Add(peg);
			}
		}

		public void RegisterPeg(Peg peg)
		{
			_allPegs.Add(peg);
		}


		public void BallEnteredBucket(Ball ball)
		{
			_remainingBalls++;
			OnRemainingBallCountChanged?.Invoke(_remainingBalls);
			BallLeftPlay(ball);
		}
	}
}