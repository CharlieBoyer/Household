using System;
using System.Collections;
using UnityEngine;

using Internal;
using UnityEngine.UI;

namespace UI
{
    public class CycleIcon : MonoBehaviourSingleton<CycleIcon>
    {
        public float fadeAnimDelay;

        private Image _icon;

        private void Start()
        {
            _icon = UIManager.Instance.cycleIcon;

            // Fallback
            if (_icon == null) {
                _icon = GetComponent<Image>();
            }
        }

        public void FadeOut() {
            Instance.StartCoroutine(Fade(1, 0));
        }
        
        public void FadeIn()
        {
            if (_icon.enabled == false)
                _icon.enabled = true;
            
            Instance.StartCoroutine(Fade(0, 1));
        }

        private IEnumerator Fade(float a, float b)
        {
            float t = 0f;
            Color iconColor = _icon.color;

            while (t < 1)
            {
                t += Time.deltaTime / fadeAnimDelay;
                iconColor.a = Mathf.Lerp(a, b, t);
                _icon.color = iconColor;
                yield return null;
            }
        }
    }
}
