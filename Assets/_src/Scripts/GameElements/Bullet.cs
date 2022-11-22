using System.Collections;
using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IPoolable
    {
        [Header("Dependencies")]
        [SerializeField] private ParticleSystem collisionParticles;

        private WaitForSeconds _waitForParticles;
        private GameObject _particlesGameObject;
        private Transform _particlesTransform;

        private Transform _originTransform;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            _particlesGameObject = collisionParticles.gameObject;
            _particlesTransform = collisionParticles.transform;
            _waitForParticles = new WaitForSeconds(collisionParticles.main.duration);
        }

        public void Initialize(Transform origin, Transform levelSpace, Vector3 position, Vector2 speed)
        {
            _particlesGameObject.SetActive(false);

            _rigidbody.simulated = true;
            _collider.enabled = true;
            _spriteRenderer.enabled = true;

            _originTransform = origin;
            transform.SetPositionAndRotation(position, Quaternion.identity);
            transform.SetParent(levelSpace);
            _rigidbody.velocity = speed;
        }

        private void DisableObject()
        {
            transform.SetParent(_originTransform);
            gameObject.SetActive(false);
        }

        public void ReleaseFromPool(bool triggerEffects)
        {
            if (!gameObject.activeInHierarchy)
                return;
            
            if (triggerEffects)
                StartCoroutine(WaitForParticlesCoroutine());
            else
                DisableObject();
        }

        private IEnumerator WaitForParticlesCoroutine()
        {
            _rigidbody.simulated = false;
            _collider.enabled = false;
            _spriteRenderer.enabled = false;

            _particlesTransform.SetParent(transform.parent);
            _particlesGameObject.SetActive(true);

            yield return _waitForParticles;
            
            _particlesTransform.SetParent(transform);
            _particlesTransform.localPosition = Vector3.zero;
            _particlesGameObject.SetActive(false);
            
            DisableObject();
        }

        private void OnDisable() => ReleaseFromPool(true);
    }
}
