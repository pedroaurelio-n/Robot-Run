using UnityEngine;
using PedroAurelio.AudioSystem;

namespace PedroAurelio.HermitCrab
{
    [RequireComponent(typeof(RunnerMovement))]
    public class Runner : MonoBehaviour, IKillable
    {
        public delegate void RunnerDeath();
        public static event RunnerDeath onRunnerDeath;

        [Header("Dependencies")]
        [SerializeField] private ParticleSystem jumpParticles;

        private bool _hasPlayedParticles;
        private bool _isAlive;

        private RunnerMovement _movement;
        private ShootBullet _shoot;
        private RunnerAnimation _runnerAnimation;

        private PlayAudioEvent _deathAudioEvent;

        private void Awake()
        {
            _isAlive = true;

            _movement = GetComponent<RunnerMovement>();
            _shoot = GetComponent<ShootBullet>();
            _runnerAnimation = GetComponentInChildren<RunnerAnimation>();

            _deathAudioEvent = GetComponent<PlayAudioEvent>();
        }

        private void Update()
        {
            if (!_isAlive)
                return;
            
            if (_movement.IsGrounded)
                _hasPlayedParticles = false;

            if (_movement.HasJumped && !_hasPlayedParticles)
            {
                jumpParticles.gameObject.SetActive(true);
                _hasPlayedParticles = true;
            }
        }

        public void Death()
        {
            if (!_isAlive)
                return;
            
            _isAlive = false;

            _movement.ResetVelocity();
            _movement.SetJumpInput(false);
            _movement.enabled = false;
            _shoot.enabled = false;

            _runnerAnimation.DeathAnimation();
            onRunnerDeath?.Invoke();
            
            _deathAudioEvent.PlayAudio();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_movement.CurrentVelocity.x <= 0.01f)
                Death();
        }
    }
}
