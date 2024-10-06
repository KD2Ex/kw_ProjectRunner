using UnityEngine;

public class DashState : BaseState
{
    private float elapsedTime;
    private bool upmove;
    private bool cancelUpMove = true;
    
    public DashState(Player player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        elapsedTime = 0f;
        animator.SetBool(player.animHash_Dash, true);
        player.ActivateDash();
    }

    public override void Update()
    {
        base.Update();
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 5f)
        {
            player.DisableDash();
            elapsedTime = 0f;
        }

        if (upmove)
        {
            if (player.transform.position.y < 0 && player.JumpInput)
            {
                player.ApplyGravity(6f, Vector2.up);
            }
            else
            {
                upmove = false;
            }

            //return;
        }
        else
        {
            if (!player.Grounded)
            {
                player.ApplyGravity(6f, Vector2.down);
            }
            
            if (player.JumpInput && !cancelUpMove)
            {
                cancelUpMove = false;
            }
            else
            {
                cancelUpMove = true;
            }
            
        }

        /*Debug.Log($"cancel {cancelUpMove}");
        Debug.Log($"Jumpinput {player.JumpInput}");
        Debug.Log($"GRound {player.Grounded}");*/

        if (!cancelUpMove) return;
        if (player.JumpInput && player.Grounded)
        {
            Debug.Log("UPmove");
            upmove = true;
            cancelUpMove = false;
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
        animator.SetBool(player.animHash_Dash, false);
    }

    public void AddDuration(float value)
    {
        elapsedTime -= value;
    }
}