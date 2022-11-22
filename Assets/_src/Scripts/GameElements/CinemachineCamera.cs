using UnityEngine;
using Cinemachine;
using DG.Tweening;

namespace PedroAurelio.HermitCrab
{
    public class CinemachineCamera : MonoBehaviour
    {
        private static CinemachineVirtualCamera _cinemachineCamera;

        private void Awake() => _cinemachineCamera = GetComponent<CinemachineVirtualCamera>();

        public static void ShakeCamera(float duration, Vector3 strength, int vibrato)
        {
            _cinemachineCamera.transform.DOShakeRotation(duration, strength, vibrato, 2f);
        }

        private void StopFollow() => _cinemachineCamera.m_Follow = null;

        private void OnEnable() => StopCameraAndInputs.onStopCameraFollow += StopFollow;
        private void OnDisable() => StopCameraAndInputs.onStopCameraFollow -= StopFollow;
    }
}
