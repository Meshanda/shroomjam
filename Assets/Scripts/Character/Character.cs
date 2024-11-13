using System;
using UnityEngine;

public class Character : Corruptible
{

    [SerializeField] private GameObject _healEffect;
    
    private void OnEnable()
    {
        GameManager.OnGameWin += OnGameWin;
        GameManager.OnGameOver += OnGameOver;
    }
    

    private void OnDisable()
    {
        GameManager.OnGameWin -= OnGameWin;
        GameManager.OnGameOver -= OnGameOver;
    }

    public void ActivateHealEffect()
    {
        _healEffect.SetActive(true);
    }
    
    public void DeactivateHealEffect()
    {
        _healEffect.SetActive(false);
    }

    private void OnGameWin()
    {
        SetGodMode();
    }
    
    private void OnGameOver(Enums.GameOverType obj)
    {
        SetGodMode();
    }

    private void SetGodMode()
    {
        DeCorrupt(float.MaxValue);
        CorruptionRate = 0;
    }
}
