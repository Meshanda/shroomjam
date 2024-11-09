using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Tilemap _specialTilemap;
    public Tilemap SpecialTilemap => _specialTilemap;
    public enum GameOverType { BaseDestroyed, PlayerDead };
    
    public static Action<GameOverType> OnGameOver;
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
        if (_specialTilemap == null) throw new ArgumentNullException("Special Tilemap is not assigned");
    }

    private void OnGameOverHandler(GameOverType gameOverType)
    {
        // You lose
        Debug.Log("You lost because " + gameOverType + " lmao");
    }

    private void OnEnemyHitBaseHandler(float baseCorruption)
    {
        // Change Camera based on "baseCorruption". And Everything we want to change based on the corruption on the base
    }
}