using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HomeStatus: MonoBehaviour
    {
        [Header("UI Fields")]
        public Slider gauge;
        public Image fillbar;
        public Image icon;

        [Header("Extra values")]
        public int gaugeMaxSize;

        [Header("Animation")]
        public Animator animator;
        public float gaugeGrowthDuration;

        // Cached animator properties
        private static readonly int IsGrowing = Animator.StringToHash("isGrowing");
        
        private RectTransform _gaugeRect;
        private const int GaugeChunkSize = 50;

        private void Start() {
            _gaugeRect = gauge.gameObject.GetComponent<RectTransform>();
        }

        public void DecreaseDurability(float clampedAmount)
        {
            // TODO <!>
        }

        public void RestoreDurability(float clampedAmount)
        {
            // TODO <!>
        }

        public void IncreaseMaxGaugeSize(int additionalChunks)
        {
            Vector2 currentSize = _gaugeRect.sizeDelta;
            int additionalSize = additionalChunks * GaugeChunkSize;
            int targetSize = Mathf.RoundToInt(currentSize.x + additionalSize);
            
            if (Math.Abs(gaugeMaxSize - currentSize.x) == 0)
                return;
            
            if (currentSize.x < gaugeMaxSize && targetSize > gaugeMaxSize) {
                targetSize = Mathf.Clamp(targetSize, 0, gaugeMaxSize);
                Debug.LogWarning("Excessive DurabilityGauge growth");
            }

            StartCoroutine(IncreaseMaxGaugeOverTime(targetSize));
        }

        private IEnumerator IncreaseMaxGaugeOverTime(float targetSize)
        {
            float t = 0f;
            float value = _gaugeRect.sizeDelta.x;
            
            animator.SetBool(IsGrowing, true);

            while (t < 1)
            {
                t += Time.deltaTime / gaugeGrowthDuration;
                _gaugeRect.sizeDelta = new Vector2(Mathf.Lerp(value, targetSize, t), _gaugeRect.sizeDelta.y);
                yield return null;
            }
            
            animator.SetBool(IsGrowing, false);
        }
    }
}
