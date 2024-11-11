using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private LayoutElement _layoutElement;
    
    [SerializeField] private int _characterWrapLimit;
    
    private RectTransform _rectTransform;

    private void OnEnable()
    {
        GetComponent<Image>().DOFade(0.8f, 0.1f);
    }

    private void OnDisable()
    {
        var color = GetComponent<Image>().color;
        color.a = 0;
        
        GetComponent<Image>().color = color;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    
    private void LateUpdate()
    {
        var position = Input.mousePosition;
        var normalizedPosition = new Vector2(position.x / Screen.width, position.y / Screen.height);
        var pivot = CalculatePivot(normalizedPosition);
        _rectTransform.pivot = pivot;
        transform.position = position;
    }
    
    private Vector2 CalculatePivot(Vector2 normalizedPosition)
    {
        var pivotTopLeft = new Vector2(-0.05f, 1.05f);
        var pivotTopRight = new Vector2(1.05f, 1.05f);
        var pivotBottomLeft = new Vector2(-0.05f, -0.05f);
        var pivotBottomRight = new Vector2(1.05f, -0.05f);

        return normalizedPosition.x switch
        {
            < 0.5f when normalizedPosition.y >= 0.5f => pivotTopLeft,
            > 0.5f when normalizedPosition.y >= 0.5f => pivotTopRight,
            <= 0.5f when normalizedPosition.y < 0.5f => pivotBottomLeft,
            _ => pivotBottomRight
        };
    }

    private void UpdateSize()
    {
        int headerLength = _headerText.text.Length;
        int contentLength = _contentText.text.Length;

        _layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) ? true : false;
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
            _headerText.gameObject.SetActive(false);
        else
        {
            _headerText.gameObject.SetActive(true);
            _headerText.text = header;
        }
        
        _contentText.text = content;

        UpdateSize();
    }
}
