﻿using UnityEngine;

public class IdleState : BaseState
{

    public IdleState(Player player, Animator animator) : base(player, animator)
    {
    }
    public override void Enter()
    {
        base.Enter();
        
        animator.SetBool(player.animHash_Idle, true);
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
        animator.SetBool(player.animHash_Idle, false);
    }
}