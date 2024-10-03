using UnityEngine;

public class DashState : BaseState
{
    private float elapsedTime;
    
    public DashState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(player.animHash_Dash, true);
        
    }

    public override void Update()
    {
        base.Update();
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 1.5f)
        {
            player.DeactiveDash();
            elapsedTime = 0f;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(player.animHash_Dash, false);
    }
}