using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Waypoint> waypoints;

    void Start()
    {
        foreach (Waypoint waypoint in waypoints)
        {
            Debug.Log($"{waypoint.name}");
        }
    }
}
