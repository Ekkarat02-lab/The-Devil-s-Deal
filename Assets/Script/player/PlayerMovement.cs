using UnityEngine;

namespace Script.player
{
    public class PlayerMovement : MonoBehaviour {

        public CharacterController2D controller;

        public float runSpeed = 40f;

        float horizontalMove = 0f;
        bool jump = false;
        bool crouch = false;

        public Animator animator;

        private void Start()
        {
            // กำหนด animator จาก GameObject นี้
            animator = this.gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // ตั้งค่า Animator สำหรับ Grounded (อยู่บนพื้น) และ Speed (ความเร็วในแนวนอน)
            animator.SetBool("Grounded", true); // จุดตรวจสอบ Grounded
            animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal"))); // เคลื่อนที่ตำแหน่ง Animation

            // หมุนฉันทำการเคลื่อนที่ไปทางซ้ายหรือขวาและตั้งค่าความเร็ว
            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 180);
            }
            else if (Input.GetAxis("Horizontal") > 0.1f)
            {
                transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector2(0, 0);
            }

            // ตรวจสอบการกดปุ่ม Jump และตั้งค่า Animator สำหรับการกระโดด
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("jump", false);
            }
        }

        // FixedUpdate จะถูกเรียกทุกครั้งที่ทำการอัปเดตโดย Physics
        void FixedUpdate()
        {
            // ย้ายตัวละคร
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

    }
}