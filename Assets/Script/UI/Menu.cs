using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.UI
{
    public class Menu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}

