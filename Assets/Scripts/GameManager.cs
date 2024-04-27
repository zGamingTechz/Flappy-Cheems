using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private player player;
    private spawner spawner;

    public Text highscore;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject homeButton;
    public int score { get; private set; }

    //To set values as game launches
    private void Awake()
    {
        Application.targetFrameRate = 60;

        player = FindObjectOfType<player>();
        spawner = FindObjectOfType<spawner>();

        score = 0;
        highscore.text = "";
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        homeButton.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        pipes[] pipes1 = FindObjectsOfType<pipes>();

        for (int i = 0; i < pipes1.Length; i++)
        {
            Destroy(pipes1[i].gameObject);
        }

    }


    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        homeButton.SetActive(false);
        highscore.text = "";
        

        Time.timeScale = 1f;
        player.enabled = true;

        pipes[] pipes1 = FindObjectsOfType<pipes>();

        for (int i = 0; i < pipes1.Length; i++)
        {
            Destroy(pipes1[i].gameObject);
        }
    }


    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
        homeButton.SetActive(true);

        //setting highscore
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
            highscore.text = "high score: " + PlayerPrefs.GetInt("highscore").ToString();
        }
        else
        {
            highscore.text = "high score: " + PlayerPrefs.GetInt("highscore").ToString();
        }

        Pause();
    }


    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }


    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }


    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    } 

}
