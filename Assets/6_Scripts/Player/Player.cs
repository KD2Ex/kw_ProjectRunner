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
    public int animHash_Bounce => Animator.StringToHash("Bounce");
    
    private bool m_JumpInput;
    private bool m_SlideInput;
    private bool m_AirDashInput;
    private bool m_JumpAnimationFinished;
    
    private float m_ChunksCurrentSpeed => so_ChunksCurrentSpeed.Value;
    private bool m_Running => m_ChunksCurrentSpeed > 0;
    
    public bool Grounded => transform.position.y <= -1.5f;
    public float JumpSpeed => 10f;
    public float GravityForce => 10f;
    public float AirDashMovementSpeed => 6f;
    public float UpperAirDashBound => 1.5f; 
    public float LowerAirDashBound => -6.6f; 
    
    public float m_AirDashMovementDirection { get; private set; }

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        
        m_StateMachine = new StateMachine();

        var idleState = new IdleState(this, m_Animator);
        var runState = new RunState(this, m_Animator);
        var jumpState = new JumpState(this, m_Animator);
        var fallingState = new FallingState(this, m_Animator);
        var slideState = new SlideState(this, m_Animator);
        var airDashState = new AirDashState(this, m_Animator);
        
        At(idleState, runState, new FuncPredicate(() => m_Running));
        At(runState, idleState, new FuncPredicate(() => !m_Running));
        At(runState, jumpState, 
            new ActionPredicate(
                () => m_JumpInput && Grounded,
                () =>
                {
                    EnableJumpCollider();
                })
            );
        At(jumpState, fallingState, new FuncPredicate(() => !m_JumpInput && m_JumpAnimationFinished));
        At(fallingState, runState, new ActionPredicate(
            () => Grounded,
            () =>
            {
                EnableRunCollider();
            })
        );
        
        At(runState, slideState, new ActionPredicate(() => m_SlideInput && Grounded,
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
        
        At(jumpState, airDashState, new ActionPredicate(
            () =>
            {
                return m_AirDashInput;
            },
            () =>
            {
                EnableAirDashCollider();

                m_JumpInput = false;
                m_SlideInput = false;
            })
        );
        
        At(airDashState, fallingState, new ActionPredicate(() => !m_AirDashInput, () => EnableJumpCollider()));
        
        
        m_StateMachine.SetState(idleState);
    }

    protected void At(IState from, IState to, IPredicate condition) => m_StateMachine.AddTransition(from, to, condition);
    protected void AtAny(IState to, IPredicate condition) => m_StateMachine.AddAnyTransition(to, condition);
    
    private void OnEnable()
    {
        m_inputReader.JumpEvent += OnJump;
        m_inputReader.SlideEvent += OnSlide;
        m_inputReader.AirDashEvent += OnAirDash;
    }

    private void OnDisable()
    {
        m_inputReader.JumpEvent -= OnJump;
        m_inputReader.SlideEvent -= OnSlide;
        m_inputReader.AirDashEvent -= OnAirDash;
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
        m_JumpInput = value;
    }

    private void OnSlide(bool value)
    {
        m_SlideInput = value;
    }

    private void OnAirDash(bool value)
    {
        m_AirDashInput = value;
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

    public void SwitchControlsToAirDash()
    {
        m_inputReader.JumpEvent -= OnJump;
        m_inputReader.SlideEvent -= OnSlide;

        //m_inputReader. += ;
        m_inputReader.AirDashMovementEvent += AirDashMovement;
    }

    public void ReturnToDefaultControls()
    {
        m_inputReader.AirDashMovementEvent -= AirDashMovement;
        
        m_inputReader.JumpEvent += OnJump;
        m_inputReader.SlideEvent += OnSlide;
    }

    private void AirDashMovement(float direction)
    {
        m_AirDashMovementDirection = direction;
    }
}
