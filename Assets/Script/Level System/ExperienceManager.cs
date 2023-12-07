using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    public delegate void ExpeienceChangeHandler(int amount);

    public event ExpeienceChangeHandler OnExperienceChange;
    //Singleton Check
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
}
