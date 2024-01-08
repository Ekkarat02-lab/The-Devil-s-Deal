using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.ManagerGame
{
    public class GameOver : MonoBehaviour
    {
        public GameObject gameOverUI;

        public void gameOver()
        {
            gameOverUI.SetActive(true);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
