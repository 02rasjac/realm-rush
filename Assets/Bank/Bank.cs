using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBalance = 150;

    [SerializeField] int currentBalance; //? TEMPORARY
    public int CurrentBalance { get { return currentBalance; } }

    void Awake()
    {
        currentBalance = startBalance;
    }

    public void Deposit(int ammount)
    {
        currentBalance += Mathf.Abs(ammount);
    }

    public void Withdraw(int ammount)
    {
        currentBalance -= Mathf.Abs(ammount);
    }
}
