using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossChunk : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private string bossSceneName;

    [SerializeField] private GameObject boss;
    
    private void OnEnable()
    {
        input.InteractEvent += Accept;
    }

    private void OnDisable()
    {
        
        input.InteractEvent -= Accept;
    }

    private void Start()
    {
        boss.transform.position = GameManager.instance.Player.transform.position;
    }

    private void Accept()
    {
        SceneManager.LoadScene("Boss Persistence");
        var async = SceneManager.LoadSceneAsync(bossSceneName, LoadSceneMode.Additive);
        
        GameManager.instance.OpenLoadingScreen(async);
    }
}