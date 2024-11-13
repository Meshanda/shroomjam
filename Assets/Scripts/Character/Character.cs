using System;

public class Character : Corruptible
{
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
