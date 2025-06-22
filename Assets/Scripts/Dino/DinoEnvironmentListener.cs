using System;
using UnityEngine;

namespace Peggle.Dino
{
	public class DinoEnvironmentListener : MonoBehaviour
	{
		private int visibleEnvironment;

		private void Start()
		{
			if (DinoSettingsManager.Settings == null)
			{
				SelectEnvironment(0);
			}
			else
			{
				SelectEnvironment(DinoSettingsManager.Settings.environmentSelector);
			}
		}

		private void Update()
		{
			if (DinoSettingsManager.Settings == null)
			{
				return;
			}

			if (visibleEnvironment != DinoSettingsManager.Settings.environmentSelector)
			{
				SelectEnvironment(DinoSettingsManager.Settings.environmentSelector);
			}
		}

		private void SelectEnvironment(int settingsEnvironmentSelector)
		{
			visibleEnvironment = settingsEnvironmentSelector;
			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i);
				child.gameObject.SetActive(i == settingsEnvironmentSelector);
			}
		}
	}
}