using UnityEngine.SceneManagement;

public class ExitToMenuSelection : UISelection
{
    public override void Press()
    {
        SaveSystem.Save();
        SceneManager.LoadSceneAsync("Main Menu");
    }
}