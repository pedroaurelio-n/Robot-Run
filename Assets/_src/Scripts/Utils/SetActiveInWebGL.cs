using UnityEngine;

namespace PedroAurelio.RobotRun
{
    public class SetActiveInWebGL : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool activeInWebGL;

        private void Awake()
        {
            #if UNITY_WEBGL || UNITY_EDITOR
            gameObject.SetActive(activeInWebGL);
            #else
            gameObject.SetActive(!activeInWebGL);
            #endif
        }
    }
}
