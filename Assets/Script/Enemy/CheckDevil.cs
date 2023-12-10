using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDevil : MonoBehaviour
{
    public EmeyWinner displayWinner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
