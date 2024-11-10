using UnityEngine;

[CreateAssetMenu(menuName = "Power/Data")]
public class PowerDataSO : ScriptableObject
{
    public Enums.PowerType PowerType;
    public float Range;
    public float Value;
    public float Duration;
    [Tooltip("In seconds")] 
    public float Cooldown;

    [Space(10)]
    private float _remainingTime;
    private bool _isAvailable;

    public float RemainingTime { get => _remainingTime; set => _remainingTime = value; }
    public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }

    public void InitData()
    {
        _isAvailable = true;
        _remainingTime = 0f;
    }
}
