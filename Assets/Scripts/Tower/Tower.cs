using System;
using System.Collections;
using UnityEngine;

public abstract class Tower : Corruptible
{
    public Enums.TowerType Type { get; protected set; }

    [Header("Corruption Feedback")]
    [SerializeField] private SpriteRenderer _corruptionRenderer;

    [Header("Tower Stats")]
    [SerializeField] private float _damage;
    [SerializeField] private float _corruptionDamage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _range;
    [SerializeField] private int _cost;
    
    [Header("References")]
    [SerializeField] private TowerBuff _towerBuff;
    [SerializeField] protected GameObject _bulletPfb;
    [SerializeField] protected GameObject _core;
    
    protected bool _isCorrupted;

    public float Damage { get => _damage; protected set => _damage = value; }
    public float CorruptionDamage { get => _corruptionDamage; protected set => _corruptionDamage = value; }
    public float Range { get => _range; protected set => _range = value; }
    public float AttackSpeed { get => _attackSpeed; protected set => _attackSpeed = value; }
    public int Cost { get => _cost; protected set => _cost = value; }

    protected CircleCollider2D _attackCollider;

    private float _defaultCorruptionRate = 0f;
    private float _defaultAttackSpeed = 0f;
    
    public bool IsCorrupted => _isCorrupted;

    protected virtual void Awake()
    {
        _attackCollider = GetComponent<CircleCollider2D>();
        _attackCollider.isTrigger = true;
        _attackCollider.radius = Range;
        
        _defaultCorruptionRate = CorruptionRate;
        _defaultAttackSpeed = AttackSpeed;
        
        UpdateCorruptionFeedback(0);
    }

    public void SetHealStatusBuff(bool value)
    {
        _towerBuff.Heal(value);
    }

    public void AddShieldToTower(float shieldRate)
    {
        _towerBuff.Shield(true);
        CorruptionRate -= CorruptionRate * shieldRate;
    }

    public void RemoveShieldFromTower()
    {
        _towerBuff.Shield(false);
        CorruptionRate = _defaultCorruptionRate;
    }
    
    public void AddAttackSpeed(float attackSpeedRate)
    {
        _towerBuff.AttackSpeed(true);
        AttackSpeed += AttackSpeed * attackSpeedRate;
    }

    public void RemoveAttackSpeed()
    {
        _towerBuff.AttackSpeed(false);
        AttackSpeed = _defaultAttackSpeed;
    }

    protected override void UpdateCorruptionFeedback(float a)
    {
        var color = _corruptionRenderer.color;
        color.a = a;
        _corruptionRenderer.color = color;
    }

}