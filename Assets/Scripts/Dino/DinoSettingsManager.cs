using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Peggle.Dino
{
	public class DinoSettingsManager : MonoBehaviour
	{
		public static DinoSettings Settings;
		[SerializeField] private DinoSettings _settings;
		private static DinoSettingsManager Instance;
		private void Awake()
		{
			Instance = this;

			if (Settings == null)
			{
				Settings = _settings;
			}

		}

		public static void SetSettings(DinoSettings settings)
		{
			Settings = settings;
			
			//eh, why not?
			if (Instance != null)
			{
				Instance._settings = settings;
			}
		}
	}
}