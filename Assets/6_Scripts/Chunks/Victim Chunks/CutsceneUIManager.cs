using System.Collections;
using UnityEngine;

public class CutsceneUIManager : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private CutScene cutSceneManager;
    [SerializeField] private ApproachingButton stopButton;
    [SerializeField] private ApproachingButton interactButton;

    private float Dist => Utils.DistanceToPlayer(transform);
    
    private bool playerNearby => Dist < 14f && Dist > -14f;
    
    private void OnEnable()
    {
        cutSceneManager.gameObject.SetActive(false);
        
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
        yield return new WaitUntil(() => Dist < 14f);
        
        stopButton.FadeIn();
        cutSceneManager.gameObject.SetActive(true);

        yield return new WaitUntil(() => Dist < -14f);
        
        stopButton.FadeOut();
        cutSceneManager.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnStop(bool value)
    {
        Debug.Log(value);
        stopButton.FadeOut();

        if (!playerNearby) return;
        interactButton.FadeIn();
    }
    
    private void OnMove(bool value)
    {
        if (!value) return;
        if (!playerNearby) return;
        
        stopButton.FadeIn();
    }
}
