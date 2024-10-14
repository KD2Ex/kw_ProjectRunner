using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Scriptable Objects/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new();

    public void AddListener(GameEventListener listener)
    {
        Debug.Log($"Listeners: {listeners}");
        Debug.Log($"Listener: {listener}");

        if (listeners.Contains(listener)) return;
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener)) return;
        listeners.Remove(listener);
    }

    public void Raise()
    {
        foreach (var listener in listeners)
        {
            listener.Response?.Invoke();
        }
    }
}
