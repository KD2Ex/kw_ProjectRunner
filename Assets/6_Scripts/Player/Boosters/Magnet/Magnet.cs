using UnityEngine;

public class Magnet : PowerUp
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