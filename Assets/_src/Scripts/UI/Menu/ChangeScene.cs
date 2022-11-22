using UnityEngine;
using UnityEngine.SceneManagement;

namespace PedroAurelio.HermitCrab
{
    public class ChangeScene : MonoBehaviour
    {
        public void LoadScene(string scene) => SceneManager.LoadScene(scene);
    }
}
