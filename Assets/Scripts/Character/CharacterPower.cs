using System;
using UnityEngine;

public class CharacterPower : MonoBehaviour
{
    [SerializeField] private GameObject _powerVisualizer;

    [Header("Data")]
    [SerializeField] private PowerData _cleanData;
    [SerializeField] private PowerData _shieldData;
    [SerializeField] private PowerData _boostData;
    
    

    private void Awake()
    {
        DisplayPowerVisualize(false);
    }

    public void OnClean()
    {
        CheckOverlapCircle(_cleanData, CleanCallback);
    }

    public void OnShield()
    {
        CheckOverlapCircle(_shieldData, ShieldCallback);
    }

    public void OnBoost()
    {
        CheckOverlapCircle(_boostData, BoostCallback);
    }

    private void CleanCallback(Collider2D collider2d, PowerData data)
    {
        Corruptible corruptible = collider2d.GetComponentInParent<Corruptible>();
        corruptible.DeCorrupt(data.Value);
    }

    private void ShieldCallback(Collider2D collider2d, PowerData data)
    {
        if(!collider2d.CompareTag("Tower")) return;
        
        Tower tower = collider2d.GetComponentInParent<Tower>();
        tower.ShieldTower(data.Value, data.Duration);
    }

    private void BoostCallback(Collider2D collider2d, PowerData data)
    {
        if(!collider2d.CompareTag("Tower")) return;

        Tower tower = collider2d.GetComponentInParent<Tower>();
        tower.BoostAttackSpeed(data.Value, data.Duration);
    }
    
    private void CheckOverlapCircle(PowerData powerData, Action<Collider2D, PowerData> callback)
    {
        Collider2D[] overlapResults = Physics2D.OverlapCircleAll(transform.position, powerData.Range / 2);
        if (overlapResults.Length > 0)
        {
            // Some results
            foreach (Collider2D collider2d in overlapResults)
            {
                if (collider2d.gameObject.layer == LayerMask.NameToLayer("Corruptible"))
                {
                    callback?.Invoke(collider2d, powerData);
                }
            }
        }
    }

    public void OnClickButton(Enums.PowerType powerType)
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
    
    public void OnButtonHoverEnter(Enums.PowerType powerType)
    {
        float value = powerType switch
        {
            Enums.PowerType.Heal => _cleanData.Range,
            Enums.PowerType.Shield => _shieldData.Range,
            Enums.PowerType.Boost => _boostData.Range,
        };

        DisplayAreaOfEffect(value);
    }
    
    public void OnButtonHoverExit()
    {
        DisplayPowerVisualize(false);
    }

    private void DisplayAreaOfEffect(float value)
    {
        DisplayPowerVisualize(true);
        ModifyCircleVisualisation(value);
    }

    private void DisplayPowerVisualize(bool value)
    {
        _powerVisualizer.SetActive(value);
    }

    private void ModifyCircleVisualisation(float value)
    {
        _powerVisualizer.transform.localScale = new Vector3(value, value, 1);
    }
}
