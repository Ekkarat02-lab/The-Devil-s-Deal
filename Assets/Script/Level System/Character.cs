// สคริปต์ Character จัดการเกี่ยวกับระบบเลเวล, ประสบการณ์, และเส้นชีวิตของผู้เล่น.

// Import namespace ที่จำเป็นของ Unity และสคริปต์
using UnityEngine;
using UnityEngine.UI;
using Script.player;
using Script.PlayerAttack; // Import สคริปต์ PlayerHealth.

namespace Script.Level_System
{
    public class Character : MonoBehaviour
    {
        // อ้างอิงไปยัง UI elements.
        public Text experienceText;
        public Text levelText;

        // ตัวแปรสำหรับเส้นชีวิตและประสบการณ์.
        [SerializeField] private int currentHealth, maxHealth, currentExperience,
            maxExperience, currentLevel;

        private void Start()
        {
            // ตรวจสอบและสร้าง instance ของ ExperienceManager หากยังไม่มี.
            // นี้จะให้การติดตามประสบการณ์ของผู้เล่น
            if (ExperienceManager.Instance == null)
            {
                Debug.LogWarning("ExperienceManager.Instance is null. Creating a new instance.");
                GameObject experienceManagerObject = new GameObject("ExperienceManager");
                experienceManagerObject.AddComponent<ExperienceManager>();
            }

            // ตรวจสอบว่า UI elements ถูกกำหนดหรือไม่.
            if (levelText != null && experienceText != null)
            {
                currentExperience = 0; // ตั้งค่าประสบการณ์เริ่มต้น
                UpdateUI();
            }
            else
            {
                Debug.LogError("Please assign the 'levelText' and 'experienceText' references in the inspector.");
            }

            // ให้แน่ใจว่า ExperienceManager.Instance ไม่เป็น null
            // ลงทะเบียนเพื่อรับ event การเปลี่ยนแปลงประสบการณ์
            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
            }
        }

        // จัดการกับการเปลี่ยนแปลงในประสบการณ์
        private void HandleExperienceChange(int newExperience)
        {
            currentExperience += newExperience;

            // เลเวลอัพเกรดหากประสบการณ์ปัจจุบันเกินประสบการณ์สูงสุด
            if (currentExperience >= maxExperience)
            {
                LevelUp();
            }

            UpdateUI(); // อัพเดท UI ทันทีที่มีการเปลี่ยนแปลง
        }

        // เลเวลอัพเกรด
        private void LevelUp()
        {
            currentLevel++;
            Debug.Log("Level up! Current level: " + currentLevel);

            UpdateUI();

            maxHealth += 10;
            currentHealth = maxHealth;

            currentExperience = 0;
            maxExperience += 100; // ปรับ maxExperience ตามต้องการ

            // เรียกใช้ LevelUp() ใน PlayerHealth
            PlayerHealth.instance.LevelUp();

            // เรียกใช้ LevelUp() ใน Attack script
            Attack AttackScript = GetComponent<Attack>();
            if (AttackScript != null)
            {
                AttackScript.LevelUp();
            }
        }

        // เมื่อเปิดใช้งาน script และลงทะเบียนเพื่อรับ event OnExperienceChange
        private void OnEnable()
        {
            if (ExperienceManager.Instance == null)
            {
                Debug.LogWarning("ExperienceManager.Instance is null. Creating a new instance.");
                GameObject experienceManagerObject = new GameObject("ExperienceManager");
                experienceManagerObject.AddComponent<ExperienceManager>();
            }

            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
            }
        }

        // เมื่อปิดใช้งาน script และยกเลิกการลงทะเบียนจาก event OnExperienceChange
        private void OnDisable()
        {
            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
            }
        }

        // อัพเดท UI elements
        private void UpdateUI()
        {
            if (levelText != null && experienceText != null)
            {
                levelText.text = "LEVEL : " + currentLevel;
                experienceText.text = "EXP : " + currentExperience + " / " + maxExperience;
            }
        }
    }
}