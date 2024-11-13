using UnityEngine;

[CreateAssetMenu(menuName = "Power/Data")]
public class PowerDataSO : ScriptableObject
{
    public Enums.PowerType PowerType;
    public float TimeTick = 1f;
    
    [Header("Stats")]
    public float Duration;
    public float Cooldown;
    public float Range;
    public float Value;
    public int Cost;

    private float _remainingTimeCooldown;
    private bool _isAvailable;

    public float RemainingTimeCooldown { get => _remainingTimeCooldown; set => _remainingTimeCooldown = value; }
    public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }

    public void InitData()
    {
        _isAvailable = true;
        _remainingTimeCooldown = 0f;
    }
}
