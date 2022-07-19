using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable = false;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManageer grid;
    Pathfinder pathfinder;

    void Start()
    {
        grid = FindObjectOfType<GridManageer>();
        pathfinder = FindObjectOfType<Pathfinder>();
        
        if (grid != null && !isPlaceable)
            grid.BlockNode(grid.GetCoordFromPos(transform.position));
    }

    void OnMouseDown()
    {
        var tileCoord = grid.GetCoordFromPos(transform.position);
        if (grid.GetNode(tileCoord).isWalkable && !pathfinder.WillBlockPath(tileCoord))
        {
            if (towerPrefab.CreateTower(towerPrefab, transform))
            {
                grid.BlockNode(tileCoord);
                pathfinder.NotifyRecievers();
            }
        }
    }
}
