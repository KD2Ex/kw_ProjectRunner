using System;
using UnityEngine;

public class VulnerableState : InvincibilityBaseState
{
    public override void Enter()
    {
        base.Enter();
        setter.Invoke(false);
        //Debug.Log($"Invincible {controller.Invincible}");
        controller.SetPlayerOpacity(1f);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public VulnerableState(InvincibilityController controller, Action<bool> setter) : base(controller, setter)
    {
    }
}