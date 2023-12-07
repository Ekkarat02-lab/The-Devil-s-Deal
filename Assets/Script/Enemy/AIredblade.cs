using System.Collections;
using UnityEngine;

namespace Script.Enemy
{
    public class AIredblade : MonoBehaviour
    {
        public Transform player;
        public float shootingRange = 15f;
        public GameObject bulletPrefab;
        public float shootInterval = 1f;

        private Animator animator;
        public string attackAnimationTrigger = "Attack";

        private bool isPlayerInRange = false;
        private bool canShoot = true;

        void Start()
        {
            // กำหนดค่าเริ่มต้น
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            if (player != null)
            {
                // คำนวณระยะห่างจาก enemy ถึง player
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
                    // คำนวณทิศทางจาก enemy ไปที่ player
                    Vector2 directionToPlayer = (player.position - transform.position).normalized;
                    float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                    // เรียกใช้ animation Attack
                    animator.SetBool(attackAnimationTrigger, true);
                    StartCoroutine(ShootWithDelay(angle));
                }

                // ถ้า player ไม่อยู่ในระยะและไม่สามารถยิงได้
                if (!isPlayerInRange && !canShoot)
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
        // Coroutine สำหรับการยิงด้วยการรอเวลา
        IEnumerator ShootWithDelay(float bulletAngle)
        {
            canShoot = false; // กำหนดให้ไม่สามารถยิงได้ในขณะนี้

            // สร้างกระสุนที่ตำแหน่งเริ่มต้นของ enemy และให้ทิศทางตามทิศทางที่ตัวละคนหันไป
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, bulletAngle));

            // ทำการหา forward vector ของตัวละคน
            Vector2 forwardVector = transform.right;

            // ทำการปรับทิศทางของกระสุนให้ตรงกับทิศทางที่ตัวละคนหันไป
            bullet.transform.right = forwardVector;

            yield return new WaitForSeconds(shootInterval); // รอเวลาตามที่กำหนด

            canShoot = true; // กำหนดให้สามารถยิงได้อีก
        }

    }
}