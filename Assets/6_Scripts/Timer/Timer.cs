using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TimerData timerData;
    public float m_ElapsedTime;

    void Update()
    {
        m_ElapsedTime += Time.deltaTime;
        timerData.ElapsedTime = m_ElapsedTime;
    }
}
