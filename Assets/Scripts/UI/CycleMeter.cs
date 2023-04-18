using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Internal;
using UnityEngine.Serialization;

namespace UI
{
    public class CycleMeter : MonoBehaviourSingleton<CycleMeter>
    {
        [Header("UI Fields")]
        public Slider slider;
        public Image sliderBar;
        public Image sliderFill;
        public Image icon;

        [Header("Extra elements")]
        public Animator iconAnimator;
        public List<Image> moonStars;

        [Header("Elements Colors")]
        public Color defaultBarColor;
        public Color fillbarDayColor;
        public Color fillbarNightColor;
        public Color sunIconColor;
        public Color moonIconColor;
        
        // Cached animator properties
        private readonly int _day = Animator.StringToHash("day");
        private readonly int _night = Animator.StringToHash("night");

        private void Start()
        {
            icon.enabled = false;
            FadeOutIcon();
            sliderFill.enabled = false;
            sliderBar.color = defaultBarColor;
        }

        // Cycle Meter "states"
        public void SetComplete()
        {
            FadeOutIcon();
            sliderFill.enabled = false;
            sliderBar.color = defaultBarColor;
        }

        public void SetSun()
        {
            iconAnimator.SetTrigger(_day);
            icon.color = sunIconColor;
            sliderFill.enabled = true;
            sliderFill.color = fillbarDayColor;
            sliderBar.color = defaultBarColor;
            SetStars(false);
            FadeInIcon();
        }

        public void SetMoon()
        {
            iconAnimator.SetTrigger(_night);
            icon.color = moonIconColor;
            sliderFill.enabled = true;
            sliderFill.color = fillbarNightColor;
            sliderBar.color = defaultBarColor;
            SetStars(true);
            FadeInIcon();
        }

        private void SetStars(bool activate)
        {
            foreach (Image star in moonStars) {
                star.enabled = activate;
            }
        }

        // Fade Coroutine
        private void FadeOutIcon() {
            Instance.StartCoroutine(Fade(1, 0));
        }

        private void FadeInIcon()
        {
            if (icon.enabled == false)
                icon.enabled = true;
            
            Instance.StartCoroutine(Fade(0, 1));
        }

        private IEnumerator Fade(float a, float b)
        {
            float t = 0f;
            Color iconColor = icon.color;
            
            while (t < 1)
            {
                t += Time.deltaTime / UIManager.Instance.FadeAnimDelay;
                iconColor.a = Mathf.Lerp(a, b, t);
                icon.color = iconColor;
                yield return null;
            }
        }
    }
}
