using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    
    
    public static Action<Enums.GameOverType> OnGameOver;

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

    private void OnGameOverHandler(Enums.GameOverType gameOverType)
    {
        // You lose
        Debug.Log("You lost because " + gameOverType + " lmao");
    }
}