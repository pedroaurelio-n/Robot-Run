using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class PlayAudioEvent : MonoBehaviour
    {
        [SerializeField] private AudioEventChannelSO audioChannel;
        [SerializeField] private AudioClipSO clipSO;
        [SerializeField] private bool willPoolOnStart;
        [SerializeField] private bool playOnStart;
        [SerializeField] private float delay;

        private bool _isReady;

        private void OnEnable()
        {
            if (willPoolOnStart)
            {
                if (playOnStart && _isReady)
                    audioChannel.RaiseEvent(clipSO, transform.position, delay);
            }
            else
            {
                if (playOnStart)
                    audioChannel.RaiseEvent(clipSO, transform.position, delay);
            }
        }

        private void OnDisable()
        {
            if (willPoolOnStart)
                _isReady = true;
        }

        public void PlayAudio()
        {
            audioChannel.RaiseEvent(clipSO, transform.position, delay);            
        }
    }
}