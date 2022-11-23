using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace PedroAurelio.RobotRun
{
    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Button oppositeButton;

        [Header("Audio")]
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private float volumeValue;
        [SerializeField] private string volumeName = "SfxVolume";

        public void TriggerVolume()
        {
            mixer.SetFloat(volumeName, Mathf.Log10(volumeValue) * 20f);

            oppositeButton.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            mixer.GetFloat(volumeName, out float vol);

            if (Mathf.Pow(10, vol / 20f) == volumeValue)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }
    }
}
