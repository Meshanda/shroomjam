using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private Tilemap _specialTilemap;
    
    [SerializeField] private BasicTowerTile _tourBasicTile;
    [SerializeField] private FastTowerTile _tourFastTile;
    [SerializeField] private SniperTowerTile _tourSniperTile;
    [SerializeField] private SupportTowerTile _tourSupportTile;
    
    public Tilemap SpecialTilemap => _specialTilemap;
    
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
    

    private void Init()
    {
        if (_specialTilemap == null) throw new ArgumentNullException("Special Tilemap is not assigned");
    }
}