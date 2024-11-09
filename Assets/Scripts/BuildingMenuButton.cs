using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(BoxCollider2D), typeof(Renderer))]
public class BuildingMenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Enums.TowerType _towerType;
    
    public Color NormalColor = Color.white;
    public Color HoverColor = Color.grey;

    private Renderer _renderer;
    private BuildingMenu _buildingMenu;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _buildingMenu = GetComponentInParent<BuildingMenu>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _buildingMenu.OnButtonClicked(_towerType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material.color = NormalColor;
    }
}