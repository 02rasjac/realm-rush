using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0.1f, 30f)] float spawnTime = 1f;
    [SerializeField] [Range(0, 50)] int poolSize = 10;

    GameObject[] pool;

    void Start()
    {
        PopulatePool();
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemy, this.transform);
            pool[i].SetActive(false);
        }
    }

    GameObject FindAvailableInstance()
    {
        foreach (var item in pool)
        {
            if (!item.activeInHierarchy)
                return item;
        }

        return null;
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            var obj = FindAvailableInstance();
            if (obj == null)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }
            obj.SetActive(true);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
