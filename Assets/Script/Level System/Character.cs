using UnityEngine;
using UnityEngine.UI;

namespace Script.Level_System
{
    public class Character : MonoBehaviour
    {
        public Text levelText;

        [SerializeField] private int currentHealth, maxHealth, currentExperience, 
            maxExperience, currentLevel;

        private void Start()
        {
            // กำหนด Level เริ่มต้นที่นี่ (ตัวอย่างเช่น Level 1)
            currentLevel = 1;

            // ตรวจสอบให้แน่ใจว่า ExperienceManager.Instance ไม่เป็น null
            if (ExperienceManager.Instance == null)
            {
                Debug.LogError("ExperienceManager.Instance is null. Creating a new instance.");
                GameObject experienceManagerObject = new GameObject("ExperienceManager");
                experienceManagerObject.AddComponent<ExperienceManager>();
            }

            // ตรวจสอบให้แน่ใจว่า levelText ถูกกำหนดให้ชี้ไปที่ Text UI ที่ถูกต้อง
            if (levelText != null)
            {
                levelText.text = "LEVEL : " + currentLevel;
            }
            else
            {
                Debug.LogError("Please assign the 'levelText' reference in the inspector.");
            }

            // ตรวจสอบให้แน่ใจว่า ExperienceManager.Instance ไม่เป็น null อีกครั้ง
            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
            }
        }
        private void HandleExperienceChange(int newExperience)
        {
            currentExperience += newExperience;
            Debug.Log($"Current XP : {currentExperience}");

            if (currentExperience >= maxExperience)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            currentLevel++;
            Debug.Log("Level up! Current level: " + currentLevel);

            // อัพเดท Text UI ที่แสดง Level
            if (levelText != null)
            {
                levelText.text = "LEVEL : " + currentLevel;
            }

            maxHealth += 10;
            currentHealth = maxHealth;

            currentExperience = 0;
            // ปรับให้ maxExperience เพิ่มขึ้นตามความต้องการ
            maxExperience += 100/* Your desired value */;
        }

        private void OnEnable()
        {
            // ตรวจสอบให้แน่ใจว่า ExperienceManager.Instance ไม่เป็น null
            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
            }
            else
            {
                Debug.LogError("ExperienceManager.Instance is null. Creating a new instance.");
                GameObject experienceManagerObject = new GameObject("ExperienceManager");
                experienceManagerObject.AddComponent<ExperienceManager>();
            }
        }

        private void OnDisable()
        {
            // ตรวจสอบให้แน่ใจว่า ExperienceManager.Instance ไม่เป็น null
            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
            }
        }
        private void UpdateUI()
        {
            // ตรวจสอบให้แน่ใจว่า levelText ถูกกำหนดให้ชี้ไปที่ Text UI ที่ถูกต้อง
            if (levelText != null)
            {
                levelText.text = "LEVEL : " + currentLevel;
            }
        }
    }
}

