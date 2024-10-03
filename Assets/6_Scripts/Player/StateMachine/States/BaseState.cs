using UnityEngine;

public abstract class BaseState : IState
{
    protected Player player;
    protected Animator animator;

    protected BaseState(Player player, Animator animator)
    {
        this.player = player;
        this.animator = animator;
    }

    public virtual void Enter()
    {
            
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}