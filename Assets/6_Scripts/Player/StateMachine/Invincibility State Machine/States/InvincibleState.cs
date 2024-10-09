using System;
using UnityEngine;

public class InvincibleState : InvincibilityBaseState
{
    private float elapsedTime;
    private float alpha;

    private const float rate = 10f; 
    
    public override void Enter()
    {
        base.Enter();
        setter.Invoke(true);
        elapsedTime = 0f;
        Debug.Log($"Invincible {controller.Invincible}");
    }

    public override void Update()
    {
        base.Update();
        elapsedTime += Time.deltaTime;

        Debug.Log(elapsedTime);
        Debug.Log(controller.Time);
        
        if (elapsedTime < controller.Time)
        {
            return;
        }
        
        controller.ResetTrigger();
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        alpha += Time.deltaTime * rate;
        controller.SetPlayerOpacity(Mathf.Abs(1 - alpha));

        if (alpha >= 2f) alpha = 0;
    }

    public override void Exit()
    {
        base.Exit();
        setter.Invoke(false);
        controller.SetPlayerOpacity(1f);
    }

    public InvincibleState(InvincibilityController controller, Action<bool> setter) : base(controller, setter)
    {
    }
}