using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    float delay = 2f;


    private void Start()
    {
        StartCoroutine("RunGameLoop");
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

    }

    // Gameplay Stage.
    IEnumerator PlayLevelRoutine()
    {
        Debug.Log("PLAY LEVEL");
        m_isGamePLaying = true;
        yield return new WaitForSeconds(delay);
        tileController.InitMap();

        while (!m_isGameOver)
        {
            // Check for Game Over condition
            // Win?
            // Lose?

            // m_isGameOver = true
            yield return null;
        }
    }

    // End Stage after gameplay is complete.
    IEnumerator EndLevelRoutine()
    {

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
}


