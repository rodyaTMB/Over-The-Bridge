using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
     [Header("UI Elements")]
    public Text scoreText;
    public Text bestScoreText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public Button pauseButton;
    public Button restartButton;
    public Button quitButton;
    
    void Start()
    {
        // Настройка кнопок
        pauseButton.onClick.AddListener(() => GameManager.Instance.TogglePause());
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        quitButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
        
        UpdateBestScore();
        HideGameOver();
        pausePanel.SetActive(false);
    }
    
    public void UpdateScore(int score)
    {
        scoreText.text = "Острова: " + score;
    }
    
    public void UpdateBestScore()
    {
        bestScoreText.text = "Рекорд: " + GameManager.Instance.bestScore;
    }
    
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        UpdateBestScore();
    }
    
    public void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }
    
    public void TogglePauseMenu(bool isPaused)
    {
        pausePanel.SetActive(isPaused);
    }
}
