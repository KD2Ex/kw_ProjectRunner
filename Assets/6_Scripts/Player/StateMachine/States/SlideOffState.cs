using UnityEngine;

public class SlideOffState : BaseState
{
    private AnimatorStateInfo stateInfo;
    private bool started;
    private float elapsedTime;
    
    public SlideOffState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if (!stateInfo.IsName("dodge_off")) return;
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime > stateInfo.length)
        {
            player.SlideOff();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        elapsedTime = 0f;
        base.Exit();
    }
}