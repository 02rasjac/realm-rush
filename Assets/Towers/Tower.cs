using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int buildCost = 75;
    [SerializeField] float buildTimePerPart = 1f;

    Bank bank;

    void Start()
    {
        StartCoroutine(Build());    
    }

    public bool CreateTower(Tower towerPrefab, Transform wpTrans)
    {
        bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            Debug.Log("There is no bank.");
            return false;
        }

        if (bank.CurrentBalance >= buildCost)
        {
            Instantiate(towerPrefab, wpTrans.position, Quaternion.identity);
            bank.Withdraw(buildCost);
            return true;
        }

        Debug.Log("Building tower failed.");
        return false;
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTimePerPart);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

}
