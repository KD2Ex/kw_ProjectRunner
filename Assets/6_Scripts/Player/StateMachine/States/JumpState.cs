using System.Linq;
using UnityEngine;

public class JumpState : BaseState
{
    private float time;
    private string m_JumpStartAnimName;
    private bool playing;
    
    public JumpState(Player player, Animator animator) : base(player, animator)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        animator.SetBool("Jump", true);
        m_JumpStartAnimName = animator.GetCurrentAnimatorClipInfo(0).First().clip.name;
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
    }


}