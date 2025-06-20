using System;
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
			Peg.OnPegLoadedIn += OnPegLoadedIn;
			Launcher.OnBallLaunch += OnBallLaunch;
			PeggleManager.OnBallInBucket += OnBallInBucket;
		}

	

		private void OnDisable()
		{
			PeggleManager.OnRoundStart += OnRoundStart;
			Peg.OnPegHit += OnPegHit;
			Peg.OnPegLoadedIn += OnPegLoadedIn;
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

		private void OnPegHit(Peg obj)
		{
			PlayPegHitSound();
			_streakCount++;
		}

		private void OnRoundStart()
		{
			_streakCount = 0;
		}

		#endregion


		private void PlayPegHitSound()
		{
			if (_streakCount < _pegHits.Length)
			{
				_source.pitch = 1;
				_source.PlayOneShot(_pegHits[_streakCount]);
				Debug.Log((_streakCount));
			}
			else
			{
				var extra = Mathf.Max(5,_streakCount - _pegHits.Length);
				_source.pitch = 1+(extra * 0.05841f);
				_source.PlayOneShot(_pegHits[^1]);
			}
		}
	}
}