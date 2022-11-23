using UnityEngine;
using UnityEngine.SceneManagement;

namespace PedroAurelio.RobotRun
{
    public class ChangeScene : MonoBehaviour
    {
        public void LoadScene(string scene) => SceneManager.LoadScene(scene);
    }
}
