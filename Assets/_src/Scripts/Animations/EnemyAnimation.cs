using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class EnemyAnimation : MonoBehaviour
    {
        [Header("Animator Params")]
        [SerializeField] private string isAlive = "IsAlive";
        [SerializeField] private string attack = "Attack";

        private bool _isAlive;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            IdleAnimation();
        }
        
        public void IdleAnimation()
        {
            if (_animator == null)
                return;
            
            _animator.SetBool(isAlive, true);
            _isAlive = true;
        }

        public void AttackAnimation()
        {
            if (_animator == null)
                return;
            
            _animator.SetTrigger(attack);
        }

        public void DeathAnimation()
        {
            if (!_isAlive || _animator == null)
                return;
            
            _animator.SetBool(isAlive, false);
            _isAlive = false;
        }
    }
}
