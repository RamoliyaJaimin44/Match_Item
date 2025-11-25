using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button resetBtn;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        int score = GameManager.Instance.score;

        scoreText.text = "Score : "+ score;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int highScore = PlayerPrefs.GetInt("HighScore");
            if (score >= highScore)
            {
                highScoreText.text = "High Score : " + score;
                PlayerPrefs.SetInt("HighScore", score);
            }
            else
            {
                highScoreText.text = "High Score : " + highScore;
                PlayerPrefs.SetInt("HighScore", highScore);
            }

        }
        else 
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score : " + score;
        }
        resetBtn.onClick.AddListener(ResetBtnClick);
    }

    void ResetBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
