using System.Collections;
using UnityEngine;

namespace PedroAurelio.HermitCrab
{
    public class DestroyByLifetime : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float lifeTime;

        private void OnEnable() => StartCoroutine(DestroyCoroutine());

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(lifeTime);
            
            if (TryGetComponent<IKillable>(out IKillable killable))
                killable.Death();
            else if (TryGetComponent<IPoolable>(out IPoolable poolable))
                poolable.ReleaseFromPool(false);
            else 
                gameObject.SetActive(false);
        }
    }
}
