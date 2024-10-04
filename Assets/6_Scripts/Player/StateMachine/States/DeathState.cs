using UnityEngine;

public class DeathState : BaseState
{
    public DeathState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        var hash = player.m_DeathType == 0 ? player.animHash_DodgeDeath : player.animHash_RunJumpDeath;
        
        animator.SetBool(hash, true);
    }

    public override void Update()
    {
        base.Update();
        
        if (!player.Grounded) player.ApplyGravity();
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