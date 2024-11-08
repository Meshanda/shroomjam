using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyDatabase", fileName = "EnemyDatabase")]
public class EnemyDatabaseSO : ScriptableObject
{
    [Serializable]
    public struct EnemyData
    {
        public StringVariableSO name;
        public Enemy prefab;
    }

    public List<EnemyData> Enemies;

    public EnemyData GetEnemyData(StringVariableSO enemyName)
    {
        foreach (var e in Enemies)
        {
            if (e.name == enemyName) return e;
        }

        throw new ArgumentOutOfRangeException("Enemy " + enemyName + " not found in the database");
    }

    public void CheckDatabaseIntegrity()
    {
        Enemies.ForEach(e =>
        {
            e.prefab.CheckValues();
            if (e.name is null)
            {
                throw new ArgumentNullException("Enemy database has an element with a null enemy name");
            }
        });
    }
}
