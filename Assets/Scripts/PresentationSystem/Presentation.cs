using System;
using System.Collections;
using System.Collections.Generic;
using PresentationSystem.Viewer.SlideViewers;
using UnityEngine;

namespace PresentationSystem
{
	public class Presentation : MonoBehaviour
	{
		public PresentationSettings PresentationSettings => _presentationSettings;
		[SerializeField] private PresentationSettings _presentationSettings;

		private int _currentSlide;
		private SlideBase _visibleSlide;

		private List<SlideBase> _slides;
		private int startingSlide = 0;

		public static Func<SlideChangeEvent, SlideChangeEvent> OnChangeSlide;
		
		private void Awake()
		{
			//must be.
			transform.tag = "SlideGroup";
			_slides = new List<SlideBase>();
			AddSlideInitiate(transform);
			_currentSlide = startingSlide;
		}

		private void AddSlideInitiate(Transform t)
		{
			var s = t.GetComponent<SlideBase>();
			if (!s)
			{
				for (int i = 0; i < t.childCount; i++)
				{
					var child = t.GetChild(i);
					AddSlideInitiate(child);
				}
			}
			else
			{
				_slides.Add(s);
				s.gameObject.SetActive(false);

			}
		}

		void Start()
		{
			StartCoroutine(GoToSlide(_slides[startingSlide]));
		}

		[ContextMenu("Next Slide")]
		public void NextSlide()
		{
			
			if (OnChangeSlide != null)
			{
				var changeEvent = new SlideChangeEvent(1, _slides[_currentSlide]);
				changeEvent = OnChangeSlide(changeEvent);
				if (changeEvent.Consumed)
				{
					return;
				}
			}
			
			_currentSlide++;
			if (_currentSlide >= _slides.Count)
			{
				_currentSlide = 0;
			}

			StartCoroutine(GoToSlide(_slides[_currentSlide]));
		}
		
		public void PreviousSlide()
		{
			_currentSlide--;

			if (_currentSlide < 0)
			{
				_currentSlide = _slides.Count -1;
			}
			StartCoroutine(GoToSlide(_slides[_currentSlide]));
		}

		private IEnumerator GoToSlide(SlideBase slide)
		{
			if (_visibleSlide != null)
			{
				yield return StartCoroutine(_visibleSlide.ExitSlide());
				_visibleSlide.gameObject.SetActive(false);
			}
			_visibleSlide = slide;
			_visibleSlide.gameObject.SetActive(true);
			yield return StartCoroutine(_visibleSlide.EnterSlide());
		}
	}
}