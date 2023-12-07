using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Script.ManagerGame
{
    public class Timer : MonoBehaviour
    {
        public float WaitSec;
        private int waitSecInt; //For Text
        public Text text;

        private void FixedUpdate()
        {
            if (WaitSec > 0)
            {
                WaitSec -= Time.fixedDeltaTime;
                waitSecInt = (int)WaitSec;
                text.text = waitSecInt.ToString();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
