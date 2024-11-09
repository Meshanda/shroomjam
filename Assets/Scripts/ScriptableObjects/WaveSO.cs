using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 0)]
public class WaveSO : ScriptableObject
{
    [Serializable]
    public struct WaveElement
    {
        [Tooltip("String reference of the enemy to spawn")] public StringVariableSO EnemyReference;
        [Tooltip("Index of the spawner to use")] public int SpawnerIndex;
        [Tooltip("Delay in milliseconds with the previous spawn")] public float Delay;
    }
    
    public List<WaveElement> Elements;
    [Range(0, 100)] public int WaveReward;
    public int IntendedSpawnerNumber;
    
    public void CheckWaveIntegrity()
    {
        Elements.ForEach(e =>
        {
            if(e.EnemyReference is null)
            {
                throw new ArgumentNullException("Wave " + name + " has an element with a null enemy reference");
            }
            if (e.SpawnerIndex >= IntendedSpawnerNumber)
            {
                throw new ArgumentOutOfRangeException("Wave " + name + " has an element with a spawner number higher than the intended number of spawner");
            }
        });

        if (IntendedSpawnerNumber <= 0)
        {
            throw new ArgumentOutOfRangeException("Wave " + name + " has an intended spawner number lower or equal to 0");
        }
    }
}