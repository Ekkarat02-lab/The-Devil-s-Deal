using UnityEngine;
using UnityEngine.UI;

namespace Script.Enemy
{
    public class EnemyRecieveDamage : MonoBehaviour
    {
        public float health;
        public float maxhealth;

        public GameObject healthBar;
        public Slider healthBarSlider;

        private int expAmount = 100;

        void Start()
        {
            health = maxhealth;
        }

        public void DealDamage(float damage)
        {
            healthBar.SetActive(true);
            health = health - damage;
            CheckDeath();
            healthBarSlider.value = CalculateHealthPercentage();
        }

        public void HealCharacter(float heal)
        {
            health += heal;
            CheckOverheal();
            healthBarSlider.value = CalculateHealthPercentage();
        }

        private void CheckOverheal()
        {
            if (health > maxhealth)
            {
                health = maxhealth;
            }
        }

        private void CheckDeath()
        {
            if (health <= 0)
            {
                ExperienceManager.Instance.AddExperience(expAmount);
                Destroy(gameObject);
            }
        }

        private float CalculateHealthPercentage()
        {
            return (health / maxhealth);
        }
    }
}
