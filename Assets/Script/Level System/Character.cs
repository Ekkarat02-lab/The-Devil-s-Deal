using UnityEngine;
using UnityEngine.UI;
using Script.Player;
using Script.PlayerAttack;

namespace Script.Level_System
{
    public class Character : MonoBehaviour
    {
        public Text experienceText;
        public Text levelText;

        [SerializeField] private int currentHealth, maxHealth, currentExperience, maxExperience, currentLevel;

        private void Start()
        {
            EnsureExperienceManagerInstance();

            if (levelText == null || experienceText == null)
            {
                Debug.LogError("Please assign the 'levelText' and 'experienceText' references in the inspector.");
                return;
            }

            currentExperience = 0;
            UpdateUI();

            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
            }
        }

        private void OnEnable()
        {
            EnsureExperienceManagerInstance();

            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
            }
        }

        private void OnDisable()
        {
            if (ExperienceManager.Instance != null)
            {
                ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
            }
        }

        private void HandleExperienceChange(int newExperience)
        {
            currentExperience += newExperience;

            if (currentExperience >= maxExperience)
            {
                LevelUp();
            }

            UpdateUI();
        }

        private void LevelUp()
        {
            currentLevel++;
            Debug.Log("Level up! Current level: " + currentLevel);

            UpdateUI();

            maxHealth += 10;
            currentHealth = maxHealth;

            currentExperience = 0;
            maxExperience += 100;

            PlayerHealth.instance.LevelUp();

            var attackScript = GetComponent<Attack>();
            if (attackScript != null)
            {
                attackScript.LevelUp();
            }
        }

        private void UpdateUI()
        {
            if (levelText != null && experienceText != null)
            {
                levelText.text = "LEVEL: " + currentLevel;
                experienceText.text = "EXP: " + currentExperience + " / " + maxExperience;
            }
        }

        private void EnsureExperienceManagerInstance()
        {
            if (ExperienceManager.Instance == null)
            {
                Debug.LogWarning("ExperienceManager.Instance is null. Creating a new instance.");
                var experienceManagerObject = new GameObject("ExperienceManager");
                experienceManagerObject.AddComponent<ExperienceManager>();
            }
        }
    }
}