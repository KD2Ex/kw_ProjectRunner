using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartGameSelection : UISelection
{
    private AudioSource source;
    
    public override void Press()
    {
        //SoundFXManager.instance.PlayClipAsChild(sounds.GetRandom(), SoundFXManager.instance.transform, 1f);
        var async = SceneManager.LoadSceneAsync("Main");
        async.allowSceneActivation = true;
        
        GameManager.instance.OpenLoadingScreen(async);
    }
}