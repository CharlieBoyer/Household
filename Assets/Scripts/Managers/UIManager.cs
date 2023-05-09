using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Internal;
using UI;
using Wave;

namespace Managers
{
    public class UIManager : MonoBehaviourSingleton<UIManager>
    {
        [Header("Sub-Components")] public CycleMeter cycleMeter;
        public HomeStatus homeStatus;

        [Header("General UI")] public TMP_Text gameInfoTextBox;
        public float gameInfoClearDelay;

        [Header("Animation Settings")] [SerializeField]
        private float gameInfoFadeDelay;

        [SerializeField] private float fadeAnimDelay;
        public float FadeAnimDelay => fadeAnimDelay;

        private bool _cycleComplete;
        private bool _displayOnce = true;
        private string _gameInfo;
        private float _gameInfoClearCountdown;

        private void Start()
        {
            _cycleComplete = true;
        }

        //TODO: [DEV ONLY] Test buttons
        public static void EnableTestsButttons(bool state)
        {
            GameObject.Find("StartDay").GetComponent<Button>().interactable = state;
            GameObject.Find("StartNight").GetComponent<Button>().interactable = state;
        }

        // GameInfo
        public void UpdateGameInfo(string text = "")
        {
            _gameInfo = text;

            if (gameInfoTextBox.text != _gameInfo)
            {
                ResetAutoClearCountdown();
                StartCoroutine(UpdateGameInfoFadeText());
                StartCoroutine(AutoClearGameInfo());
            }
        }

        private void ResetAutoClearCountdown()
        {
            _gameInfoClearCountdown = gameInfoClearDelay;
        }

        private IEnumerator UpdateGameInfoFadeText()
        {
            float t = 0f;
            Color fadeColor = gameInfoTextBox.color;

            while (t < 1)
            {
                t += Time.deltaTime / gameInfoFadeDelay;
                fadeColor.a = Mathf.Lerp(1, 0, t);
                gameInfoTextBox.color = fadeColor;
                yield return null;
            }

            t = 0f;
            gameInfoTextBox.text = _gameInfo;

            while (t < 1)
            {
                t += Time.deltaTime / gameInfoFadeDelay;
                fadeColor.a = Mathf.Lerp(0, 1, t);
                gameInfoTextBox.color = fadeColor;
                yield return null;
            }
        }

        private IEnumerator AutoClearGameInfo()
        {
            while (_gameInfoClearCountdown > 0)
            {
                _gameInfoClearCountdown -= Time.deltaTime;
                yield return null;
            }

            UpdateGameInfo();
        }

        // CycleMeter Wrapper
        public void UpdateCycleMeter(CyleState state, float cycleProgress)
        {
            if (_cycleComplete)
                ResetCycleUI(state);

            if (cycleProgress >= 1)
            {
                _cycleComplete = true;
                cycleMeter.SetComplete();
                UpdateGameInfo("Cycle Complete");
            }
            else
            {
                cycleMeter.slider.value = cycleProgress;
                if (_displayOnce)
                {
                    UpdateGameInfo((state == Wave.CyleState.Day) ? "Day Time" : "Night Time");
                    _displayOnce = false;
                }
            }
        }

        private void ResetCycleUI(CyleState state)
        {
            if (state == Wave.CyleState.Day)
                cycleMeter.SetSun();
            else
                cycleMeter.SetMoon();

            _gameInfo = "";
            _displayOnce = true;
            _cycleComplete = false;
        }
    }
}
