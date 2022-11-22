using UnityEngine;
using TMPro;

namespace PedroAurelio.HermitCrab
{
    public class ShowScore : MonoBehaviour
    {
        public int CurrentScore => _currentScore;
        
        [Header("Dependencies")]
        [SerializeField] private TextMeshProUGUI scoreNumber;

        private int _currentScore;

        private void Awake() => AddScore(0);

        private void AddScore(int score)
        {
            _currentScore += score;
            scoreNumber.text = _currentScore.ToString();
        }

        private void OnEnable() => Enemy.onEnemyDefeated += AddScore;
        private void OnDisable() => Enemy.onEnemyDefeated -= AddScore;
    }
}
