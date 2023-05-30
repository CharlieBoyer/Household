using System;
using System.Collections;
using UnityEngine;

using Internal;
using Wave;
using Foes;

namespace Managers
{
    public class WaveManager : MonoBehaviourSingleton<WaveManager>
    {
        [Header("Time cycle settings")]
        public float globalTimeMulitplier;
        public float interStateDelay;
        
        [Header("Foe Managament")]
        public FoeSpawner spawner;

        private WaveObject _currentWave;
        private CyleState _currentState;
        private WaitForSeconds _interStateDelay;

        private void Awake()
        {
            _interStateDelay = new WaitForSeconds(interStateDelay);
        }

        public void StartCycle(WaveObject wave)
        {
            StartCoroutine(StartCycleCoroutine(wave));
        }
        
        private void StartWave()
        {
            spawner.SpawnWave(_currentWave);
        }

        private IEnumerator StartCycleCoroutine(WaveObject wave)
        {
            _currentWave = wave;
            
            _currentState = CyleState.Day;
            yield return StartCoroutine(CycleTime());
            
            yield return _interStateDelay;
            
            _currentState = CyleState.Night;
            StartWave();
            yield return StartCoroutine(CycleTime());
            
        }

        private IEnumerator CycleTime()
        {
            float progress = 0f;
            float timer = 0f;
            float duration = (_currentState == CyleState.Day ? _currentWave.startTime : _currentWave.duration);
            
            UIManager.EnableTestsButttons(false);

            while (progress < 1)
            {
                timer += Time.deltaTime * globalTimeMulitplier;
                progress = timer / duration;
                UIManager.Instance.UpdateCycleMeter(_currentState, progress);
                WorldManager.Instance.RotateLight(_currentState, progress);

                yield return null;
            }

            UIManager.EnableTestsButttons(true);
        }
    }
}
