using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private InputReader m_InputReader;
    [SerializeField] private FloatVariable so_Speed;
    [SerializeField] private float m_AccelerationRate;
    [SerializeField] private float m_Diminishing;
    
    private bool m_Running;
    private float m_ElapsedTime;
    private float m_Acceleration;

    private float m_SpeedMultiplier = 1f;
    
    private void OnEnable()
    {
        m_InputReader.RunEvent += OnMove;
    }

    private void OnDisable()
    {
        m_InputReader.RunEvent -= OnMove;
    }

    void Start()
    {
        so_Speed.Value = 0f;
    }

    void Update()
    {
        if (m_Running)
        {
            IncreaseSpeed();
        }
    }

    private void OnMove(bool value)
    {
        m_Running = value;
    }

    private void Stop()
    {
        m_Running = false;
        ResetSpeed();
    }

    private void IncreaseSpeed()
    {
        m_ElapsedTime += Time.deltaTime;
        m_Acceleration = (m_AccelerationRate * m_ElapsedTime) / (1 + m_Diminishing * m_ElapsedTime);

        so_Speed.Value = m_Acceleration * m_SpeedMultiplier;
    }

    public void ResetSpeed()
    {
        m_Acceleration = 0f;
        m_ElapsedTime = 0f;
        so_Speed.Value = 0f;
    }

    public void ApplyMultiplier(float value)
    {
        m_SpeedMultiplier = value;
        so_Speed.Value = m_Acceleration * m_SpeedMultiplier;
    }
}
