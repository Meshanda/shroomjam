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
    
    [Header("Canvas")]
    [SerializeField] private Canvas _hudCanvas;
    [SerializeField] private Canvas _tooltipCanvas;
    [SerializeField] private Canvas _endScreenCanvas;
    
    private EndScreen _endScreen;
    
    private void OnEnable()
    {
        OnGameOver += OnGameOverHandler;
        OnEnemyHitBase += OnEnemyHitBaseHandler;
        OnGameWin += OnGameWinHandler;
    }

    private void OnDisable()
    {
        OnGameOver -= OnGameOverHandler;
        OnEnemyHitBase -= OnEnemyHitBaseHandler;
        OnGameWin -= OnGameWinHandler;
    }
    
    private void Start()
    {
        _hudCanvas.gameObject.SetActive(true);
        _tooltipCanvas.gameObject.SetActive(true);
        _endScreenCanvas.gameObject.SetActive(false);
    }

    protected override void SingletonAwake()
    {
        Init();
        _endScreen = _endScreenCanvas.GetComponent<EndScreen>();
    }

    private void Init()
    {
        EnemyDatabase.CheckDatabaseIntegrity();
    }

    private void OnGameOverHandler(Enums.GameOverType gameOverType)
    {
        var subtitle = gameOverType switch
        {
            Enums.GameOverType.BaseDestroyed => "The base has been destroyed!",
            Enums.GameOverType.PlayerDead => "You died!",
            _ => throw new ArgumentOutOfRangeException(nameof(gameOverType), gameOverType, null)
        };
        
        ToggleEndScreen(true);
        _endScreen.Init("Game Over", subtitle);
    }
    
    private void OnGameWinHandler()
    {
        ToggleEndScreen(true);
        _endScreen.Init("You Win!", "Congratulations!");
    }
    
    private void ToggleEndScreen(bool b)
    {
        _hudCanvas.gameObject.SetActive(!b);
        _tooltipCanvas.gameObject.SetActive(!b);
        _endScreenCanvas.gameObject.SetActive(b);
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