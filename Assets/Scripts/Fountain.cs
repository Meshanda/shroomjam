using System;
using System.Collections;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    [SerializeField] private float _corruptionHealValue;
    [SerializeField] private float _corruptionHealSpeed;
    
    private bool _isPlayerInside = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        _isPlayerInside = true;
        Character player = other.GetComponent<Character>(); 
        StartCoroutine(HealCorruptionCoroutine(player));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInside = false;
        }
    }

    private IEnumerator HealCorruptionCoroutine(Character player)
    {
        while (_isPlayerInside && player.Corruption != 0)
        {
            player.DeCorrupt(_corruptionHealValue);
            yield return new WaitForSeconds(_corruptionHealSpeed);
        }
    }
}
