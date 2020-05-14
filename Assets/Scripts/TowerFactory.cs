using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] GameObject TowerViewPrefab;
    [SerializeField] Transform towerParentTranform;
    [SerializeField] float scaleTime = 0.3f;

    public void SpawnTower(Node baseNode)
    {
        GameObject instance = Instantiate(TowerViewPrefab, baseNode.position, Quaternion.identity);
        instance.transform.parent = towerParentTranform;
        baseNode.nodeType = NodeType.Blocked;
        instance.transform.localScale = Vector3.zero;
        ShowTower(instance);
    }

    public void ShowTower(GameObject instance)
    {
        if (instance != null)
        {
            iTween.ScaleTo(instance, Vector3.one, scaleTime);
        }
    }
}
