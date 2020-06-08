using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public float secondsBetweenSpawnTime = 10f;
    [SerializeField] GameObject enemyViewPrefab;
    GameManager gameManager;
    public float scaleTime = 0.3f;
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public IEnumerator Init(Node startNode, float secondsBetweenSpawntime)
    {
        while (gameManager.IsGamePLaying == true && enemyViewPrefab != null && gameManager.levelOneEnemyCount != 0)
        {
            if (enemyViewPrefab != null && startNode.position != null)
            {
                GameObject instance = Instantiate(enemyViewPrefab, startNode.position, Quaternion.identity, this.transform);
                instance.transform.localScale = Vector3.zero;
                ShowEnemy(instance);
                gameManager.levelOneEnemyCount--;

            }
            yield return new WaitForSeconds(secondsBetweenSpawnTime);
        }


    }

    public IEnumerator InitLevelTwo(Node startNode, float secondsBetweenSpawntime)
    {
        while (gameManager.IsGamePLaying == true && enemyViewPrefab != null && gameManager.levelTwoEnemyCount != 0)
        {
            if (enemyViewPrefab != null && startNode.position != null)
            {
                GameObject instance = Instantiate(enemyViewPrefab, startNode.position, Quaternion.identity, this.transform);
                instance.transform.localScale = Vector3.zero;
                ShowEnemy(instance);
                gameManager.levelTwoEnemyCount--;

            }
            yield return new WaitForSeconds(secondsBetweenSpawnTime);
        }


    }

    public void ShowEnemy(GameObject instance)
    {
        if (instance != null)
        {
            iTween.ScaleTo(instance, Vector3.one, scaleTime);
        }
    }
}
