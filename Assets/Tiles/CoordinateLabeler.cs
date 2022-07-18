using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.black;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
    [SerializeField] Color exploredColor = Color.yellow;

    GridManageer grid;

    TextMeshPro tmp;
    Vector2Int coord;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        tmp.enabled = false;
        DisplayCoordinates();
    }

    void Start()
    {
        grid = FindObjectOfType<GridManageer>();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            tmp.enabled = true;
            DisplayCoordinates();
            UpdateName();
        }

        SetCoordinateColor();
        ToggleLabels();
    }

    void SetCoordinateColor()
    {
        if (grid == null) { return; }

        Node node = grid.GetNode(coord);
        if (node == null) { return; }

        if (!node.isWalkable)
            tmp.color = blockedColor;
        else if (node.isPath)
            tmp.color = pathColor;
        else if (node.isExplored)
            tmp.color = exploredColor;
        else
            tmp.color = defaultColor;
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            tmp.enabled = !tmp.enabled;
        }
    }

    void DisplayCoordinates()
    {
        coord.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coord.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
        tmp.text = $"{coord.x}, {coord.y}";
    }

    void UpdateName()
    {
        transform.parent.name = coord.ToString();
    }
}
