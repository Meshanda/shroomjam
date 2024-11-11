using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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

public class CorruptionStateList
{
    public CorruptionState State;
    
    public CorruptionStateList NextState;
    public CorruptionStateList PreviousState;

    public bool IsStateValid(float value)
    {
        return State.IsInRange(value);
    }
}

public class CorruptionStateHandler
{
    [SerializeField] private CorruptionStateList _currentState;

    public CorruptionStateHandler(List<CorruptionState> states)
    {
        // if no state start at 0 put a default state
        if (states[0].CorruptionPercentage != 0.0f)
        {
            _currentState = new CorruptionStateList
            {
                State = new CorruptionState()
                {
                    CorruptionPercentage = 0.0f,
                }
            };
            
            _currentState.State.SetRange(states[0].CorruptionPercentage);
            _currentState.NextState = ConstructNextHandler(states,  null);
            return;
        }
        
        _currentState = ConstructNextHandler(states,  null);
        
    }

    private CorruptionStateList ConstructNextHandler(List<CorruptionState> states, CorruptionStateList currentState)
    {
        if(states.Count > 0)
        {
            var newStateList = new CorruptionStateList
            {
                State = states[0],
                PreviousState = currentState
            };
            
            if(states.Count > 1)
            {
                newStateList.State.SetRange(states[1].CorruptionPercentage);
            }
            else
            {
                newStateList.State.SetRange(1.1f);
            }
            
            newStateList.NextState = ConstructNextHandler(states.GetRange(1, states.Count - 1),  newStateList);
            
            return newStateList;
        }

        return null;
    }

    public void CheckStatue(float corruptionPercentage)
    {
        if (_currentState != null && !_currentState.State.IsInRange(corruptionPercentage))
        {
            _currentState = SearchCurrentState(corruptionPercentage);
            _currentState.State.InvokeAction();
        }
    }
    

    private CorruptionStateList SearchCurrentState(float corruptionPercentage)
    {
        if (_currentState.State.CorruptionPercentage > corruptionPercentage)
        {
            return SearchInPreviousState(corruptionPercentage, _currentState);
        }

        return SearchInNextState(corruptionPercentage, _currentState);
    }

    private CorruptionStateList SearchInPreviousState(float corruptionPercentage, CorruptionStateList currentState)
    {
        if (currentState.PreviousState != null && currentState.PreviousState.IsStateValid(corruptionPercentage))
        {
            return currentState.PreviousState;
        }
        
        if (currentState.PreviousState == null)
        {
            return currentState;
        }

        return SearchInPreviousState(corruptionPercentage, currentState.PreviousState);
    }
    
    private CorruptionStateList SearchInNextState(float corruptionPercentage, CorruptionStateList currentState)
    {
        if (currentState.NextState != null && currentState.NextState.IsStateValid(corruptionPercentage))
        {
            return currentState.NextState;
        }

        if (currentState.NextState == null)
        {
            return currentState;
        }

        return SearchInPreviousState(corruptionPercentage, currentState.NextState);
    }
}



[RequireComponent(typeof(Collider2D))]
public abstract class Corruptible : MonoBehaviour
{
    public Action<float> OnCorruptionValueChanged;

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
