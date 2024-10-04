using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private FloatVariable timerData;
    public float m_ElapsedTime;

    private float seconds;
    public UnityEvent EverySecond;
    
    void Update()
    {
        seconds += Time.deltaTime;
        
        m_ElapsedTime += Time.deltaTime;
        timerData.Value = m_ElapsedTime;

        if (seconds > 1f)
        {
            seconds = 0f;
            EverySecond?.Invoke();
        }
    }
}
