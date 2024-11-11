using BezierSolution;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private BezierSpline _spline;
    
    public Enemy Spawn(GameObject prefab, Tilemap tilemap)
    {
        var enemySpawned = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        var enemy = enemySpawned.GetComponent<Enemy>();
        enemy.Setup(_spline, tilemap);

        return enemy;
    }
}