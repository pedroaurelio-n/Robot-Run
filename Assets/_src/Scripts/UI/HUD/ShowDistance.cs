using UnityEngine;
using TMPro;

namespace PedroAurelio.HermitCrab
{
    public class ShowDistance : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private TextMeshProUGUI distanceNumber;

        private void Update()
        {
            distanceNumber.text = RunnerDistance.CurrentDistance > 0f 
                ? RunnerDistance.CurrentDistance.ToString("0") 
                : "0";
        }
    }
}
