using Script.player;
using UnityEngine;

namespace Script.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController2D controller;
        public float runSpeed = 40f;
        public Animator animator;

        private float horizontalMove = 0f;
        private bool jump = false;
        private bool crouch = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleMovementInput();
            HandleJumpInput();
            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

        private void HandleMovementInput()
        {
            horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                MoveLeft();
            }
            else if (Input.GetAxis("Horizontal") > 0.1f)
            {
                MoveRight();
            }
        }

        private void HandleJumpInput()
        {
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }

        private void UpdateAnimator()
        {
            animator.SetBool("Grounded", true);
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (jump)
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("jump", false);
            }
        }

        private void MoveLeft()
        {
            transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }

        private void MoveRight()
        {
            transform.Translate(Vector2.right * runSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
    }
}
