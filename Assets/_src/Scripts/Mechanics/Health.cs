using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class Health : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float maxHealth;

        private float _currentHealth;

        private IKillable _destroyable;

        private void Awake()
        {
            if (!TryGetComponent<IKillable>(out _destroyable))
                Debug.LogError($"Health component needs reference to an IDestroyable.");
            
            _currentHealth = maxHealth;
        }

        public void ModifyHealth(float value)
        {
            _currentHealth += value;

            if (_currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _destroyable.Death();
        }
    }
}
