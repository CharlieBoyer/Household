using System;
using System.Collections;
using Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using UI;

namespace Managers
{
    public class SceneHandler : MonoBehaviourSingleton<SceneHandler>
    {
        public Animator animator;
        private AudioManager _audioManager;

        [Header("Transitions GameObjects")]
        public TransitionCollection transitionCollection;
        public float transitionDuration;

        private WaitForSeconds _transitionTime;
        private static readonly int StartState = Animator.StringToHash("Start");

        private void Start()
        {
            _audioManager = AudioManager.Instance;
            if (_audioManager == null) {
                throw new Exception("Error: AudioManager not found/incorrectly attached to SceneLoader");
            }

            _transitionTime = new WaitForSeconds(transitionDuration);
        }

        public void LoadGame()
        {
            StartCoroutine(LoadGameWithTransition());
        }

        public void ExitGame()
        {
            if (animator.name != "Crossfade")
            {
                animator.gameObject.SetActive(false);
                transitionCollection.Get("Crossfade").SetActive(true);
                transitionCollection.Get("Crossfade").GetComponentInChildren<CanvasGroup>().alpha = 0;
                animator = transitionCollection.Get("Crossfade").GetComponent<Animator>();
            }

            Time.timeScale = 1; // Reset TimeScale in case of a call from Pause()
            StartCoroutine(ExitWithTransition());
        }

        private IEnumerator LoadGameWithTransition()
        {
            animator.SetTrigger(StartState);
            _audioManager.FadeSound();

            yield return _transitionTime;

            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        private IEnumerator ExitWithTransition()
        {
            animator.SetTrigger(StartState);
            _audioManager.FadeSound();

            yield return _transitionTime;

            EditorApplication.ExitPlaymode(); // Remove before clean build
            Application.Quit();
        }
    }
}
