using System.Collections;
using UnityEngine;

public abstract class AIBase : MonoBehaviour
{
    public float detectionRadius = 15f;
    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    public float moveSpeed = 3f;
    public float stopDistance = 2f;

    protected Animator animator;
    protected bool isPlayerInRange = false;
    protected bool canShoot = true;
    protected Transform player;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        isPlayerInRange = false;
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                player = hit.transform;
                isPlayerInRange = true;
                break;
            }
        }

        if (player == null)
        {
            SetAnimationState(0f, false);
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (isPlayerInRange && distanceToPlayer > stopDistance)
        {
            MoveTowardsPlayer();
        }
        else
        {
            SetAnimationState(0f, false);

            if (isPlayerInRange && canShoot)
            {
                SetAnimationState(0f, true);
                StartCoroutine(ShootWithDelay());
            }
            else
            {
                SetAnimationState(0f, false);
            }
        }
    }

    protected void MoveTowardsPlayer()
    {
        SetAnimationState(moveSpeed, false);
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            Flip();
        }
    }

    protected void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    protected void SetAnimationState(float speed, bool isAttacking)
    {
        animator.SetFloat("Speed", speed);
        animator.SetBool("Attack", isAttacking);
    }

    protected abstract IEnumerator ShootWithDelay();

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}