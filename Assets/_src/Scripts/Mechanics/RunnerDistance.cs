using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    [RequireComponent(typeof(RunnerMovement))]
    public class RunnerDistance : MonoBehaviour
    {
        public static float CurrentDistance { get; private set; }

        private RunnerMovement _movement;

        private void Awake() => _movement = GetComponent<RunnerMovement>();

        private void Update()
        {
            var distanceTravelled = _movement.CurrentVelocity.x * Time.deltaTime;
            CurrentDistance -= distanceTravelled;
        }

        private void SetRemainingDistance(float distance)
        {
            CurrentDistance = distance;
        }

        private void OnEnable() => LevelGenerator.onDistanceCalculated += SetRemainingDistance;
        private void OnDisable() => LevelGenerator.onDistanceCalculated -= SetRemainingDistance;
    }
}
