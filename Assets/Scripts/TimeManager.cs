using System;
using System.Collections;
using UnityEngine;

using Internal;
using UnityEngine.UI;

public class TimeManager : MonoBehaviourSingleton<TimeManager>
{
    public enum CyleState {
        Day, Night
    }
    
    [Header("Light sources")]
    public Transform sunlight;
    public Transform moonlight;

    [Header("Time cycle settings")]
    public float dayDuration;
    public float nightDuration;
    public float timeMulitplier;

    private CyleState _currentState;
    private Vector3 _currentRotation;

    private void Start() {
        sunlight.rotation = Quaternion.identity;
    }

    private void Update()
    {
    }

    public void StartDayTime() {
        _currentState = CyleState.Day;
        StartCoroutine(CycleTime());
    }
    
    public void StartNightTime() {
        _currentState = CyleState.Night;
        StartCoroutine(CycleTime());
    }
    
    private IEnumerator CycleTime()
    {
        float progress = 0f;
        float timer = 0f;
        
        GameObject.Find("StartDay").GetComponent<Button>().interactable = false;
        GameObject.Find("StartNight").GetComponent<Button>().interactable = false;

        while (progress < 1)
        {
            timer += Time.deltaTime * timeMulitplier;
            progress = timer / (_currentState == CyleState.Day ? dayDuration : nightDuration);
            UIManager.Instance.UpdateCycleMeter(_currentState, progress);
            yield return null;
        }

        GameObject.Find("StartDay").GetComponent<Button>().interactable = true;
        GameObject.Find("StartNight").GetComponent<Button>().interactable = true;
    }


}
