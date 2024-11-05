using System.Collections;
using UnityEngine;

public class VictimObject : Enemy
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject cutSceneManager;
    [SerializeField] private ApproachingButton stopButton;
    
    private void OnEnable()
    {
        cutSceneManager.SetActive(false);
        
        input.StopEvent += OnStop;
    }

    private void OnDisable()
    {
        input.StopEvent -= OnStop;
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => DistanceToPlayer < 14f);
        
        stopButton.FadeIn();
        cutSceneManager.SetActive(true);
        
        yield return new WaitUntil(() => DistanceToPlayer < -14f);
        
        stopButton.FadeOut();
        cutSceneManager.SetActive(true);
    }

    private void Update()
    {
        Debug.Log(DistanceToPlayer);
    }

    private void OnStop(bool value)
    {
        Debug.Log(value);
        stopButton.FadeOut();
    }
}
