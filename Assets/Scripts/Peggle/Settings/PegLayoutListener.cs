using System;
using UnityEngine;

namespace Peggle
{
	public class PegLayoutListener : MonoBehaviour
	{
		[SerializeField] private PegLayout pegLayout;
		public void Awake()
		{
			var enabled = PeggleManager.Settings.pegLayout == pegLayout;
			foreach (Transform child in transform)
			{
				child.gameObject.SetActive(enabled);
			}
		}
	}
}