using System.Collections;
using UnityEngine;

namespace Script.PlayerAttack
{
    public class Attack : MonoBehaviour
    {
        public float MinDamage { get { return minDamage; } }
        public float MaxDamage { get { return maxDamage; } }
        public float fireRate = 0.2f;
        public GameObject hitArea;
        public AudioSource swordSound;

        private float nextFireRate = 0.0f;
        private Animator animator;
        public float minDamage = 0.0f;
        public float maxDamage = 0.0f;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleAttackInput();
        }

        private void HandleAttackInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFireRate)
            {
                nextFireRate = Time.time + fireRate;
                SetAttackAnimation(true);
                swordSound.Play();
                AttackHitArea();
                hitArea.GetComponent<TestProjectile>().damage = Random.Range(minDamage, maxDamage);
            }
            else
            {
                SetAttackAnimation(false);
            }
        }

        private void SetAttackAnimation(bool isAttacking)
        {
            animator.SetBool("Attack", isAttacking);
        }

        private void AttackHitArea()
        {
            StartCoroutine(DelaySlash());
        }

        private IEnumerator DelaySlash()
        {
            yield return new WaitForSeconds(0.15f);
            Instantiate(hitArea, transform.position, transform.rotation);
        }

        public void LevelUp()
        {
            minDamage += 10;
            maxDamage += 10;
        }
    }
}