using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinate;
    public Vector2Int StartCoordinate { get { return startCoordinate; } }
    [SerializeField] Vector2Int endCoordinate;
    public Vector2Int EndCoordinate { get { return endCoordinate; } }

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
            endNode = gridManageer.GetNode(endCoordinate);
        }

        CreateNewPath();
    }

    public List<Node> CreateNewPath()
    {
        gridManageer.ResetNodes();
        BreadFirstSearch();
        return BuildPath();
    }

    public bool WillBlockPath(Vector2Int coord)
    {
        if (gridManageer.Grid.ContainsKey(coord))
        { 
            var previousState = gridManageer.GetNode(coord).isWalkable;
            gridManageer.GetNode(coord).isWalkable = false;
            List<Node> newPath = CreateNewPath();
            gridManageer.GetNode(coord).isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                CreateNewPath();
                return true;
            }
        }
        
        return false;
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        
        foreach (Vector2Int dir in directionOrder)
        {
            Node neighbor = gridManageer.GetNode(currentSearchNode.coordinates + dir);
            if (neighbor != null && neighbor.isWalkable)
                neighbors.Add(neighbor);
        }

        foreach (Node neighbor in neighbors)
        {
            if (!searched.ContainsKey(neighbor.coordinates))
            {
                neighbor.connectedTo = currentSearchNode;
                searched.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadFirstSearch()
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;

        frontier.Clear();
        searched.Clear();

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

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            currentNode.isPath = true;
            path.Add(currentNode);
        }

        path.Reverse();

        return path;
    }
}
