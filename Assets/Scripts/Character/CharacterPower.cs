using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;

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
        Debug.Log("Clean");
        Collider2D[] overlapResults = Physics2D.OverlapCircleAll(transform.position, _cleanData.Range / 2);
        if (overlapResults.Length > 0)
        {
            // Some results
            foreach (Collider2D collider in overlapResults)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Corruptible"))
                {
                    Corruptible corruptible = collider.GetComponentInParent<Corruptible>();
                    corruptible.DeCorrupt(_cleanData.Value);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (_cleanData.ShowGizmos)
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.forward, _cleanData.Range / 2);
        }
        if (_shieldData.ShowGizmos)
        {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.forward, _shieldData.Range / 2);
        }
        if (_boostData.ShowGizmos)
        {
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.forward, _boostData.Range / 2);
        }
    }

    public void OnShield()
    {
        Debug.Log("Shield");
    }

    public void OnBoost()
    {
        Debug.Log("Boost");
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
