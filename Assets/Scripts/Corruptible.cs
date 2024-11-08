using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Corruptible : Tile
{
    [SerializeField] private float _corruption = 0.0f;

    public float CorruptionState
    {
        get => _corruption;
        set
        {
            if(value >= 0)
            {
                _corruption = value;
            }
            else
            {
                _corruption = 0;
            }
        }
    }
}
