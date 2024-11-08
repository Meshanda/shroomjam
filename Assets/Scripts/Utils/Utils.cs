using System;
using System.Collections;
using UnityEngine;

public static class Utils
{
    public static IEnumerator WaitRoutine(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);

        callback?.Invoke();
    }
}