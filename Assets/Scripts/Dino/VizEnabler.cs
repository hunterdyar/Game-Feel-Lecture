using System;
using UnityEngine;

namespace Peggle.Dino
{
	public class VizEnabler : MonoBehaviour
	{
		private void Update()
		{
			foreach (Transform child in transform)
			{
				child.gameObject.SetActive(DinoSettingsManager.Settings.DistanceToCactus);	
			}
		}
	}
}