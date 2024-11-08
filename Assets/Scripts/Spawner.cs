using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject prefab)
    {
        Instantiate(prefab, transform.position, Quaternion.identity, transform);
        Debug.Log("Spawned");
    }
}