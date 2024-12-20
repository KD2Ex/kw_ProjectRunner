﻿using System.Diagnostics.Tracing;
using UnityEngine;

public class SlideState : BaseState
{
    private AudioSource source;
    private float elapsedTime;
    
    public SlideState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(player.animHash_Slide, true);
        GameManager.instance.SceneMusic.Source.volume = player.VolumeDown;
        SoundFXManager.instance.PlayClipAtPoint(player.SlideOnSound, player.transform, 1f);
    }

    public override void Update()
    {
        base.Update();
        if (player.transform.position.y > -4.5f)
        {
            player.ApplyGravity(14f, Vector2.down);
        }
        else if (!player.SlideColliderActive)
        {
            player.DroppedInSlide();
        }
        
        elapsedTime += Time.deltaTime;

        if (elapsedTime > player.SlideOnSound.length && !source)
        {
            source = SoundFXManager.instance.PlayLoopedSound(player.SlideSound, player.transform, 1f);
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
        animator.SetBool(player.animHash_Slide, false);
        
        GameManager.instance.SceneMusic.Source.volume = 1f;
        
        SoundFXManager.instance.PlayClipAtPoint(player.SlideOffSound, player.transform, 1f);
        SoundFXManager.instance.DestroySource(source);
        source = null;
    }
}