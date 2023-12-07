using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.NextScenes
{
    public class Main_Menu : MonoBehaviour
    {
        public GameObject mainmenu;
        [SerializeField] private GameObject option;        public void PlayGame()
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
