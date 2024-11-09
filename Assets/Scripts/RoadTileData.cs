using System;
using UnityEngine;

public class RoadTileData : Corruptible
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [Header("Sprites")]
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _lowCorruptRoadSprite;
    [SerializeField] private Sprite _midCorruptRoadSprite;
    [SerializeField] private Sprite _highCorruptRoadSprite;
    [SerializeField] private Sprite _corruptRoadSprite;

    private void Awake()
    {
        _spriteRenderer.sprite = _defaultSprite;
    }

    public void ChangeCorruption(float value)
    {
        Corrupt(value);
        UpdateSprite();
    }
    
    private void UpdateSprite()
    {
        _spriteRenderer.sprite = Corruption switch
        {
            0.0f => _defaultSprite,
            > 0.0f and <= 0.33f => _lowCorruptRoadSprite,
            > 0.33f and <= 0.66f => _midCorruptRoadSprite,
            > 0.66f and <= 0.99f => _highCorruptRoadSprite,
            > 1.0f => _corruptRoadSprite
        };
    }
}
