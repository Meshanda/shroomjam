using BezierSolution;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private BezierSpline _spline;
    
    public void Spawn(GameObject prefab, Tilemap tilemap)
    {
        Debug.Log("Spawned");
        var enemySpawned = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        enemySpawned.GetComponent<Enemy>().Setup(_spline, tilemap);
    }
}