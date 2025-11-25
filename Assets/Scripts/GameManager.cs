using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public SelectionBar selectionBar;
    public ShelfSpawner shelfSpawner;
    public GameObject gameOverPopup;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPopup.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
