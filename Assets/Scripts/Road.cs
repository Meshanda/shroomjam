using System;
using UnityEngine;

public class Road : Corruptible
{
    public void ChangeCorruption(float value)
    {
        Corrupt(value);
    }
}
