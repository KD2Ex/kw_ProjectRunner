using UnityEngine;

public class RunState : BaseState
{
    
    public RunState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(player.animHash_Move, true);
        // subscribe W action to Jump
    }

    public override void Update()
    {
        base.Update();


    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!player.Grounded)
        {
            player.ApplyGravity();
        } 
        else if (player.transform.position.y < -1.8f)
        {
            player.ApplyGravity(14f, Vector2.up);
        }
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(player.animHash_Move, false);
    }
}