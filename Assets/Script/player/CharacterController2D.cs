// CharacterController2D script handles 2D character movement and jumping.

using System;
using UnityEngine;

namespace Script.player
{
    public class CharacterController2D : MonoBehaviour
    {
        [Header("Movement Parameters")]
        [SerializeField] private float jumpForce = 400f; // พารามิเตอร์สำหรับกำหนดแรงกระโดด
        //[Range(0, 1)] [SerializeField] private float crouchSpeed = .36f; // พารามิเตอร์สำหรับความเร็วขณะย่อตัว
        [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; // พารามิเตอร์สำหรับการทำให้การเคลื่อนที่นุ่มนวล
        [SerializeField] private bool airControl = false; // การควบคุมอากาศ

        [Header("Ground Check")]
        [SerializeField] private LayerMask whatIsGround; // แสดงถึงว่าพื้นที่ใดถือว่าเป็นพื้น
        [SerializeField] private Transform groundCheck; // ตำแหน่งที่ใช้ตรวจสอบว่าตัวละครอยู่บนพื้นหรือไม่
        [SerializeField] private Transform ceilingCheck; // ตำแหน่งที่ใช้ตรวจสอบว่าตัวละครมีหลังคาหรือไม่
        [SerializeField] private Collider2D crouchDisableCollider; // Collider ที่ใช้ปิดการใช้งานเมื่อตัวละครย่อตัว

        const float GroundedRadius = .2f; // รัศมีสำหรับตรวจสอบว่าตัวละครอยู่บนพื้นหรือไม่
        private bool grounded;
        const float CeilingRadius = .2f; // รัศมีสำหรับตรวจสอบว่าตัวละครมีหลังคาหรือไม่
        private new Rigidbody2D rigidbody2D;
        private bool facingRight = true; // ตัวแปรเก็บข้อมูลทิศทางที่ตัวละครหันไป

        private Vector3 velocity = Vector3.zero; // เวกเตอร์สำหรับเก็บความเร็วของการเคลื่อนที่

        private bool canDoubleJump = false; // ตัวแปรที่บอกว่าตัวละครสามารถ double jump ได้หรือไม่
        [SerializeField] private int maxDoubleJumps = 1; // จำนวนครั้งที่สามารถ double jump ได้
        private int currentDoubleJumps = 0; // จำนวน double jump ที่ทำไปแล้ว
        
        public AudioSource walkingSound;

        private Vector3 lastPos;

        private void Start()
        {
            
            //audioSource.loop = true;
        }

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>(); // ดึงคอมโพเนนต์ Rigidbody2D ของตัวละคร
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                walkingSound.Play();
            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                walkingSound.Stop();
            }
            
        }

        private void FixedUpdate()
        {
            grounded = false;

            // ตรวจสอบว่าตัวละครอยู่บนพื้นหรือไม่
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    grounded = true;
            }

            // หากตัวละครอยู่บนพื้น
            if (grounded)
            {
                canDoubleJump = true; // สามารถ double jump ได้
                currentDoubleJumps = 0; // รีเซ็ตจำนวน double jump ที่ทำไปแล้ว
            }
        }

        // ฟังก์ชันที่ใช้สำหรับเคลื่อนที่ตัวละคร
        public void Move(float move, bool crouch, bool jump)
        {
            
            // ตรวจสอบว่าตัวละครย่อตัวและมีหลังคาหรือไม่
            if (!crouch && Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround))
            {
                crouch = true;
            }

            // หากตัวละครยืนอยู่บนพื้นหรือสามารถควบคุมอากาศได้
            if (grounded || airControl)
            {
                

                // กำหนดความเร็วที่ตัวละครจะเคลื่อนที่
                Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
                rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

                // หากตัวละครเคลื่อนที่ไปทางขวาและไม่ไปทางขวา
                if (move > 0 && !facingRight)
                {
                    Flip(); // สลับทิศทาง
                }
                // หากตัวละครเคลื่อนที่ไปทางซ้ายและไม่ไปทางซ้าย
                else if (move < 0 && facingRight)
                {
                    Flip(); // สลับทิศทาง
                }
                
                
            }

            // เปลี่ยนจากการตรวจสอบ jump เพื่อให้สามารถ double jump ได้
            if (jump)
            {
                // หากตัวละครยืนอยู่บนพื้น
                if (grounded)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f); // รีเซ็ตความเร็วในแนวดิ่ง
                    rigidbody2D.AddForce(new Vector2(0f, jumpForce)); // กระโดด
                }
                // หากตัวละครสามารถ double jump ได้และยังไม่ได้ทำ double jump ครบตามจำนวนที่กำหนด
                else if (canDoubleJump && currentDoubleJumps < maxDoubleJumps)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f); // รีเซ็ตความเร็วในแนวดิ่ง
                    rigidbody2D.AddForce(new Vector2(0f, jumpForce)); // กระโดด
                    canDoubleJump = false; // ไม่สามารถ double jump ได้อีก
                    currentDoubleJumps++; // เพิ่มจำนวน double jump ที่ทำไปแล้ว
                }
            }
        }

        // ฟังก์ชันสำหรับสลับทิศทางของตัวละคร
        private void Flip()
        {
            facingRight = !facingRight; // สลับค่าของตัวแปรทิศทาง
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale; // สลับทิศทางของตัวละคร
        }
        
        public bool IsGrounded()
        {
            // คุณอาจต้องปรับแต่งตัวแปรต่าง ๆ เช่น raycastLength ตามโครงสร้างของเกมของคุณ
            float raycastLength = 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength);

            // ถ้า raycast ตัดพื้น, แสดงว่าตัวละครอยู่บนพื้น
            return hit.collider != null;
        }
    }
}
