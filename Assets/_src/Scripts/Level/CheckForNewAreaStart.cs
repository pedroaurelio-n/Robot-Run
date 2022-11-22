using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class CheckForNewAreaStart : MonoBehaviour
    {
        public delegate void NewAreaStart();
        public static event NewAreaStart onNewAreaStart;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<RunnerMovement>(out RunnerMovement runner))
            {
                runner.transform.SetParent(transform);
                onNewAreaStart?.Invoke();
            }
        }
    }
}
