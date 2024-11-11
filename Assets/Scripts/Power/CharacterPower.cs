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
    
    [Header("Power")]
    [SerializeField] private CleanPower _cleanPower;
    [SerializeField] private ShieldPower _shieldPower;
    [SerializeField] private AttackSpeedPower _attackSpeedPower;
    
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
    }

    public void OnClean()
    {
        _cleanPower.UseAbility();
    }

    public void OnShield()
    {
        _shieldPower.UseAbility();
    }

    public void OnBoost()
    {
        _attackSpeedPower.UseAbility();
    }
    
    private void OnPointerHoverSpellButtonHandler(bool enter, Enums.PowerType powerType)
    {
        float value = 1f;
        if (enter)
        {
            value = powerType switch
            {
                Enums.PowerType.Clean => _cleanPower.GetRange(),
                Enums.PowerType.Shield => _shieldPower.GetRange(),
                Enums.PowerType.Boost => _attackSpeedPower.GetRange(),
            };
        }
        
        DisplayAreaOfEffect(enter, value);
    }

    private void OnClickSpellButtonHandler(Enums.PowerType powerType)
    {
        switch(powerType)
        {
            case Enums.PowerType.Clean:
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
