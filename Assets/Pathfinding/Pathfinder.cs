using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Node currentSearchNode;
    GridManageer gridManageer;
    Vector2Int[] directionOrder = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    void Start()
    {
        gridManageer = FindObjectOfType<GridManageer>();
        if (gridManageer != null)
        {
            currentSearchNode = gridManageer.GetNode(currentSearchNode.coordinates);
            currentSearchNode.isPath = true;
        }
        ExploreNeighbors();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int dir in directionOrder)
        {
            Node node = gridManageer.GetNode(currentSearchNode.coordinates + dir);
            if (node == null)
                continue;
            node.isExplored = true;
            neighbors.Add(node);
        }
    }
}
