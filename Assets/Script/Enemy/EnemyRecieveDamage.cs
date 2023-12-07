using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Enemy
{
    public class EnemyRecieveDamage : MonoBehaviour
    {
        public float health;// = 100
        public float maxhealth;
    
        public GameObject healthBar;
        public Slider  healthBarSlider;

        //public GameObject lootDrop;

        private int expAmount = 100;
        // Start is called before the first frame update
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
        public void Healcharater(float heal)
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
                //Debug.Log($"{gameObject.name} HP : {health}");
                //Debug.Log($"{gameObject.name} Die!");
                ExperienceManager.Instance.AddExperience(expAmount);
                Destroy(gameObject);
                //Instantiate(lootDrop, transform.position, Quaternion.identity);
            }
        }
        private float CalculateHealthPercentage()
        {
            return (health / maxhealth);
        }
    }
}