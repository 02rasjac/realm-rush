using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro tmp;
    Vector2Int coord;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateName();
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
