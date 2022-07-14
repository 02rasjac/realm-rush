using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GoldUI : MonoBehaviour
{
    TMP_Text goldText;
    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
        goldText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        goldText.text = "Gold: " + bank.CurrentBalance.ToString();
    }
}
