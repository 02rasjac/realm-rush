using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [Tooltip("Tiles per second.")]
    [SerializeField] [Range(0f, 5f)] float speed = .1f;
    [Tooltip("Increase speed by this value when next diff-increase is speed")]
    [SerializeField] float difficultyRamp = 1f;
    
    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManageer gridManageer;
    Pathfinder pathfinder;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManageer = FindObjectOfType<GridManageer>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    public void IncreaseSpeed()
    {
        speed += difficultyRamp;
    }

    void ReturnToStart()
    {
        transform.position = gridManageer.GetPosFromCoord(pathfinder.StartCoordinate);
    }

    void RecalculatePath()
    {
        path.Clear();
        path = pathfinder.CreateNewPath();
    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManageer.GetPosFromCoord(path[i].coordinates);
            float movePerc = 0f;

            while (movePerc <= 1f)
            {
                movePerc += Time.deltaTime * speed;
                transform.LookAt(endPos);
                transform.position = Vector3.Lerp(startPos, endPos, movePerc);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
