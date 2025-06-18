using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace PresentationSystem.Viewer.SlideViewers
{
	public class TwoLineTextSlideViewer : SlideBase
	{
		[TextArea]
		public string TitleText;
		[TextArea]
		public string SubtitleText;
		private Label _title;
		private Label _subtitle;

		private void OnEnable()
		{
			SetDefaultBackgroundColor();
			_title = _uiDocument.rootVisualElement.Q<Label>("Header");
			_subtitle = _uiDocument.rootVisualElement.Q<Label>("Subheader");
			_title.text = TitleText;
			_subtitle.text = SubtitleText;
			_title.style.color = Settings.DefaultTextColor;
			_subtitle.style.color = Settings.DefaultTextColor;
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
}