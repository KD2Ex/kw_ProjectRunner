using UnityEngine;

public abstract class BaseState : IState
{
    protected Player player;

    protected BaseState(Player player)
    {
        this.player = player;
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