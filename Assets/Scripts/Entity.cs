using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected Tilemap _tilemap;
    private CustomTile _currentTile;

    public CustomTile CurrentTile => _currentTile;

    public Tilemap Tilemap
    {
        get => _tilemap;
        protected set => _tilemap = value;
    }

    protected virtual void Update()
    {
        var position = _tilemap.WorldToCell(transform.position);
        var tile = _tilemap.GetTile(position);
        if (tile is CustomTile customTile)
        {
            _currentTile = customTile;
            customTile.OnTile(this);
        }
    }
}