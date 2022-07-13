using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] int currentHealth = 5;

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHealth--;
        if (currentHealth < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
