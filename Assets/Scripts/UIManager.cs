using System.Collections;
using UnityEngine;
using TMPro;

using Internal;
using UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [Header("UI Content")]
    public TMP_Text gameInfoTextBox;
    public float gameInfoClearDelay;

    [Header("Animation Settings")]
    [SerializeField] private float gameInfoFadeDelay;
    [SerializeField] private float fadeAnimDelay;
    public float FadeAnimDelay => fadeAnimDelay;

    private CycleMeter _cycleMeter;
    private bool _cycleComplete;

    private bool _displayOnce = true;
    private string _gameInfo;
    private float _gameInfoClearCountdown;

    private void Start() {
        _cycleMeter = CycleMeter.Instance;
        _cycleComplete = true;
    }

    // ReSharper disable once MemberCanBePrivate.Global
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

    public void UpdateCycleMeter(TimeManager.CyleState state, float cycleProgress)
    {
        if (_cycleComplete)
            ResetCycleUI(state);
        
        if (cycleProgress >= 1)
        {
            _cycleComplete = true;
            _cycleMeter.SetComplete();
            UpdateGameInfo("Cycle Complete");
        }
        else
        {
            _cycleMeter.slider.value = cycleProgress;
            if (_displayOnce) {
                UpdateGameInfo((state == TimeManager.CyleState.Day) ? "Day Time" : "Night Time");
                _displayOnce = false;
            }
        }
    }

    private void ResetCycleUI(TimeManager.CyleState state)
    {
        if (state == TimeManager.CyleState.Day)
            _cycleMeter.SetSun();
        else
            _cycleMeter.SetMoon();
        
        _gameInfo = "";
        _displayOnce = true;
        _cycleComplete = false;
    }

    private void ResetAutoClearCountdown() {
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
}
