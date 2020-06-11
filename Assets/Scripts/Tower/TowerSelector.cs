using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    TowerFactory towerFactory;
    Graph graph;
    GameManager gameManager;
    Ray ray;

    private void Start()
    {
        graph = FindObjectOfType<Graph>();
        towerFactory = FindObjectOfType<TowerFactory>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            Node hitTowerNode = graph.GetNodeAt((int)hit.transform.position.x, (int)hit.transform.position.z);
            GameObject selectedGameobject = towerFactory.nodeTowerMap[hitTowerNode];
            Tower selectedTower = selectedGameobject.GetComponent<Tower>();
            FireBullet fireBullet = selectedTower.projectileParticle.GetComponent<FireBullet>();
            if (selectedGameobject.name == "FireTowerView(Clone)")
            {
                gameManager.currentTowerName.text = "Fire Tower";
            }
            else
            {
                gameManager.currentTowerName.text = selectedGameobject.name;
            }
            
        }
    }
}
