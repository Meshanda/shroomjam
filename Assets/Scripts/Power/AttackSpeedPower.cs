using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedPower : Power
{
    private List<Tower> _towers = new List<Tower>();

    public override void UseAbility()
    {
        if (!_dataSo.IsAvailable) return;
        
        MoneyManager.SpendMoney?.Invoke(_dataSo.Cost, () =>
        {
            _colliders = CheckOverlapCircle(_dataSo.Range, "Tower");
            _towers = GetGenericTypeList<Tower>();
            _powerEffect.AttackSpeedAreaEffect(transform.position);

            foreach (Tower tower in _towers)
            {
                if(tower.IsCorrupted) continue;
                tower.AddAttackSpeed(_dataSo.Value);
            }

            StartCoroutine(BoostCoroutine());
            StartCoroutine(CooldownCoroutine(_dataSo));
        });
    }
    
    private IEnumerator BoostCoroutine()
    {
        yield return new WaitForSeconds(_dataSo.Duration);
        
        foreach (Tower tower in _towers)
        {
            if(tower.IsCorrupted) continue;
            tower.RemoveAttackSpeed();
        }
    }
}
