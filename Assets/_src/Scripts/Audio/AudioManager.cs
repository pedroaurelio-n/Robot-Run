using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioEventChannelSO audioChannel;
        [SerializeField] private AudioPlayer audioPlayerPrefab;
        [SerializeField] private int initialPoolCount;

        private List<AudioPlayer> _audioPlayerPool;

        private void Awake()
        {
            _audioPlayerPool = new List<AudioPlayer>();

            InitializePool(initialPoolCount);
        }

        #region Pool Methods
        private AudioPlayer OnCreateAudioPlayer()
        {
            var audioPlayer = Instantiate(audioPlayerPrefab, transform);
            _audioPlayerPool.Add(audioPlayer);
            return audioPlayer;
        }

        private AudioPlayer OnGetAudioPlayer()
        {
            foreach (AudioPlayer audioPlayer in _audioPlayerPool)
            {
                if (!audioPlayer.gameObject.activeInHierarchy)
                    return audioPlayer;
            }

            return OnCreateAudioPlayer();
        }

        private void OnReleaseAudioPlayer(AudioPlayer audioPlayer)
        {
            audioPlayer.DisableAudioPlayer();
            audioPlayer.gameObject.SetActive(false);
        }

        private void InitializePool(int count)
        {
            for (int i = 0; i < count; i++)
                OnCreateAudioPlayer();

            for (int i = _audioPlayerPool.Count - 1; i >= 0; i--)
            {
                _audioPlayerPool[i].DisableAudioPlayer();
                _audioPlayerPool[i].gameObject.SetActive(false);
            }
        }
        #endregion

        private void PlayAudio(AudioClipSO clipSO, Vector3 position, float delay)
        {
            var audioPlayer = OnGetAudioPlayer();
            audioPlayer.gameObject.SetActive(true);
            audioPlayer.PlayAudio(clipSO, position, delay, () => OnReleaseAudioPlayer(audioPlayer));
        }

        private void OnEnable() => audioChannel.onRaiseAudio += PlayAudio;
        private void OnDisable() => audioChannel.onRaiseAudio -= PlayAudio;
    }
}