using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CustomTile: Tile
{
    public abstract void OnTile(Entity entity);
}