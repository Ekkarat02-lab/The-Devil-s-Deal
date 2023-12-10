using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmeyWinner : MonoBehaviour
{
    public Text winnerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayWinner(string winnerMessage)
    {
        winnerText.text = winnerMessage;
    }
}
