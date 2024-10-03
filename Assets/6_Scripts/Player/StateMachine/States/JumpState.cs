using System.Linq;
using UnityEngine;

public class JumpState : BaseState
{
    private bool playing;
    
    public JumpState(Player player, Animator animator) : base(player, animator)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        animator.SetBool(player.animHash_Jump, true);
    }

    public override void Update()
    {
        base.Update();

        if (player.transform.position.y < .7f)
        {
            player.transform.Translate(Vector2.up * (player.JumpSpeed * Time.deltaTime));
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(player.animHash_Jump, false);
    }
}