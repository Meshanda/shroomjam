using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBarButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _bindText;
    [SerializeField] private TextMeshProUGUI _cooldownText;
    [SerializeField] private Image _cooldownImage;

    [SerializeField] private GameObject _hoverImage;
    [SerializeField] private PowerDataSO _powerDataSo;
    [SerializeField] private string _bindKey;

    private void Start()
    {
        Init(_powerDataSo.Cost.ToString(), _bindKey);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverImage.SetActive(false);
    }

    private void Init(string cost, string bind)
    {
        _costText.text = cost;
        _bindText.text = bind;
    }

    private void Update()
    {
        if (_powerDataSo.IsAvailable)
        {
            _cooldownImage.gameObject.SetActive(false);
            _cooldownText.gameObject.SetActive(false);
            
            _cooldownImage.fillAmount = 1;
        }
        else
        {
            _cooldownImage.gameObject.SetActive(true);
            _cooldownText.gameObject.SetActive(true);
            
            _cooldownImage.fillAmount = _powerDataSo.RemainingTimeCooldown / _powerDataSo.Cooldown;
            _cooldownText.text = Math.Round(_powerDataSo.RemainingTimeCooldown, 1) + "s";
        }
    }
}
