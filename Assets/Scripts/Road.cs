using System;
using UnityEngine;

public class Road : Corruptible
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.GetComponentInParent<Enemy>() is { } enemy)
        {
            if(LastStateReached)
            {
                if (!enemy.Boosted)
                {
                    enemy.Boost();
                }
            }
            else
            {
                if (enemy.Boosted)
                {
                    enemy.Deboost();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.GetComponentInParent<Enemy>() is { } enemy)
        {
            if (enemy.Boosted)
            {
                enemy.Deboost();
            }
        }
    }

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
