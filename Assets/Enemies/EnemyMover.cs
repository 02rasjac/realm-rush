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
    [SerializeField] List<Tile> path;

    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    public void IncreaseSpeed()
    {
        speed += difficultyRamp;
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FindPath()
    {
        path.Clear();
        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");
        foreach (Tile waypoint in pathParent.GetComponentsInChildren<Tile>()) 
        {
            path.Add(waypoint);
        }
    }

    IEnumerator FollowPath()
    {
        foreach (Tile waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
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
