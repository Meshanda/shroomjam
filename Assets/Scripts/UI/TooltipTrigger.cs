using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Enums.TooltipType Type;
    [SerializeField] private string _header;
    [SerializeField] [Multiline] private string _content;
    [SerializeField] private float _delay = .1f;
    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _coroutine = StartCoroutine(Utils.WaitRoutine(_delay, () =>
            {
                TooltipSystem.Instance.Show(Type, _content, _header);
            }));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        TooltipSystem.Instance.Hide();
    }
}
