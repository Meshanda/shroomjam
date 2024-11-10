using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class CharacterPower : MonoBehaviour
{
    public static Action<bool, Enums.PowerType> OnPointerHoverSpellButton;
    public static Action<Enums.PowerType> OnClickSpellButton;
    
    [SerializeField] private GameObject _powerVisualizer;

    [FormerlySerializedAs("cleanDataSo")]
    [Header("Data")]
    [SerializeField] private PowerDataSO _cleanDataSo;
    [SerializeField] private PowerDataSO _shieldDataSo;
    [SerializeField] private PowerDataSO _boostDataSo;

    private void OnEnable()
    {
        OnPointerHoverSpellButton += OnPointerHoverSpellButtonHandler;
        OnClickSpellButton += OnClickSpellButtonHandler;
    }

    private void OnDisable()
    {
        OnPointerHoverSpellButton -= OnPointerHoverSpellButtonHandler;
        OnClickSpellButton -= OnClickSpellButtonHandler;
    }

    private void Awake()
    {
        DisplayAreaOfEffect(false);

        InitPowerSO();
    }

    public void OnClean()
    {
        OnAbility(_cleanDataSo, CleanCallback);
    }

    public void OnShield()
    {
        OnAbility(_shieldDataSo, ShieldCallback);
    }

    public void OnBoost()
    {
        OnAbility(_boostDataSo, BoostCallback);
    }

    private void InitPowerSO()
    {
        _cleanDataSo.InitData();
        _shieldDataSo.InitData();
        _boostDataSo.InitData();
    }

    private void OnAbility(PowerDataSO ability, Action<Collider2D, PowerDataSO> callback)
    {
        if (ability.IsAvailable)
        {
            LaunchAbility(ability, callback);
            StartCoroutine(CooldownCoroutine(ability));
        }
        else
        {
            // Can do something here when an ability is not available ...
            Debug.Log("Ability " + ability.PowerType + " is not available !");
        }
    }

    private IEnumerator CooldownCoroutine(PowerDataSO ability)
    {
        ability.IsAvailable = false;
        // Debug.Log("Ability " + ability.PowerType + " used !");
        
        float elapsedTime = 0f;

        while (elapsedTime < ability.Cooldown)
        {
            elapsedTime += Time.deltaTime;
            
            ability.RemainingTime = ability.Cooldown - elapsedTime;
            
            // Debug.Log("Ability " + ability.PowerType + " : " + ability.RemainingTime.ToString("F1") + "s");
            
            yield return null;
        }
        
        ability.IsAvailable = true;
        Debug.Log("Ability " + ability.PowerType + " ready !");
    }

    private void CleanCallback(Collider2D collider2d, PowerDataSO dataSo)
    {
        Corruptible corruptible = collider2d.GetComponentInParent<Corruptible>();
        corruptible.DeCorrupt(dataSo.Value);
    }

    private void ShieldCallback(Collider2D collider2d, PowerDataSO dataSo)
    {
        if(!collider2d.CompareTag("Tower")) return;
        
        Tower tower = collider2d.GetComponentInParent<Tower>();
        tower.ShieldTower(dataSo.Value, dataSo.Duration);
    }

    private void BoostCallback(Collider2D collider2d, PowerDataSO dataSo)
    {
        if(!collider2d.CompareTag("Tower")) return;

        Tower tower = collider2d.GetComponentInParent<Tower>();
        tower.BoostAttackSpeed(dataSo.Value, dataSo.Duration);
    }
    
    private void LaunchAbility(PowerDataSO powerDataSo, Action<Collider2D, PowerDataSO> callback)
    {
        Collider2D[] overlapResults = Physics2D.OverlapCircleAll(transform.position, powerDataSo.Range / 2);
        if (overlapResults.Length > 0)
        {
            // Some results
            foreach (Collider2D collider2d in overlapResults)
            {
                if (collider2d.gameObject.layer == LayerMask.NameToLayer("Corruptible"))
                {
                    callback?.Invoke(collider2d, powerDataSo);
                }
            }
        }
    }

    private void OnPointerHoverSpellButtonHandler(bool enter, Enums.PowerType powerType)
    {
        float value = 1f;
        if (enter)
        {
            value = powerType switch
            {
                Enums.PowerType.Heal => _cleanDataSo.Range,
                Enums.PowerType.Shield => _shieldDataSo.Range,
                Enums.PowerType.Boost => _boostDataSo.Range,
            };
        }
        
        DisplayAreaOfEffect(enter, value);
    }

    private void OnClickSpellButtonHandler(Enums.PowerType powerType)
    {
        switch(powerType)
        {
            case Enums.PowerType.Heal:
                OnClean();
                break;
            case Enums.PowerType.Shield:
                OnShield();
                break;
            case Enums.PowerType.Boost:
                OnBoost();
                break;
        }
    }

    private void DisplayAreaOfEffect(bool visible, float value = 1f)
    {
        _powerVisualizer.SetActive(visible);
        if(visible)
            _powerVisualizer.transform.localScale = new Vector3(value, value, 1);
    }
}
