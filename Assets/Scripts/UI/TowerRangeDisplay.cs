using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerRangeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Collider2D _attackCollider;
    [SerializeField] private GameObject _rangeDisplay;

    private void Awake()
    {
        _attackCollider = GetComponent<Collider2D>();
        _rangeDisplay.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayRange(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayRange(false);
    }

    private void DisplayRange(bool b)
    {
        _rangeDisplay.SetActive(b);
    }
}
