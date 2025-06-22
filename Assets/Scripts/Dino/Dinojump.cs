using Peggle.Dino;
using UnityEngine;
using UnityEngine.Serialization;

public class DinoJump : MonoBehaviour
{
    public float jumpHeight = 3f;
    public float jumpDuration = 0.6f;
    public Vector3 squashScale = new Vector3(1.2f, 0.8f, 1f);
    public Vector3 stretchScale = new Vector3(0.8f, 1.2f, 1f);
    
    public AudioClip jumpSound;
    public AudioClip landSound;

    public AudioSource audioSource;

    private bool isJumping = false;
    private float jumpTimer = 0f;
    private Vector3 startPos;
    private Vector3 peakPos;
    private Vector3 endPos;
    private Vector3 originalScale;
    public GameObject particles;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        originalScale = transform.localScale;
    }

    public void SpawnParticles()
    {
        if (DinoSettingsManager.Settings.ParticlesOnLand)
        {
            Instantiate(particles, new Vector3(transform.position.x, -2.95f, transform.position.z),
                Quaternion.identity);
        }
    }

    void Update()
    {
        spriteRenderer.enabled = DinoSettingsManager.Settings.VisibleDino;
        var squash = DinoSettingsManager.Settings.SquashStretchDino;
        // if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        // {
        //     StartJump();
        // }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float progress = jumpTimer / jumpDuration;

            if (progress < 0.5f)
            {
                // Ascending with ease-out
                float eased = EaseOutQuad(progress * 2f);
                transform.position = Vector3.Lerp(startPos, peakPos, eased);
                if (squash)
                {
                    transform.localScale = Vector3.Lerp(originalScale, stretchScale, eased);
                }
            }
            else if (progress < 1f)
            {
                // Descending with ease-in
                float eased = EaseInQuad((progress - 0.5f) * 2f);
                transform.position = Vector3.Lerp(peakPos, endPos, eased);
                if (squash)
                {
                    transform.localScale = Vector3.Lerp(stretchScale, squashScale, eased);
                }
            }
            else
            {
                // Landing complete
                SpawnParticles();
                GetComponent<AudioSource>().PlayOneShot(landSound);
                transform.position = endPos;
                transform.localScale = originalScale;
                isJumping = false;
            }
        }
    }

    public void StartJump()
    {
        audioSource.PlayOneShot(jumpSound);
        isJumping = true;
        jumpTimer = 0f;
        startPos = transform.position;
        peakPos = new Vector3(startPos.x, startPos.y + jumpHeight, startPos.z);
        endPos = startPos;
    }

    // Easing functions
    float EaseOutQuad(float t) => 1f - (1f - t) * (1f - t);
    float EaseInQuad(float t) => t * t;
}
