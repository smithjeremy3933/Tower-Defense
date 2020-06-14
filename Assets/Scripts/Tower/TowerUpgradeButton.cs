using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeButton : MonoBehaviour
{
    TowerSelector towerSelector;
    GameManager gameManager;
    // move base cost to projectile script so it can have an increased cost as damage increases.
    int m_baseDamageUpgradeCost = 200;

    private void Start()
    {
        towerSelector = FindObjectOfType<TowerSelector>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void IncreaseDamage()
    {
        if (towerSelector.CurrentTower == null)
        {
            return;
        }

        if (towerSelector.CurrentTower.name == "FireTowerView(Clone)" || towerSelector.CurrentTower.name == "Fire Tower")
        {
            if (m_baseDamageUpgradeCost <= gameManager.cashAmount)
            {
                FireBullet fireBullet = towerSelector.CurrentTower.projectileParticle.GetComponent<FireBullet>();
                fireBullet.damage = fireBullet.damage + 1;
                gameManager.currentTowerDamage.text = "DAMAGE: " + fireBullet.damage.ToString();
                gameManager.cashAmount = gameManager.cashAmount - m_baseDamageUpgradeCost;
            }
            else
            {
                Debug.Log("Not enough to upgrade a Fire Tower!");
                return;
            }


        }

        if (towerSelector.CurrentTower.name == "IceTowerView(Clone)" || towerSelector.CurrentTower.name == "Ice Tower")
        {
            IceBullet iceBullet = towerSelector.CurrentTower.projectileParticle.GetComponent<IceBullet>();
            iceBullet.damage = iceBullet.damage + 1;
            gameManager.currentTowerDamage.text = "DAMAGE: " + iceBullet.damage.ToString();
        }

        if (towerSelector.CurrentTower.name == "LightningTowerView(Clone)" || towerSelector.CurrentTower.name == "Lightning Tower")
        {
            LightningBullet lightningBullet = towerSelector.CurrentTower.projectileParticle.GetComponent<LightningBullet>();
            lightningBullet.damage = lightningBullet.damage + 1;
            gameManager.currentTowerDamage.text = "DAMAGE: " + lightningBullet.damage.ToString();
        }
    }
}
