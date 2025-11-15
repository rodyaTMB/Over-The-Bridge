using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OverBridge
{
    public class HUDCOntroller : MonoBehaviour
    {
        public UnityEvent OnGameOver;

        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Text mainScore;

        private void Awake()
        {
            OnGameOver.AddListener(GameOver);
        }

        void GameOver()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }

        public void UPDScore()
        {
            mainScore.text = $"{GameManager.Score}";
        }
    }
}

