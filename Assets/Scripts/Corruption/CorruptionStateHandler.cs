using System.Collections.Generic;
using UnityEngine;

public class CorruptionStateHandler
{
    [SerializeField] private CorruptionStateList _currentState;

    public CorruptionStateHandler(List<CorruptionState> states)
    {
        if (states.Count <= 0)
            return;
        
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

        return SearchInNextState(corruptionPercentage, currentState.NextState);
    }
}