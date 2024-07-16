using System;
using UnityEngine;

namespace Script.player
{
    public class CharacterController2D : MonoBehaviour
    {
        [Header("Movement Parameters")]
        [SerializeField] private float jumpForce = 400f;
        [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
        [SerializeField] private bool airControl = false;

        [Header("Ground Check")]
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform ceilingCheck;
        [SerializeField] private Collider2D crouchDisableCollider;

        const float GroundedRadius = .2f;
        private bool grounded;
        const float CeilingRadius = .2f;
        private new Rigidbody2D rigidbody2D;

        private Vector3 velocity = Vector3.zero;
        private bool canDoubleJump = false;
        [SerializeField] private int maxDoubleJumps = 1;
        private int currentDoubleJumps = 0;

        public AudioSource walkingSound;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
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

            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GroundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    grounded = true;
            }

            if (grounded)
            {
                canDoubleJump = true;
                currentDoubleJumps = 0;
            }
        }

        public void Move(float move, bool crouch, bool jump)
        {
            if (!crouch && Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround))
            {
                crouch = true;
            }

            if (grounded || airControl)
            {
                Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
                rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
            }

            if (jump)
            {
                if (grounded)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
                    rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                }
                else if (canDoubleJump && currentDoubleJumps < maxDoubleJumps)
                {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
                    rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                    canDoubleJump = false;
                    currentDoubleJumps++;
                }
            }
        }

        public bool IsGrounded()
        {
            float raycastLength = 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength);
            return hit.collider != null;
        }
    }
}
