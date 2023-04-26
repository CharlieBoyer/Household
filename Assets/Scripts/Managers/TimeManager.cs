using System.Collections;
using Internal;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class TimeManager : MonoBehaviourSingleton<TimeManager>
    {
        public enum CyleState {
            Day, Night
        }

        [Header("Time cycle settings")]
        public float dayDuration;
        public float nightDuration;
        public float timeMulitplier;

        private CyleState _currentState;

        public void StartDayTime() {
            _currentState = CyleState.Day;
            WorldManager.Instance.MoonRise(false);
            StartCoroutine(CycleTime());
        }
    
        public void StartNightTime() {
            _currentState = CyleState.Night;
            WorldManager.Instance.MoonRise(true);
            StartCoroutine(CycleTime());
        }

        private IEnumerator CycleTime()
        {
            float progress = 0f;
            float timer = 0f;
            
            UIManager.EnableTestsButttons(false);

            while (progress < 1)
            {
                timer += Time.deltaTime * timeMulitplier;
                progress = timer / (_currentState == CyleState.Day ? dayDuration : nightDuration);
                UIManager.Instance.UpdateCycleMeter(_currentState, progress);
                WorldManager.Instance.RotateLight(_currentState, progress);
                yield return null;
            }

            UIManager.EnableTestsButttons(true);
        }
    }
}
