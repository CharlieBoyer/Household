using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Internal;
using Managers;

namespace UI
{
    public class CycleMeter : MonoBehaviour
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
            StartCoroutine(FadeComplete());
        }

        public void SetSun()
        {
            iconAnimator.SetTrigger(_day);
            sliderFill.enabled = true;
            sliderFill.color = fillbarDayColor;
            sliderBar.color = defaultBarColor;
            SetStars(false);
            FadeInIcon();
        }

        public void SetMoon()
        {
            iconAnimator.SetTrigger(_night);
            sliderFill.enabled = true;
            sliderFill.color = fillbarNightColor;
            sliderBar.color = defaultBarColor;
            SetStars(true);
            FadeInIcon();
        }
        
        // Enables/Disable stars for moon icon 
        private void SetStars(bool activate)
        {
            foreach (Image star in moonStars) {
                star.enabled = activate;
            }
        }

        // Fade Coroutine
        private void FadeOutIcon() {
            StartCoroutine(Fade(1, 0));
        }

        private void FadeInIcon()
        {
            if (icon.enabled == false)
                icon.enabled = true;
            
            StartCoroutine(Fade(0, 1));
        }

        private IEnumerator FadeComplete()
        {
            SetStars(false);
            yield return Fade(1, 0);
            
            sliderFill.enabled = false;
            sliderBar.color = defaultBarColor;
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
