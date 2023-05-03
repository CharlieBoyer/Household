using System;
using System.Collections;
using System.Collections.Generic;
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

        [Header("Gauge parameters")]
        public int gaugeMaxSize;
        public float criticalThreshold;
        public Color defaultColor;
        public Color criticalColor;
        public Color repairColor;
        public Color damageColor;

        [Header("Animation")]
        public Animator animator;
        public float gaugeGrowthDuration;
        public float durabilityChangeDelay;
        
        // Cached animator properties
        private static readonly int IsGrowing = Animator.StringToHash("isGrowing");

        private Coroutine _undergoingUpdate;
        
        private RectTransform _gaugeRect;
        private const int GaugeChunkSize = 50;
        
        private float _currentDurabilityPercentage;

        private void Start() {
            _gaugeRect = gauge.gameObject.GetComponent<RectTransform>();
            _currentDurabilityPercentage = 0f;
            UpdateDurability(1);
        }

        // Update Gauge
        public void UpdateDurability(float targetClampedAmount)
        {
            if (targetClampedAmount < 0 || targetClampedAmount > 1)
                Debug.LogWarning("Warning: UpdateDurability to unclampled amount");
            else if (Math.Abs(targetClampedAmount - gauge.value) == 0)
                Debug.LogWarning("Warning: Useless UpdateDurability call");
            
            if (_undergoingUpdate != null)
                StopAllCoroutines();
            
            _undergoingUpdate = StartCoroutine(UpdateDurabilityOverTime(targetClampedAmount));
        }

        private IEnumerator UpdateDurabilityOverTime(float target)
        {
            float t = 0f;
            float start = gauge.value;
            Coroutine safeguardWait;
            
            safeguardWait = StartCoroutine(target < _currentDurabilityPercentage ? DamageColorFlash() : RepairColorFlash());
            while (t < 1)
            {
                t += Time.deltaTime / durabilityChangeDelay;
                gauge.value = Mathf.Lerp(start, target, t);
                yield return null;
            }

            yield return safeguardWait; //TODO: Extra waiting in case the color switch didn't finished;
            Debug.LogAssertion("safeGuard passed");
        }

        private IEnumerator RepairColorFlash()
        {
            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(defaultColor, 0f), new GradientColorKey(repairColor, 1f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
    
            float t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / durabilityChangeDelay;
                GradientColorKey[] colorKeys = gradient.colorKeys;
                colorKeys[0].time = t;
                gradient.SetKeys(colorKeys, gradient.alphaKeys);
                fillbar.color = gradient.Evaluate(gauge.value / gauge.maxValue);
                yield return null;
            }
        }

        // TODO: [HEAVY EXPERIMENTAL STUFF]
        
        private IEnumerator AdvancedRepairColorFlash()
        {
            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.yellow, 0f), new GradientColorKey(Color.white, 1f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });

            int numFlashes = 2;
            float flashDuration = durabilityChangeDelay / (numFlashes * 2f);
            int flashesRemaining = numFlashes;
            bool flashOn = true;

            while (flashesRemaining > 0)
            {
                float t = 0f;
                while (t < durabilityChangeDelay)
                {
                    t += Time.deltaTime / durabilityChangeDelay;
                    GradientColorKey[] colorKeys = gradient.colorKeys;
                    colorKeys[0].time = t;
                    gradient.SetKeys(colorKeys, gradient.alphaKeys);
                    fillbar.color = gradient.Evaluate(gauge.value / gauge.maxValue);
                    yield return null;
                }

                // Update Flash effect
                flashOn = !flashOn;
                if (flashOn)
                {
                    gradient.SetKeys(
                        new GradientColorKey[] { new (defaultColor, 0f), new (repairColor, 1f) },
                        new GradientAlphaKey[] { new (1f, 0f), new (1f, 1f) });
                    flashesRemaining--;
                }
                else
                {
                    gradient.SetKeys(
                        new GradientColorKey[] { new (repairColor, 0f), new (repairColor, 1f) },
                        new GradientAlphaKey[] { new (0f, 0f), new (0f, 1f) });
                }
            }

            // Reset bar to default;
            gradient.SetKeys(
                new GradientColorKey[] { new (Color.white, 0f), new (Color.white, 1f) },
                new GradientAlphaKey[] { new (0f, 0f), new (0f, 1f) });
            fillbar.color = gradient.Evaluate(gauge.value / gauge.maxValue);
        }

        private IEnumerator DamageColorFlash()
        {
            float timer = durabilityChangeDelay;

            animator.SetBool("isDamaged", true);
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            animator.SetBool("isDamaged", false);
        }
        
        // TODO: [HEAVY EXPERIMENTAL]
        /* private class DamagePortion
        {
            public float damageAmount;
            public float duration;
            public Color damageColor;
            public DamagePortion nextPortion;
            public float normalizedTime;

            public DamagePortion(float damageAmount, float duration, Color damageColor)
            {
                this.damageAmount = damageAmount;
                this.duration = duration;
                this.damageColor = damageColor;
                this.nextPortion = null;
                this.normalizedTime = 0f;
            }
        }
        private List<DamagePortion> _damagePortions = new List<DamagePortion>();

        private IEnumerator TakeDamage(float damageAmount, float duration, Color damageColor)
        {
            DamagePortion newPortion = new DamagePortion(damageAmount, duration, damageColor);

            // Check if there are any existing damage portions
            if (_damagePortions.Count > 0)
            {
                // Get the most recent damage portion and add the new portion to it
                DamagePortion latestPortion = _damagePortions[_damagePortions.Count - 1];
                latestPortion.nextPortion = newPortion;
            }

            // Add the new portion to the list
            _damagePortions.Add(newPortion);

            // Loop through all the damage portions and set their normalized time based on their total duration
            float totalDuration = 0f;
            foreach (DamagePortion portion in _damagePortions)
            {
                totalDuration += portion.duration;
            }

            float currentTime = 0f;
            foreach (DamagePortion portion in _damagePortions)
            {
                portion.normalizedTime = currentTime / totalDuration;
                currentTime += portion.duration;
            }

            // Loop until all damage portions have finished
            while (_damagePortions.Count > 0)
            {
                // Get the total damage amount and color for all the portions
                float totalDamageAmount = 0f;
                Gradient gradient = new Gradient();

                foreach (DamagePortion portion in _damagePortions)
                {
                    totalDamageAmount += portion.damageAmount;

                    GradientColorKey[] colorKeys = gradient.colorKeys;
                    GradientAlphaKey[] alphaKeys = gradient.alphaKeys;

                    // Set the color and alpha keys for the portion's color
                    colorKeys[0].color = portion.damageColor;
                    colorKeys[0].time = portion.normalizedTime;
                    alphaKeys[0].alpha = 1f;
                    alphaKeys[0].time = portion.normalizedTime;

                    // Set the color and alpha keys for the next portion's color (if there is one)
                    if (portion.nextPortion != null)
                    {
                        colorKeys[1].color = portion.nextPortion.damageColor;
                        colorKeys[1].time = portion.normalizedTime;
                        alphaKeys[1].alpha = 1f;
                        alphaKeys[1].time = portion.normalizedTime;
                    }

                    gradient.SetKeys(colorKeys, alphaKeys);
                }

                // Update the fillbar color with the gradient for the total damage amount
                fillbar.color = gradient.Evaluate(gauge.value / gauge.maxValue) * totalDamageAmount;

                // Reduce the normalized time for all the portions and remove any that have finished
                for (int i = _damagePortions.Count - 1; i >= 0; i--)
                {
                    DamagePortion portion = _damagePortions[i];
                    portion.normalizedTime -= Time.deltaTime / portion.duration;

                    if (portion.normalizedTime <= 0f)
                    {
                        _damagePortions.RemoveAt(i);
                    }
                }

                yield return null;
            }
        }
        */
        
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
