﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static float distance = 0;
    private static float difficultyMultiplier = 1f;
    private static float difficultyOffset = 100f;
    private static int enemyCount = 0;

    public static float Distance { get => distance; set => distance = value; }
    public static float DifficultyMultiplier { get => difficultyMultiplier; set => difficultyMultiplier = value; }
    public static float DifficultyOffset { get => difficultyOffset; set => difficultyOffset = value; }
    public static int EnemyCount { get => enemyCount; set => enemyCount = value; }


    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        difficultyMultiplier = 1 + (distance / difficultyOffset);
    }




}