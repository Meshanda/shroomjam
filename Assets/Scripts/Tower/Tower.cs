using System;
using System.Collections;
using UnityEngine;

public abstract class Tower : Corruptible
{
    [SerializeField] protected GameObject _bulletPfb;
    
    public Enums.TowerType Type { get; protected set; }

    [Header("Tower Stats")]
    [SerializeField] private float _damage;
    [SerializeField] private float _corruptionDamage;
    [SerializeField] private float _range;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _cost;

    public float Damage { get => _damage; protected set => _damage = value; }
    public float CorruptionDamage { get => _corruptionDamage; protected set => _corruptionDamage = value; }
    public float Range { get => _range; protected set => _range = value; }
    public float AttackSpeed { get => _attackSpeed; protected set => _attackSpeed = value; }
    public int Cost { get => _cost; protected set => _cost = value; }

    protected CircleCollider2D _attackCollider;

    protected virtual void Awake()
    {
        _attackCollider = GetComponent<CircleCollider2D>();
        _attackCollider.isTrigger = true;
        _attackCollider.radius = Range;
    }

    public void BoostAttackSpeed(float boostRate, float duration)
    {
        // Change attack speed for the duration
        float oldAttackSpeed = AttackSpeed;
        AttackSpeed += AttackSpeed * boostRate;
        StartCoroutine(ChangeAttackSpeedCoroutine(duration, oldAttackSpeed));
    }

    private IEnumerator ChangeAttackSpeedCoroutine(float waitDuration, float oldAttackSpeed)
    {
        yield return new WaitForSeconds(waitDuration);
        AttackSpeed = oldAttackSpeed;
    }

    public void ShieldTower(float shieldRate, float duration)
    {
        float oldDefensiveRate = CorruptionRate;
        CorruptionRate -= CorruptionRate * shieldRate;
        StartCoroutine(ChangeDefensiveRateCoroutine(duration, oldDefensiveRate));
    }
    
    private IEnumerator ChangeDefensiveRateCoroutine(float waitDuration, float oldAttackSpeed)
    {
        yield return new WaitForSeconds(waitDuration);
        CorruptionRate = oldAttackSpeed;
    }
}