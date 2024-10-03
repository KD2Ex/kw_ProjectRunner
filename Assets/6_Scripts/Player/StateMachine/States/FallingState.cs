using UnityEngine;

public class FallingState : BaseState
{
    public FallingState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();

        animator.SetBool("Jump", false);
    }

    public override void Update()
    {
        base.Update();
        
        if (player.transform.position.y > -1.4f)
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