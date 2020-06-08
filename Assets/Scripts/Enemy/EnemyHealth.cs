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
        int damage;
        if (other.name == "Bullet")
        {
            damage = other.GetComponent<FireBullet>().damage;
        }
        else if (other.name == "IceBullet")
        {
            damage = other.GetComponent<IceBullet>().damage;
        }
        else
        {
            damage = 1;
        }
        ProcessHit(damage);
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

    public void ProcessHit(int damage)
    {
        healthPoints = healthPoints - damage;

    }
}
