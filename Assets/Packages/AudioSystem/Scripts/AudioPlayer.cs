using System;
using System.Collections;
using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        private AudioClipSO _clipSO;

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        public void DisableAudioPlayer()
        {
            _audioSource.playOnAwake = false;
            _audioSource.enabled = false;
        }

        public void PlayAudio(AudioClipSO clipSO, Vector3 position, float delay, Action releaseAction)
        {
            _clipSO = clipSO;

            _audioSource.enabled = true;
            _audioSource.loop = _clipSO.Loop;
            _audioSource.spatialBlend = _clipSO.SpatialBlend;
            _audioSource.clip = _clipSO.Clip;
            _audioSource.volume = clipSO.Volume;
            _audioSource.pitch = clipSO.Pitch;
            _audioSource.outputAudioMixerGroup = _clipSO.MixerGroup;
            transform.position = position;

            if (delay == 0f)
                _audioSource.Play();
            else
                _audioSource.PlayDelayed(delay);
            
            if (!_audioSource.loop)
                StartCoroutine(WaitForClipCoroutine(delay, releaseAction));
        }

        private IEnumerator WaitForClipCoroutine(float delay, Action releaseAction)
        {
            if (delay != 0f)
                yield return new WaitForSeconds(delay);

            yield return new WaitForSeconds(_clipSO.Clip.length);
            _clipSO.RemoveInstance();
            _clipSO = null;

            releaseAction.Invoke();
        }

        private void OnDisable()
        {
            if (_clipSO != null)
                _clipSO.RemoveInstance();
        }
    }
}