using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject StartScreen;
    public GameObject PauseButton;

    public static int score;
    public static int Highscore;
    public Text scoreText;
    public Text HighscoreText;

    public static bool isGamePaused;

    void Start()
    {
        score = 0;
        Highscore = PlayerPrefs.GetInt("HIGHSCORE: ", 0);
        Time.timeScale = 1;
        gameOver = isGameStarted = isGamePaused = false;
        UpdateHighscoreText();
    }

    void Update()
    {
        scoreText.text = "SCORE:" + score.ToString();
        if (gameOver)
        {
            Time.timeScale = 0;
            UpdateHighscore(score);
            gameOverPanel.SetActive(true);
            PauseButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        StartScreen.SetActive(false);
        SceneManager.LoadScene("Main");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        Application.Quit();
        Debug.Log("Tested and Trusted");
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "SCORE:" + score;
    }

    public void UpdateHighscore(int score)
    {
        if (score > Highscore) 
        {
            Highscore = score; 
            PlayerPrefs.SetInt("HIGHSCORE: ", Highscore);
            UpdateHighscoreText(); 
        }
    }

    void UpdateHighscoreText()
    {
        HighscoreText.text = "HIGHSCORE: " + Highscore.ToString();
        if(score > PlayerPrefs.GetInt("HIGHSCORE", 0))
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
        }
    }

    public void DoubleScore()
    {
        score += score;
    }
}