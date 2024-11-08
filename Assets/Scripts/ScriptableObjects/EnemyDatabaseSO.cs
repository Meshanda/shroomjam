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
        public GameObject prefab;
        public int health;
        public int damage;
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
            if (e.name is null)
            {
                throw new ArgumentNullException("Enemy database has an element with a null enemy name");
            }

            if (e.prefab is null)
            {
                throw new ArgumentNullException("Enemy database has an element with a null enemy prefab");
            }

            if (e.health <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "Enemy database has an element with a health lower or equal to 0");
            }
        });
    }
}
