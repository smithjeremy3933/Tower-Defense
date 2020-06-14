using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeleteButton : MonoBehaviour
{
    TowerSelector towerSelector;
    GameManager gameManager;
    TowerFactory towerFactory;

    private void Start()
    {
        towerSelector = FindObjectOfType<TowerSelector>();
        gameManager = FindObjectOfType<GameManager>();
        towerFactory = FindObjectOfType<TowerFactory>();
    }

    public void DeleteTower()
    {
        if (!towerFactory.nodeTowerMap.ContainsKey(towerSelector.CurrentNode))
        {
            Debug.Log("There is no valid tower to delete.");
            return;
        }
        Destroy(towerSelector.SelectedGameobject);
        towerFactory.nodeTowerMap.Remove(towerSelector.CurrentNode);
        towerSelector.CurrentNode.nodeType = NodeType.Open;
        towerSelector.CurrentNode = null;
        towerSelector.CurrentTower = null;
        towerSelector.SelectedGameobject = null;
        gameManager.currentTowerName.text = "(No Tower)";
        gameManager.currentTowerDamage.text = "DAMAGE: 0";
    
    }
}
