using System;
using PresentationSystem.Viewer.SlideViewers;
using UnityEngine;

namespace Peggle
{
	[RequireComponent(typeof(SlideBase))]
	public class PeggleSetOnSlides : MonoBehaviour
	{
		[SerializeField] private PeggleManager _peggleManager;
		private SlideBase _slideBase;
		public PeggleSettings Settings;
		private void Awake()
		{
			_slideBase = GetComponent<SlideBase>();
			_slideBase.OnEnterSlide += OnEnterSlide;
		}

		void OnDestroy()
		{
			_slideBase.OnEnterSlide -= OnEnterSlide;
		}
		
		private void OnEnterSlide()
		{
			_peggleManager.SetSettings(Settings);
		}
	}
}