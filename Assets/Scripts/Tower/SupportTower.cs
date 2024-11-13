using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTower : Tower
{
    private List<Corruptible> _targetList = new ();
    private bool _isHealing;
    
    protected override void Awake()
    {
        base.Awake();
        
        _isHealing = false;
    }
    
    private void Update()
    {
        if (_targetList == null)
        {
            ResetAggro();
            return;
        }

        if (!_isHealing)
        {
            _isHealing = true;
            StartCoroutine(HealTargetsRoutine());
        }
    }

    private IEnumerator HealTargetsRoutine()
    {
        while (_isHealing)
        {
            yield return new WaitForSeconds(1f / AttackSpeed);
            
            if (_isCorrupted)
                Corrupt();
            else
                Heal();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Tower")) return; // If the other object is not a tower
        
        var corruptible = other.GetComponentInParent<Corruptible>();
        if (corruptible == null) return; // If the other object is not corruptible
        
        if (_targetList.Contains(corruptible)) return; // If the other object is in the target list

        _targetList.Add(corruptible);
    }

    private void Heal()
    {
        _targetList.ForEach(corruptible =>
        {
            corruptible.DeCorrupt(CorruptionDamage);
        });
    }

    private void Corrupt()
    {
        _targetList.ForEach(corruptible =>
        {
            corruptible.Corrupt(CorruptionDamage);
        });
    }
    
    private void ResetAggro()
    {
        _targetList = null;
        _isHealing = false;
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