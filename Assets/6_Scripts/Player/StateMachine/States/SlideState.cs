using UnityEngine;

public class SlideState : BaseState
{
    public SlideState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(player.animHash_Slide, true);
        player.CenterController.ChangeAlignment(Align.Bottom);
    }

    public override void Update()
    {
        base.Update();
        if (player.transform.position.y > -4.25f)
        {
            player.ApplyGravity(14f, Vector2.down);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(player.animHash_Slide, false);
    }
}