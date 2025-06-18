using UnityEngine;

public class Cactus : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float resetX = 10f;
    public float offscreenX = -10f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < offscreenX)
        {
            Destroy(this.gameObject);
        }
    }
}