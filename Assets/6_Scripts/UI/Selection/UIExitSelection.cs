﻿using UnityEngine;
using UnityEngine.Events;

public class UIExitSelection : UISelection
{
    public UnityEvent OnExit;

    private void OnEnable()
    {
        Select(true);
    }

    public override void Press()
    {
        base.Press();
        Debug.Log("Exit");
        OnExit?.Invoke();
    }
}