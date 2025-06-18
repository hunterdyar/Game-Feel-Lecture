using UnityEngine;

public class CactusDistanceVisual : MonoBehaviour
{
    public GameObject dino;             // Assign in inspector or via script
    public string cactusTag = "Cactus"; // Tag for all Cactus prefabs
    public float minScale = 0.5f;       // Minimum X scale
    public float maxScale = 3f;         // Maximum X scale
    public float maxDistance = 20f;     // Distance at which scale reaches minScale
    public float minXThreshold = -6.18f; // Ignore cacti left of this X

    private void Update()
    {
        if (dino == null) return;

        GameObject[] cacti = GameObject.FindGameObjectsWithTag(cactusTag);
        if (cacti.Length == 0) return;

        float closestDistance = float.MaxValue;
        Vector3 dinoPos = dino.transform.position;

        foreach (GameObject cactus in cacti)
        {
            float cactusX = cactus.transform.position.x;

            // Ignore cacti to the left of threshold
            if (cactusX < minXThreshold) continue;

            // Calculate horizontal (X-axis) distance
            float distX = Mathf.Abs(dinoPos.x - cactusX);
            if (distX < closestDistance)
            {
                closestDistance = distX;
            }
        }

        // If no valid cactus was found, don't update scale
        if (closestDistance == float.MaxValue) return;

        // Normalize distance and compute scale
        float t = Mathf.Clamp01(1 - (closestDistance / maxDistance));
        float newScaleX = Mathf.Lerp(minScale, maxScale, t);

        // Apply X scale only
        Vector3 localScale = transform.localScale;
        localScale.x = newScaleX;
        transform.localScale = localScale;
    }
}