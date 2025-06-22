using System;
using PresentationSystem.Viewer.SlideViewers;
using UnityEngine;

namespace PresentationSystem.Listeners
{
	[RequireComponent(typeof(SlideBase))]
	public class SlideBackgroundColor : MonoBehaviour
	{
		private SlideBase _slideBase;
		[SerializeField] private Color _backgroundColor;
		private void Awake()
		{
			_slideBase = GetComponent<SlideBase>();
			_slideBase.OnEnterSlide += OnEnterSlide;
		}

		private void OnDestroy()
		{
			_slideBase.OnEnterSlide -= OnEnterSlide;
		}

		private void OnEnterSlide()
		{
			_slideBase.SetBackgroundColor(_backgroundColor);
		}
	}
}