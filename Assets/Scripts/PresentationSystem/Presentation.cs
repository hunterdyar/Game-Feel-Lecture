using System;
using System.Collections;
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

		private SlideBase[] _slides;
		private int startingSlide = 0;

		public static Func<SlideChangeEvent, SlideChangeEvent> OnChangeSlide;
		
		private void Awake()
		{
			_slides = new SlideBase[transform.childCount];
			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i);
				_slides[i] = child.GetComponent<SlideBase>();
				if (_slides[i] == null)
				{
					Debug.LogError($"All Children of Presentation should be a slide. {child.name} is not.", child);
				}
				child.gameObject.SetActive(false);
			}

			_currentSlide = startingSlide;
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
			if (_currentSlide >= _slides.Length)
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
				_currentSlide = _slides.Length-1;
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