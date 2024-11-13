using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CleanPower : Power
{
    private float _cleanRemainingDuration;
    private float _tick = 0f;
    
    private float _deltaTime = 0f;
    private List<Corruptible> _corruptibles = new List<Corruptible>();
    
    private void Update()
    {
        _deltaTime = Time.deltaTime;
        
        PowerHandle(_deltaTime);
    }
    
    public override void UseAbility()
    {
        if (!_dataSo.IsAvailable) return;

        MoneyManager.SpendMoney?.Invoke(_dataSo.Cost, () =>
        {
            _colliders = CheckOverlapCircle(_dataSo.Range);
            _corruptibles = GetGenericTypeList<Corruptible>();
            SetTowerHealBuff(true);
            _cleanRemainingDuration = _dataSo.Duration;
            _tick = 1f;
            StartCoroutine(CooldownCoroutine(_dataSo));
        });
    }
    
    private void PowerHandle(float deltaTime)
    {
        if (_dataSo.IsAvailable == false && _cleanRemainingDuration > 0)
        {
            if (_tick >= _dataSo.TimeTick)
            {
                foreach (Corruptible corruptible in _corruptibles)
                {
                    corruptible.DeCorrupt(_dataSo.Value);
                }

                _tick = 0;
            }

            _tick += deltaTime;
            _cleanRemainingDuration -= deltaTime;

            // Detect the moment the spell ends
            if (_cleanRemainingDuration <= 0)
            {
                SetTowerHealBuff(false);
            }
        }
    }

    private void SetTowerHealBuff(bool value)
    {
        foreach (Corruptible corruptible in _corruptibles)
        {
            if (corruptible is Tower tower)
            {
                tower.SetHealStatusBuff(value);
            }
        }
    }
}
