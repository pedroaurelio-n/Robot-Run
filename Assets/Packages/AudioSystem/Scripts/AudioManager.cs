using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioEventChannelSO sfxChannel;
        [SerializeField] private AudioEventChannelSO musicChannel;
        [SerializeField] private AudioEventChannelSO uiChannel;
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
            var newPlayer = new GameObject("AudioPlayer");
            newPlayer.transform.SetParent(transform);

            newPlayer.AddComponent<AudioSource>();
            var audioPlayer = newPlayer.AddComponent<AudioPlayer>();

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
            if (!clipSO.CanActivateNewInstance())
                return;
            
            clipSO.AddInstance();
            var audioPlayer = OnGetAudioPlayer();
            audioPlayer.gameObject.SetActive(true);
            audioPlayer.PlayAudio(clipSO, position, delay, () => OnReleaseAudioPlayer(audioPlayer));
        }

        private void OnEnable()
        {
            sfxChannel.onRaiseAudio += PlayAudio;
            musicChannel.onRaiseAudio += PlayAudio;
            uiChannel.onRaiseAudio += PlayAudio;
        }

        private void OnDisable()
        {
            sfxChannel.onRaiseAudio -= PlayAudio;
            musicChannel.onRaiseAudio -= PlayAudio;
            uiChannel.onRaiseAudio -= PlayAudio;
        }
    }
}