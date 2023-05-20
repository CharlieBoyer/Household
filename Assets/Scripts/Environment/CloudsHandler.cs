using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

using Managers;

namespace Environment
{
    public class CloudsHandler
    {
        private readonly WorldManager _world;
        private readonly VisualEffect _graph;

        private float SpawRate {
            get => _graph.GetFloat("Rate");
            set => _graph.SetFloat("Rate", value);
        }
        private float Darkening {
            get => _graph.GetFloat("Darkening");
            set => _graph.SetFloat("Darkening", value);
        }

        public CloudsHandler(VisualEffect graph)
        {
            if (graph == null)
                throw new Exception("CloudHandler > Unable to initialize/Invalid VFX Graph");

            _graph = graph;
            _world = WorldManager.Instance;
        }

        // Prefered handling method based on Weather state
        public void UpdateClouds(Weather weather)
        {
            switch (weather)
            {
                case Weather.Clear:
                    SetCloudState(0, 1, 0, 10);
                    break;
                case Weather.MildlyCloudy:
                    SetCloudState(0.5f, 5, -20, 10);
                    break;
                case Weather.Cloudy:
                    SetCloudState(60, 10, -60, 5);
                    break;
                case Weather.Rainy:
                    SetCloudState(100, 7, -100, 5);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weather), weather, "CloudsHandler > Invalid Weather state");
            }
        }

        // Manual cloud state setter
        private void SetCloudState(float targetRate, float rateTime, float targetDarkening, float darkeningTime)
        {
            StopCurrentCloudTransition();
            _world.StartCoroutine(SetSpawnRate(targetRate, rateTime));
            _world.StartCoroutine(SetDarkening(targetDarkening, darkeningTime));
        }

        private void StopCurrentCloudTransition()
        {
            _world.StopCoroutine(nameof(SetSpawnRate));
            _world.StopCoroutine(nameof(SetDarkening));
        }
        
        private IEnumerator SetSpawnRate(float target, float duration)
        {
            float t = 0f;
            float start = SpawRate;
            
            // Break the Coroutine if the VFX should switch to the same state
            if (Math.Abs(SpawRate - target) == 0) {
                yield break;
            }

            while (t < 1)
            {
                    t += Time.deltaTime / duration;
                    SpawRate = Mathf.Lerp(start, target, t);
                    yield return null;
            }
        }
        
        private IEnumerator SetDarkening(float target, float duration)
        {
            float t = 0f;
            float start = Darkening;
            
            // Break the Coroutine if the VFX should switch to the same state
            if (Math.Abs(Darkening - target) == 0) {
                yield break;
            }

            while (t < 1)
            {
                t += Time.deltaTime / duration;
                Darkening = Mathf.Lerp(start, target, t);
                yield return null;
            }
        }
    }
}
