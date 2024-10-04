using System;
using UnityEngine;
using UnityEngine.Events;

public class FormatTimerData : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private FloatVariable timer;

    [Header("Output")] 
    [SerializeField] private FloatVariable seconds;
    [SerializeField] private FloatVariable minutes;
    [SerializeField] private FloatVariable concat;


    public UnityEvent OnFormat;
    
    public void Format()
    {
        int value = (int) timer.Value;
        seconds.Value = value % 60;
        minutes.Value = value / 60;

        string format = FormatTime(timer.Value);
        concat.Value = Convert.ToInt32(format);
        OnFormat?.Invoke();
    }
    
    private string FormatTime(float time)
    {
        var seconds = Mathf.FloorToInt(time);
        var minutes = Mathf.FloorToInt(seconds / 60);
        
        seconds %= 60;
        
        return $"{minutes / 10}{minutes % 10}{seconds / 10}{seconds % 10}";
    }
}
