using UnityEngine;

public class AirDashState : BaseState
{
    private AudioSource source;    
    private float elapsedTime;    
    
    public AirDashState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(player.animHash_Bounce, true);
        player.SwitchControlsToAirDash();
        player.BoostersParentController.ChangeParent(Align.Top);
        
        SoundFXManager.instance.PlayClipAtPoint(player.BounceOnSound, player.transform, 1f);
    }

    public override void Update()
    {
        base.Update();

        var translation = new Vector3(0f, player.m_AirDashMovementDirection, 0f);
        var speed = player.AirDashMovementSpeed * Time.deltaTime;

        var predictPosition = player.transform.position + translation * speed;
        
        if (!MathUtils.IsFloatInBounds(predictPosition.y, player.UpperAirDashBound, player.LowerAirDashBound)) return;
        
        player.transform.Translate(translation * speed);

        elapsedTime += Time.deltaTime;

        if (elapsedTime > player.BounceOnSound.length && !source)
        {
            source = SoundFXManager.instance.PlayLoopedSound(player.BounceSound, player.transform, 1f);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        player.BoostersParentController.ChangeParent(Align.Center);
        animator.SetBool(player.animHash_Bounce, false);
        player.ReturnToDefaultControls();
        elapsedTime = 0f;
        
        SoundFXManager.instance.PlayClipAtPoint(player.BounceOffSound, player.transform, 1f);

        
        SoundFXManager.instance.DestroySource(source);
        source = null;
        
        if (player.Grounded) player.transform.position = new Vector3(player.transform.position.x, -1.5f, 0f);
    }
}