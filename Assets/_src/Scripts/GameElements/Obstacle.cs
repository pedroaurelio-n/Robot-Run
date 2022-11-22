using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class Obstacle : MonoBehaviour, IKillable
    {
        [Header("Dependencies")]
        [SerializeField] private ParticleSystem deathParticles;

        [Header("Shake Settings")]
        [SerializeField] private float duration = 0.4f;
        [SerializeField] private Vector3 strength = Vector3.one;
        [SerializeField] private int vibrato = 20;

        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Death()
        {
            _collider.enabled = false;
            _spriteRenderer.enabled = false;

            deathParticles.gameObject.SetActive(true);
            
            CinemachineCamera.ShakeCamera(duration, strength, vibrato);
        }

        private void OnEnable()
        {
            _collider.enabled = true;
            _spriteRenderer.enabled = true;
        }
    }
}
