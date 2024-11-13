using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerRangeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer _rangeRenderer;
    [SerializeField] private float _fadeValue;
    
    private Tower _tower;
    private Coroutine _coroutine;
    
    private void Awake()
    {
        _tower = GetComponentInParent<Tower>();
        _rangeRenderer.transform.localScale = new Vector3(_tower.Range, _tower.Range, 1);
        
        _coroutine = null;
        
        DisplayRange(false, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayRange(true, _fadeValue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisplayRange(false, 0);
    }

    private void DisplayRange(bool b, float fadeValue)
    {
        if (b)
            _coroutine = StartCoroutine(Utils.WaitRoutine(0.1f, () =>
            {
                _rangeRenderer.DOFade(fadeValue, 0.1f);
            }));

        if (b) return;
        var color = _rangeRenderer.color;
        color.a = 0;
        _rangeRenderer.color = color;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
