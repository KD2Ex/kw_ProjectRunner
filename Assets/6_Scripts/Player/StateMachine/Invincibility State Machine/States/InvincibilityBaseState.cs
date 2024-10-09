using System;

public class InvincibilityBaseState : IState
{
    protected InvincibilityController controller;
    protected Action<bool> setter;

    public InvincibilityBaseState(InvincibilityController controller, Action<bool> setter)
    {
        this.controller = controller;
        this.setter = setter;
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