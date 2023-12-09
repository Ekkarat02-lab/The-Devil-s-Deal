using System.Collections;
using Script.PlayerAttack;
using UnityEngine;
using UnityEngine.UI;

namespace Script.player
{
    public class PlayerHealth : MonoBehaviour
    {
        public static PlayerHealth instance;  // ตัวแปรสำหรับเก็บอินสแตนซ์เดียวของคลาส

        private Animator animator;

        [Header("ความเสียหายจากศัตรู")]
        public float minDamageInEnemy = 10.0f;
        public float maxDamageInEnemy = 20.0f;

        [Header("สุขภาพของผู้เล่น")]
        public int initialHealth = 100;
        public Text healthText;  // ข้อความบน Canvas
        public Slider healthSlider;

        [Header("เหรียญและเพชร")]
        public int coins;
        public int gems;

        public Text coinsValue;
        public Text gemsValue;

        private int currentHealth;
        public static PlayerHealth playerHealth;  // ตัวแปรที่ไม่ได้ใช้ อาจจะลบได้

        [Header("เกมโอเวอร์")]
        public GameOver gameOverManager;
        private static readonly int Death = Animator.StringToHash("Death");

        private void Awake()
        {
            instance = this;  // กำหนดค่าให้กับตัวแปรอินสแตนซ์
            coins = 0;
        }

        private void Start()
        {
            // กำหนดค่าเริ่มต้นที่จำเป็น
            animator = GetComponent<Animator>();
            currentHealth = initialHealth;

            // กำหนดค่า UI
            healthSlider.maxValue = initialHealth;
            healthSlider.value = initialHealth;
            UpdateHealthText();
        }

        private bool deathAnimationPlayed = false;

        private void Update()
        {
            // ตรวจสอบสุขภาพและทำการทำลายเมื่อสุขภาพน้อยกว่าหรือเท่ากับ 0
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
            // ทำลาย GameObject ของผู้เล่น
            animator.SetTrigger("Death");
            deathAnimationPlayed = true;

            // ปิดการควบคุมผู้เล่นหรือลบ component ที่เกี่ยวข้องตามความต้องการ
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Attack>().enabled = false;

            // รอ 0.5 วินาที ก่อนที่จะทำลาย GameObject
            StartCoroutine(WaitAndDestroy(0.5f));
        }

        private IEnumerator WaitAndDestroy(float waitTime)
        {
            // รอเวลาที่กำหนด
            yield return new WaitForSeconds(waitTime);

            // ปิด GameObject แทนที่จะทำลาย
            gameObject.SetActive(false);
        }

        private void UpdateHealthText()
        {
            // อัปเดตข้อความสุขภาพที่แสดงบน Canvas
            healthText.text = "HEALTH: " + currentHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // ตรวจสอบการชนกับออบเจกต์ที่ต้องการ
            if (other.gameObject.CompareTag("Health"))
            {
                // เพิ่มสุขภาพเมื่อชนกับออบเจกต์ที่มี Tag "Health"
                if (currentHealth < initialHealth)  // ตรวจสอบว่า currentHealth ยังไม่เต็ม
                {
                    int healthToAdd = Mathf.Min(initialHealth - currentHealth, 50);  // คำนวณสุขภาพที่จะเพิ่ม
                    currentHealth += healthToAdd;
                    Destroy(other.gameObject);
                }
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                // ลดสุขภาพเมื่อชนกับศัตรู
                TakeDamage(Random.Range(minDamageInEnemy, maxDamageInEnemy));
            }
            else if (other.gameObject.CompareTag("DeathZone"))
            {
                // ตั้งค่าสุขภาพเป็น 0 เมื่อชนกับ "DeathZone"
                currentHealth = 0;
            }

            // อัปเดตข้อมูล UI
            UpdateHealthText();
        }

        private void TakeDamage(float damage)
        {
            // ลดสุขภาพตามความเสียหายที่ระบุ
            currentHealth = Mathf.Max(0, currentHealth - (int)damage);

            // เรียกใช้ AddExperience เพื่อเพิ่มประสบการณ์ (experience)
            ExperienceManager.Instance?.AddExperience((int)damage);
    
            // อัปเดตข้อมูล UI
            UpdateHealthText();
        }

        public void AddCurrency(CurrencyPickup currency)
        {
            // เพิ่มค่าเงินหรือเพชรตามประเภทของออบเจกต์ที่รับ
            if (currency.currentObject == CurrencyPickup.PickupObject.COIN)
            {
                coins += currency.pickupQuantity;
                //coinsValue.text = "GOLD : " + coins.ToString();
            }
            else if (currency.currentObject == CurrencyPickup.PickupObject.GEM)
            {
                gems += currency.pickupQuantity;
                gemsValue.text = "GEMS : " + gems.ToString();
            }
        }

        public void LevelUp()
        {
            // เพิ่มค่าสุขภาพเมื่อ Level Up
            initialHealth += 50;
            currentHealth = initialHealth;

            // อัปเดต UI สุขภาพ
            healthSlider.maxValue = initialHealth;
            healthSlider.value = initialHealth;
            UpdateHealthText();
        }
    }
}
