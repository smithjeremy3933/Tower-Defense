using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : MonoBehaviour
{
    public int damage = 1;
    public float radius = 2;
    EnemyHealth enemyHealth;

    public void ProcessHit(int damage, float radius, int health)
    {
        if (radius == 0)
        {
            DoDamage(damage, health);
        }
        else
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, radius);
          
            foreach (Collider c in cols)
            {
                if (c.GetComponent<EnemyHealth>())
                {
                    enemyHealth = c.GetComponent<EnemyHealth>();
                    enemyHealth.DoDamage(damage);
                    if (enemyHealth.healthPoints <= 0)
                    {
                        enemyHealth.KillEnemy();
                    }
                }
            }
        }


    }

    public void DoDamage(int damage, int healthPoints)
    {
        healthPoints = healthPoints - damage;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
