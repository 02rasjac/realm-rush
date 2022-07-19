using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManageer : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [Tooltip("Should match the size of grid-snapping.")]
    [SerializeField] int unityGridSize = 10;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coord)
    {
        if (!grid.ContainsKey(coord)) { return null; }

        return grid[coord];
    }

    public Vector2Int GetCoordFromPos(Vector3 pos)
    {
        Vector2Int coord = new Vector2Int();
        coord.x = Mathf.RoundToInt(pos.x / unityGridSize);
        coord.y = Mathf.RoundToInt(pos.z / unityGridSize);

        return coord;
    }
    
    public Vector3 GetPosFromCoord(Vector2Int coord)
    {
        Vector3 pos = new Vector3();
        pos.x = coord.x * unityGridSize;
        pos.z = coord.y * unityGridSize;

        return pos;
    }

    public void BlockNode(Vector2Int coord)
    {
        if (grid.ContainsKey(coord))
            grid[coord].isWalkable = false;
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
            entry.Value.connectedTo = null;
        }
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);
                grid.Add(coord, new Node(coord, true));
            }
        }
    }
}
