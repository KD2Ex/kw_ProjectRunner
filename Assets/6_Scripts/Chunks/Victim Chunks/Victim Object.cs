using System;
using System.Collections;
using UnityEngine;

public class VictimObject : Enemy
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject cutSceneManager;
    [SerializeField] private ApproachingButton stopButton;
    [SerializeField] private ApproachingButton interactButton;

    private bool playerNearby => DistanceToPlayer < 14f && DistanceToPlayer > -14f;
    
    private void OnEnable()
    {
        cutSceneManager.SetActive(false);
        
        input.StopEvent += OnStop;
        input.RunEvent += OnMove;
    }

    private void OnDisable()
    {
        input.StopEvent -= OnStop;
        input.RunEvent -= OnMove;
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => DistanceToPlayer < 14f);
        
        stopButton.FadeIn();
        
        yield return new WaitUntil(() => DistanceToPlayer < -14f);
        
        stopButton.FadeOut();
        cutSceneManager.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    private void OnStop(bool value)
    {
        Debug.Log(value);
        stopButton.FadeOut();
        interactButton.FadeIn();
        
        cutSceneManager.SetActive(true);
    }
    
    private void OnMove(bool value)
    {
        if (!value) return;
        if (!playerNearby) return;
        
        stopButton.FadeIn();
    }
}
