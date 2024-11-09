using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public abstract class CorruptibleTile : CustomTile
{
    private float _corruption = 0.0f;
    
    [Range(1f, 1000f)]
    [SerializeField] protected float _maxCorruption = 1f;

    public float Corruption
    {
        get => _corruption;
        private set => _corruption = Mathf.Clamp(value, 0f, _maxCorruption);
    }

    public void Corrupt(float corruptionValue)
    {
        Corruption += corruptionValue;
    }
}
