﻿using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    public UnityEvent Response;

    private void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }
}