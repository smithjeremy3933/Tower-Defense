using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    TowerFactory towerFactory;
    Graph graph;
    GameManager gameManager;
    Ray ray;
    Tower previousSelectedTower;


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
            if (!towerFactory.nodeTowerMap.ContainsKey(hitTowerNode))
            {
                Debug.Log("No tower to select!");
                return;
            }
            GameObject selectedGameobject = towerFactory.nodeTowerMap[hitTowerNode];
            Tower selectedTower = selectedGameobject.GetComponent<Tower>();
            if (hasHit && selectedGameobject != null && selectedTower != null && selectedTower.IsSelected == false)
            {
                if (previousSelectedTower != null && previousSelectedTower != selectedTower)
                {
                    previousSelectedTower.IsSelected = false;
                }
                previousSelectedTower = selectedTower;
                selectedTower.IsSelected = true;
                if (selectedGameobject.name == "FireTowerView(Clone)")
                {
                    gameManager.currentTowerName.text = "Fire Tower";
                    FireBullet fireBullet = selectedTower.projectileParticle.GetComponent<FireBullet>();
                    gameManager.currentTowerDamage.text = "DAMAGE: " + fireBullet.damage.ToString();
                }
                else
                {
                    gameManager.currentTowerName.text = selectedGameobject.name;
                }
            }
            else if (selectedTower == null)
            {
                return;
            }
            
        }
    }
}
