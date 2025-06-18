using System;
using System.Collections;
using PresentationSystem;
using PresentationSystem.Viewer.SlideViewers;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SimpleImageSlideViewer : SlideBase
{
	public UnityEngine.Texture2D Image;
	private VisualElement _img;
	
	private void OnEnable()
	{
		SetDefaultBackgroundColor();
		_img = _uiDocument.rootVisualElement.Q<VisualElement>("Image");
		_img.style.backgroundImage = Image;
	}

	public override IEnumerator EnterSlide()
	{
		yield break;
	}

	public override IEnumerator ExitSlide()
	{
		yield break;
	}
}
