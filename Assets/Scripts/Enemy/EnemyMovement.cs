using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Vector3 m_currentPosition; 
    public Vector3 CurrentPosition { get { return m_currentPosition; } }
    Node goalNodePosition;
    GameManager gameManager;
    public float moveSpeed = 1.5f;
    public float delay = 0f;
    public float movementDelay = 0.5f;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    bool isMoving = false;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        if (pathfinder != null)
        {
            goalNodePosition = pathfinder.GoalNode;
        }

        var path = pathfinder.PathNodes;
        if (path != null && goalNodePosition.position != null)
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



    public IEnumerator FollowPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            if (gameObject != null)
            {
                isMoving = true;
                yield return new WaitForSeconds(movementDelay);
                iTween.MoveTo(gameObject, iTween.Hash(
                    "x", node.position.x,
                    "y", node.position.y,
                    "z", node.position.z,
                    "delay", delay,
                    "easetype", easeType,
                    "speed", moveSpeed
                ));

                while (Vector3.Distance(node.position, transform.position) > 0.01f)
                {
                    yield return null;
                }

                iTween.Stop(gameObject);
                transform.position = node.position;
                isMoving = true;
            }
        }
    }

    public void EnemyReachedGoal()
    {
        Destroy(gameObject);
        gameManager.lives--;
        Debug.Log(gameManager.lives);
    }

}
