using System;
using UnityEngine;

public class AutoJump : MonoBehaviour
{
    public DinoJump dino;
    public Sprite dinoScare;
    public Animator animator;

    private void Awake()
    {
        dino = GetComponent<DinoJump>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("Cactus", true); // Trigger animation
        dino.StartJump();
        dino.GetComponent<SpriteRenderer>().sprite = dinoScare;

        // Reset the bool after a short delay (e.g., 0.5 seconds)
        Invoke(nameof(ResetCactusTrigger), 0.25f);
    }

    private void ResetCactusTrigger()
    {
        animator.SetBool("Cactus", false);
    }
}