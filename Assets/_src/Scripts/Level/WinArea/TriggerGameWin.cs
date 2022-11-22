using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class TriggerGameWin : MonoBehaviour
    {
        public delegate void GameWin();
        public static event GameWin onGameWin;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<RunnerMovement>(out RunnerMovement runnerMovement))
            {
                runnerMovement.ResetVelocity();
                runnerMovement.enabled = false;
                onGameWin?.Invoke();
            }
        }
    }
}
