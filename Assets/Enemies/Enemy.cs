using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxGoldReward = 25;
    [SerializeField] int minGoldReward = 5;
    [SerializeField][Range(0, 15)] int goldRewardRamp = 1;
    [SerializeField] int goldPenalty    = 25;

    int goldReward;

    Bank bank;
    ObjectPool pool;

    Array difficultyIncreases;
    DifficultyIncrease nextIncrease;

    public enum DifficultyIncrease
    {
        health,
        speed,
        numOfEnemies,
        spawnTime,
        goldReward
    }

    void OnEnable()
    {
        UpdateNextDiffIncrease();
    }

    void Awake()
    {
        goldReward = maxGoldReward;
        difficultyIncreases = Enum.GetValues(typeof(DifficultyIncrease));
        UpdateNextDiffIncrease();
    }

    void Start()
    {
        bank = FindObjectOfType<Bank>();
        pool = FindObjectOfType<ObjectPool>();
    }

    public void IncreaseDifficulty()
    {
        switch (nextIncrease)
        {
            case DifficultyIncrease.health:
                GetComponent<EnemyHealth>().IncreaseMaxHealth();
                break;
            case DifficultyIncrease.speed:
                GetComponent<EnemyMover>().IncreaseSpeed();
                break;
            case DifficultyIncrease.numOfEnemies:
                pool.IncreaseMaxActive();
                break;
            case DifficultyIncrease.spawnTime:
                pool.DecreaseSpawnTime();
                break;
            case DifficultyIncrease.goldReward:
                DecreaseGoldReward();
                break;
            default:
                break;
        }

        UpdateNextDiffIncrease();
    }

    void DecreaseGoldReward()
    {
        if (goldReward >= minGoldReward)
        {
            goldReward -= goldRewardRamp;
        }

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
