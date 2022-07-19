using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable = false;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManageer grid;

    void Start()
    {
        grid = FindObjectOfType<GridManageer>();
        
        if (grid != null && !isPlaceable)
            grid.BlockNode(grid.GetCoordFromPos(transform.position));
    }

    void OnMouseDown()
    {
        if (isPlaceable)
        {
            isPlaceable = !towerPrefab.CreateTower(towerPrefab, transform);
        }
    }
}
