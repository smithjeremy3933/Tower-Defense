using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public static event EventHandler<OnTowerSelectedEventArgs> OnTowerSelected;
    public class OnTowerSelectedEventArgs : EventArgs
    {
        public Tower currentTower;
        public GameObject SelectedTowerGO;
        public int currentTowerDamage;
        public Node curentNode;
    }
    public int CurrentTowerDamage { get => m_currentTowerDamage; set => m_currentTowerDamage = value; }
    public Tower CurrentTower { get => m_currentTower; set => m_currentTower = value; }
    public GameObject SelectedGameobject { get => m_selectedGameobject; set => m_selectedGameobject = value; }
    public Node CurrentNode { get => m_currentNode; set => m_currentNode = value; }

    TowerFactory towerFactory;
    Graph graph;
    GameManager gameManager;
    Ray ray;
    Tower previousSelectedTower;
    Tower m_currentTower;
    GameObject m_selectedGameobject;
    Node m_currentNode;
    int m_currentTowerDamage;

    private void Start()
    {
        graph = FindObjectOfType<Graph>();
        towerFactory = FindObjectOfType<TowerFactory>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        SelectTower();
    }

    private void SelectTower()
    {
        if (Input.GetMouseButtonDown(1) && towerFactory.nodeTowerMap != null)
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
                m_currentNode = hitTowerNode;
                SelectValidTower(selectedGameobject, selectedTower);
            }
            else if (selectedTower == null)
            {
                return;
            }

        }
        else if (Input.GetMouseButtonDown(1) && towerFactory.nodeTowerMap == null)
        {
            Debug.Log("No towers to Select!");
            return;
        }
    }

    private void SelectValidTower(GameObject selectedGameobject, Tower selectedTower)
    {
        ProcessSelection(selectedGameobject, selectedTower);

        switch (selectedGameobject.name)
        {
            case "FireTowerView(Clone)":
                FireBullet fireBullet = selectedTower.projectileParticle.GetComponent<FireBullet>();
                m_currentTowerDamage = fireBullet.damage;
                OnTowerSelected?.Invoke(this, new OnTowerSelectedEventArgs {
                    currentTower = m_currentTower,
                    SelectedTowerGO = m_selectedGameobject,
                    curentNode = m_currentNode,
                    currentTowerDamage = m_currentTowerDamage });
                gameManager.currentTowerName.text = "Fire Tower";
                gameManager.currentTowerDamage.text = "DAMAGE: " + fireBullet.damage.ToString();
                break;
            case "IceTowerView(Clone)":
                IceBullet iceBullet = selectedTower.projectileParticle.GetComponent<IceBullet>();
                m_currentTowerDamage = iceBullet.damage;
                gameManager.currentTowerName.text = "Ice Tower";
                gameManager.currentTowerDamage.text = "DAMAGE: " + iceBullet.damage.ToString();
                break;
            case "LightningTowerView(Clone)":
                LightningBullet lightningBullet = selectedTower.projectileParticle.GetComponent<LightningBullet>();
                m_currentTowerDamage = lightningBullet.damage;
                gameManager.currentTowerName.text = "Lightning Tower";
                gameManager.currentTowerDamage.text = "DAMAGE: " + lightningBullet.damage.ToString();
                break;
            default:
                gameManager.currentTowerName.text = selectedGameobject.name;
                break;
        }
    }

    private void ProcessSelection(GameObject selectedGameobject, Tower selectedTower)
    {
        if (previousSelectedTower != null && previousSelectedTower != selectedTower)
        {
            previousSelectedTower.IsSelected = false;
        }
        previousSelectedTower = selectedTower;
        selectedTower.IsSelected = true;
        m_currentTower = selectedTower;
        m_selectedGameobject = selectedGameobject;
    }
}
