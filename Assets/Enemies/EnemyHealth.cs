using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 2;

    private void OnParticleCollision(GameObject other)
    {
        health--;
        if (health < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
