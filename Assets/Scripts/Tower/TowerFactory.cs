using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    GameObject towerViewPrefab;
    [SerializeField] GameObject defaultFireTowerPrefab;
    [SerializeField] Transform towerParentTranform;
    [SerializeField] float scaleTime = 0.3f;
    [SerializeField] int towerCost = 200;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        towerViewPrefab = defaultFireTowerPrefab;
    }

    public void SelectTower(GameObject towerToSelect)
    {
        towerViewPrefab = towerToSelect;
    }

    public void SpawnTower(Node baseNode)
    {
        if (towerCost <= gameManager.cashAmount && towerViewPrefab != null)
        {
            gameManager.cashAmount -= towerCost;
            GameObject instance = Instantiate(towerViewPrefab, baseNode.position, Quaternion.identity);
            instance.transform.parent = towerParentTranform;
            baseNode.nodeType = NodeType.Blocked;
            instance.transform.localScale = Vector3.zero;
            ShowTower(instance);
        }
        else
        {
            Debug.Log("You don't have enough money for a tower Bitch");
        }

    }

    public void ShowTower(GameObject instance)
    {
        if (instance != null)
        {
            iTween.ScaleTo(instance, Vector3.one, scaleTime);
        }
    }
}
