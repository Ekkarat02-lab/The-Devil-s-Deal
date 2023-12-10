using UnityEngine;
using UnityEngine.UI;
using Script.player;

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
                // ตรวจสอบว่า player ยังมีชีวิต
                PlayerHealth playerHealth = PlayerHealth.instance;
                if (playerHealth != null)
                {
                    // เพิ่มประสบการณ์ให้ player เมื่อ enemy ตาย
                    ExperienceManager.Instance.AddExperience(expAmount);

                    // ทำลาย enemy
                    Destroy(gameObject);
                }
            }
        }

        private float CalculateHealthPercentage()
        {
            return (health / maxhealth);
        }
    }
}