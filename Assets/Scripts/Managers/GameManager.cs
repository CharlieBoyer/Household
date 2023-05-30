using System.Collections.Generic;
using Internal;
using UnityEngine;

using Wave;

namespace Managers
{
    public class GameManager: MonoBehaviourSingleton<GameManager>
    {
        [Header("Game composition")]
        public List<WaveObject> waves;

        [HideInInspector] public bool readyToStart;

        private int _remainingActiveFoes;
        
        private void FixedUpdate()
        {
            _remainingActiveFoes = GameObject.Find("FoeContainer").transform.childCount;
        }

        public void StartGame()
        {
            readyToStart = true;
            
            if (readyToStart)
            {
                foreach (WaveObject wave in waves)
                {
                    WaveManager.Instance.StartCycle(wave);
                    WaveCompleted();
                }
            }
        }

        private void WaveCompleted()
        {
            float timer = 5f;

            while (_remainingActiveFoes > 0)
            {
                // Wait for finishing the wave or until HomeManager stops the game
            }

            UIManager.Instance.UpdateGameInfo("Wave Complete !");
            while (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }

        public void GameOver()
        {
            
        }
    }
}
