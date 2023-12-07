using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.NextScenes
{
    public class Regame : MonoBehaviour
    {
        public void ResetTheGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            print("The button is working.");
        }
    }
}
