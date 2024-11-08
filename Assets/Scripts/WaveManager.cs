using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private List<WaveSO> _waves;
    
    private WaveSO _currentWave;

    public void Awake()
    {   
        Init();
    }

    private void Start()
    {
        SpawnWave(0);
    }
    
    private void Init()
    {
        _waves.ForEach(wave =>
        {
            wave.CheckWaveIntegrity();
            if (wave.IntendedSpawnerNumber != _spawners.Count)
            {
                throw new ArgumentOutOfRangeException("Wave " + wave.name + " has an intended spawner number different from the number of spawners");
            }
        });
    }
    
    private void SpawnWave(int waveIndex)
    {
        if (waveIndex >= _waves.Count)
        {
            Debug.Log("No more waves");
            return;
        }

        _currentWave = _waves[waveIndex];

        //NextWaveElement(0); c'est débile, JE SUIS DE-BILE
        StartCoroutine(SpawnWaveRoutine(_currentWave, WaveEnded));
    }
    private IEnumerator SpawnWaveRoutine(WaveSO currentWave, Action callback)
    {
        foreach (var element in currentWave.Elements)
        {
            yield return new WaitForSeconds(element.Delay);
            
            SpawnEnemy(element);
        }
        
        callback?.Invoke();
    }
    
    private void WaveEnded()
    {
        Debug.Log("wave ended");
    }

    // private void NextWaveElement(int currentWaveElement)
    // {
    //     SpawnEnemyAfterDelay(_currentWave.Elements[currentWaveElement], () => NextWaveElement(currentWaveElement++));
    // }
    //
    // private void SpawnEnemyAfterDelay(WaveSO.WaveElement element, Action callback)
    // {
    //     StartCoroutine(Utils.WaitRoutine(element.Delay, () => SpawnEnemy(element, callback)));
    // }

    private void SpawnEnemy(WaveSO.WaveElement element, Action callback = null)
    {
        var enemyData = GameManager.Instance.EnemyDatabase.GetEnemyData(element.EnemyReference);
        var spawner = _spawners[element.SpawnerIndex];
        spawner.Spawn(enemyData.prefab.gameObject);
        
        callback?.Invoke();
    }
}