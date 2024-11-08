using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public abstract class CorruptibleTile : CustomTile
{
    [SerializeField] private float _corruption = 0.0f;

    public float Corruption
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
