using System;
using System.Collections;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private float rate;

    private void Awake()
    {
        GameManager.instance.SlowTime = this;
    }

    private void OnEnable()
    {
        input.SlowTimeEvent += Execute;
    }

    private void OnDisable()
    {
        input.SlowTimeEvent -= Execute;

        Time.timeScale = 1f;
    }

    private void Execute()
    {
        StopAllCoroutines();
        StartCoroutine(Freeze());
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void RevertTime()
    {
        StopAllCoroutines();
        StartCoroutine(Coroutines.FadeFloat(Time.timeScale, 1f, rate, Setter));
    }

    private IEnumerator Freeze()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(Coroutines.FadeFloat(Time.timeScale, 1f, rate, Setter));
    }

    private void Setter(float value)
    {
        Time.timeScale = value;
    }
}