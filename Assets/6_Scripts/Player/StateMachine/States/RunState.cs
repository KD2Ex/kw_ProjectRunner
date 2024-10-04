﻿using UnityEngine;

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

        if (!player.Grounded)
        {
            player.ApplyGravity();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(player.animHash_Move, false);
    }
}