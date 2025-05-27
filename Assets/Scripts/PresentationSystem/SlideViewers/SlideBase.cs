using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace PresentationSystem.Viewer.SlideViewers
{
	public abstract class SlideBase : MonoBehaviour
	{
		[HideInInspector, SerializeField] protected Presentation _presentation;
		public PresentationSettings Settings => _presentation.PresentationSettings;
		protected UIDocument _uiDocument;
		protected virtual void Awake()
		{
			_uiDocument = gameObject.GetComponent<UIDocument>();
			if (!_presentation)
			{
				_presentation = gameObject.GetComponentInParent<Presentation>();
			}
		}

		public abstract IEnumerator EnterSlide();

		public abstract IEnumerator ExitSlide();
		private void OnValidate()
		{
			_presentation = gameObject.GetComponentInParent<Presentation>();
		}

		protected void SetDefaultBackgroundColor()
		{
			SetBackgroundColor(Settings.DefaultBackgroundColor);
		}

		protected void SetBackgroundTransparent()
		{
			SetBackgroundColor(new Color(0,0,0,0));
		}
		protected void SetBackgroundColor(Color color)
		{
			_uiDocument.rootVisualElement.style.backgroundColor = color;
		}
	}
}