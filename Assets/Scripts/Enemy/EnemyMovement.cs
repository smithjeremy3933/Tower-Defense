using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static event EventHandler OnEnemyReachedGoal;
    public Vector3 CurrentPosition { get { return m_currentPosition; } }

    Vector3 m_currentPosition;
    Node goalNodePosition;
    float moveSpeed = 1.5f;
    float delay = 0f;
    float movementDelay = 0.5f;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    bool isMoving = false;
    bool isSlowed = false;

    void Start()
    {
        Move();
    }

    private void Move()
    {
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

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "IceBullet" && !isSlowed)
        {
            StartCoroutine(SlowMovement());
        }
    }

    public IEnumerator FollowPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            if (gameObject != null)
            {
                float slowedSpeed = moveSpeed * 0.5f;
                isMoving = true;
                yield return new WaitForSeconds(movementDelay);
                iTween.MoveTo(gameObject, iTween.Hash(
                    "x", node.position.x,
                    "y", node.position.y,
                    "z", node.position.z,
                    "delay", delay,
                    "easetype", easeType,
                    "speed", isSlowed ? slowedSpeed : moveSpeed
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
        OnEnemyReachedGoal?.Invoke(this, EventArgs.Empty);
    }

    IEnumerator SlowMovement()
    {
        isSlowed = true;
        yield return new WaitForSeconds(5f);
        isSlowed = false;
    }

}
