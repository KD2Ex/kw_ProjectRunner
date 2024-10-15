using UnityEngine;
using UnityEngine.EventSystems;

public class Magnet : Booster
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log(gameObject.name);
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        Debug.Log(gameObject.name);
    }
}