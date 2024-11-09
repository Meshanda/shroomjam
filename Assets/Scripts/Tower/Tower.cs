using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Tower : Corruptible
{
    [SerializeField] protected GameObject _bulletPfb;
    
    public Enums.TowerType Type { get; protected set; }

    [Header("Tower Stats")]
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _cost;

    public float Damage { get => _damage; protected set => _damage = value; }
    public float Range { get => _range; protected set => _range = value; }
    public float AttackSpeed { get => _attackSpeed; protected set => _attackSpeed = value; }
    public int Cost { get => _cost; protected set => _cost = value; }

    private CircleCollider2D _attackCollider;
    private Enemy _currentTarget;
    private bool _isAttacking;

    private void Awake()
    {
        _attackCollider = GetComponent<CircleCollider2D>();
        _attackCollider.isTrigger = true;
        _attackCollider.radius = Range;
        
        _isAttacking = false;
    }

    private void Update()
    {
        if (_currentTarget == null)
        {
            ResetAggro();
            return;
        }

        if (!_isAttacking)
        {
            _isAttacking = true;
            StartCoroutine(AttackTarget());
        }
    }

    private IEnumerator AttackTarget()
    {
        while (_isAttacking)
        {
            yield return new WaitForSeconds(1f / AttackSpeed);
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_currentTarget == null)
        {
            ResetAggro();
            return;
        }
        
        var bullet = Instantiate(_bulletPfb, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Init(_currentTarget.transform, Damage);
    }

    private void ResetAggro()
    {
        _currentTarget = null;
        _isAttacking = false;
    }
    
    private void SetTarget(Enemy target)
    {
        _currentTarget = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (_currentTarget != null) return;
        
        SetTarget(other.GetComponentInParent<Enemy>());
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        if (_currentTarget != other.GetComponentInParent<Enemy>()) return;

        ResetAggro();
    }
}