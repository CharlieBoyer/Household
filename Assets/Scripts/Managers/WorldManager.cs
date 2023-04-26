using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

using Internal;
using Environment;

namespace Managers
{
    public class WorldManager : MonoBehaviourSingleton<WorldManager>
    {
        [Header("Light sources")]
        public Transform sunlight;
        public Transform moonlight;

        [Header("VFX Objects")]
        public VisualEffect rainGraph;
        public VisualEffect cloudGraph;

        [Header("Transition delays and VFX values")]
        public float rainTransitionTime;
        
        private CloudsHandler _clouds;
        private Vector3 _currentLightRotation;

        private const float MaxRainRate = 24f;
        private float RainRate {
            get => rainGraph.GetFloat("RainRate");
            set => rainGraph.SetFloat("RainRate", value);
        }

        private void Awake() {
            _clouds = new CloudsHandler(cloudGraph);
            _currentLightRotation = Quaternion.identity.eulerAngles;
            sunlight.rotation = Quaternion.identity;
        }

        private void Start() {
            rainGraph.Reinit();
            cloudGraph.Reinit();
            SetWeather(Weather.Clear);
        }

        // Light Management
        public void RotateLight(TimeManager.CyleState currentState, float cycleProgress)
        {
            float startAngle = currentState == TimeManager.CyleState.Day ? 0f : 180f;
            float endAngle = currentState == TimeManager.CyleState.Day ? 180f : 360f;

            float angle = Mathf.Lerp(startAngle, endAngle, cycleProgress);

            _currentLightRotation.x = angle;
            sunlight.rotation = Quaternion.Euler(_currentLightRotation);
        }

        public void MoonRise(bool state) {
            moonlight.gameObject.SetActive(state);
        }
        
        // Weather Management
        public void SetWeather(Weather weather)
        {
            bool isRaining = (weather == Weather.Rainy);

            SetRain(isRaining);
            _clouds.UpdateClouds(weather);
        }

        public void SetWeatherWrapper(int weather) {
            SetWeather((Weather) weather);
        }
        
        private void SetRain(bool isRaining)
        {
            StopCoroutine(nameof(SetRain));
            StartCoroutine(TransitionRain(
                (isRaining) ? MaxRainRate : 0,
                rainTransitionTime
            ));
        }

        private IEnumerator TransitionRain(float targetRate, float duration)
        {
            float t = 0f;
            float startingRate = RainRate;
            
            // Break the Coroutine if the VFX should repeat the Rainy state
            if (Math.Abs(RainRate - targetRate) == 0) {
                yield break;
            }

            while (t < 1)
            {
                t += Time.deltaTime / duration;
                RainRate = Mathf.Lerp(startingRate, targetRate, t);
                yield return null;
            }
        }
    }
}
