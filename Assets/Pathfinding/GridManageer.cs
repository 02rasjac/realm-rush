using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManageer : MonoBehaviour
{
    [SerializeField] Node node;

    void Start()
    {
        Debug.Log(node.coordinates);
        Debug.Log($"IsWalkable = {node.isWalkable}");
    }
}
