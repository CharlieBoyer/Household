using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

using Internal;
using Environment;
using UnityEngine.Serialization;
using Wave;

namespace Managers
{
    public class WorldManager : MonoBehaviourSingleton<WorldManager>
    {
        [Header("Skybox Management")]
        public SkyboxBlend skyboxBlendHandler;
        public float transitionThreshold;
        public float transitionDuration;
        
        [Header("Light sources")]
        public Transform sun;
        private Light _sunlight;
        public Transform moon;
        private Light _moonlight;

        [Header("Default Light settings")]
        
        public Color defaultColor;
        public Color cloudyColor;
        public Color rainyColor;

        [Header("VFX Objects")]
        public VisualEffect rainGraph;
        public VisualEffect cloudGraph;

        [Header("Transition delays and VFX values")]
        public float lightTransitionTime;
        public float rainTransitionTime;

        private float _currentCycleProgress;

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
            sun.rotation = Quaternion.identity;
            _sunlight = sun.GetComponent<Light>();
            _moonlight = moon.GetComponent<Light>();
        }

        private void Start() {
            rainGraph.Reinit();
            cloudGraph.Reinit();
            SetWeather(Weather.Clear);
        }
        
        // Blend Skybox

        private IEnumerator BlendSkybox(float targetBlend)
        {
            float t = 0f;
            float currentBlend = skyboxBlendHandler.blendAmount;

            while (t < 1)
            {
                // Calculate the blend amount gradually
                t = (_currentCycleProgress - transitionThreshold) / (1f - transitionThreshold);
                float updatedBlend = Mathf.Lerp(currentBlend, targetBlend, t);

                // Update the blend amount in the SkyboxBlend component
                skyboxBlendHandler.UpdateBlendAmount(updatedBlend);
                yield return null;
            }
        }
        
        private IEnumerator BlendSkyboxStart(float targetBlend)
        {
            float t = 0f;
            float currentBlend = skyboxBlendHandler.blendAmount;

            if (_currentCycleProgress >= transitionThreshold)
            {
                while (t < 1)
                {
                    // Calculate the blend amount gradually
                    t += Time.deltaTime / transitionDuration;
                    float updatedBlend = Mathf.Lerp(currentBlend, targetBlend, t);

                    // Update the blend amount in the SkyboxBlend component
                    skyboxBlendHandler.UpdateBlendAmount(updatedBlend);

                    yield return null;
                }
            }
        }

        // Light Management
        public void RotateLight(CyleState currentState, float cycleProgress)
        {
            float startAngle = currentState == CyleState.Day ? 0f : 180f;
            float endAngle = currentState == CyleState.Day ? 180f : 360f;

            _currentCycleProgress = cycleProgress;

            RenderSettings.sun = currentState == CyleState.Night ? _moonlight : _sunlight;
            StartCoroutine(currentState == CyleState.Day ? BlendSkyboxStart(1f) : BlendSkyboxStart(0f));

            float angle = Mathf.Lerp(startAngle, endAngle, _currentCycleProgress);

            _currentLightRotation.x = angle;
            sun.rotation = Quaternion.Euler(_currentLightRotation);
        }

        private void SetLightColor(Weather weather)
        {
            Color targetColor;
            
            switch (weather)
            {
                case Weather.Clear or Weather.MildlyCloudy:
                    targetColor = defaultColor;
                    break;
                case Weather.Cloudy:
                    targetColor = cloudyColor;
                    break;
                case Weather.Rainy:
                    targetColor = rainyColor;
                    break;
                default:
                      throw new ArgumentOutOfRangeException(nameof(weather), weather, "WorldManager > Invalid Weather state");  
            }
            
            StartCoroutine(TransitionLightColor(targetColor, lightTransitionTime));
        }

        private IEnumerator TransitionLightColor(Color target, float duration)
        {
            float t = 0f;
            Color start = _sunlight.color;
            Color value;

            while (t < 1)
            {
                t += Time.deltaTime / duration;
                value = Color.Lerp(start, target, t);
                _sunlight.color = value;
                yield return null;
            }
        }
        
        // Weather Management
        public void SetWeather(Weather weather)
        {
            bool isRaining = (weather == Weather.Rainy);

            SetLightColor(weather);
            _clouds.UpdateClouds(weather);
            SetRain(isRaining);
        }

        // TODO: [DEV-ONLY]
        public void SetWeatherWrapper(int weather) {
            SetWeather((Weather) weather);
        }
        
        // Rain Management
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
