using UnityEngine;
using UnityEngine.Audio;
 
namespace PedroAurelio.AudioSystem
{
    [CreateAssetMenu(fileName = "New Audio Clip", menuName = "Audio/Audio Clip")]
    public class AudioClipSO : ScriptableObject
    {
        [Header("Audio Dependencies")]
        public AudioClip Clip;
        public AudioMixerGroup MixerGroup;

        [Header("Audio Source Settings")]
        public bool Loop;
        [Range(0f, 1f)] public float SpatialBlend;
    }
}