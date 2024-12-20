﻿using System.Collections;
using UnityEngine;

public class DeathVideoHintButton : MonoBehaviour
{
    [SerializeField] private float seconds;
    [SerializeField] private ApproachingButton button;
    private WaitForSecondsRealtime waiter;
    private WaitForSecondsRealtime waitForSecond;

    private void Awake()
    {
        waiter = new WaitForSecondsRealtime(seconds);
        waitForSecond = new WaitForSecondsRealtime(1f);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(DeathVideoStarts());
    }
    
    private IEnumerator DeathVideoStarts()
    {
        while (true)
        {
            yield return waiter;
            button.FadeIn();
            yield return waitForSecond;
            button.FadeOut();
        }
    }
   
}