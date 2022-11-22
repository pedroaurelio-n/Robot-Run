using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    [RequireComponent(typeof(RunnerMovement))]
    [RequireComponent(typeof(ShootBullet))]
    public class PlayerInput : MonoBehaviour
    {
        private float _screenHalfWidth;
        
        private RunnerMovement _movement;
        private ShootBullet _shoot;

        private void Awake()
        {
            _movement = GetComponent<RunnerMovement>();
            _shoot = GetComponent<ShootBullet>();

            _screenHalfWidth = Screen.width * 0.5f;
        }

        private void Update()
        {
            HandleTouchInputs();

            #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                _shoot.SetShootInput(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _shoot.SetShootInput(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _movement.SetJumpInput(true);
            }

            if (Input.GetMouseButtonUp(1))
            {
                _movement.SetJumpInput(false);
            }
            #endif
        }

        private void HandleTouchInputs()
        {
            if (Input.touchCount == 0)
                return;
            
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x <= _screenHalfWidth)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began: _movement.SetJumpInput(true); break;
                        case TouchPhase.Ended: _movement.SetJumpInput(false); break;
                        default: break;
                    }
                }
                else
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began: _shoot.SetShootInput(true); break;
                        case TouchPhase.Ended: _shoot.SetShootInput(false); break;
                        default: break;
                    }
                }
            }
        }
    }
}
