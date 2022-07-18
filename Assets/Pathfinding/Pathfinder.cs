using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    [SerializeField] Vector2Int endCoordinate;

    Node startNode;
    Node endNode;
    Node currentSearchNode;

    GridManageer gridManageer;
    Vector2Int[] directionOrder = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> searched = new Dictionary<Vector2Int, Node>();

    void Start()
    {
        gridManageer = FindObjectOfType<GridManageer>();
        if (gridManageer != null)
        {
            startNode = gridManageer.GetNode(startCoordinate);
            endNode   = gridManageer.GetNode(endCoordinate);
        }
        BreadFirstSearch();
    }

    void ExploreNeighbors()
    {
        foreach (Vector2Int dir in directionOrder)
        {
            Node node = gridManageer.GetNode(currentSearchNode.coordinates + dir);
            if (node == null || searched.ContainsKey(node.coordinates))
                continue;
            frontier.Enqueue(node);
        }
    }

    void BreadFirstSearch()
    {
        frontier.Enqueue(startNode);
        currentSearchNode = startNode;

        while (frontier.Count > 0 && currentSearchNode.coordinates != endCoordinate)
        {
            currentSearchNode = frontier.Dequeue();
            ExploreNeighbors();
            if (!searched.ContainsKey(currentSearchNode.coordinates))
                searched.Add(currentSearchNode.coordinates, currentSearchNode);
            currentSearchNode.isExplored = true;
        }
    }
}
