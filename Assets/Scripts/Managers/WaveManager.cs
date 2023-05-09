using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Internal;
using Wave;

namespace Managers
{
    public class WaveManager : MonoBehaviourSingleton<WaveManager>
    {
        [Header("Time cycle settings")]
        public float dayDuration;
        public float nightDuration;
        public float timeMulitplier;

        [Header("Starting Waves")]
        public List<WaveObject> waves;

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
