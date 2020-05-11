using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.PathNodes;
        if (path != null)
        {
            StartCoroutine(FollowPath(path));
        }
        Debug.Log(path);

    }

    IEnumerator FollowPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            transform.position = node.position;
            yield return new WaitForSeconds(2f);
        }
    }
}
