using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class Enemy : MonoBehaviour, IKillable
    {
        public delegate void EnemyDefeated(int score);
        public static event EnemyDefeated onEnemyDefeated;

        [Header("Dependencies")]
        [SerializeField] private ParticleSystem deathParticles;

        [Header("Settings")]
        [SerializeField] private int scoreOnDefeat;
        
        [Header("Shake Settings")]
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private Vector3 strength = Vector3.one;
        [SerializeField] private int vibrato = 10;

        private Collider2D _collider;
        private EnemyAnimation _enemyAnimation;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        }

        public void Death()
        {
            _collider.enabled = false;
            _enemyAnimation.DeathAnimation();
            
            onEnemyDefeated?.Invoke(scoreOnDefeat);
            deathParticles.gameObject.SetActive(true);
            
            CinemachineCamera.ShakeCamera(duration, strength, vibrato);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Runner>(out Runner runner))
            {
                _enemyAnimation.AttackAnimation();
            }
        }

        private void OnEnable()
        {            
            _enemyAnimation.IdleAnimation();
            _collider.enabled = true;
        }
    }
}
