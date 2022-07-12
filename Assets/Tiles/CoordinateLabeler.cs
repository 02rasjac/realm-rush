using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blocketColor = Color.black;

    TextMeshPro tmp;
    Vector2Int coord;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        tmp.enabled = false;
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            tmp.enabled = true;
            DisplayCoordinates();
            UpdateName();
        }

        ColorCoordinates();
        ToggleLabels();
    }

    void ColorCoordinates()
    {
        if (GetComponentInParent<Waypoint>().IsPlaceable)
        {
            tmp.color = defaultColor;
        }
        else
        {
            tmp.color = blocketColor;
        }
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
