using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private FloatVariable timerData;
    public float m_ElapsedTime;

    private bool isRunning = false;
    
    private float seconds;
    public UnityEvent EverySecond;

    private void Awake()
    {
        timerData.Value = 0f;
    }

    void Update()
    {
        if (!isRunning) return;
        
        seconds += Time.deltaTime;
        
        m_ElapsedTime += Time.deltaTime;
        timerData.Value = m_ElapsedTime;

        if (seconds > 1f)
        {
            seconds = 0f;
            EverySecond?.Invoke();
        }
    }

    public void Stop()
    {
        isRunning = false;
    }

    public void Run()
    {
        //Debug.Log("RUn");
        isRunning = true;
    }
}
