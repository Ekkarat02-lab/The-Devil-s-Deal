using UnityEngine;

public class SlashBlade : MonoBehaviour
{
    public float speed = 6f;
    public float secondsToDestroy = 0.15f;

    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        // Move the object to the right
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Check if it should be destroyed
        if (Time.time - startTime >= secondsToDestroy)
        {
            Destroy(gameObject);
        }
    }
}