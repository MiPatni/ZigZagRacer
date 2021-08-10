using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameStarted;
    public GameObject platformSpawner;
    int score = 0;
    public Text scoreText;
    public GameObject gamePlayUI;
    public GameObject menuUI;
    public Text highscoreText;
    int highscore;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Highscore : " + highscore;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        gameStarted = true;
        platformSpawner.SetActive(true);
        StopCoroutine("UpdateScore");
        gamePlayUI.SetActive(true);
        menuUI.SetActive(false);
        StartCoroutine("UpdateScore");
    }

    public void GameOver()
    {
        gameStarted = false;
        platformSpawner.SetActive(false);
        SaveHighscore();
        Invoke("ReloadLevel", 1f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            score++;
            scoreText.text = score.ToString();
        }
    }

    void SaveHighscore()
    {
        if(PlayerPrefs.HasKey("Highscore"))
        {
            //Already have highscore
            if(score > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", score);
            }
        }
        else
        {
            //Playing for the first time
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
