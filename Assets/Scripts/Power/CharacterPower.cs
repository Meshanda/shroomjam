using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class CharacterPower : MonoBehaviour
{
    public static Action<bool, Enums.PowerType> OnPointerHoverSpellButton;
    public static Action<Enums.PowerType> OnClickSpellButton;
    
    [SerializeField] protected PowerEffect _powerEffect;
    
    [Header("Visual Feedback")]
    [SerializeField] private GameObject _powerVisualizerHover;
    [SerializeField] private float _showDelay = .1f;
    [SerializeField] private float _fadeValue = .6f;
    [SerializeField] private float _fadeDelay = .1f;
    
    [Header("Power")]
    [SerializeField] private CleanPower _cleanPower;
    [SerializeField] private ShieldPower _shieldPower;
    [SerializeField] private AttackSpeedPower _attackSpeedPower;
    
    private Coroutine _coroutine;
    private Tween _tween;
    private SpriteRenderer _visualizerSpriteRenderer;
    
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
        _visualizerSpriteRenderer = _powerVisualizerHover.GetComponent<SpriteRenderer>();
        DisplayAreaOfEffect(false);

        InitPowers();
    }

    private void InitPowers()
    {
        _cleanPower.SetPowerEffect(_powerEffect);
        _shieldPower.SetPowerEffect(_powerEffect);
        _attackSpeedPower.SetPowerEffect(_powerEffect);
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
        if (visible)
            ShowAoe(value);
        else
            HideAoe();
    }
    
    private void ShowAoe(float value)
    {
        _coroutine = StartCoroutine(Utils.WaitRoutine(_showDelay, () =>
        {
            _powerVisualizerHover.SetActive(true);
            _powerVisualizerHover.transform.localScale = new Vector3(value, value, 1);
            _tween = _visualizerSpriteRenderer.DOFade(_fadeValue, _fadeDelay);
        }));
    }

    private void HideAoe()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _tween?.Kill();
        _powerVisualizerHover.SetActive(false);
        
        var color = _visualizerSpriteRenderer.color;
        color.a = 0;
        _visualizerSpriteRenderer.color = color;
    }

    
}
