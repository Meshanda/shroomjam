using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BezierSolution;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[Serializable]
public struct WaveInfo : IEquatable<WaveInfo>
{
    public WaveSO WaveData;
    public float WaveDelay;

    public bool Equals(WaveInfo other)
    {
        return Equals(WaveData, other.WaveData) && WaveDelay.Equals(other.WaveDelay);
    }

    public override bool Equals(object obj)
    {
        return obj is WaveInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(WaveData, WaveDelay);
    }
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private List<WaveInfo> _waves;
    
    private WaveInfo _currentWave;
    
    private List<Enemy> _currentEnemies = new List<Enemy>();

    private bool _currentWaveFinishedSpawning = false;
    
    [SerializeField] private bool _manuallySpawnWaves = false;

    public void Awake()
    {   
        Init();
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += StopWaves;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= StopWaves;
    }

    private void Start()
    {
        if (!_manuallySpawnWaves)
            SpawnNextWave();
    }
    
    private void Init()
    {
        _waves.ForEach(wave =>
        {
            wave.WaveData.CheckWaveIntegrity();
            if (wave.WaveData.IntendedSpawnerNumber != _spawners.Count)
            {
                throw new ArgumentOutOfRangeException("Wave " + wave.WaveData.name + " has an intended spawner number different from the number of spawners");
            }
        });
    }
    
    
    private void StopWaves(Enums.GameOverType gameOverType)
    {
        _waves.Clear();
    }
    
    private void SpawnNextWave()
    {
        if (_waves.Count <= 0)
        {
            Debug.Log("No more waves");
            GameManager.OnGameWin?.Invoke();
            return;
        }

        _currentWave = _waves[0];
        _waves.Remove(_currentWave);
        
        _currentWaveFinishedSpawning = false;

        StartCoroutine(SpawnWaveRoutine(_currentWave, WaveEndedSpawning));
    }
    
    private IEnumerator SpawnWaveRoutine(WaveInfo currentWave, Action callback)
    {
        if(!_manuallySpawnWaves)
            yield return new WaitForSeconds(currentWave.WaveDelay);
        
        foreach (var element in currentWave.WaveData.Elements)
        {
            yield return new WaitForSeconds(element.Delay);
            
            SpawnEnemy(element);
        }

        _currentWaveFinishedSpawning = true;
        callback?.Invoke();
    }
    
    private void WaveEndedSpawning()
    {
        if (!_manuallySpawnWaves && _currentEnemies.Count == 0)
        {
            SpawnNextWave();
        }
    }

    private void SpawnEnemy(WaveSO.WaveElement element)
    {
        var enemyData = GameManager.Instance.EnemyDatabase.GetEnemyData(element.EnemyReference);
        var spawner = _spawners[element.SpawnerIndex];
        var enemySpawned = spawner.Spawn(enemyData.prefab.gameObject, TileManager.Instance.SpecialTilemap);

        _currentEnemies.Add(enemySpawned);

        enemySpawned.OnDeath += EnemyDied;
    }

    private void EnemyDied(Enemy deadEnemy)
    {
        _currentEnemies.Remove(deadEnemy);

        if (_currentEnemies.Count == 0 && _currentWaveFinishedSpawning && !_manuallySpawnWaves)
        {
            SpawnNextWave();
        }
    }
}