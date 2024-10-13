using UnityEngine;

public class IdleState : BaseState
{

    public IdleState(Player player, Animator animator) : base(player, animator)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        animator.SetBool(player.animHash_Idle, true);
        player.transform.position = new Vector3(player.transform.position.x, -1.77f, 0f);
        
        SoundFXManager.instance.PlayClipAtPoint(player.StopSound, player.transform, 1f);
        
        player.FoodUseManager.Consume();
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