using System;
using PresentationSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace a
{
	public class PresentationInput : MonoBehaviour
	{
		public InputActionReference nextSlideAction;
		public InputActionReference prevSlideAction;

		private Presentation _presentation;
		private void Awake()
		{
			_presentation = GetComponent<Presentation>();
			nextSlideAction.action.Enable();
			prevSlideAction.action.Enable();
		}

		private void Update()
		{
			if (nextSlideAction.action.WasPerformedThisFrame())
			{
				_presentation.NextSlide();
			}else if (prevSlideAction.action.WasPerformedThisFrame())
			{
				_presentation.PreviousSlide();
			}
		}
	}
}