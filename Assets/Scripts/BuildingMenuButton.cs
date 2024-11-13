using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class BuildingMenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Enums.TowerType _towerType;
    [SerializeField] private TextMeshPro _costText;
    
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _hoverSprite;

    private SpriteRenderer _renderer;
    private BuildingMenu _buildingMenu;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _buildingMenu = GetComponentInParent<BuildingMenu>();
    }

    private void Start()
    {
        _costText.text = GameManager.Instance.GetTowerCost(_towerType).ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _buildingMenu.OnButtonClicked(_towerType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.sprite = _hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.sprite = _normalSprite;
    }
}