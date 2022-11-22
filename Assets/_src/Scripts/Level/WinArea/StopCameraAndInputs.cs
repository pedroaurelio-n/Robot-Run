using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class StopCameraAndInputs : MonoBehaviour
    {
        public delegate void StopCameraFollow();
        public static event StopCameraFollow onStopCameraFollow;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerInput>(out PlayerInput playerInput))
            {
                playerInput.enabled = false;
                onStopCameraFollow?.Invoke();
            }
        }
    }
}
