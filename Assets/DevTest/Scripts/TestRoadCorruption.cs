using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestRoadCorruption : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private float _corruptValue;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int position = _tilemap.WorldToCell(mousePosition);
            
            GameObject go = _tilemap.GetInstantiatedObject(position);

            if (!go) return;

            if(!go.TryGetComponent<Corruptible>(out Corruptible roadTileData)) return;
            
            roadTileData.Corrupt(_corruptValue);
        }
    }
}
