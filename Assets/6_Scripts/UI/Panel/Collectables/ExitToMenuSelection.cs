using UnityEngine.SceneManagement;

public class ExitToMenuSelection : UISelection
{
    public override void Press()
    {
        //SoundFXManager.instance.PlayClipAsChild(sounds.GetRandom(), SoundFXManager.instance.transform, 1f);
        //SaveSystem.Save();
        var async = SceneManager.LoadSceneAsync("Main Menu");
        GameManager.instance.OpenLoadingScreen(async);
    }
}