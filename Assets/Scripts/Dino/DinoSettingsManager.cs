using System;
using UnityEngine;

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
			Settings = _settings;
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