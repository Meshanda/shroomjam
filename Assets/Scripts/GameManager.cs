using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    public static Action<Enums.GameOverType> OnGameOver;
    public static Action OnGameWin;
    public static Action<float> OnEnemyHitBase;

    private void OnEnable()
    {
        OnGameOver += OnGameOverHandler;
        OnEnemyHitBase += OnEnemyHitBaseHandler;
    }

    private void OnDisable()
    {
        OnGameOver -= OnGameOverHandler;
        OnEnemyHitBase -= OnEnemyHitBaseHandler;

    }

    public EnemyDatabaseSO EnemyDatabase;
    
    protected override void SingletonAwake()
    {
        Init();
    }

    private void Init()
    {
        EnemyDatabase.CheckDatabaseIntegrity();
    }

    private void OnGameOverHandler(Enums.GameOverType gameOverType)
    {
        // You lose
        Debug.Log("You lost because " + gameOverType + " lmao");
    }

    private void OnEnemyHitBaseHandler(float baseCorruption)
    {
        // Change Camera based on "baseCorruption". And Everything we want to change based on the corruption on the base
    }
}