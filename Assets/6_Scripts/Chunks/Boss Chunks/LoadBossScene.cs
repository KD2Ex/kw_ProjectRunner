using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBossScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    public void Execute()
    {
        SceneManager.LoadScene("Boss Persistence");
        var async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        GameManager.instance.OpenLoadingScreen(async);
    }
}