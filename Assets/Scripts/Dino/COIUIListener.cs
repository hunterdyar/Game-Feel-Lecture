using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Peggle.Dino
{
	public class COIUIListener : MonoBehaviour
	{
		private UIDocument _document;

		void Awake()
		{
			_document = GetComponent<UIDocument>();
		}

		private void Update()
		{
			_document.rootVisualElement.visible = DinoSettingsManager.Settings.ShowCOIUI;
		}
	}
}