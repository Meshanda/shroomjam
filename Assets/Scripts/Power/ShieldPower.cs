using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : Power
{
    private List<Tower> _towers = new List<Tower>();
    
    public override void UseAbility()
    {
        if (!_dataSo.IsAvailable) return;
        
        _colliders = CheckOverlapCircle(_dataSo.Range, "Tower");
        _towers = GetGenericTypeList<Tower>();
        
        foreach (Tower tower in _towers)
        {
            tower.AddShieldToTower(_dataSo.Value);
        }

        StartCoroutine(ShieldCoroutine());
        StartCoroutine(CooldownCoroutine(_dataSo));
    }
    
    private IEnumerator ShieldCoroutine()
    {
        yield return new WaitForSeconds(_dataSo.Duration);
        
        foreach (Tower tower in _towers)
        {
            tower.RemoveShieldFromTower();
        }
    }
}
