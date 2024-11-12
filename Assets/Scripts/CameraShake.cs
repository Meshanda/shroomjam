using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : CinemachineImpulseSource
{
    public static Action<float> OnEnemyHitBase;

    [SerializeField] private float _shakeForce = 0.2f;

    private void OnEnable()
    {
        OnEnemyHitBase += OnEnemyHitBaseHandler;
    }
    
    private void OnDisable()
    {
        OnEnemyHitBase -= OnEnemyHitBaseHandler;
    }

    private void OnEnemyHitBaseHandler(float corruptionPercentage)
    {
        GenerateImpulseWithForce(_shakeForce);
    }
}
