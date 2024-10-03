using UnityEngine;

public class RunState : BaseState
{
    public RunState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool("Move", true);
        // subscribe W action to Jump
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
        animator.SetBool("Move", false);
    }
}