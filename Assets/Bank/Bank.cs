using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

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
        if (currentBalance < 0)
        {
            var scene = EditorSceneManager.GetActiveScene().buildIndex;
            EditorSceneManager.LoadSceneAsync(scene);
        }
    }
}
