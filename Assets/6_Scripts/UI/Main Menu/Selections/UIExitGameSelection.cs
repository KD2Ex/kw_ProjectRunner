using Application = UnityEngine.Application;

public class UIExitGameSelection : UISelection
{
    public override void Press()
    {
        base.Press();
        Application.Quit();
    }
}