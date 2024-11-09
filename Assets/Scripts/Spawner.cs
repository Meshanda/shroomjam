using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Spawn(GameObject prefab)
    {
        Debug.Log("Spawned");
        return Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }
}