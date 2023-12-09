using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    // ตัวแปรสำหรับเป็น Singleton
    public static ExperienceManager Instance;

    // สำหรับรับ Event เมื่อมีการเปลี่ยนแปลงค่าประสบการณ์
    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;

    // เช็ค Singleton ในช่วงเวลา Awake
    private void Awake()
    {
        // ถ้า Instance มีค่าแล้วและไม่ใช่ตัวเดียวกัน
        if (Instance != null && Instance != this)
        {
            // ทำลายตัวเอง
            Destroy(this.gameObject);
        }
        else
        {
            // กำหนดตัวเองเป็น Instance ที่ถูกต้อง
            Instance = this;
        }
    }

    // เพิ่มค่าประสบการณ์และเรียก Event ในกรณีที่มีผู้ฟัง
    public void AddExperience(int amount)
    {
        // เรียก Event และส่งค่าประสบการณ์ที่เพิ่มเข้าไป
        OnExperienceChange?.Invoke(amount);
    }
}