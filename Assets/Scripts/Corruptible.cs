using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Corruptible : MonoBehaviour
{
    private float _corruption = 0.0f;
    [Range(0.1f, 1000f)]
    [SerializeField] private float _maxCorruption = 0.1f;

    public float CorruptionRate { get; protected set; } = 1f;

    public float Corruption
    {
        get => _corruption;
        private set => _corruption = Mathf.Clamp(value, 0f, _maxCorruption);
    }
    
    public float MaxCorruption { get; private set; }

    public void Corrupt(float corruptionValue)
    {
        Corruption += corruptionValue * CorruptionRate;
    }

}
