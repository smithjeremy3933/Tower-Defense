using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector3 m_currentPosition; 
    public Vector3 CurrentPosition { get { return m_currentPosition; } }
    Node goalNodePosition;
    GameManager gameManager;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        goalNodePosition = pathfinder.GoalNode;
        var path = pathfinder.PathNodes;
        if (path != null)
        {
            StartCoroutine(FollowPath(path));
        }

    }

    private void Update()
    {
        if (transform.position == goalNodePosition.position)
        {
            EnemyReachedGoal();
        }
    }

    IEnumerator FollowPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            transform.position = node.position;
            m_currentPosition = node.position;
            yield return new WaitForSeconds(2f);
        }
    }

    public void EnemyReachedGoal()
    {
        Destroy(gameObject);
        gameManager.lives--;
        Debug.Log(gameManager.lives);
    }
}
