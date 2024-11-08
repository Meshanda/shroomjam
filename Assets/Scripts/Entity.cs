using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected Tilemap _tilemap;
    protected virtual void Update()
    {
        var position = _tilemap.WorldToCell(transform.position);
        var tile = _tilemap.GetTile(position);
        if (tile is CustomTile customTile)
        {
            customTile.OnTile(this);
        }
    }
}