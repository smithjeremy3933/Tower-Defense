using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 100;
    Enemy enemy;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        enemy = FindObjectOfType<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (healthPoints < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
        gameManager.cashAmount += enemy.enemyValue;
    }

    void ProcessHit()
    {
        healthPoints = healthPoints - 1;

    }
}
