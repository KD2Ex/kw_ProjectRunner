using UnityEngine;

public class DeathState : BaseState
{
    private int hash;
    
    public DeathState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioClip clip;

        Debug.Log(player.transform.position.y);
        
        if (player.transform.position.y <= -2.2f/*player.m_DeathType == DeathType.SLIDE*/)
        {
            hash = player.animHash_DodgeDeath;
            clip = player.SlideDeathSound;
        }
        else
        {
            hash = player.animHash_RunJumpDeath;
            clip = player.DeathSound;
        }
        
        SoundFXManager.instance.PlayClipAtPoint(clip, player.transform, 1f);
        animator.SetBool(hash, true);
    }

    public override void Update()
    {
        base.Update();

        
        /*var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("run_jump_death")
            && !stateInfo.IsName("dodge_death"))
        {
            animator.SetBool(player.animHash_RunJumpDeath, true);
        }*/
        
      
        
        
        
        /*if (player.m_DeathType == DeathType.SLIDE 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("dodge_off"))
        {
            if (player.transform.position.y > Player.SlideGroundLine)
            {
                player.ApplyGravity(15f, Vector2.down);
            }

            return;
        }
        
        if (!player.Grounded) player.ApplyGravity();*/

        /*
        if (player.transform.position.y < -1.77f)
        {
            player.transform.position = new Vector3(player.transform.position.x, -1.77f, 0f);
        }
        */


    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (hash == player.animHash_DodgeDeath)
        {
            if (player.transform.position.y > Player.SlideGroundLine)
            {
                player.ApplyGravity(15f, Vector2.down);
            }
        }
        else
        {
            if (!player.Grounded)
            {
                player.ApplyGravity(15f, Vector2.down);
            }
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}