using UnityEngine;

public class Magnet : Booster
{
    protected override void OnEnable()
    {
        Debug.Log(gameObject.name);
    }
    
    protected override void OnDisable()
    {
        Debug.Log(gameObject.name);
    }
}