using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0.1f, 30f)] float spawnTime = 1f;
    [SerializeField] [Range(0, 50)] int maxActiveInstances = 5;
    [SerializeField] int difficultyRamp = 1;
    [SerializeField] [Range(0, 50)] int poolSize = 10;

    GameObject[] pool;

    void Start()
    {
        PopulatePool();
        StartCoroutine(SpawnEnemies());
    }

    public void IncreaseDifficulty()
    {
        maxActiveInstances += difficultyRamp;
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

    GameObject FindAvailableInstance(ref int numOfActives)
    {
        GameObject firstAvailable = null;
        foreach (var item in pool)
        {
            if (item.activeInHierarchy)
                numOfActives++;
            else if (!item.activeInHierarchy && firstAvailable == null)
                firstAvailable = item;
        }

        return firstAvailable;
    }

    GameObject FindAvailableInstance()
    {
        int numOfActives = 0;
        return FindAvailableInstance(ref numOfActives);
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int numOfActives = 0;
            var obj = FindAvailableInstance(ref numOfActives);
            if (obj == null || numOfActives >= maxActiveInstances)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }
            obj.SetActive(true);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
