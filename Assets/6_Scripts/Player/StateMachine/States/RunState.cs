public class RunState : BaseState
{
    public RunState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // subscribe W action to Jump
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
    }
}