using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private Tilemap _specialTilemap;
    [SerializeField] private Tilemap _corruptedTilemap;
    
    [SerializeField] private BasicTowerTile _tourBasicTile;
    [SerializeField] private FastTowerTile _tourFastTile;
    [SerializeField] private SniperTowerTile _tourSniperTile;
    [SerializeField] private SupportTowerTile _tourSupportTile;
    
    public Tilemap SpecialTilemap => _specialTilemap;
    public Tilemap CorruptedTilemap => _corruptedTilemap;
    
    protected override void SingletonAwake()
    {
        Init();
    }
    
    public void SetTile(Vector3Int position, TileBase tile)
    {
        _specialTilemap.SetTile(position, tile);
    }

    public void SetTileTourBasic(Vector3Int position)
    {
        _specialTilemap.SetTile(position, _tourBasicTile);
    }
    
    public void SetTileTourFast(Vector3Int position)
    {
        _specialTilemap.SetTile(position, _tourFastTile);
    }
    
    public void SetTileTourSniper(Vector3Int position)
    {
        _specialTilemap.SetTile(position, _tourSniperTile);
    }
    
    public void SetTileTourSupport(Vector3Int position)
    {
        _specialTilemap.SetTile(position, _tourSupportTile);
    }

    public void EnemyDead(Vector3 position, float damage)
    {
        Vector3Int tilePosition = _specialTilemap.WorldToCell(position);
        GameObject go = _specialTilemap.GetInstantiatedObject(tilePosition);
        
        if(!go.GetComponent<Road>())
        {
            Debug.LogError($"{go.name} is not a road");
            return;
        }
        
        Road road = go.GetComponent<Road>();
        road.ChangeCorruption(damage);
    }

    private void Init()
    {
        if (_specialTilemap == null) throw new ArgumentNullException("Special Tilemap is not assigned");
    }
}