using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class RunnerAnimation : MonoBehaviour
    {
        [Header("Animator Params")]
        [SerializeField] private string isMoving = "IsMoving";
        [SerializeField] private string isGrounded = "IsGrounded";
        [SerializeField] private string shoot = "Shoot";
        [SerializeField] private string die = "Die";

        private bool _isAlive;

        private RunnerMovement _movement;
        private ShootBullet _shoot;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            var parent = transform.parent;            
            _movement = parent.GetComponent<RunnerMovement>();
            _shoot = parent.GetComponent<ShootBullet>();

            _isAlive = true;
        }

        private void Update()
        {
            if (_movement.CurrentVelocity == Vector2.zero)
            {
                _animator.SetBool(isMoving, false);
                return;
            }

            _animator.SetBool(isMoving, true);
            _animator.SetBool(isGrounded, _movement.IsGrounded);

            if (_shoot.HasShot)
            {
                _animator.SetTrigger(shoot);
                _shoot.HasShot = false;
            }
        }

        public void DeathAnimation()
        {
            if (!_isAlive)
                return;
            
            _animator.SetTrigger(die);
            _isAlive = false;
        }
    }
}
