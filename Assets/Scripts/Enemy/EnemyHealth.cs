using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int healthPoints = 100;
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
        float radius;
        if (other.name == "Bullet")
        {
            damage = other.GetComponent<FireBullet>().damage;
            radius = other.GetComponent<FireBullet>().radius;
            ProcessHit(damage, radius);
        }
        else if (other.name == "IceBullet")
        {
            damage = other.GetComponent<IceBullet>().damage;
            radius = other.GetComponent<IceBullet>().radius;
            ProcessHit(damage, radius);
        }
        else if (other.name == "LightningBullet")
        {
            damage = other.GetComponent<LightningBullet>().damage;
            radius = other.GetComponent<LightningBullet>().radius;
            other.GetComponent<LightningBullet>().ProcessHit(damage, radius, healthPoints);
        }
        else
        {
            damage = 1;
            radius = 0;
        }

        if (healthPoints < 1)
        {
            KillEnemy();
        }
        
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
        gameManager.cashAmount += enemy.enemyValue;
    }

    public void ProcessHit(int damage, float radius)
    {
        if (radius == 0)
        {
            DoDamage(damage);
        }
        else
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, radius);        
            foreach (Collider c in cols)
            {
                if (c.GetComponent<EnemyHealth>())
                {
                    c.GetComponent<EnemyHealth>().DoDamage(damage);
                }
            }
        }


    }

    public void DoDamage(int damage)
    {
        healthPoints = healthPoints - damage;
    }
}
