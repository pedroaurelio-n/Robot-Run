using System;
using System.Collections;
using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        public void DisableAudioPlayer()
        {
            _audioSource.playOnAwake = false;
            _audioSource.enabled = false;
        }

        public void PlayAudio(AudioClipSO clipSO, Vector3 position, float delay, Action releaseAction)
        {
            _audioSource.enabled = true;
            _audioSource.loop = clipSO.Loop;
            _audioSource.clip = clipSO.Clip;
            _audioSource.outputAudioMixerGroup = clipSO.MixerGroup;
            transform.position = position;

            if (delay == 0f)
                _audioSource.Play();
            else
                _audioSource.PlayDelayed(delay);
            
            if (!_audioSource.loop)
                StartCoroutine(WaitForClipCoroutine(clipSO.Clip.length, delay, releaseAction));
        }

        private IEnumerator WaitForClipCoroutine(float duration, float delay, Action releaseAction)
        {
            if (delay != 0f)
                yield return new WaitForSeconds(delay);

            yield return new WaitForSeconds(duration);
            releaseAction.Invoke();
        }
    }
}