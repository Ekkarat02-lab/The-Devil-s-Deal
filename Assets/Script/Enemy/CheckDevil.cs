using UnityEngine;

namespace Script.Enemy
{
    public class CheckDevil : MonoBehaviour
    {
        public EmeyWinner displayWinner;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // ตรวจสอบว่ามี object enemy ใน Scene หรือไม่
                GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

                if (enemy != null)
                {
                    // มี object enemy อยู่
                    // เรียกใช้ฟังก์ชัน DisplayWinner() และส่งข้อความ "Winner"
                    displayWinner.DisplayWinner("Winner");
                }

            }
        }
    }
}
