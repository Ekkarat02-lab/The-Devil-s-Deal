using System.Collections;
using UnityEngine;

namespace Script.Enemy
{
    public class BulletController : MonoBehaviour
    {
        public float bulletSpeed = 5f; // ความเร็วของกระสุน
        public float destroyDelay = 3f; // ระยะเวลาก่อนที่กระสุนจะถูกทำลาย

        private Rigidbody2D rb;
        private Transform player;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            // คำนวณทิศทางของกระสุนจากตำแหน่งปัจจุบันไปยังตำแหน่งของ player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;

            StartCoroutine(DestroyBulletAfterDelay());
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }

        IEnumerator DestroyBulletAfterDelay()
        {
            yield return new WaitForSeconds(destroyDelay); // รอเวลาที่กำหนด

            Destroy(gameObject); // ทำลาย GameObject ของกระสุน
        }
    }
}