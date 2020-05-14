using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField] GameObject towerViewPrefab;
    TowerFactory towerFactory;
    TowerButton[] towerButtons;
    

    private void Start()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        towerButtons = FindObjectsOfType<TowerButton>();
    }

    public void ClickedSelectedTower()
    {
        towerFactory.SelectTower(towerViewPrefab);
    }
}
