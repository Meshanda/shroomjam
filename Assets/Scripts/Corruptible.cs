using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Corruptible : MonoBehaviour
{
    public Action<float> OnCorruptionValueChanged;

    private float _corruption = 0.0f;
    [Range(0.1f, 1000f)]
    [SerializeField] private float _maxCorruption = 0.1f;

    public float CorruptionRate { get; protected set; } = 1f;

    public float Corruption
    {
        get => _corruption;
        private set => _corruption = Mathf.Clamp(value, 0f, _maxCorruption);
    }
    
    public float MaxCorruption { get => _maxCorruption; private set => _maxCorruption = value; }
    
    public void Corrupt(float corruptionValue)
    {
        Corruption += corruptionValue * CorruptionRate;
        OnCorruptionValueChanged?.Invoke(Corruption / MaxCorruption);
    }

    public void DeCorrupt(float decorruptionValue)
    {
        Corruption -= decorruptionValue * CorruptionRate;
        OnCorruptionValueChanged?.Invoke(Corruption / MaxCorruption);
    }
}
