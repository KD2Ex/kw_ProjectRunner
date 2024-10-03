using UnityEngine;

public class AirDashState : BaseState
{
    private float yPosition => player.transform.position.y;
    
    public AirDashState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(player.animHash_Bounce, true);
        player.SwitchControlsToAirDash();
    }

    public override void Update()
    {
        base.Update();

        var translation = new Vector3(0f, player.m_AirDashMovementDirection, 0f);
        var speed = player.AirDashMovementSpeed * Time.deltaTime;

        var predictPosition = player.transform.position + translation * speed;
        
        if (!MathUtils.IsFloatInBounds(predictPosition.y, player.UpperAirDashBound, player.LowerAirDashBound)) return;
        
        player.transform.Translate(translation * speed);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(player.animHash_Bounce, false);
        player.ReturnToDefaultControls();

        if (player.Grounded) player.transform.position = new Vector3(player.transform.position.x, -1.5f, 0f);
    }
}