using UnityEngine;

public class Shield : PowerUp
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
