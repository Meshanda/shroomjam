using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Tilemap _tilemap;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _player.transform.position += Vector3.right * (3 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _player.transform.position += Vector3.left * (3 * Time.deltaTime);
        } 
        else if (Input.GetKey(KeyCode.W))
        {
            _player.transform.position += Vector3.up * (3 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _player.transform.position += Vector3.down * (3 * Time.deltaTime);
        }
        
        
        Vector3Int playerPosition = _tilemap.WorldToCell(_player.transform.position);
        TileBase tile = _tilemap.GetTile(playerPosition);
        
    }
}
