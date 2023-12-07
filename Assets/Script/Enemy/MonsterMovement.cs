using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 0.8f;
    public float range = 3;

    private float startingX;
    private int dir = 1;

    // Scale information
    private Vector3 originalScale;
    private Vector3 flippedScale;

    public Transform playertransform;
    public bool isChasing;
    public float chaseDistance;

    // Animator
    private Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        startingX = transform.position.x;
        originalScale = transform.localScale;
        flippedScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        // รับค่า Animator component จาก GameObject
        animator = GetComponent<Animator>();

        isChasing = false;  // เพิ่มบรรทัดนี้เพื่อให้ isChasing เป็น false เมื่อเริ่มต้น
    }

    private void FixedUpdate()
    {
        if (playertransform != null)
        {
            if (isChasing)
            {
                if (transform.position.x > playertransform.position.x)
                {
                    Flip(1);
                    transform.position += Vector3.left * speed * Time.deltaTime;
                }
                if (transform.position.x < playertransform.position.x)
                {
                    Flip(-1);
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }

                // เล่น animation ในสถานะการตามหาผู้เล่น
                animator.SetFloat(Speed, 1f);
            }
            else
            {
                if (Vector2.Distance(transform.position, playertransform.position) < chaseDistance)
                {
                    isChasing = true;
                }
                else
                {
                    isChasing = false;
                    animator.SetFloat(Speed, 0f);
                }

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
}
