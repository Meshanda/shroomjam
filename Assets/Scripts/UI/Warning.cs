using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _messageDuration = 2f;
    [SerializeField] private float _fadeDuration = .1f;

    private void OnEnable()
    {
        _text.DOFade(1, _fadeDuration).OnComplete(() =>
        {
            _text.DOFade(0, _messageDuration).SetEase(Ease.OutSine).OnComplete(() => Destroy(gameObject));
        });
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
