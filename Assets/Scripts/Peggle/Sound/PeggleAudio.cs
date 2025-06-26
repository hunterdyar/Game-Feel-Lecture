using UnityEngine;

namespace Peggle.Peggle
{
	[RequireComponent(typeof(AudioSource))]
	public class PeggleAudio : MonoBehaviour
	{
		[SerializeField] private PeggleManager _peggleManager;
		private int _streakCount;
		private AudioSource _source;

		[SerializeField] private AudioClip[] _pegHits;
		[SerializeField] private AudioClip _happy;
		[SerializeField] private AudioClip _launch;
		private void Awake()
		{
			_source = GetComponent<AudioSource>();
		}

		#region Event Subscription
		private void OnEnable()
		{
			PeggleManager.OnRoundStart += OnRoundStart;
			Peg.OnPegHit+= OnPegHit;
			Wall.OnHit += OnWallHit;
			Peg.OnPegLoadedIn += OnPegLoadedIn;
			Launcher.OnBallLaunch += OnBallLaunch;
			PeggleManager.OnBallInBucket += OnBallInBucket;
		}
		
		private void OnDisable()
		{
			PeggleManager.OnRoundStart -= OnRoundStart;
			Peg.OnPegHit -= OnPegHit;
			Wall.OnHit -= OnWallHit;
			Peg.OnPegLoadedIn -= OnPegLoadedIn;
			Launcher.OnBallLaunch -= OnBallLaunch;
			PeggleManager.OnBallInBucket -= OnBallInBucket;
		}

		private void OnPegLoadedIn(Peg obj)
		{
			
		}

		private void OnBallInBucket()
		{
			_source.pitch = 1;
			_source.PlayOneShot(_happy);
			_streakCount = 0;
		}
		private void OnBallLaunch(Ball b)
		{
			_source.pitch = 1;
			_source.PlayOneShot(_launch);
			_streakCount = 0;
		}

		private void OnPegHit(Peg peg, bool litUpThisHit)
		{
			if (litUpThisHit)
			{
				_streakCount++;
			}
			PlayPegHitSound();
		}

		private void OnWallHit()
		{
			PlayWallHitSound();
		}

		private void OnRoundStart()
		{
			_streakCount = 0;
		}

		#endregion

		private void PlayWallHitSound()
		{
			if (!PeggleManager.Settings.turnOnSounds)
			{
				return;
			}
			
			_source.pitch = 1;
			_source.PlayOneShot(_pegHits[0]);
		}
		private void PlayPegHitSound()
		{
			if (!PeggleManager.Settings.turnOnSounds)
			{
				return;
			}

			if (!PeggleManager.Settings.raisePitchOnStreak)
			{
				_source.pitch = 1;
				_source.PlayOneShot(_pegHits[0]);
				return;
			}
			
			if (_streakCount < _pegHits.Length)
			{
				_source.pitch = 1;
				_source.PlayOneShot(_pegHits[_streakCount]);
				return;
			}
			else
			{
				var extra = Mathf.Max(5,_streakCount - _pegHits.Length);
				_source.pitch = 1+(extra * 0.05841f);
				_source.PlayOneShot(_pegHits[^1]);
				return;
			}
		}
	}
}