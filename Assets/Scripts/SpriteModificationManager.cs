using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SpriteModificationManager : MonoBehaviour
{
    [SerializeField] private Corruptible _corruptible;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _lowCorruptSprite;
    [SerializeField] private Sprite _midCorruptSprite;
    [SerializeField] private Sprite _highCorruptSprite;
    [SerializeField] private Sprite _corruptSprite;
    
    private void Awake()
    {
        _spriteRenderer.sprite = _defaultSprite;
    }

    private void OnEnable()
    {
        _corruptible.OnCorruptionValueChanged += OnCorruptionValueChangedHandler;
    }

    private void OnDisable()
    {
        _corruptible.OnCorruptionValueChanged -= OnCorruptionValueChangedHandler;
    }

    private void OnCorruptionValueChangedHandler(float corruptionPercentage)
    {
        Sprite sprite = corruptionPercentage switch
        {
            >= 0.0f and < 0.1f => _defaultSprite,
            >= 0.1f and < 0.3f => _lowCorruptSprite,
            >= 0.3f and < 0.6f => _midCorruptSprite,
            >= 0.6f and < 0.9f => _highCorruptSprite,
            >= 0.9f => _corruptSprite,
            _ => _defaultSprite
        };

        _spriteRenderer.sprite = sprite;
    }
}
