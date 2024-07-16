using System.Collections;
using Script.ManagerGame;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth instance;

        private Animator animator;

        [Header("Attack For Enemy")]
        public float minDamageInEnemy = 10.0f;
        public float maxDamageInEnemy = 20.0f;

        [Header("Health")]
        public int initialHealth = 100;
        public Text healthText;
        public Slider healthSlider;

        private int currentHealth;

        [Header("GameOver")]
        public GameOver gameOverManager;
        private static readonly int Death = Animator.StringToHash("Death");

        private bool deathAnimationPlayed = false;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            currentHealth = initialHealth;
            healthSlider.maxValue = initialHealth;
            healthSlider.value = initialHealth;
            UpdateHealthText();
        }

        private void Update()
        {
            if (currentHealth <= 0 && !deathAnimationPlayed)
            {
                currentHealth = 0;
                DestroyPlayer();
                gameOverManager.gameOver();
            }
            else
            {
                healthSlider.value = currentHealth;
            }
        }

        private void DestroyPlayer()
        {
            animator.SetTrigger("Death");
            deathAnimationPlayed = true;
            StartCoroutine(WaitAndDestroy(1f));
        }

        private IEnumerator WaitAndDestroy(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            gameObject.SetActive(false);
        }

        private void UpdateHealthText()
        {
            healthText.text = "HEALTH: " + currentHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Health"))
            {
                if (currentHealth < initialHealth)
                {
                    int healthToAdd = Mathf.Min(initialHealth - currentHealth, 50);
                    currentHealth += healthToAdd;
                    Destroy(other.gameObject);
                }
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                TakeDamage(Random.Range(minDamageInEnemy, maxDamageInEnemy));
            }
            else if (other.gameObject.CompareTag("DeathZone"))
            {
                currentHealth = 0;
            }

            UpdateHealthText();
        }

        private void TakeDamage(float damage)
        {
            if (currentHealth > 0)
            {
                currentHealth = Mathf.Max(0, currentHealth - (int)damage);

                if (currentHealth > 0)
                {
                    UpdateHealthText();
                }
                else
                {
                    ExperienceManager.Instance?.AddExperience((int)damage);
                }
            }
        }

        public void LevelUp()
        {
            initialHealth += 50;
            currentHealth = initialHealth;
            healthSlider.maxValue = initialHealth;
            healthSlider.value = initialHealth;
            UpdateHealthText();
        }
    }
}
