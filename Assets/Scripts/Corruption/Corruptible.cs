using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Corruptible : MonoBehaviour
{
    public event Action OnCorruptionMaxReached;

    [SerializeField] private List<CorruptionState> _corruptionStates;

    private CorruptionStateHandler _corruptionStateHandler;

    private float _corruption = 0.0f;
    
    [Header("Corruption Stats")]
    [SerializeField] private float _corruptionRate = 1f;
    [Min(1f)] [SerializeField] private float _maxCorruption = 0.1f;

    public float CorruptionRate { get => _corruptionRate; protected set => _corruptionRate = value; }

    public bool LastStateReached => _corruptionStateHandler.LastStateReached;

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

        UpdateCorruptionFeedback(Corruption / MaxCorruption);
    }

    

    public void DeCorrupt(float decorruptionValue)
    {
        Corruption -= decorruptionValue * CorruptionRate;
        
        if(_corruptionStateHandler != null)
            _corruptionStateHandler.CheckStatue(Corruption / MaxCorruption);
        
        UpdateCorruptionFeedback(Corruption / MaxCorruption);
    }
    
    protected virtual void UpdateCorruptionFeedback(float a)
    {
    }
}
