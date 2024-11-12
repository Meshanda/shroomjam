using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Corruptible : MonoBehaviour
{
    public Action<float> OnCorruptionValueChanged;
    public event Action OnCorruptionMaxReached;

    [SerializeField] private List<CorruptionState> _corruptionStates;

    private CorruptionStateHandler _corruptionStateHandler;

    private float _corruption = 0.0f;
    [Min(1f)]
    [SerializeField] private float _maxCorruption = 0.1f;
    
    public float CorruptionRate { get; protected set; } = 1f;

    private void Start()
    {
        _corruptionStateHandler = new CorruptionStateHandler(_corruptionStates);
    }

    public float Corruption
    {
        get => _corruption;
        protected set => _corruption = Mathf.Clamp(value, 0f, _maxCorruption);
    }
    
    public float MaxCorruption { get => _maxCorruption; private set => _maxCorruption = value; }
    
    public void Corrupt(float corruptionValue)
    {
        Corruption += corruptionValue * CorruptionRate;

        if (Corruption >= MaxCorruption) // if corruption is maxed out
            OnCorruptionMaxReached?.Invoke();
        
        if(_corruptionStateHandler != null)
            _corruptionStateHandler.CheckStatue(Corruption / MaxCorruption);
        
        OnCorruptionValueChanged?.Invoke(Corruption / MaxCorruption);
    }
    
    public void DeCorrupt(float decorruptionValue)
    {
        Corruption -= decorruptionValue * CorruptionRate;
        
        if(_corruptionStateHandler != null)
            _corruptionStateHandler.CheckStatue(Corruption / MaxCorruption);
        
        OnCorruptionValueChanged?.Invoke(Corruption / MaxCorruption);
    }
}