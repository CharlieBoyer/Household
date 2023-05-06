using System;
using System.Collections;
using Internal;
using UnityEngine;
using UnityEngine.UI;

using Managers;

namespace UI
{
    public class HomeStatus: MonoBehaviour
    {
        [Header("UI Fields")]
        public Slider gauge;
        public Image fillbar;
        public Image icon;

        [Header("Gauge parameters")]
        public int gaugeMaxSize;
        public Gradient gaugeGradient;
        public Color maxIncreaseBlinkColor;
        
        [Header("Animation")]
        public float gaugeGrowthDuration;
        public float durabilityChangeDelay;
        
        private Coroutine _undergoingUpdate;
        private RectTransform _gaugeRect;
        private float _criticalThreshold;
        private const int GaugeChunkSize = 50;

        private static bool startUpdate = true;

        private void Start() {
            _gaugeRect = gauge.gameObject.GetComponent<RectTransform>();
            _criticalThreshold = gaugeGradient.colorKeys[0].time;
        }

        // Update Gauge
        public void UpdateDurability(float targetClampedAmount, bool byPassThresholdWarning)
        {
            if (targetClampedAmount is < 0 or > 1)
                Debug.LogWarning("Warning: UpdateDurability to unclampled amount");
            else if (Math.Abs(targetClampedAmount - gauge.value) == 0 && !startUpdate)
                return;

            if (_undergoingUpdate != null)
                StopAllCoroutines();
            
            _undergoingUpdate = StartCoroutine(UpdateDurabilityOverTime(targetClampedAmount, byPassThresholdWarning));
            startUpdate = false;
        }

        private IEnumerator UpdateDurabilityOverTime(float target, bool byPassThresholdWarning)
        {
            float t = 0f;
            float start = gauge.value;
            bool once = true;

            // StartCoroutine(target < _currentDurabilityPercentage ? DamageOverTime() : RepairOvertTime());
            while (t < 1)
            {
                t += Time.deltaTime / durabilityChangeDelay;
                gauge.value = Mathf.Lerp(start, target, t);
                fillbar.color = CurrentGaugeColor();
                if (gauge.value <= _criticalThreshold && once && !byPassThresholdWarning)
                {
                    UIManager.Instance.UpdateGameInfo("Warning: Durability Low");
                    StartCoroutine(BlinkOverTime(-1, 0.5f, Color.white, Color.red));
                    once = false;
                }
                yield return null;
            }
            
            _undergoingUpdate = null;
        }

        // Max Gauge
        public void IncreaseMaxGaugeSize(float additionalChunks)
        {
            Vector2 currentSize = _gaugeRect.sizeDelta;
            int additionalSize = Mathf.RoundToInt(additionalChunks * GaugeChunkSize);
            int targetSize = Mathf.RoundToInt(currentSize.x + additionalSize);
            
            if (Math.Abs(gaugeMaxSize - currentSize.x) == 0)
                return;
            
            if (currentSize.x < gaugeMaxSize && targetSize > gaugeMaxSize) {
                targetSize = Mathf.Clamp(targetSize, 0, gaugeMaxSize);
            }

            StartCoroutine(IncreaseMaxGaugeOverTime(targetSize));
        }

        private IEnumerator IncreaseMaxGaugeOverTime(float targetSize)
        {
            float t = 0f;
            float value = _gaugeRect.sizeDelta.x;

            Coroutine crt = StartCoroutine(BlinkOverTime(gaugeGrowthDuration, 0.05f, maxIncreaseBlinkColor, Color.white));

            while (t < 1)
            {
                t += Time.deltaTime / gaugeGrowthDuration;
                _gaugeRect.sizeDelta = new Vector2(Mathf.Lerp(value, targetSize, t), _gaugeRect.sizeDelta.y);
                yield return null;
            }

            StopCoroutine(crt);
            fillbar.color = CurrentGaugeColor();
        }
        
        // Utility
        private Color CurrentGaugeColor()
        {
            return gaugeGradient.Evaluate(gauge.value / 1);
        }

        private IEnumerator BlinkOverTime(float duration, float speed, Color blink, Color defaultColor)
        {
            float t = 0f;

            switch (duration)
            {
                // Timed animation
                case > 0: 
                {
                    while (t < duration) {
                        t += Time.deltaTime / duration;
                        fillbar.color = Mathf.PingPong(t, speed) > speed/2 ? blink : defaultColor;
                        yield return null;
                    }

                    break;
                }
                // Low durability animation
                case <= 0:
                {
                    while (gauge.value <= _criticalThreshold)
                    {
                        t += Time.deltaTime;
                        fillbar.color = Mathf.PingPong(t, speed) > speed/2 ? blink : defaultColor;
                        yield return null;
                    }

                    break;
                }
            }
            
            fillbar.color = CurrentGaugeColor();
        }
    }
}
