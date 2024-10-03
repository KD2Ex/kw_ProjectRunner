using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader m_inputReader;

    [Header("Reference values")] [SerializeField]
    private FloatVariable so_ChunksCurrentSpeed;
    
    private Rigidbody2D m_rigidbody;
    private StateMachine m_StateMachine;
    
    private float m_ChunksCurrentSpeed => so_ChunksCurrentSpeed.Value;
    private bool m_Running => m_ChunksCurrentSpeed > 0;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_StateMachine = new StateMachine();

        var idleState = new IdleState(this);
        var runState = new RunState(this);
        
        At(idleState, runState, new FuncPredicate(() => m_Running));
        At(runState, idleState, new FuncPredicate(() => m_Running));
        
        m_StateMachine.SetState(idleState);
    }

    protected void At(IState from, IState to, IPredicate condition) => m_StateMachine.AddTransition(from, to, condition);
    protected void AtAny(IState to, IPredicate condition) => m_StateMachine.AddAnyTransition(to, condition);
    
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }
    
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
       
    }

    private void FixedUpdate()
    {
    }
}
