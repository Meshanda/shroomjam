using UnityEngine;

public class Corruptible : MonoBehaviour
{
    [SerializeField] private float _corruptionState = 0.0f;

    public float CorruptionState
    {
        get => _corruptionState;
        set
        {
            _corruptionState = value;
        }
    }
}
