using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 0.8f;
    public float range = 3f;
    public float detectionRadius = 5f; // Radius for detecting the player

    private float startingX;
    private int dir = 1;

    // Scale information
    private Vector3 originalScale;
    private Vector3 flippedScale;

    private Transform playerTransform;
    private bool isChasing;

    // Animator
    private Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        startingX = transform.position.x;
        originalScale = transform.localScale;
        flippedScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        // Get Animator component from GameObject
        animator = GetComponent<Animator>();

        isChasing = false;
    }

    private void FixedUpdate()
    {
        DetectPlayer();

        if (isChasing && playerTransform != null)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void DetectPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerTransform = hit.transform;
                isChasing = true;
                return;
            }
        }

        // If no player found within detection radius, stop chasing
        playerTransform = null;
        isChasing = false;
    }

    private void ChasePlayer()
    {
        if (playerTransform == null) return;

        if (transform.position.x > playerTransform.position.x)
        {
            Flip(1);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (transform.position.x < playerTransform.position.x)
        {
            Flip(-1);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // Play chase animation
        animator.SetFloat(Speed, 1f);
    }

    private void Patrol()
    {
        animator.SetFloat(Speed, 0f);

        transform.Translate(Vector2.left * speed * Time.deltaTime * dir);

        if (transform.position.x < startingX - range)
        {
            ChangeDirection(-1);
        }
        else if (transform.position.x > startingX + range)
        {
            ChangeDirection(1);
        }
    }

    private void ChangeDirection(int newDir)
    {
        dir = newDir;
        Flip(newDir);
    }

    private void Flip(int flipValue)
    {
        transform.localScale = flipValue == 1 ? originalScale : flippedScale;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the Scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}