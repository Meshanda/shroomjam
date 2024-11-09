using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D), typeof(Renderer))]
public class BuildingMenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Color NormalColor = Color.white;
    public Color HoverColor = Color.grey;
    public int ButtonIndex = 0;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Button {ButtonIndex} Clicked");
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