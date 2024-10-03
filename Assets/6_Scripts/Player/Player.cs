using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader m_inputReader;

    [Header("Reference values")] [SerializeField]
    private FloatVariable so_ChunksCurrentSpeed;

    [Header("Hit boxes")] 
    [SerializeField] private Collider2D m_RunCollider;
    [SerializeField] private Collider2D m_JumpCollider;
    
    private Rigidbody2D m_rigidbody;
    private Animator m_Animator;
    private StateMachine m_StateMachine;

    private bool m_Jumping;
    private bool m_Airborne;
    private bool m_JumpAnimationFinished;
    
    private float m_ChunksCurrentSpeed => so_ChunksCurrentSpeed.Value;
    private bool m_Running => m_ChunksCurrentSpeed > 0;
    private bool m_Grounded => transform.position.y < -1.4f;

    public float JumpSpeed => 10f;
    public float GravityForce => 10f;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        
        m_StateMachine = new StateMachine();

        var idleState = new IdleState(this, m_Animator);
        var runState = new RunState(this, m_Animator);
        var jumpState = new JumpState(this, m_Animator);
        var airState = new AirState(this, m_Animator);
        var fallingState = new FallingState(this, m_Animator);
        
        At(idleState, runState, new FuncPredicate(() => m_Running));
        At(runState, idleState, new FuncPredicate(() => !m_Running));
        At(runState, jumpState, 
            new ActionPredicate(
                () => m_Jumping && m_Grounded,
                () =>
                {
                    m_RunCollider.enabled = false;
                    m_JumpCollider.enabled = true;
                })
            );
        At(jumpState, fallingState, new ActionPredicate(() => !m_Jumping && m_JumpAnimationFinished, () => m_Airborne = true));
        At(fallingState, runState, new ActionPredicate(
            () => m_Grounded,
            () =>
            {
                m_RunCollider.enabled = true;
                m_JumpCollider.enabled = false;
            })
        );
        
        m_StateMachine.SetState(idleState);
    }

    protected void At(IState from, IState to, IPredicate condition) => m_StateMachine.AddTransition(from, to, condition);
    protected void AtAny(IState to, IPredicate condition) => m_StateMachine.AddAnyTransition(to, condition);
    
    private void OnEnable()
    {
        m_inputReader.JumpEvent += OnJump;
    }

    private void OnDisable()
    {
        m_inputReader.JumpEvent -= OnJump;
    }
    
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
       m_StateMachine.Update();
    }

    private void FixedUpdate()
    {
        m_StateMachine.FixedUpdate();
    }

    private void OnJump(bool value)
    {
        m_Jumping = value;
    }

    private bool IsAnimationPlaying()
    {
        Debug.Log("Normalized time " + m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Debug.Log(m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        return m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;
    }
    
    // used as event in jump_on animation
    public void JumpAnimationFinished()
    {
        m_JumpAnimationFinished = true;
    }
    
    // used as event in jump_on animation
    public void JumpAnimationStarted()
    {
        m_JumpAnimationFinished = false;
    }
}
