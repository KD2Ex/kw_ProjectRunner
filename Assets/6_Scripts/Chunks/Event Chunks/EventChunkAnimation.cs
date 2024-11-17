using System;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    [SerializeField] private EventChunk eventChunk;
    [SerializeField] private Animator animator;
    
    private Vector3 position;
    public Vector3 InitPos => position;

    private void Awake()
    {
        Debug.Log("Event animation awake");
        position = transform.localPosition;
        Debug.Log(position);
    }

    public void EventEnded()
    {
        gameObject.SetActive(false);
        eventChunk.gameObject.SetActive(false);
        GameManager.instance.SceneMusic.Source.UnPause();
    }

    private void Update()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime > 1f)
        {
            EventEnded();
        }
    }
}
