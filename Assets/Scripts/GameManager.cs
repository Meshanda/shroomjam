using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameOverType { BaseDestroyed, PlayerDead };
    public static Action<GameOverType> OnGameOver;

    private void OnEnable()
    {
        OnGameOver += OnGameOverHandler;
    }

    private void OnDisable()
    {
        OnGameOver -= OnGameOverHandler;
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

    private void OnGameOverHandler(GameOverType gameOverType)
    {
        // You lose
    }
}