using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeButton : MonoBehaviour
{
    TowerSelector towerSelector;
    GameManager gameManager;

    private void Start()
    {
        towerSelector = FindObjectOfType<TowerSelector>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void IncreaseDamage()
    {
        if (towerSelector.CurrentTower.name == "FireTowerView(Clone)" || towerSelector.CurrentTower.name == "Fire Tower")
        {
            FireBullet fireBullet = towerSelector.CurrentTower.projectileParticle.GetComponent<FireBullet>();
            fireBullet.damage = fireBullet.damage + 1;
            gameManager.currentTowerDamage.text = "DAMAGE: " + fireBullet.damage.ToString();
        }
        else if (towerSelector.CurrentTower.name == "IceTowerView(Clone)" || towerSelector.CurrentTower.name == "Ice Tower")
        {
            IceBullet iceBullet = towerSelector.CurrentTower.projectileParticle.GetComponent<IceBullet>();
            iceBullet.damage = iceBullet.damage + 1;
            gameManager.currentTowerDamage.text = "DAMAGE: " + iceBullet.damage.ToString();
        }
        else
        {
            LightningBullet lightningBullet = towerSelector.CurrentTower.projectileParticle.GetComponent<LightningBullet>();
            lightningBullet.damage = lightningBullet.damage + 1;
            gameManager.currentTowerDamage.text = "DAMAGE: " + lightningBullet.damage.ToString();
        }
    }
}
