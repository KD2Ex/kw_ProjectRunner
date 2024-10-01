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
    
    private void OnEnable()
    {
        m_InputReader.RunEvent += OnMove;
    }

    private void OnDisable()
    {
        m_InputReader.RunEvent -= OnMove;
    }

    // Start is called before the first frame update
    void Start()
    {
        so_Speed.Value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Running)
        {
            IncreaseSpeed();
        }
        else
        {
            
        }
    }

    private void OnMove(bool value)
    {
        m_Running = value;
    }

    private void IncreaseSpeed()
    {
        m_ElapsedTime += Time.deltaTime;
        m_Acceleration = (m_AccelerationRate * m_ElapsedTime) / (1 + m_Diminishing * m_ElapsedTime);

        so_Speed.Value = m_Acceleration;
    }
}
