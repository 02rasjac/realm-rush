using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] float range = 15f;
    
    Transform target;
    float distance;

    void Update()
    {
        FindClosestEnemy();
        AimWeapon();
        Shoot();
    }

    void AimWeapon()
    {
        weapon.transform.LookAt(target.transform.position);
    }

    void Shoot()
    {
        bool isShooting = distance <= range;
        var em = weapon.GetComponentInChildren<ParticleSystem>().emission;
        em.enabled = isShooting;
    }

    void FindClosestEnemy()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < maxDistance)
            {
                maxDistance = distance;
                closestTarget = enemy.transform;
            }
        }

        target = closestTarget;
        distance = maxDistance;
    }
}
