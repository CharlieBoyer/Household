using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Internal;
using UI;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [Header("Cycle & Time UI Elements")]
    public Slider cycleSlider;
    public Image cycleSliderBar;
    public Image cycleSliderFill;
    public Image cycleIcon;

    [Header("UI Colors")]
    public Color sliderBarDefaultColor;
    public Color sliderFillDayColor;
    public Color sliderFillNightColor;
    public Color sunIconColor;
    public Color moonIconColor;

    [Header("UI Content")]
    public TMP_Text generalTextBox;
    public Sprite sunIcon;
    public Sprite moonIcon;

    [Header("Animation Settings")]
    public float gameInfoFadeDelay;

    private bool _cycleComplete;

    private string _gameInfo;
    private float _gameInfoDisplayTimer;

    private void Start()
    {
        cycleIcon.enabled = false;
        CycleIcon.Instance.FadeOut();
        cycleSliderFill.enabled = false;
        cycleSliderBar.color = sliderBarDefaultColor;
        
        _cycleComplete = true;
    }

    private void Update() {
        UpdateGameInfo();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public void UpdateGameInfo(string text = "")
    {
        if (!string.IsNullOrEmpty(text))
            _gameInfo = text;

        if (generalTextBox.text != _gameInfo)
            StartCoroutine(FadeGameInfoText());
    }

    public void UpdateCycleMeter(TimeManager.CyleState state, float cycleProgress)
    {
        if (_cycleComplete)
            ResetCycleUI(state);
        
        if (cycleProgress >= 1)
        {
            _cycleComplete = true;
            CycleIcon.Instance.FadeOut();
            cycleSliderFill.enabled = false;
            cycleSliderBar.color = sliderBarDefaultColor;
            
            _gameInfo = "Cycle Complete";
        }
        else
        {
            cycleSlider.value = cycleProgress;
            _gameInfo = (state == TimeManager.CyleState.Day) ? "Day Time" : "Night Time";
        }
        
        UpdateGameInfo();
    }

    private void ResetCycleUI(TimeManager.CyleState state)
    {
        CycleIcon.Instance.FadeIn();
        cycleIcon.sprite = (state == TimeManager.CyleState.Day) ? sunIcon : moonIcon;
        cycleIcon.color = (cycleIcon.sprite == sunIcon) ? sunIconColor : moonIconColor;
        cycleSliderFill.enabled = true;
        cycleSliderBar.color = sliderBarDefaultColor;
        cycleSliderFill.color = (cycleIcon.sprite == sunIcon) ? sliderFillDayColor : sliderFillNightColor;

        _gameInfo = "";
        _cycleComplete = false;
    }
    
    private IEnumerator FadeGameInfoText()
    {
        float t = 0f;
        Color fadeColor = generalTextBox.color;

        while (t < 1)
        {
            t += Time.deltaTime / gameInfoFadeDelay;
            fadeColor.a = Mathf.Lerp(1, 0, t);
            generalTextBox.color = fadeColor;
            yield return null;
        }

        t = 0f;
        generalTextBox.text = _gameInfo;

        while (t < 1)
        {
            t += Time.deltaTime / gameInfoFadeDelay;
            fadeColor.a = Mathf.Lerp(0, 1, t);
            generalTextBox.color = fadeColor;
            yield return null;
        }
    }
}
