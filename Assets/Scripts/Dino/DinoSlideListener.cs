using System;
using PresentationSystem.Viewer.SlideViewers;
using UnityEngine;

namespace Peggle.Dino
{
	[RequireComponent(typeof(SlideBase))]
	public class DinoSlideListener : MonoBehaviour
	{
		[SerializeField] private DinoSettings _settings;
		private SlideBase _slideBase;
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
			DinoSettingsManager.SetSettings(_settings);
		}
	}
}