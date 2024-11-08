using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "CustomTile/FountainTile")]
public class FountainTile : CustomTile
{
    public override void OnTile(Entity entity)
    {
        Debug.Log("JE SUIS " + entity.name + " ET JE SUIS SUR " + name);
    }
}