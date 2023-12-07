using System.Collections;
using UnityEngine;

public class AIenemy : MonoBehaviour
{
    // ตัวแปรที่เกี่ยวข้องกับการยิง
    public Transform player; // ตำแหน่งของผู้เล่น
    public float shootingRange = 15f; // ระยะทางที่ enemy สามารถยิงได้
    public GameObject bulletPrefab; // โปรเจคไบ prefab ของกระสุน
    public Transform firePoint; // ตำแหน่งที่กระสุนจะถูกยิง
    public float shootInterval = 1f; // ระยะเวลาระหว่างการยิง

    // ตัวแปรที่เกี่ยวข้องกับ Animation
    private Animator animator;
    public string attackAnimationTrigger = "Attack"; // Trigger สำหรับเรียกใช้ Animation การโจมตี

    // ตัวแปรสถานะ
    private bool isPlayerInRange = false; // ใช้เพื่อตรวจสอบว่า player อยู่ในระยะการยิงหรือไม่
    private bool canShoot = true; // ใช้เพื่อตรวจสอบว่า enemy สามารถยิงได้หรือไม่

    void Start()
    {
        // กำหนดค่าเริ่มต้น
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // ตรวจสอบระยะห่างจาก enemy ถึง player
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            // ตรวจสอบว่า player อยู่ในระยะยิงหรือไม่
            if (distanceToPlayer <= shootingRange)
            {
                isPlayerInRange = true;
            }
            else
            {
                isPlayerInRange = false;
            }
            // ถ้า player อยู่ในระยะและสามารถยิงได้
            if (isPlayerInRange && canShoot)
            {
                // เรียกใช้ animation Attack
                animator.SetBool(attackAnimationTrigger, true);
                StartCoroutine(ShootWithDelay());
            }
            // ถ้า player ไม่อยู่ในระยะและไม่สามารถยิงได้
            if (!isPlayerInRange & !canShoot)
            {
                // ปิด animation Attack
                animator.SetBool(attackAnimationTrigger, false);
            }
        }
        else
        {
            // ถ้า player เสียหายหรือไม่มีอยู่
            animator.SetBool(attackAnimationTrigger, false);
        }
    }

    // Coroutine สำหรับการยิงด้วยการรอเวลา
    IEnumerator ShootWithDelay()
    {
        canShoot = false; // กำหนดให้ไม่สามารถยิงได้ในขณะนี้

        // สร้างกระสุนที่ firePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        yield return new WaitForSeconds(shootInterval); // รอเวลาตามที่กำหนด

        canShoot = true; // กำหนดให้สามารถยิงได้อีก
    }
}
