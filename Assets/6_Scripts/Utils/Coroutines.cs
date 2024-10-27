using System;
using System.Collections;
using UnityEngine;

public class Coroutines
{
    public static IEnumerator WaitFor(float seconds, Action before = null, Action after = null)
    {
        before?.Invoke();
        yield return new WaitForSeconds(seconds);
        after?.Invoke();
    }
}