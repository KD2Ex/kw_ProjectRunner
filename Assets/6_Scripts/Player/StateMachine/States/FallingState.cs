using UnityEngine;

public class FallingState : BaseState
{

    public FallingState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        if (!player.Grounded)
        {
            player.ApplyGravity();
            //player.transform.Translate(Vector2.down * (player.GravityForce * Time.deltaTime));
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();

        player.transform.position = new Vector3(player.transform.position.x, Player.GroundLine, 0f);
    }
}