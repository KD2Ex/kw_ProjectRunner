using System;
using UnityEngine;

[RequireComponent(typeof(SpeedController))]
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

    private SpeedController m_SpeedController;
    private Rigidbody2D m_rigidbody;
    private Animator m_Animator;
    private StateMachine m_StateMachine;

    public int animHash_Jump => Animator.StringToHash("Jump");
    public int animHash_Move => Animator.StringToHash("Move");
    public int animHash_Slide => Animator.StringToHash("Dodge");
    public int animHash_Bounce => Animator.StringToHash("Bounce");
    public int animHash_Dash => Animator.StringToHash("Faster");
    public int animHash_Idle => Animator.StringToHash("Idle");
    
    private bool m_StopInput;
    private bool m_JumpInput;
    private bool m_SlideInput;
    private bool m_AirDashInput;
    private bool m_DashInput;
    
    private bool m_JumpAnimationFinished;
    private bool m_Dashing;
    
    private float m_ChunksCurrentSpeed => so_ChunksCurrentSpeed.Value;
    private bool m_Running => m_ChunksCurrentSpeed > 0;

    public static readonly float GroundLine = -1.77f; // replace with so data
    public bool Grounded => transform.position.y <= GroundLine;
    public float JumpSpeed => 10f;
    public float GravityForce => 10f;
    public float AirDashMovementSpeed => 6f;
    public float UpperAirDashBound => 1.5f; 
    public float LowerAirDashBound => -6.6f;
    public float DashSpeedMultiplier => 1.5f;
    
    public float m_AirDashMovementDirection { get; private set; }

    private void Awake()
    {
        m_SpeedController = GetComponent<SpeedController>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        
        m_StateMachine = new StateMachine();

        var sleepState = new SleepState(this, m_Animator);
        var idleState = new IdleState(this, m_Animator);
        var runState = new RunState(this, m_Animator);
        var jumpState = new JumpState(this, m_Animator);
        var fallingState = new FallingState(this, m_Animator);
        var slideState = new SlideState(this, m_Animator);
        var airDashState = new AirDashState(this, m_Animator);
        var dashState = new DashState(this, m_Animator);
        
        At(sleepState, runState, new FuncPredicate(() => m_Running));
        At(idleState, runState, new ActionPredicate(() => m_Running, () => m_StopInput = false));
        At(runState, idleState, new ActionPredicate(() => m_StopInput, () => m_SpeedController.ResetSpeed()));
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
        At(runState, dashState, new ActionPredicate(() => m_DashInput, () =>
        {
            m_SpeedController.ApplyMultiplier(DashSpeedMultiplier);
            m_Dashing = true;
            m_DashInput = false;
        }));
        
        At(dashState, runState, new ActionPredicate(() => !m_Dashing, () =>
        {
            m_SpeedController.ApplyMultiplier(1f);
        }));
        
        m_StateMachine.SetState(sleepState);
    }

    protected void At(IState from, IState to, IPredicate condition) => m_StateMachine.AddTransition(from, to, condition);
    protected void AtAny(IState to, IPredicate condition) => m_StateMachine.AddAnyTransition(to, condition);
    
    private void OnEnable()
    {
        m_inputReader.JumpEvent += OnJump;
        m_inputReader.SlideEvent += OnSlide;
        m_inputReader.StopEvent += OnStop;
        m_inputReader.AirDashEvent += OnAirDash;
        m_inputReader.DashAbilityTest += OnDash;
    }

    private void OnDisable()
    {
        m_inputReader.JumpEvent -= OnJump;
        m_inputReader.SlideEvent -= OnSlide;
        m_inputReader.StopEvent -= OnStop;
        m_inputReader.AirDashEvent -= OnAirDash;
        m_inputReader.DashAbilityTest -= OnDash;
    }
    
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        //Time.timeScale = .5f;
    }

    void Update()
    {
       m_StateMachine.Update();
    }

    private void FixedUpdate()
    {
        m_StateMachine.FixedUpdate();
    }

    private void OnStop(bool value) => m_StopInput = value;
    private void OnJump(bool value) => m_JumpInput = value;
    private void OnSlide(bool value) => m_SlideInput = value;
    private void OnAirDash(bool value) => m_AirDashInput = value;
    private void OnDash()
    {
        Debug.Log(m_StateMachine.CurrentState.ToString());
        m_DashInput = m_StateMachine.CurrentState.ToString() == "RunState";
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

        m_inputReader.AirDashMovementEvent += AirDashMovement;
    }

    public void ReturnToDefaultControls()
    {
        m_inputReader.AirDashMovementEvent -= AirDashMovement;

        m_AirDashMovementDirection = 0f;
        
        m_inputReader.JumpEvent += OnJump;
        m_inputReader.SlideEvent += OnSlide;
    }

    private void AirDashMovement(float direction)
    {
        m_AirDashMovementDirection = direction;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On Trigger:  " + other.gameObject.name);

        var tag = other.gameObject.tag;

        switch (tag)
        {
            case "Coin":
                other.gameObject.SetActive(false);
                // add coin
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        
        m_SpeedController.ResetSpeed();
        
        var tag = other.gameObject.tag;

        switch (tag)
        {
            case "Enemy": 
                // dead
                break;
            case "Coin":
                // collect coin
                other.gameObject.SetActive(false);
                break;
        }
    }

    private void Die()
    {
        
    }

    public void DeactiveDash()
    {
        m_Dashing = false;
    }
    
}
