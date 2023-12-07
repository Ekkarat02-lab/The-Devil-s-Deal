using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.NextScenes
{
    public class NextScenes : MonoBehaviour
    {
        public string sceneName;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(LoadSceneGame(sceneName));
            }
        }

        public IEnumerator LoadSceneGame(string sceneName)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            loadOperation.allowSceneActivation = false;
            while (!loadOperation.isDone)
            {
                float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
                Debug.Log((progress * 100).ToString("n0") + "%");

                if (progress == 1f)
                {
                    loadOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}