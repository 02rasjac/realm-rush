using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = false;

    void OnMouseDown()
    {
        if (isPlaceable)
        {
            Debug.Log(this.name); 
        }
    }
}
