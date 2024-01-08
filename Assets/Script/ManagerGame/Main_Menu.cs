using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.ManagerGame
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainmenu;
        
        [SerializeField] private GameObject option;
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);
        }

        public void Option()
        {
            option.SetActive(true);
        }

        public void BackToMainMenu()
        {
            option.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
