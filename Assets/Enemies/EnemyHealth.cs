using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] int difficultyRamp = 1;

    int currentHealth = 5;

    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void OnParticleCollision(GameObject other)
    {
        currentHealth--;
        if (currentHealth < 1)
        {
            enemy.RewardGold();
            maxHealth += difficultyRamp; // Increase difficulty
            gameObject.SetActive(false);
        }
    }
}
