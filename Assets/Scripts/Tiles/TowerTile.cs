using UnityEngine;
using UnityEngine.Tilemaps;


public class TowerTile : CustomTile
{
    public override void OnTile(Entity entity)
    {
        Debug.Log("JE SUIS " + entity.name + " ET JE SUIS SUR " + name);
    }
}
