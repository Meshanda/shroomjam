using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Tilemap _specialTilemap;
    public Tilemap SpecialTilemap => _specialTilemap;
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
        if (_specialTilemap == null) throw new ArgumentNullException("Special Tilemap is not assigned");
    }

    private void OnGameOverHandler(GameOverType gameOverType)
    {
        // You lose
        Debug.Log("You lost because " + gameOverType + " lmao");
    }
}