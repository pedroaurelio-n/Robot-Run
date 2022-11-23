using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class PlayAudioEvent : MonoBehaviour
    {
        [SerializeField] private AudioEventChannelSO audioChannel;
        [SerializeField] private AudioClipSO clipSO;
        [SerializeField] private bool playOnStart;
        [SerializeField] private bool playOnNextEnable;
        [SerializeField] private float delay;

        private bool _hasPlayerOnStart;

        private void Start()
        {
            if (playOnStart)
            {
                PlayAudio();
                _hasPlayerOnStart = true;
            }
        }

        public void PlayAudio() => audioChannel.RaiseEvent(clipSO, transform.position, delay);

        private void OnEnable()
        {
            if (!_hasPlayerOnStart)
                return;

            if (playOnNextEnable)
                PlayAudio();
        }
    }
}