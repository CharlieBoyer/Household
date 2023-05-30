using System;
using System.Collections;
using Internal;
using UnityEngine;

namespace Managers
{
    public class AudioManager: MonoBehaviourSingleton<AudioManager>
    {
        public AudioSource audioSource;
        public AudioClip ambienceDay;
        public AudioClip ambienceNight;

        public enum AmbienceType { Day, Night }

        [Range(0, 1)]
        public float maximumVolume;

        private AudioClip _pendingAudioClip;

        private void Awake() {
            audioSource.volume = maximumVolume;
        }

        public void SetAudioSourceVolume(float userMultiplier)
        {
            userMultiplier /= 100;
            audioSource.volume = maximumVolume * userMultiplier;
        }

        public void PlayClip(AudioClip newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }

        public void PauseAudio()
        {
            audioSource.Pause();
        }

        public void ResumeAudio()
        {
            audioSource.Play();
        }
        
        public void ChangeTo(AmbienceType ambience)
        {
            ChangeAudioClip(ambience == AmbienceType.Day ? ambienceDay : ambienceNight);
        }

        public void ChangeAudioClip(AudioClip newClip)
        {
            if (newClip == null)
                return;

            _pendingAudioClip = newClip;
            StartCoroutine(SwitchAudioClipWithFade());
        }

        public void ChangeAudioClip(string pathToAudioClip)
        {
            _pendingAudioClip = Resources.Load<AudioClip>(pathToAudioClip);
            StartCoroutine(SwitchAudioClipWithFade());
        }

        public IEnumerator FadeSound()
        {
            const float fadeTime = 1f;
            float t = 0f;
            float initialVolume = audioSource.volume;

            while (t < 1)
            {
                t += Time.deltaTime/fadeTime;
                audioSource.volume = Mathf.Lerp(initialVolume, 0.00f, t);
                yield return null;
            }
        }

        private IEnumerator SwitchAudioClipWithFade()
        {
            const float fadeTime = 1f;
            float t = 0f;
            float initialVolume = audioSource.volume;

            while (t < 1)
            {
                t += Time.unscaledDeltaTime / fadeTime;
                audioSource.volume = Mathf.Lerp(initialVolume, 0.00f, t);
                yield return null;
            }

            audioSource.Stop();
            audioSource.clip = _pendingAudioClip;
            audioSource.Play();
            t = 0f;

            while (t < 1)
            {
                t += Time.unscaledDeltaTime / fadeTime;
                audioSource.volume = Mathf.Lerp(0.00f, initialVolume, t);
                yield return null;
            }
            
            _pendingAudioClip = null;
        }
    }
}
