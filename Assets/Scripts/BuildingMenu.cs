using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    [SerializeField] private List<BuildingMenuButton> _buttons;

    private void OnEnable()
    {
        for (var i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].ButtonIndex = i+1;
        }
    }
}