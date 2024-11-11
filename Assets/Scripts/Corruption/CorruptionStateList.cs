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