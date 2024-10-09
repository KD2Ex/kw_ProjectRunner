using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum DeathType
{
    SLIDE,
    RUN
}

[RequireComponent(typeof(SpeedController))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader m_inputReader;
    [SerializeField] private PlayerBoostersParentController m_BoostersParentController;
    [FormerlySerializedAs("m_FoodUse")] [SerializeField] private FoodUseManager mFoodUseManager;
    public FoodUseManager FoodUseManager => mFoodUseManager;
    public PlayerBoostersParentController BoostersParentController => 
        m_BoostersParentController;
    
    [Header("Stats")]
    [Range(0, 3)]
    [SerializeField] private float m_JumpTime;
    [SerializeField] private float m_EnergyToDash;
    
    private float m_DashEnergy = 0f;
    private bool m_DashEnergyFull => m_DashEnergy >= m_EnergyToDash;
    
    public float JumpTime => m_JumpTime;

    #region PowerUps

    [SerializeField] private Shield m_Shield;
    [SerializeField] private Magnet m_Magnet;
    [SerializeField] private CoinMultiplier m_CoinMultiplier;

    public CoinMultiplier CoinMultiplier => m_CoinMultiplier;
    public Shield Shield => m_Shield;
    public Magnet Magnet => m_Magnet;
    
    #endregion
    
    #region SO Data

    [Header("Reference values")] 
    [SerializeField] private FloatVariable so_ChunksCurrentSpeed;
    [SerializeField] private FloatVariable so_JumpTime;
    [SerializeField] private FloatVariable so_DashEnergy;
    
    #endregion
   
    #region Colliders

    [Header("Hit boxes")] 
    [SerializeField] private Collider2D m_RunCollider;
    [SerializeField] private Collider2D m_JumpCollider;
    [SerializeField] private Collider2D m_SlideCollider;
    [SerializeField] private Collider2D m_AirDashCollider;
    [SerializeField] private Collider2D m_DashCollider;

    private List<Collider2D> m_Colliders = new();
    
    #endregion

    #region Components

    private Coins m_Coins;
    private SpeedController m_SpeedController;
    private Rigidbody2D m_rigidbody;
    private Animator m_Animator;

    #endregion

    #region Inventories

    private PlayerInventory m_Inventory;

    #endregion
    
    #region Animation Hashes

    public int animHash_Jump => Animator.StringToHash("Jump");
    public int animHash_Move => Animator.StringToHash("Move");
    public int animHash_Slide => Animator.StringToHash("Dodge");
    public int animHash_Bounce => Animator.StringToHash("Bounce");
    public int animHash_Dash => Animator.StringToHash("Faster");
    public int animHash_Idle => Animator.StringToHash("Idle");
    public int animHash_DodgeDeath => Animator.StringToHash("Dead0");
    public int animHash_RunJumpDeath => Animator.StringToHash("Dead1");
    
    #endregion

    #region Input flags

    private bool m_StopInput;
    private bool m_JumpInput;
    private bool m_SlideInput;
    private bool m_AirDashInput;
    private bool m_DashInput;
    private bool m_RunInput;

    #endregion

    #region State Flags

    private bool m_JumpAnimationFinished;
    private bool m_Dashing;
    private bool m_IsDead;
    private bool m_Restart;
    private bool m_Running => m_ChunksCurrentSpeed > 0;

    #endregion

    #region Consts

    public static readonly float GroundLine = -1.77f; // replace with so data
    public static readonly float SlideGroundLine = -4.2f; // replace with so data
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

    #endregion
    
    #region Events

    public UnityEvent OnDashEvent;
    public UnityEvent OnStartRunning;
    public UnityEvent OnStopRunning;
    public UnityEvent OnDeath;

    #endregion

    #region States
    
    private StateMachine m_StateMachine;
    private IState sleepState;
    private DashState dashState;
    
    #endregion

    #region Misc

    public void ForceToGetDown()
    {
        m_JumpInput = false;
    }
    public bool JumpInput => m_JumpInput;

    public DeathType m_DeathType { get; private set; }

    private float m_ChunksCurrentSpeed => so_ChunksCurrentSpeed.Value;

    public float m_AirDashMovementDirection { get; private set; }

    #endregion
    
    private void Awake()
    {
        m_Colliders.Add(m_AirDashCollider);
        m_Colliders.Add(m_DashCollider);
        m_Colliders.Add(m_JumpCollider);
        m_Colliders.Add(m_SlideCollider);
        m_Colliders.Add(m_RunCollider);

        m_Inventory = GetComponent<PlayerInventory>();
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
        dashState = new DashState(this, m_Animator);
        var deathState = new DeathState(this, m_Animator);
        var foodUseState = new FoodUseState(this, m_Animator);
        
        At(sleepState, runState, new ActionPredicate(() => m_Running, () => OnStartRunning?.Invoke()));
        At(idleState, runState, new ActionPredicate(() => m_Running, () =>
        {
            OnStartRunning?.Invoke();
            m_StopInput = false;
        }));
        At(runState, idleState, new ActionPredicate(() => m_StopInput, () =>
        {
            m_SpeedController.ResetSpeed();
            OnStopRunning?.Invoke();
        }));
        /*At(idleState, foodUseState, new FuncPredicate(() => m_Inventory.Food.Items.Count > 0));
        At(foodUseState, idleState, new FuncPredicate(() => ));*/
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
        At(runState, dashState, new ActionPredicate(() => m_DashEnergyFull && m_inputReader.RunTriggered, () =>
        {
            OnDashStateEnter();
            SpendDashEnergy();
        }));
        
        At(dashState, runState, new ActionPredicate(() => !m_Dashing, () =>
        {
            m_SpeedController.ApplyMultiplier(1f);
            EnableOnly(m_RunCollider);
        }));
        
        AtAny(deathState, new ActionPredicate(() => m_IsDead, () => OnStopRunning?.Invoke()));
        AtAny(sleepState, new ActionPredicate(() => m_Restart, () => m_Restart = false));

        m_rigidbody.excludeLayers = 0;
        
    }

    public void EnterDashState()
    {
        OnDashStateEnter();
        dashState.AddDuration(5f); // replace with so_DashPickupDuration
        m_StateMachine.ChangeState(dashState);
    }
    
    private void OnDashStateEnter()
    {
        m_SpeedController.ApplyMultiplier(DashSpeedMultiplier);
        OnDashEvent?.Invoke();
        m_Dashing = true;
        m_DashInput = false;
            
        EnableOnly(m_DashCollider);
    }

    protected void At(IState from, IState to, IPredicate condition) => m_StateMachine.AddTransition(from, to, condition);
    protected void AtAny(IState to, IPredicate condition) => m_StateMachine.AddAnyTransition(to, condition);
    
    private void OnEnable()
    {
        m_inputReader.RunEvent += OnRun;
        m_inputReader.JumpEvent += OnJump;
        m_inputReader.SlideEvent += OnSlide;
        m_inputReader.StopEvent += OnStop;
        m_inputReader.AirDashEvent += OnAirDash;
        m_inputReader.DashAbilityTest += OnDash;
    }

    private void OnDisable()
    {
        m_inputReader.RunEvent -= OnRun;
        m_inputReader.JumpEvent -= OnJump;
        m_inputReader.SlideEvent -= OnSlide;
        m_inputReader.StopEvent -= OnStop;
        m_inputReader.AirDashEvent -= OnAirDash;
        m_inputReader.DashAbilityTest -= OnDash;
    }

    private SpriteRenderer sprite;
    
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        //Time.timeScale = .5f;
        m_StateMachine.SetState(sleepState);

        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       m_StateMachine.Update();
    }

    private void FixedUpdate()
    {
        m_StateMachine.FixedUpdate();
    }

    private void OnRun(bool value) => m_RunInput = value;
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

    public void PickupCoin(int value)
    {
        m_Coins.AddCoins(value * CoinMultiplier.Value);
        AddDashEnergy(value);
    }

    public void GetPowerUp(PowerUp powerUp)
    {
        if (powerUp.gameObject.activeSelf)
        {
            powerUp.AddDuration();
        }
        else
        {
            powerUp.gameObject.SetActive(true);
        }
    }

    public void PickupFood(FoodInventoryItem foodInventoryItem)
    {
        m_Inventory.Food.AddItem(foodInventoryItem);
        //var type = foodData.Type;
    }

    public void PickupCreature( /*creature item*/)
    {
        // m_Inventory.Creature.AddItem(creatureItem);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log(other.gameObject.name);
        other.TryGetComponent<Collectable>(out var collectable);
        if (collectable)
        {
            collectable.Pickup(this);
            return;
        }
        
        other.TryGetComponent<Obstacle>(out var obstacle);
        
        /*switch (tag)
        {
            case "Coin":
                other.gameObject.SetActive(false);

                return;
        }*/

        if (!other.CompareTag("Enemy")) return;

        
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
        m_DeathType = m_StateMachine.CurrentState.ToString() == "SlideState" ? DeathType.SLIDE : DeathType.RUN;
        m_IsDead = true;

        m_SpeedController.ResetSpeed();
        m_Coins.ResetValue();
        DisableInput();
        OnDeath?.Invoke();
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
        //m_rigidbody.excludeLayers = c_LayerMaskDefault;
        m_Dashing = false;
    }

    public void SpendDashEnergy()
    {
        m_DashEnergy -= m_EnergyToDash;
        so_DashEnergy.Value -= m_EnergyToDash;
        //m_rigidbody.excludeLayers = c_LayerMaskDash;
    }

    private void AddDashEnergy(float value)
    {
        if (m_DashEnergy + value >= m_EnergyToDash)
        {
            m_DashEnergy = m_EnergyToDash;
            so_DashEnergy.Value = m_EnergyToDash;
            return;
        }
        m_DashEnergy += value;
        so_DashEnergy.Value = m_DashEnergy;
    }

    
    
    public bool Invincible => m_Dashing || m_Shield.gameObject.activeSelf;
}
