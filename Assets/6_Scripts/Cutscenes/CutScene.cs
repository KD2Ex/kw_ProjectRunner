using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private VideoPlayer video;

    public UnityEvent OnEnd;

    private void OnEnable()
    {
        input.InteractEvent += End;
    }

    private void OnDisable()
    {
        input.InteractEvent -= End;
    }
    
    void End()
    {
        gameObject.SetActive(false);
        OnEnd?.Invoke();
    }
}