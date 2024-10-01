using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader m_inputReader;
    [SerializeField] private float m_AccelerationRate;
    [SerializeField] private float m_Diminishing;
    
    private Rigidbody2D m_rigidbody;

    private float m_Acceleration;
    private float m_ElapsedTime;
    private bool m_running;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_inputReader.RunEvent += OnRun;
    }

    private void OnDisable()
    {
        m_inputReader.RunEvent -= OnRun;
    }
    
    private void OnRun(bool value)
    {
        m_running = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Move();

        if (m_running)
        {
            IncreaseAcceleration();
        }
        else
        {
            DecreaseAcceleration();
            /*m_Acceleration = 0f;
            m_ElapsedTime = 0f;*/
        }
    }

    private void Move()
    {
        //m_Acceleration += Time.deltaTime * m_AccelerationRate; 
        m_rigidbody.MovePosition(transform.position + Vector3.right * (m_Acceleration * Time.deltaTime));
    }

    private void IncreaseAcceleration()
    {
        m_ElapsedTime += Time.deltaTime;
        m_Acceleration = (m_AccelerationRate * m_ElapsedTime) / (1 + m_Diminishing * m_ElapsedTime);
    }

    private void DecreaseAcceleration()
    {
        
    }
}
