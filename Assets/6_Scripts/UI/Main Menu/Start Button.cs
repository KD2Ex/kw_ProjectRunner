using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(LoadLocation);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void LoadLocation()
    {
        var async = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        StartCoroutine(OnLoading(async));
    }

    IEnumerator OnLoading(AsyncOperation async)
    {
        yield return new WaitUntil(() => async.isDone);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        SceneManager.UnloadSceneAsync("Main Menu");
        
    }
}
