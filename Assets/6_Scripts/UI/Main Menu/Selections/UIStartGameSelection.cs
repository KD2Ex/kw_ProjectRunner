using UnityEngine.SceneManagement;

public class UIStartGameSelection : UISelection
{
    public override void Press()
    {
        SoundFXManager.instance.PlayClipAsChild(sounds.GetRandom(), SoundFXManager.instance.transform, 1f);
        SceneManager.LoadScene("Main");
    }
}