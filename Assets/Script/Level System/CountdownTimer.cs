using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Transform playerTransform;
    public Transform enemyTransform;
    public Transform checkpointTransform;
    public float detectionRange = 10f;
    public float countdownTime = 10f;
    public Text countdownText;

    private bool playerInDetectionRange = false;
    private float currentTime;
    private bool isGameOver = false;

    void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("กรุณาเพิ่ม Text UI element ใน Scene และกำหนดให้ countdownText ชี้ไปที่นั้น");
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (playerTransform.gameObject != null)
            {
                playerInDetectionRange = Vector2.Distance(playerTransform.position, enemyTransform.position) <= detectionRange;

                if (playerInDetectionRange)
                {
                    currentTime += Time.deltaTime;

                    if (currentTime >= countdownTime)
                    {
                        // กระทำที่ต้องการเมื่อเวลาเคาดาวน์หมด
                        Debug.Log("เวลาเคาดาวน์หมด!");

                        // แสดง UI Text
                        if (countdownText != null)
                        {
                            countdownText.text = "เย้ ๆชนะแล้ววว";

                            // เพิ่มโค้ดที่ต้องการให้กระทำเมื่อชนะเกม
                            // เช่น เรียกฟังก์ชันที่ทำการจบเกม, โหลด Scene ใหม่, หรือส่งผลการเล่นอื่น ๆ

                            // ล็อคสถานะเกมเป็น Game Over
                            isGameOver = true;
                        }

                        // หยุดเวลา
                        Time.timeScale = 0f;
                    }
                    else
                    {
                        // แสดงเวลาที่เหลือทาง UI
                        if (countdownText != null)
                        {
                            countdownText.text = "เวลาที่เหลือ: " + (countdownTime - currentTime).ToString("F1") + " วินาที";
                        }
                    }
                }
                else
                {
                    currentTime = 0f;

                    if (countdownText != null)
                    {
                        countdownText.text = "รอ Player เข้าในระยะของ Enemy";
                    }
                }
            }
        }
        else
        {
            // แสดง UI Text เมื่อชนะเกม
            if (countdownText != null)
            {
                countdownText.text = "เย้ ๆชนะแล้ววว";
            }

            // แสดง UI Text เมื่อหมดเวลา
            if (countdownText != null)
            {
                countdownText.text = "ฮ่า ๆแล้วพบกันใหม่";
            }
        }
    }

    void RespawnPlayer()
    {
        if (checkpointTransform != null)
        {
            // Respawn player ไปยัง checkpoint
            playerTransform.position = checkpointTransform.position;

            // เปิดให้ Player เป็น Active อีกครั้ง (ถ้า Player มีการ Set Active/Inactive)
            playerTransform.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("ไม่มี checkpointTransform ที่ถูกกำหนด");
        }
    }
}
