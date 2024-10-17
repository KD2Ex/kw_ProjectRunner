using UnityEngine.SceneManagement;

public class UIStartGameSelection : UISelection
{
    public override void Press()
    {
        SceneManager.LoadScene("Main");
    }
}