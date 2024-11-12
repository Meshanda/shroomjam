using System;
using System.Collections;
using UnityEngine;

public abstract class Tower : Corruptible
{
    [SerializeField] protected GameObject _bulletPfb;
    
    [SerializeField] protected GameObject _core;
    
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

    private float _defaultCorruptionRate = 0f;
    private float _defaultAttackSpeed = 0f;

    protected virtual void Awake()
    {
        _attackCollider = GetComponent<CircleCollider2D>();
        _attackCollider.isTrigger = true;
        _attackCollider.radius = Range;
        
        _defaultCorruptionRate = CorruptionRate;
        _defaultAttackSpeed = AttackSpeed;
    }

    public void AddShieldToTower(float shieldRate)
    {
        CorruptionRate -= CorruptionRate * shieldRate;
    }

    public void RemoveShieldFromTower()
    {
        CorruptionRate = _defaultCorruptionRate;
    }
    
    public void AddAttackSpeed(float attackSpeedRate)
    {
        AttackSpeed += AttackSpeed * attackSpeedRate;
    }

    public void RemoveAttackSpeed()
    {
        AttackSpeed = _defaultAttackSpeed;
    }


}