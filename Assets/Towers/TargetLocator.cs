using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    
    GameObject target;

    void Start()
    {
        target = FindObjectOfType<Enemy>().gameObject;
    }

    void Update()
    {
        weapon.transform.LookAt(target.transform.position);
    }
}
