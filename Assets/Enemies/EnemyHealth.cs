using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [Tooltip("Increase health by this value when next diff-increase is health")]
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

    public void IncreaseDifficulty()
    {
        maxHealth += difficultyRamp;
    }

    void OnParticleCollision(GameObject other)
    {
        currentHealth--;
        if (currentHealth < 1)
        {
            enemy.RewardGold();
            enemy.IncreaseDifficulty();

            gameObject.SetActive(false);
        }
    }
}
