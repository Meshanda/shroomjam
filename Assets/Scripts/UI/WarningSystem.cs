using System;
using UnityEngine;

public class WarningSystem : Singleton<WarningSystem>
{
    [SerializeField] private GameObject _warningPrefab;
    [SerializeField] private Transform _parent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddWarning("Test warning");
        }
    }

    public void AddWarning(string text)
    {
        var warning = Instantiate(_warningPrefab, _parent).GetComponent<Warning>();
        
        warning.SetText(text);
        warning.gameObject.SetActive(true);
    }
}
