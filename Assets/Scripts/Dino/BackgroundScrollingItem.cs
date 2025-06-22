using System;
using UnityEngine;

namespace Peggle.Dino
{
	public class BackgroundScrollingItem : MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;
		[SerializeField] private float _bgScrollingModifier = 1;
		private void Awake()
		{
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		}
		
		private float offscreenX = -10f;
		private void Update()
		{
			transform.position += Vector3.left * (DinoSettingsManager.Settings.moveSpeed * _bgScrollingModifier* Time.deltaTime);

			if (transform.position.x < offscreenX)
			{
				Destroy(this.gameObject);
			}
		}
	}
}