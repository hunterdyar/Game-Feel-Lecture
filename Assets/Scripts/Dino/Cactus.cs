using System;
using Peggle.Dino;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public float resetX = 10f;
    public float offscreenX = -10f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.enabled = DinoSettingsManager.Settings.VisibleCactus;
        transform.position += Vector3.left * (DinoSettingsManager.Settings.moveSpeed * Time.deltaTime);

        if (transform.position.x < offscreenX)
        {
            Destroy(this.gameObject);
        }
    }
}