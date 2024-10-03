using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader m_inputReader;

    [Header("Reference values")] [SerializeField]
    private FloatVariable so_ChunksCurrentSpeed;

    [Header("Hit boxes")] 
    [SerializeField] private Collider2D m_RunCollider;
    [SerializeField] private Collider2D m_JumpCollider;
    [SerializeField] private Collider2D m_SlideCollider;
    [SerializeField] private Collider2D m_AirDashCollider;
    
    private Rigidbody2D m_rigidbody;
    private Animator m_Animator;
    private StateMachine m_StateMachine;

    public int animHash_Jump => Animator.StringToHash("Jump");
    public int animHash_Move => Animator.StringToHash("Move");
    public int animHash_Slide => Animator.StringToHash("Dodge");
    
    private bool m_Jumping;
    private bool m_Airborne;
    private bool m_SlideInput;
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
        var slideState = new SlideState(this, m_Animator);
        
        At(idleState, runState, new FuncPredicate(() => m_Running));
        At(runState, idleState, new FuncPredicate(() => !m_Running));
        At(runState, jumpState, 
            new ActionPredicate(
                () => m_Jumping && m_Grounded,
                () =>
                {
                    EnableJumpCollider();
                })
            );
        At(jumpState, fallingState, new ActionPredicate(() => !m_Jumping && m_JumpAnimationFinished, () => m_Airborne = true));
        At(fallingState, runState, new ActionPredicate(
            () => m_Grounded,
            () =>
            {
                EnableRunCollider();
            })
        );
        
        At(runState, slideState, new ActionPredicate(() => m_SlideInput && m_Grounded,
            () =>
            {
                EnableSlideCollider();
            })
        );
        
        At(slideState, runState, new ActionPredicate(() => !m_SlideInput,
            () =>
            {
                EnableRunCollider();
            })
        );
        
        
        m_StateMachine.SetState(idleState);
    }

    protected void At(IState from, IState to, IPredicate condition) => m_StateMachine.AddTransition(from, to, condition);
    protected void AtAny(IState to, IPredicate condition) => m_StateMachine.AddAnyTransition(to, condition);
    
    private void OnEnable()
    {
        m_inputReader.JumpEvent += OnJump;
        m_inputReader.SlideEvent += OnSlide;
    }

    private void OnDisable()
    {
        m_inputReader.JumpEvent -= OnJump;
        m_inputReader.SlideEvent -= OnSlide;
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

    private void OnSlide(bool value)
    {
        m_SlideInput = value;
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

    private void EnableRunCollider()
    {
        m_RunCollider.enabled = true;
        
        m_JumpCollider.enabled = false;
        m_SlideCollider.enabled = false;
        m_AirDashCollider.enabled = false;
    }

    private void EnableJumpCollider()
    {
        m_JumpCollider.enabled = true;
        
        m_RunCollider.enabled = false;
        m_SlideCollider.enabled = false;
        m_AirDashCollider.enabled = false;
    }

    private void EnableSlideCollider()
    {
        m_SlideCollider.enabled = true;
        
        m_RunCollider.enabled = false;
        m_JumpCollider.enabled = false;
        m_AirDashCollider.enabled = false;
    }

    private void EnableAirDashCollider()
    {
        m_AirDashCollider.enabled = true;
        
        m_SlideCollider.enabled = false;
        m_RunCollider.enabled = false;
        m_JumpCollider.enabled = false;
    }
}
