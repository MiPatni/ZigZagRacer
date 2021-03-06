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
    AudioSource audioSource;
    public AudioClip[] gameMusic;
    int adCounter = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Highscore : " + highscore;
        CheckAdCount();
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
        audioSource.clip = gameMusic[1];
        audioSource.Play();
        StartCoroutine("UpdateScore");
    }

    public void GameOver()
    {
        gameStarted = false;
        platformSpawner.SetActive(false);
        SaveHighscore();
        //AdsManager.instance.ShowAd();
        
        if(adCounter >= 4)
        {
            adCounter = 0;
            PlayerPrefs.SetInt("AdCount", 0);
            AdsManager.instance.ShowRewardedAd();
        }
        else
        {
            Invoke("ReloadLevel", 1f);
        }
    }

    public void ReloadLevel()
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

    public void IncrementScore()
    {
        score += 2;
        scoreText.text = score.ToString();
        audioSource.PlayOneShot(gameMusic[2], 0.2f);
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

    void CheckAdCount()
    {
        if(PlayerPrefs.HasKey("AdCount"))
        {
            adCounter = PlayerPrefs.GetInt("AdCount");
            adCounter++;

            PlayerPrefs.SetInt("AdCount", adCounter);
        }
        else
        {
            PlayerPrefs.SetInt("AdCount", 0);
        }
    }
}
