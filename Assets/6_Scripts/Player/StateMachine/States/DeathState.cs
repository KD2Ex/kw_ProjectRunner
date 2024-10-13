﻿using UnityEngine;

public class DeathState : BaseState
{
    public DeathState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundFXManager.instance.PlayClipAtPoint(player.DeathSound, player.transform, 1f);
        int hash;
        
        if (player.m_DeathType == DeathType.SLIDE)
        {
            hash = player.animHash_DodgeDeath;
        }
        else
        {
            hash = player.animHash_RunJumpDeath;
        }
        
        
        animator.SetBool(hash, true);
    }

    public override void Update()
    {
        base.Update();

        if (player.m_DeathType == DeathType.SLIDE)
        {
            if (player.transform.position.y > Player.SlideGroundLine)
            {
                player.ApplyGravity(15f, Vector2.down);
            }

            return;
        }
        
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