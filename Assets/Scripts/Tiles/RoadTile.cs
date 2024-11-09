using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "CustomTile/RoadTile")]
public class RoadTile : CorruptibleTile
{
    public override void OnTile(Entity entity)
    {
        Debug.Log("JE SUIS " + entity.name + " ET JE MARCHE SUR " + name);
    }
    
}
