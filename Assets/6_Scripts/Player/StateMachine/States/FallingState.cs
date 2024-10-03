using UnityEngine;

public class FallingState : BaseState
{

    public FallingState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();

        animator.SetBool(player.animHash_Jump, false);
    }

    public override void Update()
    {
        base.Update();
        
        if (!player.Grounded)
        {
            player.transform.Translate(Vector2.down * (player.GravityForce * Time.deltaTime));
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}