using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScenes : MonoBehaviour
{
    [SerializeField] private InputReader m_Input;
    [SerializeField] private string sceneName;
    private Player player;
    
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

    }

    private void OnEnable()
    {
        if (!m_Input) return;
        m_Input.RestartScenesEvent += Execute;
    }

    private void OnDisable()
    {
        if (!m_Input) return;
        m_Input.RestartScenesEvent -= Execute;
    }

    public void Execute()
    {
        Debug.Log("reset");
        /*player = PlayerLocator.instance.playerTransform.GetComponent<Player>();
        PlayerLocator.instance.playerTransform.position = PlayerLocator.instance.DefaultPosition;
        player.Restart();*/

        SceneManager.LoadScene(sceneName);
        m_Input.EnableGameplayInput();
        /*var async = SceneManager.UnloadSceneAsync("Test");
        StartCoroutine(Unloading(async, () => SceneManager.LoadScene("Test")));*/
    }

    private IEnumerator Unloading(AsyncOperation async, Action func)
    {
        yield return new WaitUntil(() => async.isDone);

        func();
    }
    
    private void ReloadAllScenes()
    {
        var count = SceneManager.sceneCount;

        SceneManager.LoadScene("Persistent");
        
        for (int i = 0; i < count; i++)
        {
            var scene = SceneManager.GetSceneAt(i);

            if (scene.name == "Persistent") continue;
            
            SceneManager.UnloadSceneAsync(scene.name);
            SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
        }
    }
}
