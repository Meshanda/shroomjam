using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
{
    public static Action<Enums.GameOverType> OnGameOver;
    public static Action OnGameWin;
    public static Action<float> OnEnemyHitBase;
    
    [Header("Database")]
    [SerializeField] private EnemyDatabaseSO _enemyDatabase;
    public EnemyDatabaseSO EnemyDatabase => _enemyDatabase;
    
    [Header("Tower Prefabs")]
    [SerializeField] private BasicTower _basicTowerPrefab;
    [SerializeField] private FastTower _fastTowerPrefab;
    [SerializeField] private SniperTower _sniperTowerPrefab;
    [SerializeField] private SupportTower _supportTowerPrefab;

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

    public int GetTowerCost(Enums.TowerType type)
    {
        return type switch
        {
            Enums.TowerType.Basic => _basicTowerPrefab.Cost,
            Enums.TowerType.Fast => _fastTowerPrefab.Cost,
            Enums.TowerType.Sniper => _sniperTowerPrefab.Cost,
            Enums.TowerType.Support => _supportTowerPrefab.Cost,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}