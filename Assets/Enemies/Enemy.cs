using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Seconds per tile. Lower = faster")]
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    [SerializeField] List<Waypoint> path;

    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos   = waypoint.transform.position;
            float movePerc   = 0f;

            while (movePerc <= 1f)
            {
                movePerc += Time.deltaTime * speed;
                transform.LookAt(endPos);
                transform.position = Vector3.Lerp(startPos, endPos, movePerc);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
