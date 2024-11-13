using System;

public class Character : Corruptible
{
    private void OnEnable()
    {
        GameManager.OnGameWin += OnGameWin;
    }
    
    private void OnDisable()
    {
        GameManager.OnGameWin -= OnGameWin;
    }

    private void OnGameWin()
    {
        SetGodMode();
    }

    private void SetGodMode()
    {
        DeCorrupt(float.MaxValue);
        CorruptionRate = 0;
    }
}
