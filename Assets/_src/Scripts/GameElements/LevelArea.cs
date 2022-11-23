using UnityEngine;

namespace PedroAurelio.RobotRun
{
    public class LevelArea : MonoBehaviour, IPoolable
    {
        public int Id { get; set; }

        public void Initialize(Vector3 position) => transform.SetPositionAndRotation(position, Quaternion.identity);
        public void ReleaseFromPool(bool triggerEffects) => gameObject.SetActive(false);
    }
}
