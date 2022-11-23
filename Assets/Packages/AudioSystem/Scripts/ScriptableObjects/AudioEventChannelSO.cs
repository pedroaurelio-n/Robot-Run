using UnityEngine;
using UnityEngine.Events;
 
namespace PedroAurelio.AudioSystem
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Audio/Audio Channel")]
    public class AudioEventChannelSO : ScriptableObject
    {
        public UnityAction<AudioClipSO, Vector3, float> onRaiseAudio;

        public void RaiseEvent(AudioClipSO clipSO, Vector3 position, float delay) => onRaiseAudio?.Invoke(clipSO, position, delay);
    }
}