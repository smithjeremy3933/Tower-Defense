using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool m_hasLevelStarted = false;

    bool m_hasLevelTwoStarted = false;

    bool m_isGamePLaying = false;

    bool m_isGameOver = false;

    bool m_hasLevelFinished = false;

    public bool HasLevelStarted { get => m_hasLevelStarted; set => m_hasLevelStarted = value; }
    public bool IsGamePLaying { get => m_isGamePLaying; set => m_isGamePLaying = value; }
    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
    public bool HasLevelFinished { get => m_hasLevelFinished; set => m_hasLevelFinished = value; }
    public TileController tileController;
    public EnemySpawner enemySpawner;

    public float delay = 2f;
    public int lives = 1;
    public int cashAmount = 1000;
    public int levelOneEnemyCount = 3;
    public int levelTwoEnemyCount = 3;

    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent playLevelTwoEvent;
    public UnityEvent endLevelEvent;
    [SerializeField] Text livesText;
    [SerializeField] Text cashText;
    [SerializeField] Text levelText;
    [SerializeField] public Text currentTowerName;
    [SerializeField] public Text currentTowerDamage;
    List<EnemyHealth> enemies;
    int currentLevel;
    float levelDelay = 3f;


    private void Start()
    {
        StartCoroutine("RunGameLoop");
    }

    private void Update()
    {
        DisplayLives();
        DisplayCash();
        DisplayLevel();
        enemies = FindObjectsOfType<EnemyHealth>().ToList();
        ControlTime();
    }

    IEnumerator RunGameLoop()
    {
        yield return StartCoroutine("StartLevelRoutine");
        yield return StartCoroutine("PlayLevelRoutine");
        if (lives > 0)
        {
            yield return StartCoroutine("PlayLevelTwoRoutine");
        }
        yield return StartCoroutine("EndLevelRoutine");
    }

    // Initial stage after the level is loaded
    IEnumerator StartLevelRoutine()
    {
        Debug.Log("START LEVEL");
        while (!m_hasLevelStarted)
        {
            // Show start screen.
            // User Presses button to start.
            // HasLevelStarted = true.
            
            yield return null;
        }

        if (startLevelEvent != null)
        {
            startLevelEvent.Invoke();
        }

    }

    // Gameplay Stage.
    IEnumerator PlayLevelRoutine()
    {
        Debug.Log("PLAY LEVEL 1");
        currentLevel = 1;
        m_isGamePLaying = true;
        m_hasLevelStarted = true;
        yield return new WaitForSeconds(delay);
        tileController.InitMap();


        if (playLevelEvent != null)
        {
            playLevelEvent.Invoke();
        }

        while (!m_isGameOver)
        {
            // Check for Game Over condition
            // Win?
            if (enemies.Count == 0 && levelOneEnemyCount == 0)
            {
                break;
            }

            // Lose?
            m_isGameOver = IsLoser();

            // m_isGameOver = true
            yield return null;
        }
     
    }

    IEnumerator PlayLevelTwoRoutine()
    {
        Debug.Log("START LEVEL 2");
        currentLevel = 2;
        yield return new WaitForSeconds(delay);
        StartCoroutine(enemySpawner.InitLevelTwo(tileController.StartNode, levelDelay));

        if (playLevelTwoEvent != null)
        {
            playLevelTwoEvent.Invoke();
        }

        while (!m_isGameOver)
        {
            // Check for Game Over condition
            // Win?
            m_isGameOver = IsWinner();

            // Lose?
            m_isGameOver = IsLoser();

            // m_isGameOver = true
            yield return null;
        }
        m_isGamePLaying = false;
    }

    // End Stage after gameplay is complete.
    IEnumerator EndLevelRoutine()
    {
        Debug.Log("END LEVEL");
        Time.timeScale = 0;
        if (endLevelEvent != null)
        {
            endLevelEvent.Invoke();
        }

        while (!m_hasLevelFinished)
        {
            yield return null;
        }
   
        RestartLevel();
    }

    private void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }

    public void PlayLevel()
    {
        m_hasLevelStarted = true;
    }

    private void DisplayLives()
    {
        int currentLives = lives;
        livesText.text = "Lives: " + currentLives.ToString();
    }

    private void DisplayCash()
    {
        int currentCash = cashAmount;
        cashText.text = "Cash: " + "$" + currentCash.ToString();
    }

    private void DisplayLevel()
    {
        levelText.text = "WAVE: " + currentLevel.ToString();
        if (levelText.text == null)
        {
            levelText.text = "WAVE: 1";
        }
    }

    private void ControlTime()
    {
        if (Input.GetKeyDown(KeyCode.Space) && lives > 0 && enemies.Count > 0)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    private bool IsLoser()
    {
        if (lives <= 0)
        {
            return true;

        }
        return false;
    }

    private bool IsWinner()
    {
        return false;
    }
}


