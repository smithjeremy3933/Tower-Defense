using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 100;

    void Start()
    {

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
    }

    void ProcessHit()
    {
        healthPoints = healthPoints - 1;

    }
}
