using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward  = 25;
    [SerializeField] int goldPenalty = 25;

    Bank bank;

    Array difficultyIncreases;
    DifficultyIncrease nextIncrease;

    public enum DifficultyIncrease
    {
        health,
        speed
    }

    void OnEnable()
    {
        UpdateNextDiffIncrease();
    }

    void Awake()
    {
        difficultyIncreases = Enum.GetValues(typeof(DifficultyIncrease));
        UpdateNextDiffIncrease();
    }

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void IncreaseDifficulty()
    {
        switch (nextIncrease)
        {
            case DifficultyIncrease.health:
                GetComponent<EnemyHealth>().IncreaseDifficulty();
                break;
            case DifficultyIncrease.speed:
                GetComponent<EnemyMover>().IncreaseDifficulty();
                break;
            default:
                break;
        }

        UpdateNextDiffIncrease();
    }

    public void RewardGold()
    {
        if (bank == null) { return; }
        bank.Deposit(goldReward);
    }
    
    public void StealGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenalty);
    }

    void UpdateNextDiffIncrease()
    {
        System.Random rnd = new System.Random();
        nextIncrease = (DifficultyIncrease)difficultyIncreases.GetValue(rnd.Next(difficultyIncreases.Length));
    }
}
