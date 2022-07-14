using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int buildCost = 75;

    Bank bank;

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
}
