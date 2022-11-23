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

        [Header("Clip Settings")]
        public int MaxInstances = 3;

        [Header("Source Settings")]
        public bool Loop = false;
        [Range(0f, 1f)] public float SpatialBlend = 0f;
        [SerializeField] private Vector2 volumeRange = Vector2.one;
        [SerializeField] private Vector2 pitchRange = Vector2.one;
        public float Volume => Random.Range(volumeRange.x, volumeRange.y);
        public float Pitch => Random.Range(pitchRange.x, pitchRange.y);

        private int _currentInstances;

        private void OnValidate()
        {
            if (volumeRange.x > volumeRange.y)
                volumeRange.x = volumeRange.y;

            if (volumeRange.x < 0f)
                volumeRange.x = 0f;
            
            if (volumeRange.y < volumeRange.x)
                volumeRange.y = volumeRange.x;

            if (volumeRange.y > 1f)
                volumeRange.y = 1f;

            if (pitchRange.x > pitchRange.y)
                pitchRange.x = pitchRange.y;
            
            if (pitchRange.x < -3f)
                pitchRange.x = -3f;

            if (pitchRange.y < pitchRange.x)
                pitchRange.y = pitchRange.x;

            if (pitchRange.y > 3f)
                pitchRange.y = 3f;
        }

        public bool CanActivateNewInstance()
        {
            if (MaxInstances <= 0)
                MaxInstances = int.MaxValue;
            
            return _currentInstances < MaxInstances;
        }

        public void AddInstance() => _currentInstances++;
        public void RemoveInstance() => _currentInstances--;
    }
}