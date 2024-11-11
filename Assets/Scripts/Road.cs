using System;
using UnityEngine;

public class Road : Corruptible
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void ChangeCorruption(float value)
    {
        Corrupt(value);
    }
    
    public void ChangeSprite(Sprite sprite)
    {
        Debug.Log(sprite);
        _spriteRenderer.sprite = sprite;
    }
}
