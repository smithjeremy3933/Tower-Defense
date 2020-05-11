using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawnTime = 10f;
    [SerializeField] GameObject enemyViewPrefab;

    public IEnumerator Init(Node startNode, float secondsBetweenSpawntime)
    {
        while (true) // Spawn forever
        {
            Instantiate(enemyViewPrefab, startNode.position, Quaternion.identity);
            yield return new WaitForSeconds(secondsBetweenSpawnTime);
        }
     
    }
}
