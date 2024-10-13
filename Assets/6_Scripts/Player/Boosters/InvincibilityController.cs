using System;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private Player player;
    private StateMachine stateMachine;
    private bool triggered;
    public bool Invincible { get; private set; }

    [field:SerializeField] public float Time { get; private set; }
    
    
    private void Awake()
    {
        player = GetComponent<Player>();
        stateMachine = new StateMachine();
    }
    
    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);

    private void Start()
    {
        
        var defaultState = new VulnerableState(this, SetValue);
        var invincibleState = new InvincibleState(this, SetValue);
        
        At(defaultState, invincibleState, new FuncPredicate(() => triggered && !player.Invincible));
        At(invincibleState, defaultState, new FuncPredicate(() => !triggered));
        stateMachine.SetState(defaultState);

    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void SetValue(bool value)
    {
        Invincible = value;
    }

    public void Trigger()
    {
        triggered = true;
    }

    public void ResetTrigger()
    {
        triggered = false;
    }

    public void SetPlayerOpacity(float alpha)
    {
        var color = player.Sprite.color;
        player.Sprite.color = new Color(color.r, color.b, color.g, alpha);
    }
}
