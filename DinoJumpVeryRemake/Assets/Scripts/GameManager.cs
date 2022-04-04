using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator transition;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI endingScoreText;
    
    [HideInInspector] public float obstacleSpeedup = 5f;
    [HideInInspector] public float bgSpeedup = 5f;
    [HideInInspector] public float spawnerSpeedup = 2f;
    [HideInInspector] public float itemSpeedup = 5f;
    [HideInInspector] public bool powerupScore = false;
    
    private float time;
    private float timeCounter;
    private float score;
   
    [SerializeField] private TextMeshProUGUI scoreText;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        highScoreText.text = "High Score : " + PlayerPrefs.GetFloat("HighScore",0).ToString("00000000");
        HideEndingPanel();
    }

    private void Update()
    {
        PanelResume();

        //Score overtime
        if (!PlayerMovement.instance.isDead)
        {
            time += Time.deltaTime * 100;
            if(time > 1)
            {
                ScorePlus(1);
                if (powerupScore)
                {
                    ScorePlus(10);
                }
                time = 0;
            }

            //Set Score Text
            scoreText.text = score.ToString("00000000");
            endingScoreText.text = "Score : " + score.ToString("00000000");

            if (score > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", score);
            }
            highScoreText.text = "High Score : " + PlayerPrefs.GetFloat("HighScore").ToString("00000000");

            //Speed up the game
            ItemMove.instance.speed = itemSpeedup;
            Spawner.instance.spawnerTimer = spawnerSpeedup;
            BgMove.instance.speed = bgSpeedup;
            ObstacleMove.instance.speed = obstacleSpeedup;
            timeCounter += Time.deltaTime;
            if (timeCounter > 1.5f && spawnerSpeedup > 0.7f)
            {
                bgSpeedup += 0.2f;
                obstacleSpeedup += 0.2f;
                itemSpeedup += 0.2f;
                spawnerSpeedup -= 0.02f;
                timeCounter = 0;
            }

            if(score > 50000)
            {
                Light.instance.NightTime();
            }
        }
    }

    public void ScorePlus(int number)
    {
        score += number;
    }

    public void RestartScene()
    {
        HideMenu();
        HideEndingPanel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PanelResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerMovement.instance.isDead)
        {
            if (pausePanel.active)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    public void HideMenu()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowMenu()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeleteScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    public IEnumerator ShowEndingPanel()
    {
        yield return new WaitForSeconds(1);
        endingPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideEndingPanel()
    {
        endingPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitButton()
    {
        Application.Quit();
        DeleteScore();
        /*
        StartCoroutine(ExitGame());
        DeleteScore();
        transition.SetTrigger("Start");
        */
    }

    IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("B");
        QuitGame();
    }

    void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

}
