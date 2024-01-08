// สคริปต์ Attack ควบคุมกลไกการโจมตีของผู้เล่น.

// Import namespace ที่จำเป็นของ Unity และสคริปต์

using System.Collections;
using UnityEngine;

namespace Script.PlayerAttack
{
    public class Attack : MonoBehaviour
    {
        // คุณสมบัติสาธารณะสำหรับควบคุมพารามิเตอร์การโจมตี
        public float MinDamage { get { return minDamage; } }
        public float MaxDamage { get { return maxDamage; } }
        public float fireRate = 0.2f;

        // ตัวแปรส่วนตัวสำหรับการจัดการเวลาการโจมตีและแอนิเมชัน
        private float nextFireRate = 0.0f;
        private Animator animator;

        // การอ้างอิงถึงออบเจกต์พื้นที่ที่โดนโจมตี
        public GameObject hitArea;

        public AudioSource swordSound;

        // คุณสมบัติ minDamage และ maxDamage
        public float minDamage = 0.0f;
        public float maxDamage = 0.0f;

        // เมื่อสคริปต์เริ่มต้นการทำงาน
        private void Start()
        {
            // กำหนดค่าเริ่มต้นให้กับคอมโพเนนต์แอนิเมเตอร์เพื่อควบคุมแอนิเมชัน
            animator = GetComponent<Animator>();
        }

        // เมื่อเรียกใช้ทุกเฟรม
        private void Update()
        {
            // ตรวจสอบการป้อนคำสั่งการโจมตี
            HandleAttackInput();
        }

        // เมธอดที่ใช้ในการจัดการการป้อนคำสั่งการโจมตี
        private void HandleAttackInput()
        {
            // ตรวจสอบว่าป้อนคำสั่งการโจมตีถูกเรียกและผ่านเวลาคูลดาวน์การโจมตี
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFireRate)
            {
                // ปรับปรุงเวลาคูลดาวน์การโจมตีครั้งถัดไป
                nextFireRate = Time.time + fireRate;

                // เริ่มแอนิเมชันการโจมตี
                SetAttackAnimation(true);
                swordSound.Play();

                // ทำการโจมตีและตั้งค่าความเสียหายสุ่มให้กับพื้นที่ที่โดนโจมตี
                AttackHitArea();
                hitArea.GetComponent<TestProjectile>().damage = Random.Range(minDamage, maxDamage);
            }
            else
            {
                // หากไม่มีการป้อนคำสั่งการโจมตี หยุดแอนิเมชันการโจมตี
                SetAttackAnimation(false);
            }
        }

        // เมธอดที่ใช้ในการตั้งค่าสถานะแอนิเมชันการโจมตี
        private void SetAttackAnimation(bool isAttacking)
        {
            animator.SetBool("Attack", isAttacking);
        }

        // เมธอดที่ใช้ในการเริ่มการโจมตีและจัดการความล่าช้า
        private void AttackHitArea()
        {
            // เริ่มคอรูทีนเพื่อทำให้มีความล่าช้าก่อนที่จะดำเนินการโจมตี
            StartCoroutine(DelaySlash());
        }

        // คอรูทีนที่ใช้ในการมีความล่าช้าก่อนทำการโจมตี
        private IEnumerator DelaySlash()
        {
            // รอเป็นเวลาสั้น ๆ ก่อนที่จะทำการโจมตี
            yield return new WaitForSeconds(0.15f);

            // สร้างออบเจกต์พื้นที่ที่โดนโจมตีที่ตำแหน่งและหมุนของผู้เล่น
            Instantiate(hitArea, transform.position, transform.rotation);
        }

        // เมธอดที่ใช้ในการเพิ่มความเสียหายเมื่อ Level Up
        public void LevelUp()
        {
            minDamage += 10;
            maxDamage += 10;
        }
    }
}
