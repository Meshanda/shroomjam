using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct CorruptionState
{
    public UnityEvent OnCorruptionStateEnter;

    [Range(0.0f, 1.0f)]
    public float CorruptionPercentage;

    private (float min, float max) _corruptionRange;

    public void InvokeAction()
    {
        OnCorruptionStateEnter?.Invoke();
    }
    
    public void SetRange(float maxRange)
    {
        _corruptionRange = (CorruptionPercentage, maxRange);
    }

    public bool IsInRange(float value)
    {
        return value >= _corruptionRange.min && value < _corruptionRange.max;
    }
}