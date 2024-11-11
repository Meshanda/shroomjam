using System;
using UnityEngine;

public class WarningSystem : Singleton<WarningSystem>
{
    [SerializeField] private GameObject _warningPrefab;
    [SerializeField] private Transform _parent;

    public void AddWarning(string text)
    {
        var warning = Instantiate(_warningPrefab, _parent).GetComponent<Warning>();
        
        warning.SetText(text);
        warning.gameObject.SetActive(true);
    }
}
