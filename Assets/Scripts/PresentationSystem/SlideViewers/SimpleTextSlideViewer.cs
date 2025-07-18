using System;
using System.Collections;
using PresentationSystem;
using PresentationSystem.Viewer.SlideViewers;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SimpleTextSlideViewer : SlideBase
{
	[TextArea]
	public string Text;
	private Label _label;
	
	private void OnEnable()
	{
		SetDefaultBackgroundColor();
		_label = _uiDocument.rootVisualElement.Q<Label>("Header");
		_label.text = Text;
		_label.style.color = Settings.DefaultTextColor;
	}

	public override IEnumerator EnterSlide()
	{
		OnEnterSlide?.Invoke();
		yield break;
	}

	public override IEnumerator ExitSlide()
	{
		yield break;
	}
}
