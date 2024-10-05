using System.Collections.Generic;
using Unity.PlasticSCM.Editor.UI;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpeedController))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader m_inputReader;

    [Header("Stats")]
    [Range(0, 3)]
    [SerializeField] private float m_JumpTime;

    public float JumpTime => m_JumpTime;
    
    [Header("Reference values")] 
    [SerializeField] private FloatVariable so_ChunksCurrentSpeed;
    [SerializeField] private FloatVariable so_JumpTime;

    [Header("Hit boxes")] 
    [SerializeField] private Collider2D m_RunCollider;
    [SerializeField] private Collider2D m_JumpCollider;
    [SerializeField] private Collider2D m_SlideCollider;
    [SerializeField] private Collider2D m_AirDashCollider;
    [SerializeField] private Collider2D m_DashCollider;

    private List<Collider2D> m_Colliders = new();
    
    private Coins m_Coins;
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
    public int animHash_DodgeDeath => Animator.StringToHash("Dead0");
    public int animHash_RunJumpDeath => Animator.StringToHash("Dead1");
    
    private bool m_StopInput;
    private bool m_JumpInput;
    private bool m_SlideInput;
    private bool m_AirDashInput;
    private bool m_DashInput;

    public void ForceToGetDown()
    {
        m_JumpInput = false;
    }
    public bool JumpInput => m_JumpInput;
    
    private bool m_JumpAnimationFinished;
    private bool m_Dashing;

    private bool m_IsDead;
    private bool m_Restart;

    public int m_DeathType { get; private set; }
    
    private float m_ChunksCurrentSpeed => so_ChunksCurrentSpeed.Value;
    private bool m_Running => m_ChunksCurrentSpeed > 0;

    public static readonly float GroundLine = -1.77f; // replace with so data
    public static readonly float XPosition = -12.95f;
    private static readonly int c_LayerMaskDefault = 0;
    private static readonly int c_LayerMaskDash = 55;
    public bool Grounded => transform.position.y - .01f <= GroundLine;
    public float JumpSpeed => 10f;
    public float GravityForce => 10f;
    public float AirDashMovementSpeed => 6f;
    public float UpperAirDashBound => 1.5f; 
    public float LowerAirDashBound => -6.6f;
    public float DashSpeedMultiplier => 1.5f;
    
    public float m_AirDashMovementDirection { get; private set; }

    private IState sleepState;

    public UnityEvent OnDashEvent;
    
    private void Awake()
    {
        m_Colliders.Add(m_AirDashCollider);
        m_Colliders.Add(m_DashCollider);
        m_Colliders.Add(m_JumpCollider);
        m_Colliders.Add(m_SlideCollider);
        m_Colliders.Add(m_RunCollider);
        
        m_Coins = GetComponent<Coins>();
        m_SpeedController = GetComponent<SpeedController>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        
        m_StateMachine = new StateMachine();

        sleepState = new SleepState(this, m_Animator);
        var idleState = new IdleState(this, m_Animator);
        var runState = new RunState(this, m_Animator);
        var jumpState = new JumpState(this, m_Animator);
        var fallingState = new FallingState(this, m_Animator);
        var slideState = new SlideState(this, m_Animator);
        var airDashState = new AirDashState(this, m_Animator);
        var dashState = new DashState(this, m_Animator);
        var deathState = new DeathState(this, m_Animator);
        
        At(sleepState, runState, new FuncPredicate(() => m_Running));
        At(idleState, runState, new ActionPredicate(() => m_Running, () => m_StopInput = false));
        At(runState, idleState, new ActionPredicate(() => m_StopInput, () => m_SpeedController.ResetSpeed()));
        At(runState, jumpState, 
            new ActionPredicate(
                () => m_JumpInput && Grounded,
                () =>
                {
                    so_JumpTime.Value = m_JumpTime;
                    EnableOnly(m_JumpCollider);
                })
            );
        At(jumpState, fallingState, new FuncPredicate(() => !m_JumpInput && m_JumpAnimationFinished));
        At(fallingState, runState, new ActionPredicate(
            () => Grounded,
            () =>
            {
                EnableOnly(m_RunCollider);
            })
        );
        
        At(runState, slideState, new ActionPredicate(() => m_SlideInput && Grounded,
            () =>
            {
                EnableOnly(m_SlideCollider);
            })
        );
        
        At(slideState, runState, new ActionPredicate(() => !m_SlideInput,
            () =>
            {
                EnableOnly(m_RunCollider);
            })
        );
        
        At(jumpState, airDashState, new ActionPredicate(
            () =>
            {
                return m_AirDashInput;
            },
            () =>
            {
                EnableOnly(m_AirDashCollider);

                if (m_SlideInput) m_AirDashMovementDirection = -1f;
                else
                if (m_JumpInput) m_AirDashMovementDirection = 1f;
                
                m_JumpInput = false;
                m_SlideInput = false;
            })
        );
        
        At(airDashState, fallingState, new ActionPredicate(() => !m_AirDashInput, () => EnableOnly(m_JumpCollider)));
        At(runState, dashState, new ActionPredicate(() => m_DashInput, () =>
        {
            m_SpeedController.ApplyMultiplier(DashSpeedMultiplier);
            OnDashEvent?.Invoke();
            m_Dashing = true;
            m_DashInput = false;
            
            EnableOnly(m_DashCollider);
        }));
        
        At(dashState, runState, new ActionPredicate(() => !m_Dashing, () =>
        {
            m_SpeedController.ApplyMultiplier(1f);
            EnableOnly(m_RunCollider);
        }));
        
        AtAny(deathState, new FuncPredicate(() => m_IsDead));
        AtAny(sleepState, new ActionPredicate(() => m_Restart, () => m_Restart = false));

        m_rigidbody.excludeLayers = 0;
        
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
        m_StateMachine.SetState(sleepState);
        
    }

    void Update()
    {
       m_StateMachine.Update();
       
       /*Debug.Log($"Jump {m_JumpInput}");
       Debug.Log($"Slide {m_SlideInput}");
       Debug.Log($"Grounded {Grounded}");*/

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

    private void DisableAllColliders()
    {
        foreach (var item in m_Colliders)
        {
            item.enabled = false;
        }
    }

    private void EnableOnly(Collider2D playerCollider2D)
    {
        DisableAllColliders();
        playerCollider2D.enabled = true;
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

    public void ApplyGravity()
    {
        transform.Translate(Vector2.down * (GravityForce * Time.deltaTime));
    }

    public void RevertGravity()
    {
        transform.Translate(Vector2.up * (GravityForce * Time.deltaTime));
    }

    public void ApplyGravity(float force, Vector2 direction)
    {
        transform.Translate(direction * (force * Time.deltaTime));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;

        switch (tag)
        {
            case "Coin":
                other.gameObject.SetActive(false);
                m_Coins.AddCoins(1);
                return;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);

        transform.position = new Vector3(XPosition, transform.position.y, 0f);
        other.gameObject.TryGetComponent<Obstacle>(out var obstacle);

        if (Invincible)
        {
            if (obstacle)
            {
                obstacle.GetDestroyed();
            }
            return;
        }
        
        if (m_StateMachine.CurrentState.ToString() == "SlideState")
        {
            if (obstacle)
            {
                obstacle.GetDestroyed();
            }
            else
            {
                Die();
            }
        }
        else
        {
            m_SpeedController.ResetSpeed();
            Die();
        }
    }

    private void Die()
    {
        m_DeathType = m_StateMachine.CurrentState.ToString() == "SlideState" ? 0 : 1;
        m_IsDead = true;

        m_SpeedController.ResetSpeed();
        m_Coins.ResetValue();
        DisableInput();
    }

    public void Restart()
    {
        m_Restart = true;
        m_SpeedController.ResetSpeed();
    }
    
    private void DisableInput()
    {
        m_inputReader.DisableGameplayInput();
    }
    
    public void DisableDash()
    {
        m_rigidbody.excludeLayers = c_LayerMaskDefault;
        m_Dashing = false;
        Invincible = false;
    }

    public void ActivateDash()
    {
        m_rigidbody.excludeLayers = c_LayerMaskDash;
        Invincible = true;
    }
    
    public bool Invincible { get; private set; }
}
