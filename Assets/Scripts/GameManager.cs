using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool m_hasLevelStarted = false;

    bool m_isGamePLaying = false;

    bool m_isGameOver = false;

    bool m_hasLevelFinished = false;

    public bool HasLevelStarted { get => m_hasLevelStarted; set => m_hasLevelStarted = value; }
    public bool IsGamePLaying { get => m_isGamePLaying; set => m_isGamePLaying = value; }
    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
    public bool HasLevelFinished { get => m_hasLevelFinished; set => m_hasLevelFinished = value; }
    public TileController tileController;

    public float delay = 2f;
    public int lives = 1;
    public int cash = 1000;
    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;
    [SerializeField] Text livesText;
    [SerializeField] Text cashText;


    private void Start()
    {
        StartCoroutine("RunGameLoop");
    }

    private void Update()
    {
        DisplayLives();
        DisplayCash();
    }

    IEnumerator RunGameLoop()
    {
        yield return StartCoroutine("StartLevelRoutine");
        yield return StartCoroutine("PlayLevelRoutine");
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
        Debug.Log("PLAY LEVEL");
        m_isGamePLaying = true;
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
            m_isGameOver = IsWinner();

            // Lose?
            m_isGameOver = IsLoser();

            // m_isGameOver = true
            yield return null;
        }
        m_isGamePLaying = false;
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

    // End Stage after gameplay is complete.
    IEnumerator EndLevelRoutine()
    {
        Debug.Log("END LEVEL");
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
        int currentCash = cash;
        cashText.text = "Cash: " + "$" + currentCash.ToString();
    }
}


