using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawnTime = 10f;
    [SerializeField] EnemyMovement enemyViewPrefab;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public IEnumerator Init(Node startNode, float secondsBetweenSpawntime)
    {
        while (true)
        {
            Instantiate(enemyViewPrefab, startNode.position, Quaternion.identity, this.transform);
            yield return new WaitForSeconds(secondsBetweenSpawnTime);
        }


    }
}
