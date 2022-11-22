using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class ApplyDamage : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float damage;
        [SerializeField] private LayerMask damageLayers;

        private void OnValidate()
        {
            if (damage < 0)
                damage = 0;
        }

        private void CheckForDamage(GameObject other)
        {
            var otherLayer = other.gameObject.layer;
            var otherIsInDamageLayer = (1 << otherLayer & damageLayers) != 0;

            if (otherIsInDamageLayer)
            {
                if (other.gameObject.TryGetComponent<Health>(out Health otherHealth))
                    otherHealth.ModifyHealth(-damage);
            }
        }

        private void OnCollisionEnter2D(Collision2D other) => CheckForDamage(other.gameObject);
        private void OnTriggerEnter2D(Collider2D other) => CheckForDamage(other.gameObject);
    }
}
