using UnityEngine;
using UnityEngine.Events;

public class UIExitSelection : UISelection
{
    public UnityEvent OnExit;

    public override void Press()
    {
        Debug.Log("Exit");
        OnExit?.Invoke();
    }
}