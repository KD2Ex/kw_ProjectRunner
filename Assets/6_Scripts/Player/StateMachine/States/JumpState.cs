using UnityEngine;

public class JumpState : BaseState
{
    private float time;
    private float elapsedTime = 0f;
    
    public JumpState(Player player, Animator animator) : base(player, animator)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        time = player.JumpTime;
        animator.SetBool(player.animHash_Jump, true);
        player.CenterController.ChangeAlignment(Align.Top);
    }

    public override void Update()
    {
        base.Update();

        if (player.transform.position.y < .7f)
        {
            player.transform.Translate(Vector2.up * (player.JumpSpeed * Time.deltaTime));
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime > time)
        {
            elapsedTime = 0f;
            player.ForceToGetDown();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        elapsedTime = 0f;
        animator.SetBool(player.animHash_Jump, false);
    }
}