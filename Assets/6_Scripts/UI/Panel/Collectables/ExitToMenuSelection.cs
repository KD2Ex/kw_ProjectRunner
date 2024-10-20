using UnityEngine.SceneManagement;

public class ExitToMenuSelection : UISelection
{
    public override void Press()
    {
        SoundFXManager.instance.PlayClipAsChild(sounds.GetRandom(), SoundFXManager.instance.transform, 1f);
        SaveSystem.Save();
        SceneManager.LoadSceneAsync("Main Menu");
    }
}