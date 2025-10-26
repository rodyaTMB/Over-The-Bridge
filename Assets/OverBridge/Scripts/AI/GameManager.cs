using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game State")]
    public bool isGameActive = false;
    public bool isGamePaused = false;
    
    [Header("Score")]
    public int currentScore = 0;
    public int bestScore = 0;
    
    [Header("References")]
    public HeroController hero;
    public BridgeController bridge;
    public IslandGenerator islandGenerator;
    public UIManager uiManager;
    
    void Awake()
    {
        Instance = this;
        LoadBestScore();
    }
    
    void Start()
    {
        StartGame();
    }
    
    public void StartGame()
    {
        isGameActive = true;
        currentScore = 0;
        uiManager.UpdateScore(currentScore);
    }
    
    public void GameOver()
    {
        isGameActive = false;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            SaveBestScore();
        }
        uiManager.ShowGameOver();
    }
    
    public void AddScore()
    {
        currentScore++;
        uiManager.UpdateScore(currentScore);
    }
    
    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
        uiManager.TogglePauseMenu(isGamePaused);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }
    
    void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }
}
