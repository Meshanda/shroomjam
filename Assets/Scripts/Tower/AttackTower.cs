using System;
using System.Collections;
using UnityEngine;

public class AttackTower : Tower
{
    private bool _isAttacking;
    private Transform _currentTarget;
    private Corruptible _corruptibleTarget;
    protected bool _isCorrupted;

    
    
    protected override void Awake()
    {
        base.Awake();
        
        _isAttacking = false;
        _isCorrupted = false;
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
        bullet.Init(_currentTarget.transform, Damage, CorruptionDamage, _core);
    }

    private void ResetAggro()
    {
        _currentTarget = null;
        _isAttacking = false;
        
        if (_corruptibleTarget == null) return;
        _corruptibleTarget.OnCorruptionMaxReached -= ResetAggro;
        _corruptibleTarget = null;
    }
    
    private void SetTarget(Transform target)
    {
        _currentTarget = target;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log($"{other.name} is on");
        if (_currentTarget != null) return; // If we already have a target
        
        if (!_isCorrupted && other.CompareTag("Enemy")) // If we are not corrupted and the other is an enemy
            SetTarget(other.transform);

        if (_isCorrupted && other.gameObject.layer.Equals(LayerMask.NameToLayer("Corruptible"))) // If we are corrupted and the other is a corruptible
        {
            _corruptibleTarget = other.GetComponentInParent<Corruptible>();
            if (_corruptibleTarget.Corruption >= _corruptibleTarget.MaxCorruption) return; // If the corruptible is already corrupted

            _corruptibleTarget.OnCorruptionMaxReached += ResetAggro;
            SetTarget(other.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_currentTarget != other.transform) return; // if the other is not the current target

        ResetAggro();
    }

    public void OnCorrupted()
    {
        _isCorrupted = true;
    }
    
    public void OnDecorrupted()
    {
        _isCorrupted = false;
    }
}
