using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject towerObject;
    [SerializeField] bool isPlaceable = false;
    public bool IsPlaceable { get { return isPlaceable; } }
    
    void OnMouseDown()
    {
        if (isPlaceable)
        {
            isPlaceable = false;
            Instantiate(towerObject, transform.position, Quaternion.identity);
        }
    }
}
